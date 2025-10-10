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
using System.Runtime.InteropServices;

namespace Wwise.AkCommandBuffer
{
    
    [Serializable]
    public class InvalidCommandCastException: InvalidCastException
    {
        public InvalidCommandCastException() : base() { }
        
        public InvalidCommandCastException(string message) : base(message) { }
        
        public InvalidCommandCastException(string message, Exception innerException) : base(message, innerException) { }
    
    }
    
    public class CommandIteratorPayload
    {
        internal AkCommandHeader header;
        internal IntPtr payload;

        public CommandIteratorPayload(AkCommandHeader header, IntPtr payload)
        {
            this.header = header;
            this.payload = payload;
        }

        public T GetCommand<T>() where T : IAkCommandType, new()
        {
            var cmd = new T();
            if (cmd.CommandType != (AkCommand)header.Code)
            {
                throw new InvalidCommandCastException($"Error trying to cast to {cmd.CommandType}. Data is of type {(AkCommand)header.Code}.");
            }
            return Marshal.PtrToStructure<T>(payload);
        }
        
        public AkCommand GetCommandType()
        {
            return (AkCommand)header.Code;
        }

        public AKRESULT GetCommandResult()
        {
            return (AKRESULT)header.Result;
        }
    }
}
#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.