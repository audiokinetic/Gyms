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

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace Tests
{
    public class BasicSetGlobalGameParameterTests : GymTests
    {
        const string SceneName = "BasicSetGlobalGameParameter";

        private float _initialValue = 0.2f;

        private BasicSetGlobalGameParameterTests_Component testComponents;

        [UnityTest]
        public IEnumerator BasicSetGlobalGameParameter_Tests()
        {
            yield return StartTest(SceneName);

            testComponents = gameObject.GetComponent<BasicSetGlobalGameParameterTests_Component>();

            _initialValue = testComponents.rtpcClass.GetGlobalValue();

            // Set value
            float expected = 0.3f;
            testComponents.rtpcClass.SetGlobalValue(expected);
            yield return new WaitForSeconds(0.2f);
            float value = testComponents.rtpcClass.GetGlobalValue();
            Assert.AreApproximatelyEqual(expected, value, 0.01f);
            LogOutput("Set value: ", true);

            // Set out of bounds value (lower)
            // Current behaviour is to return the value as given, even if that's outside the Min Range
            expected = -2f;
            testComponents.rtpcClass.SetGlobalValue(expected);
            yield return new WaitForSeconds(0.2f);
            value = testComponents.rtpcClass.GetGlobalValue();
            Assert.AreApproximatelyEqual(expected, value, 0.01f);
            LogOutput("Set out of bounds lower: ", true);

            // Set out of bounds value (upper)
            // Current behaviour is to return the value as given, even if that's outside the Max range
            expected = 300f;
            testComponents.rtpcClass.SetGlobalValue(300f);
            yield return new WaitForSeconds(0.2f);
            value = testComponents.rtpcClass.GetGlobalValue();
            Assert.AreApproximatelyEqual(expected, value, 0.01f);
            LogOutput("Set out of bounds upper: ", true);

            testComponents.rtpcClass.SetGlobalValue(_initialValue);

            yield return FinishTest(SceneName);
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            testComponents.rtpcClass.SetGlobalValue(_initialValue);
            float value = testComponents.rtpcClass.GetGlobalValue();
            if (Math.Abs(value - _initialValue) >= 0.01f)
            {
                Debug.LogError($"Failed to reset Global GameParamter Value. Actual: {value}. Expected: {_initialValue}");
            }
            yield return null;
        }
    }
}
