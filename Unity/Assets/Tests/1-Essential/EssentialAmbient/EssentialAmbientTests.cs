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
    public class EssentialAmbientTests : GymTests
    {
        const string SceneName = "EssentialAmbient";
        [UnityTest]
        public IEnumerator EssentialAmbient_Tests()
        {
            yield return StartTest(SceneName);

            AkAmbient[] ambients = gameObject.GetComponents<AkAmbient>();
            AkAmbient ambient = ambients[0];
            AkAmbient empty = ambients[1];
            AkBank bank = gameObject.GetComponent<AkBank>();

            bank.data.Unload();
            yield return new WaitForEndOfFrame();
            //Play unloaded Ambient
            uint id = ambient.data.Post(gameObject);
#if UNITY_ADDRESSABLES && AK_WWISE_ADDRESSABLES
            ExpectedLogError("will be delayed", type : LogType.Warning);
            Assert.AreEqual(AkSoundEngine.AK_PENDING_EVENT_LOAD_ID, id);
#elif UNITY_EDITOR
            ExpectedLogError("Could not post event");
            Assert.AreEqual(0, id);
#endif
#if !(UNITY_ADDRESSABLES && AK_WWISE_ADDRESSABLES) && UNITY_EDITOR
	        ExpectedLogError("Event ID not found");
#endif

            LogOutput("Play unloaded ambient: ", true);
            
            yield return new WaitForEndOfFrame();
            bank.data.Load();
            uint expected = PostSilence() + 1;

            //Play an ambient test
            id = ambient.data.Post(gameObject);
            Assert.AreEqual(expected, id);
            ambient.Stop(0);
            LogOutput("Play an ambient: ", true);

            //Stop an ambient that is not playing
            ambient.Stop(0);
            yield return new WaitForEndOfFrame();
            
            //Post no event tests
            id = empty.data.Post(gameObject);
            Assert.AreEqual(0, id);
            LogOutput("Play an empty ambient: ", true);
            yield return new WaitForEndOfFrame();
            
            //Stop an empty ambient
            yield return FinishTest(SceneName);
        }
    }
}
