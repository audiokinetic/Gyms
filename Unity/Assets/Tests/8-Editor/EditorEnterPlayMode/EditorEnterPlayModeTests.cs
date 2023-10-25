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
using UnityEngine;

namespace Tests
{
    public class EditorEnterPlayModeTests : GymTests
    {
        const string SceneName = "EditorEnterPlayMode";
        [UnityTest]
        public IEnumerator EditorEnterPlayMode_Tests()
        {
#if UNITY_EDITOR && WWISE_ADDRESSABLES_2023

            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Gyms/8-Editor/" + SceneName + "/" + SceneName + ".unity");

            UnityEditor.EditorApplication.Step();
            gameObject = GameObject.Find(GeneratePath(SceneName));

            uint playingId = PostSilence();

            LogOutput("Object is registered in Editor: ", ObjectRegisteredCheck(gameObject));

            bool expectDomainReload = ExpectsDomainReload();
            yield return new EnterPlayMode(expectDomainReload);

            UnityEditor.EditorApplication.Step();

            gameObject = GameObject.Find(GeneratePath(SceneName));

            LogOutput("Object is registered in Play mode: ", ObjectRegisteredCheck(gameObject));

            AkBank bank = gameObject.GetComponent<AkBank>();
            bank.data.Load();

            if (playingId == 0)
            {
                playingId = PostSilence();
            }

            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            Assert.AreEqual(++playingId, akEvent.data.Post(gameObject));
            LogOutput("Play Sound in Play mode: ", true);
            GameObject copy = GameObject.Instantiate<GameObject>(gameObject);
            LogOutput("Object created during Play mode is registered: ", ObjectRegisteredCheck(copy));
            yield return new ExitPlayMode();

            UnityEditor.EditorApplication.Step();
            //After calling ExitPlayMode, every local variables of the test are set to their default values. Reinitialize them. The bank should remain loaded.
            expectDomainReload = true;
#if UNITY_2019_3_OR_NEWER
            expectDomainReload = !UnityEditor.EditorSettings.enterPlayModeOptions.HasFlag(UnityEditor.EnterPlayModeOptions.DisableDomainReload);
#endif

            gameObject = GameObject.Find(GeneratePath(SceneName));
            akEvent = gameObject.GetComponent<AkEvent>();

            bank = gameObject.GetComponent<AkBank>();

            if (AkWwiseEditorSettings.Instance.LoadSoundEngineInEditMode)
            {
                bank.data.Load();
            }

            Assert.AreEqual(AkWwiseEditorSettings.Instance.LoadSoundEngineInEditMode ? ++playingId : 0, akEvent.data.Post(gameObject));
            LogOutput("Play Sound in Edit mode: ", true);

            LogOutput("Object created in Edit mode is registered: ", ObjectRegisteredCheck(gameObject));

            AkSoundEngine.StopAll();
#else
            yield return null;
#endif
        }

#if UNITY_EDITOR && WWISE_ADDRESSABLES_2023
        bool ObjectRegisteredCheck(GameObject gameObject)
        {
            //If the editor is playing, objects should be registered
            if(UnityEditor.EditorApplication.isPlaying)
            {
                Assert.IsTrue(AkSoundEngine.IsGameObjectRegistered(gameObject));
            }
            //If the editor is not playing, object should be registered only if the "Load Sound Engine in Edit Mode" setting is on
            else if (AkWwiseEditorSettings.Instance.LoadSoundEngineInEditMode)
            {
                Assert.IsTrue(AkSoundEngine.IsGameObjectRegistered(gameObject));
            }
            else
            {
                Assert.IsFalse(AkSoundEngine.IsGameObjectRegistered(gameObject));
            }
            return AkSoundEngine.IsGameObjectRegistered(gameObject);
        }
#endif
    }
}
