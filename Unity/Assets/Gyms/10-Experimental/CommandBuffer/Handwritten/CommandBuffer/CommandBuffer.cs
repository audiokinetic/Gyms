/*******************************************************************************
The content of this file includes portions of the AUDIOKINETIC Wwise Technology
released in source code form as part of the SDK installer package.

Commercial License Usage

Licensees holding valid commercial licenses to the AUDIOKINETIC Wwise Technology
may use this file in accordance with the end user license agreement provided 
with the software or, alternatively, in accordance with the terms contained in a
written agreement between you and Audiokinetic Inc.

Apache License Usage

Alternatively, this file may be used under the Apache License, Version 2.0 (the 
"Apache License"); you may not use this file except in compliance with the 
Apache License. You may obtain a copy of the Apache License at 
http://www.apache.org/licenses/LICENSE-2.0.

Unless required by applicable law or agreed to in writing, software distributed
under the Apache License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES
OR CONDITIONS OF ANY KIND, either express or implied. See the Apache License for
the specific language governing permissions and limitations under the License.
*******************************************************************************/
#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Wwise.AkCommandBuffer
{
    public class CommandBufferAllocException : Exception
    {
        public CommandBufferAllocException(string s): base(s)
        {
        }
    }

    public enum BufferAllocMode
    {
        Managed,
        AkMemoryManager
    }
    
    public partial class CommandBuffer : System.IDisposable
    {
        protected AkCommandCallbackFunc _handleCallback;
        private System.IntPtr _cookie;
        private GCHandle m_selfHandle;
       
        private System.IntPtr Buffer { get; set; }

        private bool IsInitialized { get; set; }

        private BufferAllocMode AllocMode { get; set; }

        internal AkCommandBufferHeader Header
        {
            get
            {
                unsafe
                {
                    if (Buffer == System.IntPtr.Zero)
                    {
                        return new AkCommandBufferHeader();
                    }

                    return *(AkCommandBufferHeader*)Buffer;
                }
            }
        }
        
        
        public object callbackEventLock { get; } = new object();
        public EventHandler _userCallbacks;

        public event EventHandler CompletionCallback
        {
            add
            {
                lock (callbackEventLock)
                {
                    _userCallbacks += value;
                }
            }
            remove
            {
                lock (callbackEventLock)
                {
                    _userCallbacks -= value;
                }
            }
        }

        private unsafe AkCommandBufferHeader* HeaderPtr
        {
            get { return (AkCommandBufferHeader*)Buffer; }
        }

        public uint Size => Header.BufferSize;

        public uint LastCommandOffset => Header.LastCommandOffset;
            
        public static void CmdBufferOnCompletionCallback(IntPtr cookie)
        {
            if (cookie == IntPtr.Zero)
            {
                return;
            }

            GCHandle handle = GCHandle.FromIntPtr(cookie);
            CommandBuffer instance = handle.Target as CommandBuffer;

            if (instance != null)
            {
                instance._userCallbacks?.Invoke(instance, EventArgs.Empty);
                instance.ReInit();
            }
        }

        public CommandBuffer(nuint desiredSize, BufferAllocMode bufferAllocMode = BufferAllocMode.Managed)
        {
            if (desiredSize < GetMinSize())
            {
                throw new ArgumentException($"Buffer size [{desiredSize}] is less than minium of [{GetMinSize()}] ");
            }

            AllocMode = bufferAllocMode;

            Init(desiredSize, true);
        }

        public CommandBuffer(System.IntPtr buffer)
        {
            unsafe
            {
                Buffer = buffer;
            }
        }

        public CommandBuffer(int desiredSize, BufferAllocMode bufferAllocMode = BufferAllocMode.Managed) : this(
            (nuint)desiredSize, bufferAllocMode)
        {
        }

        public static nuint GetMinSize()
        {
            return AkCommandBuffer.AK_CommandBuffer_MinSize();
        }

        public static nuint CmdSize(AkCommand cmd)
        {
            return AkCommandBuffer.AK_CommandBuffer_CmdSize(cmd);
        }

        public uint Init(nuint desiredSize, bool clearExisting = false)
        {
            bool canReuse = Size == desiredSize;
            if (clearExisting && IsInitialized)
            {
                if (!canReuse)
                {
                    FreeBuffer();
                    if (m_selfHandle.IsAllocated)
                    {
                        m_selfHandle.Free();
                    }
                }
            }

            else if (!clearExisting && Buffer != IntPtr.Zero)
            {
                throw new ArgumentException(
                    $" Trying to re-init existing buffer. Specify {nameof(clearExisting)} or destroy buffer first.");
            }

            if (!canReuse)
            {
                switch (AllocMode)
                {
                    case BufferAllocMode.Managed:
                        Buffer = Marshal.AllocHGlobal((int)desiredSize);
                        break;
                    case BufferAllocMode.AkMemoryManager:
                        Create(desiredSize);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(AllocMode), AllocMode, null);
                }
            }

            var header = AkCommandBuffer.AK_CommandBuffer_Init(Buffer, desiredSize);
            if (header == IntPtr.Zero)
            {
                throw new Exception("CommandBuffer init failed");
            }
            
            if (!m_selfHandle.IsAllocated)
            {
                m_selfHandle = GCHandle.Alloc(this, GCHandleType.Normal);
            }

            SetCallbackHandle();
            
            unsafe
            {
                HeaderPtr->CompletionCallback = _handleCallback;
                HeaderPtr->CompletionCallbackCookie = GCHandle.ToIntPtr(m_selfHandle);
            }

            
            IsInitialized = true;

            return Header.BufferSize;
        }

        public uint Init(int desiredSize, bool clearExisting = false)
        {
            return Init((nuint)desiredSize, clearExisting);
        }

        public void ReInit()
        {
            Init(Size, true);
        }

        private static AkCommandBufferHeader Create(nuint desiredSize)
        {
            var header = AkCommandBuffer.AK_CommandBuffer_Create(desiredSize);
            if (header == System.IntPtr.Zero)
            {
                throw new CommandBufferAllocException(
                    "Cannot alloc new Command Buffer. Is AkMemoryManager initialized?");
            }

            return Marshal.PtrToStructure<AkCommandBufferHeader>(header);
        }

        public void FreeBuffer()
        {
            if (Buffer != System.IntPtr.Zero)
            {
                if (AllocMode == BufferAllocMode.Managed)
                {
                    Marshal.FreeHGlobal(Buffer);
                }
                else
                {
                    AkCommandBuffer.AK_CommandBuffer_Destroy(Buffer);
                }
                Buffer = System.IntPtr.Zero;
            }
        }

        public unsafe T* AddCommandUnsafe<T>() where T : unmanaged, IAkCommandType
        {
            // IAkCommandType can't be made static abstract until C# 11, so we need an instance
            T newCommand = new T();
            IntPtr commandAddr = AkCommandBuffer.AK_CommandBuffer_Add(Buffer, newCommand.CommandType);
            return (T*)commandAddr;
        }

        public bool AddCommand<T>(ref T command, AkCmd_Callback? callback) where T : IAkCommandType, new()
        {
            bool success = AddCommand(ref command);
            if (callback == null)
            {
                return success;
            }

            AkCmd_Callback callbackCommand = callback.Value;
            return AddCommand(ref callbackCommand);
        }
		
        public bool AddCommand<T>(ref T command) where T : IAkCommandType, new()
        {
            IntPtr commandAddr = AkCommandBuffer.AK_CommandBuffer_Add(Buffer, command.CommandType);
            if (commandAddr == System.IntPtr.Zero)
            {
                return false;
            }
            Marshal.StructureToPtr(command, commandAddr, false);
            return true;
        }
		
        public static nuint StringSize(string inString)
        {
            return AkCommandBuffer.AK_CommandBuffer_StringSize(inString);
        }

        public string? AddString(string inString)
        {
            var stringAddress = AkCommandBuffer.AK_CommandBuffer_AddString(Buffer, inString);
            if (stringAddress == System.IntPtr.Zero)
            {
                return null;
            }

            return Marshal.PtrToStringUTF8(stringAddress);
        }
        
        public static nuint ArraySize(nuint itemSize, ushort numItems)
        {
            return AkCommandBuffer.AK_CommandBuffer_ArraySize(itemSize, numItems);
        }
		
        public bool AddUnmanagedArray<T>(T[] items) where T : unmanaged
        {
            unsafe
            {
                fixed (void* pItems = &items[0])
                {
                    var result = AkCommandBuffer.AK_CommandBuffer_AddArray(Buffer, (nuint)sizeof(T), (ushort)items.Length, new System.IntPtr(pItems));

                    return result != IntPtr.Zero;
                }
            }
        }
        
        public bool AddArray<T>(T[] items)
        {
            var handle = GCHandle.Alloc(items, GCHandleType.Pinned);
            var ptr = (IntPtr)handle;
            try
            {
                var result =  AkCommandBuffer.AK_CommandBuffer_AddArray(Buffer, (nuint)Marshal.SizeOf<T>(), (ushort)items.Length, ptr);
                
                return result != IntPtr.Zero;
            }
            finally
            {
                handle.Free();
            }
        }
        
        public ulong ExternalSourcesSize(AkExternalSourceInfoSafeHandle[] sources)
        {
            ulong result = 0;
            foreach (var source in sources)
            {
                var handle = GCHandle.Alloc(source);
                result += AkCommandBuffer.AK_CommandBuffer_ExternalSourcesSize(1, ref source.AkExternalSourceInfoInternal);
                handle.Free();
            }
            return result;
        }
        
        public void AddExternalSources(AkExternalSourceInfoSafeHandle[] sources)
        {
            foreach (var source in sources)
            {
                var handle = GCHandle.Alloc(source);
                AkCommandBuffer.AK_CommandBuffer_AddExternalSources(Buffer, 1, ref source.AkExternalSourceInfoInternal);
                handle.Free();
            }
        }
        
        public ulong GeometrySize(AkGeometryParamsSafeHandle geometry)
        {
            var handle = GCHandle.Alloc(geometry);
            var result = AkCommandBuffer.AK_CommandBuffer_GeometrySize(ref geometry.AkAcousticSurfaceInternal);
            handle.Free();
            return result;
        }
        
        public void AddGeometry(AkGeometryParamsSafeHandle geometry)
        {
            var handle = GCHandle.Alloc(geometry);
            AkCommandBuffer.AK_CommandBuffer_AddGeometry(Buffer, ref geometry.AkAcousticSurfaceInternal);
            handle.Free();
        }

        public void RemoveLastCommand()
        {
            AkCommandBuffer.AK_CommandBuffer_Remove(Buffer);
        }

        public bool IsActive()
        {
            return Buffer != System.IntPtr.Zero;
        }

        public IEnumerable<CommandIteratorPayload> GetCommands()
        {
            AkCommandBufferIterator iterator;
            AkCommandBuffer.AK_CommandBuffer_Begin(Buffer, out iterator);

            while (AkCommandBuffer.AK_CommandBuffer_Next(ref iterator) > 0)
            {
                yield return new CommandIteratorPayload(Marshal.PtrToStructure<AkCommandHeader>(iterator._header), iterator._payload);
            }
        }

        public void Submit()
        {
            AkCommandBuffer.AK_CommandBuffer_Submit(Buffer);
        }
        
        public void SubmitNonBlocking()
        {
            AkCommandBuffer.AK_CommandBuffer_SubmitNonBlocking(Buffer);
        }
        
        public static uint GeneratePlayingId()
        {
            return AkCommandBuffer.AK_SoundEngine_GeneratePlayingID();
        }
        
        public static uint GetIdFromString(string inString)
        {
            return AkCommandBuffer.AK_SoundEngine_GetIDFromString(inString);
        }
        
        public static ulong GetOutputID(uint shareset, uint device)
        {
            return AkCommandBuffer.AK_SoundEngine_GetOutputID(shareset, device);
        }

        public virtual void SetCallbackHandle()
        {
            _handleCallback = CmdBufferOnCompletionCallback;
        }

        private void ReleaseUnmanagedResources()
        {
            FreeBuffer();
            if (m_selfHandle.IsAllocated)
            {
                m_selfHandle.Free();
            }
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~CommandBuffer()
        {
            ReleaseUnmanagedResources();
        }
    }
}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.