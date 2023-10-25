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
    public class StressSwitchesValueWithoutSwitchContainerTests : GymTests
    {
        const string SceneName = "StressSwitchesValueWithoutSwitchContainer";
        [UnityTest]
        public IEnumerator StressSwitchesValueWithoutSwitchContainer_Tests()
        {
            UnityEngine.Debug.Log("This Gym is not implemented yet.");
            yield break;
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();
            SwitchValueWithoutSwitchContainerSwitch switchesHolder = gameObject.GetComponent<SwitchValueWithoutSwitchContainerSwitch>();

            bank.data.Load();

            // Check that Switch One is not loaded.
            // TODO: Currently no way to check/load/unload individual Switch Value.

            // Load Switch One and make sure it is loaded.
            // TODO: Currently no way to check/load/unload individual Switch Value.

            // Make sure this doesn't load Switch Two.
            // TODO: Currently no way to check/load/unload individual Switch Value.

            // Unload Switch One and make sure it is unloaded.
            // TODO: Currently no way to check/load/unload individual Switch Value.

            // Stress Test Load/ Unload Asset.Expect Switch Two to be unloaded at the end.
            // TODO: Currently no way to check/load/unload individual Switch Value.


            Assert.IsTrue(false);

            yield return new UnityEngine.WaitForSeconds(0.2f);
            yield return FinishTest(SceneName);
            bank.data.Unload();
        }
    }
}
