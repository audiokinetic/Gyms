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
    public class StressSwitchesUnloadWhilePlayingTests : GymTests
    {
        const string SceneName = "StressSwitchesUnloadWhilePlaying";
        [UnityTest]
        public IEnumerator StressSwitchesUnloadWhilePlaying_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();
            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            AkSwitch akSwitch = gameObject.GetComponent<AkSwitch>();
            uint expected = PostSilence();

            // Load Play_Simple_Switch_withDefault and Set Simple_Switch-Switch2
            bank.data.Load();
            akSwitch.data.SetValue(gameObject);
            yield return new UnityEngine.WaitForSeconds(0.1f);

            // Posting Simple_Switch_withDefault and unloading Simple_Switch-Switch2 at the same time. SineSweep_440 - 1k_2s_mono
            ExpectedLogError("SineSweep_440-1k_2s_mono is expecting to play");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "SineSweep_440-1k_2s_mono"));
            yield return new UnityEngine.WaitForSeconds(0.5f);
            bank.data.Unload();
            yield return new UnityEngine.WaitForSeconds(0.5f);

            yield return FinishTest(SceneName);
            bank.data.Unload();
        }
    }
}
