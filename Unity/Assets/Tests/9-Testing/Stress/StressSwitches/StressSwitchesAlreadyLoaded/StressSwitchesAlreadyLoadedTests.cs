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
    public class StressSwitchesAlreadyLoadedTests : GymTests
    {
        const string SceneName = "StressSwitchesAlreadyLoaded";
        [UnityTest]
        public IEnumerator StressSwitchesAlreadyLoaded_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            AkBank[] banks = gameObject.GetComponents<AkBank>();
            AlreadyLoadedSwitchSwitch eventSwitchHolder = gameObject.GetComponent<AlreadyLoadedSwitchSwitch>();
            uint expected = PostSilence();

            // Loading Simple_Switch_withDefault and Simple_Switch - Switch1 and make sure the media is loaded.Sine_880Hz_500ms_mono
            banks[0].data.Load();
            banks[1].data.Load();
            ExpectedLogError("Sine_880Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventSwitchHolder.play_Simple_Switch_withDefault.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_880Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.2f);

            // Load Simple_Switch which uses the same Switch and post it.Sine_880Hz_500ms_mono
            eventSwitchHolder.switch1.data.SetValue(gameObject);
            ExpectedLogError("Sine_880Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventSwitchHolder.play_Simple_Switch.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_880Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.2f);

            // Unloading Simple_Switch - Switch1 and posting Simple_Switch.Sine_880Hz_500ms_mono
            //TODO Unload Simple_Switch-Switch1. Currently still loaded, however, we still expect the media to be loaded because of the Simple_Switch_withDefault event.
            ExpectedLogError("Sine_880Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventSwitchHolder.play_Simple_Switch.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_880Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.2f);

            Assert.IsTrue(false); // Failing since it misses a TODO.

            yield return new UnityEngine.WaitForSeconds(0.2f);
            banks[0].data.Unload();
            banks[1].data.Unload();
            yield return FinishTest(SceneName);
        }
    }
}
