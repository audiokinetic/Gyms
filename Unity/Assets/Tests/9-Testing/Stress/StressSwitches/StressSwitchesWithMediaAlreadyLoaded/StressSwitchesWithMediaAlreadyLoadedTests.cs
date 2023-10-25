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
using UnityEngine;

namespace Tests
{
    public class StressSwitchesWithMediaAlreadyLoadedTests : GymTests
    {
        const string SceneName = "StressSwitchesWithMediaAlreadyLoaded";
        [UnityTest]
        public IEnumerator StressSwitchesWithMediaAlreadyLoaded_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            AkSoundEngine.StartProfilerCapture("meow.prof");
            yield return StartTest(SceneName);
            SwitchWithMediaAlreadyLoadedSwitch eventSwitchHolder = gameObject.GetComponent<SwitchWithMediaAlreadyLoadedSwitch>();

            uint expected = PostSilence();

            // 1 - Loading Play_Simple_Switch_withDefault to load Sine_880Hz_500ms_mono media.
            Debug.Log("### 1 - Loading Play_Simple_Switch_withDefault to load Sine_880Hz_500ms_mono media. ###");
            eventSwitchHolder.simple_Switch_withDefault_bank.data.Load();
            ExpectedLogError("Sine_880Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventSwitchHolder.simple_Switch_withDefault.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_880Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // 2 - Loading Simple_Switch - Switch1 and Play_Simple_Switch which uses Sine_880Hz_500ms_mono.
            Debug.Log("### 2 - Loading Simple_Switch - Switch1 and Play_Simple_Switch which uses Sine_880Hz_500ms_mono. ###");
            eventSwitchHolder.play_Simple_Switch_bank.data.Load();
            eventSwitchHolder.simple_Switch_Switch1.data.SetValue(gameObject);
            ExpectedLogError("Sine_880Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventSwitchHolder.play_Simple_Switch.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_880Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // 3 - Unload Play_Simple_Switch_withDefault and check if the media is still loaded.
            Debug.Log("### 3 - Unload Play_Simple_Switch_withDefault and check if the media is still loaded. ###");
            eventSwitchHolder.simple_Switch_withDefault_bank.data.Unload();
            yield return new UnityEngine.WaitForSeconds(0.1f);
            ExpectedLogError("Sine_880Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventSwitchHolder.play_Simple_Switch.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_880Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // 4 - Unload Switch1, load and Set Simple_Switch2 to load Sine_440Hz_500ms_mono.Sine_880Hz_500ms_mono should be unloaded after that.
            Debug.Log("### 4 - Unload Switch1, load and Set Simple_Switch2 to load Sine_440Hz_500ms_mono.Sine_880Hz_500ms_mono should be unloaded after that. ###");
            //TODO actually no way to unload switch1.
            eventSwitchHolder.simple_Switch_Switch2.data.SetValue(gameObject);
            ExpectedLogError("Sine_440Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventSwitchHolder.play_Simple_Switch.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_440Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // 5 - Load MultiNested - Switch3 Switch and Posting it, it uses the same media Sine_440Hz_500ms_mono.
            Debug.Log("### 5 - Load MultiNested - Switch3 Switch and Posting it, it uses the same media Sine_440Hz_500ms_mono. ###");
            eventSwitchHolder.play_MultiNestedSwitch_bank.data.Load();
            eventSwitchHolder.multiNested_Switch3.data.SetValue(gameObject);
            ExpectedLogError("Sine_440Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventSwitchHolder.play_MultiNestedSwitch.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_440Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // 6 - Unload MultiNestedSwitch Switch and check if the media is unloaded; it should not.
            Debug.Log("### 6 - Unload MultiNestedSwitch Switch and check if the media is unloaded; it should not. ###");
            eventSwitchHolder.play_MultiNestedSwitch_bank.data.Unload();
            yield return new UnityEngine.WaitForSeconds(0.1f);
            ExpectedLogError("Sine_440Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventSwitchHolder.play_Simple_Switch.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_440Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            yield return FinishTest(SceneName);
            eventSwitchHolder.play_Simple_Switch_bank.data.Unload();
            AkSoundEngine.StopProfilerCapture();
        }
    }
}
