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
    public class StressSwitchesSetEventWithAnotherTests : GymTests
    {
        const string SceneName = "StressSwitchesSetEventWithAnother";
        [UnityTest]
        public IEnumerator StressSwitchesSetEventWithAnother_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            SetSwitchEventWithAnotherEvents eventsHolder = gameObject.GetComponent<SetSwitchEventWithAnotherEvents>();
            AkSwitch multiNestedAkSwitch = gameObject.GetComponent<AkSwitch>();
            uint expected = PostSilence();

            // Loading Post_SetStatePlaySwitch_withAnother and post it. ShouldNotPlay
            eventsHolder.Post_SetStatePlaySwitch_withAnother_bank.data.Load();
            ExpectedLogError("No default Switch value selected in group");
            ExpectedLogError("Unknown I/O device error");
            Assert.AreEqual(++expected, eventsHolder.Post_SetStatePlaySwitch_withAnother.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShouldNotPlay"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // Loading and Setting MultiNested - Switch1.Expect only media Sine_880Hz_500ms_mono to be loaded.
            eventsHolder.Play_MultiNestedSwitch_bank.data.Load();
            multiNestedAkSwitch.data.SetValue(gameObject);

            // Posting Post_SetStatePlaySwitch_withAnother.Sine_880Hz_500ms_mono
            ExpectedLogError("Sine_880Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventsHolder.Post_SetStatePlaySwitch_withAnother.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_880Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // Checking that no other Media was loaded with it. (Set Switch with string to another switch and make sure that the media do not play)
            AkSoundEngine.SetSwitch("MultiNested_1", "Switch_1_1", gameObject);
            AkSoundEngine.SetSwitch("MultiNested_1_1", "Switch_1_1_1", gameObject);
            ExpectedLogError("No default Switch value selected in group");
            ExpectedLogError("Unknown I/O device error");
            Assert.AreEqual(++expected, eventsHolder.Post_SetStatePlaySwitch_withAnother.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShouldNotPlay"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            eventsHolder.Post_SetStatePlaySwitch_withAnother_bank.data.Unload();
            eventsHolder.Play_MultiNestedSwitch_bank.data.Unload();
            yield return FinishTest(SceneName);
        }
    }
}
