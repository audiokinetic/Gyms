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
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class SmokeSubLevelNestedTests : GymTests
    {
        const string SceneName = "SmokeSubLevelNested";
        [UnityTest]
        public IEnumerator SmokeSubLevelNested_Tests()
        {
            yield return StartTest(SceneName);
            AkBank bank = gameObject.GetComponent<AkBank>();
            AkSwitch akSwitch = gameObject.GetComponent<AkSwitch>();
            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            LoadBank(bank);

            uint expected = PostSilence() + 1;

            yield return LoadScene(SceneName + "_2", LoadSceneMode.Additive);

            yield return LoadScene(SceneName + "_3", LoadSceneMode.Additive);

            akSwitch.data.SetValue(gameObject);

            LogOutput("Post Event refrencing a loaded nested switch:", true);
            Assert.AreEqual(expected, akEvent.data.Post(gameObject));
            
            yield return UnloadScene(SceneName + "_3");

            LogOutput("Post Event refrencing an unloaded nested switch:", true);
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject));

            yield return UnloadScene(SceneName + "_2");

            LogOutput("Post a nested event with only the first map loaded:", true);
            Assert.AreEqual(++expected, akEvent.data.Post(gameObject));

            yield return FinishTest(SceneName);
        }
    }
}
