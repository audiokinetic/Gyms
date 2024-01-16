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

using UnityEngine;

public class BasicPostLocation_Event : MonoBehaviour
{
    public AK.Wwise.Event akEventLocation {get; set;}
    public AK.Wwise.CallbackFlags flags {get; set;}
    public AkEvent.CallbackData callBackEvent = default;
    public AkEvent akEvent { get; set;}


    public void CreateAkEvent() {
        akEvent = gameObject.AddComponent<AkEvent>();

        akEvent.useCallbacks = true;
        akEvent.data = akEventLocation;
        akEvent.triggerList.Clear(); //Make sure the trigger is on nothing

        SetCallBackEvent();
        akEvent.Callbacks.Add(callBackEvent);
    }

    private void SetCallBackEvent() {
        callBackEvent.Flags = flags;
        callBackEvent.FunctionName = "CallBackEnd";
        callBackEvent.GameObject = gameObject;
    }

    public void Post(AK.Wwise.Bank bank) {
        bank.Load();
        akEvent.HandleEvent(gameObject);
    }

    public void CallBackEnd(AkEventCallbackMsg callback) {
        Destroy(gameObject);
    }
    

    public void NewPosition(Vector3 pos) {
        gameObject.transform.position = pos;
    }

}
