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
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

namespace Tests
{
    public class EditorInspectorTests : GymTests
    {
        const string SceneName = "EditorInspector";
        [UnityTest]
        public IEnumerator EditorInspector_Tests()
        {
#if UNITY_EDITOR && WWISE_ADDRESSABLES_2023

            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Gyms/8-Editor/" + SceneName + "/" + SceneName + ".unity");
            UnityEditor.EditorApplication.Step();

            gameObject = GameObject.Find(GeneratePath(SceneName));
            LoadBank(gameObject.GetComponent<AkBank>());
            uint playingId = PostSilence();

            AkEvent akEvent = gameObject.GetComponent<AkEvent>();
            AkEventInspector editor = UnityEditor.Editor.CreateEditor(akEvent) as AkEventInspector;
            editor.PlayEvent();

            if (AkWwiseEditorSettings.Instance.LoadSoundEngineInEditMode)
            {
                Assert.AreEqual(playingId + 2, PostSilence());
            }
            else
            {
                ExpectedLogError("Sound Engine is not initialized", 1, LogType.Warning);
            }
            editor.StopEvent();


            LogOutput("Playing an event in the Inspector in edit mode:", true);

            bool expectDomainReload = ExpectsDomainReload();
            yield return new EnterPlayMode(expectDomainReload);

            UnityEditor.EditorApplication.Step();
            gameObject = GameObject.Find(GeneratePath(SceneName));
            LoadBank(gameObject.GetComponent<AkBank>());
            playingId = PostSilence();
            editor = UnityEditor.Editor.CreateEditor(gameObject.GetComponent<AkEvent>()) as AkEventInspector;
            editor.PlayEvent();
            Assert.AreEqual(playingId + 2, PostSilence());
            editor.StopEvent();

            yield return new ExitPlayMode();
            editor.StopEvent();


            AkSoundEngine.StopAll();
#else
            yield return null;
#endif
        }
    }
}
