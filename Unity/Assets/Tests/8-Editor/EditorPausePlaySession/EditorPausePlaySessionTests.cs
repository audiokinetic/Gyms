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
    public class EditorPausePlaySessionTests : GymTests
    {
        const string SceneName = "EditorPausePlaySession";
        [UnityTest]
        public IEnumerator EditorPausePlaySession_Tests()
        {
#if UNITY_EDITOR && UNITY_2019_3_OR_NEWER && WWISE_ADDRESSABLES_2023
            if (UnityEditor.EditorSettings.enterPlayModeOptionsEnabled)
            {
                //Due to the nature of Start with domain reload disabled and the way,
                //the test framework handles the starts, this test cannot pass if Domain Reload is enabled.
                if(!UnityEditor.EditorSettings.enterPlayModeOptions.HasFlag(UnityEditor.EnterPlayModeOptions.DisableDomainReload))
                {
                    UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Gyms/8-Editor/" + SceneName + "/" + SceneName + ".unity");
                    UnityEditor.EditorApplication.Step();

                    UnityEditor.EditorApplication.isPaused = true;

                    gameObject = UnityEngine.GameObject.Find(GeneratePath(SceneName));
                    AkGameObj akGameObj = gameObject.GetComponent<AkGameObj>();
                    akGameObj.Register();
                    yield return new EnterPlayMode(ExpectsDomainReload());
                    gameObject = UnityEngine.GameObject.Find(GeneratePath(SceneName));

                    akGameObj = gameObject.GetComponent<AkGameObj>();
                    akGameObj.Unregister();
                    akGameObj.Register();
                    AkBank bank = gameObject.GetComponent<AkBank>();
                    bank.HandleEvent(gameObject);
                    uint expected = PostSilence() + 1;

                    UnityEditor.EditorApplication.Step();
                    UnityEditor.EditorApplication.Step();
                    Assert.AreEqual(expected, PostSilence());

                    LogOutput("Event is not posted when the editor is paused: ", true);

                    UnityEditor.EditorApplication.isPaused = false;
                    UnityEditor.EditorApplication.Step();
                    UnityEditor.EditorApplication.Step();
                    Assert.AreEqual(expected + 2, PostSilence());

                    WaitForEventCallback durationCallback = gameObject.GetComponent<WaitForEventCallback>();
                    WaitForCallback waitForCallback = new WaitForCallback(durationCallback, 5f, 0.1f);
                    yield return waitForCallback;
                    Assert.IsTrue(durationCallback.callbackCalled);

                    LogOutput("Event is posted and rendered when the editor is unpaused: ", true);

                    AkSoundEngine.StopAll();
                    UnityEditor.EditorApplication.Step();
                    AkEvent akEvent = gameObject.GetComponent<AkEvent>();

                    yield return new ExitPlayMode();
                }
            }
#endif
            yield return null;
        }
    }
}
