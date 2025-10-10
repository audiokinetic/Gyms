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
using UnityEngine;

namespace Wwise.AkCommandBuffer
{
    public class Manager
    {
        private static Manager m_Instance;
        private static CommandBuffer commandBuffer;
        private EventHandler handleCommandBufferCompletion;
        
        Manager()
        {
            commandBuffer = new UnityCommandBuffer(1000);
        }

        public static Manager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Manager();
                }

                return m_Instance;
            }
        }

        public AkCmd_Callback CreateCallbackCommand(AkCommandCallbackFunc callback,
            System.IntPtr? callbackCookie = null)
        {
            AkCmd_Callback command = new AkCmd_Callback()
            {
                Callback = callback,
                CallbackCookie = callbackCookie ?? System.IntPtr.Zero
            };
            return command;
        }

        public void Tick()
        {
            commandBuffer.Submit();
        }

        public void PostEvent(uint eventId, GameObject gameObject)
        {
            PostEvent(eventId, gameObject, null, IntPtr.Zero);
        }

        public void PostEvent(uint eventId, GameObject gameObject, AkEventCallbackFunc callback, IntPtr cookie)
        {
            var gameObjectId = AkUnitySoundEngine.GetAkGameObjectID(gameObject);
            var command = new AkCmd_PostEvent();
            command.EventID = eventId;
            command.PlayingID = CommandBuffer.GeneratePlayingId();
            command.GameObjectID = gameObjectId;
            if (callback != null)
            {
                command.Flags = (uint)(Wwise.AkCommandBuffer.AkCallbackType.AK_EndOfEvent);
                command.Callback = callback;
                command.CallbackCookie = cookie;
            }

            commandBuffer.AddCommand(ref command);
        }

        public void RegisterGameObject(GameObject gameObject, AkCmd_Callback? callback = null)
        {
            var command = new AkCmd_RegisterGameObject()
            {
                _gameObjectID = AkUnitySoundEngine.GetAkGameObjectID(gameObject)
            };
            commandBuffer.AddCommand(ref command, callback);
            commandBuffer.AddString(gameObject.name);
        }

        public void StopAll(GameObject gameObject, AkCmd_Callback? callback = null)
        {
            var command = new AkCmd_StopAll()
            {
                _gameObjectID = AkUnitySoundEngine.GetAkGameObjectID(gameObject)
            };
            commandBuffer.AddCommand(ref command, callback);
        }

        public void SetRtpc(uint rtpcID, float value, GameObject gameObject, AkCmd_Callback? callback = null)
        {
            var gameObjectId = AkUnitySoundEngine.GetAkGameObjectID(gameObject);
            var command = new AkCmd_SetRTPC()
            {
                _rtpcID = rtpcID,
                _rtpcValue = value,
                _gameObjectID = gameObjectId
            };
            commandBuffer.AddCommand(ref command, callback);
        }
    }
}
