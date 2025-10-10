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
using System.Collections;
using System.Runtime.InteropServices;
using AOT;
using Wwise.AkCommandBuffer;

namespace Wwise.AkCommandBuffer
{
    public class TestEventCallback : TestCallback
    {
        private uint playingId = 0;
        private GCHandle m_selfHandle;

        public uint PlayingId => playingId;
        public IntPtr Cookie => GCHandle.ToIntPtr(m_selfHandle);

        public TestEventCallback()
        {
            m_selfHandle = GCHandle.Alloc(this, GCHandleType.Normal);
        }

        ~TestEventCallback()
        {
            if (m_selfHandle.IsAllocated)
            {
                m_selfHandle.Free();
            }
        }

        [MonoPInvokeCallback(typeof(AkEventCallbackFunc))]
        public static void PostEventCallback(Wwise.AkCommandBuffer.AkCallbackType inEType,
            ref Wwise.AkCommandBuffer.AkEventCallbackInfo inPEventInfo, IntPtr inPCallbackInfo, IntPtr inPCookie)
        {
            if (inPCookie == IntPtr.Zero)
            {
                return;
            }

            GCHandle handle = GCHandle.FromIntPtr(inPCookie);
            TestEventCallback instance = handle.Target as TestEventCallback;

            if (instance != null)
            {
                instance.playingId = inPEventInfo.PlayingID;
                instance.MarkCompleted();
            }
        }
    }
}