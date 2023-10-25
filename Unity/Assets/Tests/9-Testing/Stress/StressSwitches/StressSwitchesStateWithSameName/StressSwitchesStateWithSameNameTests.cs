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

using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;

namespace Tests
{
    public class StressSwitchesStateWithSameNameTests : GymTests
    {
        const string SceneName = "StressSwitchesStateWithSameName";
        [UnityTest]
        public IEnumerator StressSwitchesStateWithSameName_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();
            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            StateSwitchWithSameNameSwitch switchHandle = gameObject.GetComponent<StateSwitchWithSameNameSwitch>();
            uint expected = PostSilence();

            // Loading Event and parent switch then Set SwitchState_Switch-Switch value.
            bank.data.Load();
            switchHandle.switchState_Switch.data.SetValue(gameObject);

            // Loading SwitchState Switch, set is to One and post the event. ShortMedia_0
            switchHandle.switchState_Switch_one.data.SetValue(gameObject);
            ExpectedLogError("ShortMedia_0 is expecting to play");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShortMedia_0"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // Loading SwitchState State, set is to One and post the event. Should still be Media_0 playing.
            switchHandle.switchState_State_one.data.SetValue();
            ExpectedLogError("ShortMedia_0 is expecting to play");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShortMedia_0"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // Set Switch SwitchState_Switch to State and post the event. Should be media_2 playing.
            switchHandle.switchState_State.data.SetValue(gameObject);
            ExpectedLogError("ShortMedia_2 is expecting to play");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShortMedia_2"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // Unloading Switch SwitchSate-One, media_0 should be unloaded.
            //TODO currently no way to unload only SwitchState-One
            Assert.IsTrue(false);

            yield return FinishTest(SceneName);
        }
    }
}
