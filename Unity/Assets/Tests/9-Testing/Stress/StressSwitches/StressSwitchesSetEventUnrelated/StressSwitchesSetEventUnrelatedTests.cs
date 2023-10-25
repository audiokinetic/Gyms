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
    public class StressSwitchesSetEventUnrelatedTests : GymTests
    {
        const string SceneName = "StressSwitchesSetEventUnrelated";
        [UnityTest]
        public IEnumerator StressSwitchesSetEventUnrelated_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            AkBank[] banks = gameObject.GetComponents<AkBank>();
            SetSwitchEventUnrelatedEvent eventHolder = gameObject.GetComponent<SetSwitchEventUnrelatedEvent>();
            uint expected = PostSilence();

            // Loading Play_Simple_Switch.
            banks[0].data.Load();
            banks[1].data.Load();

            // Loading Post_SetStatePlaySwitch_unrelated and post it. ShortMedia_6
            ExpectedLogError("ShortMedia_6 is expecting to play");
            Assert.AreEqual(++expected, eventHolder.Post_SetStatePlaySwitch_unrelated.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShortMedia_6"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // Post Play_Simple_Switch.Sine_880Hz_500ms_mono
            ExpectedLogError("Sine_880Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventHolder.play_simple_Switch.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_880Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            banks[0].data.Unload();
            banks[1].data.Unload();
            yield return FinishTest(SceneName);
        }
    }
}
