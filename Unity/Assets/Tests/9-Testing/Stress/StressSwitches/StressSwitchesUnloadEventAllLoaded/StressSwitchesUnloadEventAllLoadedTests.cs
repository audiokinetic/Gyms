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
    public class StressSwitchesUnloadEventAllLoadedTests : GymTests
    {
        const string SceneName = "StressSwitchesUnloadEventAllLoaded";
        [UnityTest]
        public IEnumerator StressSwitchesUnloadEventAllLoaded_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();
            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            UnloadEventAllLoadedState states = gameObject.GetComponent<UnloadEventAllLoadedState>();

            uint expected = PostSilence();

            // Load Event and AkState and post the event. ShortMedia_0
            bank.data.Load();
            states.stateHigh.data.SetValue();
            ExpectedLogError("ShortMedia_0 is expecting to play");
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject, (uint)AkCallbackType.AK_Marker, CheckFilePlaying, "ShortMedia_0"));
            yield return new UnityEngine.WaitForSeconds(0.1f);


            // Unload Play_Simple_2. Expect all media to be unloaded (Currently no way to unload and test).
            Assert.IsTrue(false);


            yield return FinishTest(SceneName);
        }
    }
}
