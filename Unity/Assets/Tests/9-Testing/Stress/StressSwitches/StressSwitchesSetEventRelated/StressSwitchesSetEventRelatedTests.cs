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
    public class StressSwitchesSetEventRelatedTests : GymTests
    {
        const string SceneName = "StressSwitchesSetEventRelated";
        [UnityTest]
        public IEnumerator StressSwitchesSetEventRelated_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            SetSwitchEventRelatedEvent eventBankHolder = gameObject.GetComponent<SetSwitchEventRelatedEvent>();
            uint expected = PostSilence();

            // Loading Post_SetStatePlaySwitch_Related and post it. Sine_880Hz_500ms_mono
            eventBankHolder.Post_SetStatePlaySwitch_related_bank.data.Load();
            ExpectedLogError("Sine_880Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventBankHolder.Post_SetStatePlaySwitch_related.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_880Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.1f);


            // Unloading Event. Media should be unloading. 
            eventBankHolder.Post_SetStatePlaySwitch_related_bank.data.Unload();
            yield return new UnityEngine.WaitForSeconds(0.1f);

            //Load and Post Post_SetNestedSwitchPlaySwitch_related.Expect to have only Sine_880Hz_500ms_mono media loaded. Sine_880Hz_500ms_mono
            eventBankHolder.Post_SetNestedSwitchPlaySwitch_related_bank.data.Load();
            ExpectedLogError("Sine_880Hz_500ms_mono is expecting to play");
            Assert.AreEqual(++expected, eventBankHolder.Post_SetNestedSwitchPlaySwitch_related.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "Sine_880Hz_500ms_mono"));
            yield return new UnityEngine.WaitForSeconds(0.1f);

            Assert.IsTrue(false); // All media are still loaded and not only Sine_880Hz_500ms_mono

            eventBankHolder.Post_SetNestedSwitchPlaySwitch_related_bank.data.Unload();
            yield return FinishTest(SceneName);
        }
    }
}
