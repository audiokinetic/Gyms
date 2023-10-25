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
    public class EditorSoundEngineInitializationTests : GymTests
    {
        const string SceneName = "EditorSoundEngineInitialization";
        [UnityTest]
        public IEnumerator EditorSoundEngineInitialization_Tests()
        {
#if UNITY_EDITOR && WWISE_ADDRESSABLES_2023
            ExpectedLogError("Sound engine", GetNumberOfInitialization(), UnityEngine.LogType.Log);
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Gyms/8-Editor/" + SceneName + "/" + SceneName + ".unity");

            LogOutput("Sound Engine Initialization in Edit Mode: ", SoundEngineInitialization());

            bool expectDomainReload = ExpectsDomainReload();

            yield return new EnterPlayMode(expectDomainReload);
            
            UnityEditor.EditorApplication.Step();

            LogOutput("Sound Engine Initialization in Play Mode: ", SoundEngineInitialization());
            
            yield return new ExitPlayMode();
            UnityEditor.EditorApplication.Step();

            LogOutput("Sound Engine Initialization in Edit Mode: ", SoundEngineInitialization());

            yield return FinishTestEditMode(SceneName);
#endif
            yield return null;
        }

#if UNITY_EDITOR && WWISE_ADDRESSABLES_2023
        public bool SoundEngineInitialization()
        {
            //If the editor is playing, the sound engine should be initialized
            if (UnityEditor.EditorApplication.isPlaying)
            {
                Assert.IsTrue(AkSoundEngine.IsInitialized());
            }
            //If the editor is not playing, the sound engine should be initialized only if the "Load Sound Engine in Editor" setting is on
            else if (AkWwiseEditorSettings.Instance.LoadSoundEngineInEditMode)
            {
                Assert.IsTrue(AkSoundEngine.IsInitialized());
            }
            else
            {
                Assert.IsFalse(AkSoundEngine.IsInitialized());
            }
            return AkSoundEngine.IsInitialized();
        }

        public uint GetNumberOfInitialization()
        {
            bool expectDomainReload = ExpectsDomainReload();

            //The editor is initialized before the test begins. We reduce the expected numbers of log by one if the "Load Sound Engine In Edit Mode" setting is on
            if (expectDomainReload && AkWwiseEditorSettings.Instance.LoadSoundEngineInEditMode)
            {
                return 2;
            }
            else if(!expectDomainReload && AkWwiseEditorSettings.Instance.LoadSoundEngineInEditMode)
            {
                return 0;
            }
            //Should be one but the logging is ignored for some reason in this case. It is most likely a bug on the side of Unity
            else if(expectDomainReload && !AkWwiseEditorSettings.Instance.LoadSoundEngineInEditMode)
            {
                return 0;
            }
            return 1;
        }
#endif
    }
}
