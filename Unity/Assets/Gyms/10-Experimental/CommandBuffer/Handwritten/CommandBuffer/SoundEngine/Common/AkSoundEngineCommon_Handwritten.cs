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
using System;
using System.Runtime.InteropServices;

namespace Wwise.AkCommandBuffer
{
    public class AkExternalSourceInfoSafeHandle: SafeHandle
    {
        private AkExternalSourceInfo _akExternalSourceInfoInternal;
        private bool _ownsSzFile = false;

        internal ref AkExternalSourceInfo AkExternalSourceInfoInternal => ref _akExternalSourceInfoInternal;

        internal AkExternalSourceInfoSafeHandle(AkExternalSourceInfo internalSurface): this()
        {
            _akExternalSourceInfoInternal = internalSurface;
        }

        public AkExternalSourceInfoSafeHandle() : base(new System.IntPtr(0), true)
        {
        }

        public static int InternalSize()
        {
            return Marshal.SizeOf<AkExternalSourceInfo>();
        }
        
        public uint IExternalSrcCookie
        {
            get => _akExternalSourceInfoInternal.IExternalSrcCookie;
            set => _akExternalSourceInfoInternal.IExternalSrcCookie = value;
        }

        public uint IdCodec
        {
            get => _akExternalSourceInfoInternal.IdCodec;
            set => _akExternalSourceInfoInternal._idCodec = value;
        }

        public string SzFile
        {
            get
            {
                return Marshal.PtrToStringUTF8(_akExternalSourceInfoInternal._szFile);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    FreeSzFile();
                    return;
                }
                
                if (!_ownsSzFile && _akExternalSourceInfoInternal._szFile != IntPtr.Zero)
                { throw new Exception($"Already using native string at {_akExternalSourceInfoInternal._szFile}");
                }

                else if (_ownsSzFile && _akExternalSourceInfoInternal._szFile != IntPtr.Zero)
                {
                    FreeSzFile();
                }

                _ownsSzFile = true;
                _akExternalSourceInfoInternal._szFile = Marshal.StringToCoTaskMemUTF8(value);
            }
        }

        public System.IntPtr PInMemory
        {
            get => _akExternalSourceInfoInternal._pInMemory;
            set => _akExternalSourceInfoInternal._pInMemory = value;
        }
        
        public uint UiMemorySize
        {
            get => _akExternalSourceInfoInternal._uiMemorySize;
            set => _akExternalSourceInfoInternal._uiMemorySize = value;
        }

        public uint IdFile
        {
            get => _akExternalSourceInfoInternal.IdFile;
            set => _akExternalSourceInfoInternal.IdFile = value;
        }

        private void FreeSzFile()
        {
            if (_ownsSzFile && _akExternalSourceInfoInternal._szFile != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(_akExternalSourceInfoInternal._szFile);
                _akExternalSourceInfoInternal._szFile = IntPtr.Zero;
            }
        }
        
        private void ReleaseUnmanagedResources()
        {
            FreeSzFile();
        }

        protected override bool ReleaseHandle()
        {
            ReleaseUnmanagedResources();
            return true;
        }

        public override bool IsInvalid => false;
    }
}