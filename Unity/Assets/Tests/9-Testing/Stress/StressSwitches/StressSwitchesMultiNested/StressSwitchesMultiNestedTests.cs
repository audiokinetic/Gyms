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
    public class StressSwitchesMultiNestedTests : GymTests
    {
        const string SceneName = "StressSwitchesMultiNested";
        [UnityTest]
        public IEnumerator StressSwitchesMultiNested_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();
            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            MultiNestedSwitchSwitch switchesHolder = gameObject.GetComponent<MultiNestedSwitchSwitch>();
            uint expected = PostSilence();

            // Load Event and Associated Switches and post the event. MultiNested-Switch1, MultiNested_1-Switch_1_1, MultiNested_1_1-Switch_1_1_1 -> ShortMedia_0
            bank.data.Load();
            switchesHolder.MultiNested_Switch1.data.SetValue(gameObject);
            switchesHolder.MultiNested_1_Switch_1_1.data.SetValue(gameObject);
            switchesHolder.MultiNested_1_1_Switch_1_1_1.data.SetValue(gameObject);
            ExpectedLogError("ShortMedia_0 is expecting to play");
            akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShortMedia_0");
            yield return new UnityEngine.WaitForSeconds(0.2f);

            // Unloading Switch_1_1 and expect ShortMedia_0 to be unloaded. ShouldNotPlay
            //TODO Unload Switch_1_1
            ExpectedLogError("was not loaded for this source");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShouldNotPlay"));
            yield return new UnityEngine.WaitForSeconds(0.2f);

            // Load and set MultiNested_1_2-Switch_1_2_2 and expect ShortMedia_3 to not be loaded. MultiNested_1_2-Switch_1_2_2 -> ShouldNotPlay
            //TODO load Switch_1_2_Switch_1_2_2. Actually already loaded.
            switchesHolder.MultiNested_1_2_Switch_2_2_2.data.SetValue(gameObject);
            ExpectedLogError("was not loaded for this source");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShouldNotPlay"));
            yield return new UnityEngine.WaitForSeconds(0.2f);

            // Load and Set MultiNested_1-Switch_1_2 value and expect ShortMedia_3 to load.
            //TODO load Switch_1_2. Actually already loaded.
            switchesHolder.MultiNested_1_Switch_1_2.data.SetValue(gameObject);
            ExpectedLogError("ShortMedia_3 is expecting to play");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShortMedia_3"));
            yield return new UnityEngine.WaitForSeconds(0.2f);

            yield return FinishTest(SceneName);
        }
    }
}
