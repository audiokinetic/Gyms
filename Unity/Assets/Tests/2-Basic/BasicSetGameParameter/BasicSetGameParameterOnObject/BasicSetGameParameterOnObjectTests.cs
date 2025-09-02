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
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BasicSetGameParameterOnObjectTests : GymTests
    {
        const string SceneName = "BasicSetGameParameterOnObject";
        [UnityTest]
        public IEnumerator BasicSetGameParameterOnObject_Tests()
        {
            yield return StartTest(SceneName);

            BasicSetGameParameterOnObjectTests_Component onObjectTestComponents = gameObject.GetComponent<BasicSetGameParameterOnObjectTests_Component>();

            //Set value
            onObjectTestComponents.rtpcClass.SetValue(onObjectTestComponents.gameObject, 300.0f);
            yield return new WaitForSeconds(0.2f);
            float value = onObjectTestComponents.rtpcClass.GetValue(onObjectTestComponents.gameObject);
            float expected = 300.0f;
            Assert.AreEqual(expected, value);
            LogOutput("Set value: ", true);

            //Set out of bounds value (lower)
            onObjectTestComponents.rtpcClass.SetValue(onObjectTestComponents.gameObject, -200.0f);
            yield return new WaitForSeconds(0.2f);
            value = onObjectTestComponents.rtpcClass.GetValue(onObjectTestComponents.gameObject);
            expected = -200.0f;
            Assert.AreEqual(expected, value);
            LogOutput("Set out of bounds lower: ", true);

            //Set out of bounds value (upper)
            onObjectTestComponents.rtpcClass.SetValue(onObjectTestComponents.gameObject, 1600.0f);
            yield return new WaitForSeconds(0.2f);
            value = onObjectTestComponents.rtpcClass.GetValue(onObjectTestComponents.gameObject);
            expected = 1600.0f;
            Assert.AreEqual(expected, value);
            LogOutput("Set out of bounds upper: ", true);

            //Set Game Parameter with Transition
            onObjectTestComponents.rtpcClass.SetValue(onObjectTestComponents.gameObject, 0.0f, 400);
            yield return new WaitForSeconds(0.2f);
            value = onObjectTestComponents.rtpcClass.GetValue(onObjectTestComponents.gameObject);
            //Game Parameter will transition linearly from 1600 to 0 in 0.4 seconds. It should be around 800 after 0.2 seconds 
            Assert.LessOrEqual(value, 850.0f);
#if UNITY_ADDRESSABLES && AK_WWISE_ADDRESSABLES
            //Due to the async nature of Addressables, we are a bit more lenient.
            Assert.GreaterOrEqual(value, 685.0f);
#else
            Assert.GreaterOrEqual(value, 750.0f);
#endif
            LogOutput("Set Game Parameter with Transition: ", true);

            yield return FinishTest(SceneName);
        }
    }
}
