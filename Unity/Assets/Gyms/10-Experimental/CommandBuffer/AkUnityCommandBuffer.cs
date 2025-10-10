using System;
using AOT;

namespace Wwise.AkCommandBuffer
{
    public class UnityCommandBuffer : CommandBuffer
    {
        public UnityCommandBuffer(nuint desiredSize, BufferAllocMode bufferAllocMode = BufferAllocMode.Managed) : base(desiredSize, bufferAllocMode)
        {
        }

        public UnityCommandBuffer(IntPtr buffer) : base(buffer)
        {
        }

        public UnityCommandBuffer(int desiredSize, BufferAllocMode bufferAllocMode = BufferAllocMode.Managed) : base(desiredSize, bufferAllocMode)
        {
        }

        [MonoPInvokeCallback(typeof(AkCommandCallbackFunc))]
        public static void UnityCmdBufferOnCompletionCallback(IntPtr cookie)
        {
            CmdBufferOnCompletionCallback(cookie);
        }
        
        public override void SetCallbackHandle()
        {
            _handleCallback = UnityCmdBufferOnCompletionCallback;
        }

    }
}