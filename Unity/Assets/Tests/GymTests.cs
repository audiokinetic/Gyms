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
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class GymTests
{
    protected GameObject gameObject;
    protected IEnumerator StartTest(string SceneName)
    {
        yield return LoadScene(SceneName, LoadSceneMode.Single);
        LoadAsset(SceneName);
        yield return new WaitForEndOfFrame();
    }

    protected void LoadAsset(string SceneName)
    {
        GameObject testObject = Resources.Load<GameObject>(GeneratePath(SceneName));
        gameObject = GameObject.Instantiate(testObject);
    }

    protected IEnumerator FinishTest(string SceneName)
    {
        AkSoundEngine.StopAll();
        if (gameObject != null)
        {
            //Destroy the object to make sure the Bank is unloaded before starting the next test
            GameObject.Destroy(gameObject);
            yield return new WaitForEndOfFrame();
        }
        LogAssert.ignoreFailingMessages = false;
        yield return null;
    }
    
    protected IEnumerator FinishTestEditMode(string SceneName)
    {
        AkSoundEngine.StopAll();
        LogAssert.ignoreFailingMessages = false;
        yield return new WaitForEndOfFrame();
    }

    protected void LogOutput(string description, bool result)
    {
        Debug.Log(description + result);
    }

    protected void LoadBank(AkBank bank, bool loadInitBankAsync = false)
    {
#if UNITY_ADDRESSABLES && AK_WWISE_ADDRESSABLES
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying || AkWwiseEditorSettings.Instance.LoadSoundEngineInEditMode)
        {
            AK.Wwise.Unity.WwiseAddressables.AkAddressableBankManager.Instance.LoadInitBank(loadInitBankAsync);
        }
#else
        AK.Wwise.Unity.WwiseAddressables.AkAddressableBankManager.Instance.LoadInitBank(loadInitBankAsync);
#endif
#endif
        bank.HandleEvent(gameObject);
    }

    protected bool AreApproximatelyEqual(float actual, float expected, float precision)
    {
        bool result = false;
        if(Mathf.Abs(actual - expected) < precision)
        {
            result = true;
        }
        else
        {
            Debug.LogError("Expected: " + expected + "\n But was: " + actual);
        }
        Assert.AreEqual(true, result);
        return result;
    }

    protected string GeneratePath(string pathName)
    {
        return "TestObject_" + pathName;
    }

    protected IEnumerator LoadScene(string SceneName, LoadSceneMode mode)
    {
        if (mode != LoadSceneMode.Single)
        {
            yield return SceneManager.LoadSceneAsync(SceneName, mode);
        }
        else
        {
            SceneManager.LoadScene(SceneName);   
        }
        yield return null;
    }

    protected IEnumerator UnloadScene(string SceneName)
    {
        yield return SceneManager.UnloadSceneAsync(SceneName);
    }

    protected uint PostSilence()
    {
        return AkSoundEngine.PostEvent("Silence", gameObject);
    }

    protected void ExpectedLogError(string pattern, uint occurences = 1, LogType type = LogType.Error)
    {
        for(uint i = 0; i < occurences; i++)
        {
            string formatedPattern = @"\b" + pattern + @"\b";
            LogAssert.Expect(type, new Regex(formatedPattern));
        }
    }
    
    protected void ExpectedLogErrorAtLeastOnce(string pattern)
    {
        LogAssert.ignoreFailingMessages = true;
        string formatedPattern = @"\b" + pattern + @"\b";
        LogAssert.Expect(LogType.Error, new Regex(formatedPattern));
    }
#if UNITY_EDITOR
    protected bool ExpectsDomainReload()
    {
        bool expectDomainReload = true;
#if UNITY_2019_3_OR_NEWER
        if (UnityEditor.EditorSettings.enterPlayModeOptionsEnabled)
        {
            expectDomainReload = !UnityEditor.EditorSettings.enterPlayModeOptions.HasFlag(UnityEditor.EnterPlayModeOptions.DisableDomainReload);
        }
#endif
        return expectDomainReload;
    }
#endif

    protected void CheckFilePlaying(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (in_type != AkCallbackType.AK_Marker)
            return;

        Assert.IsTrue(((AkMarkerCallbackInfo)in_info).strLabel == (string)in_cookie, string.Format("Expected {0} to play, but it was {1}", (string)in_cookie, ((AkMarkerCallbackInfo)in_info).strLabel));
        Debug.LogError(string.Format("{0} is expecting to play (This is an expected error)", (string)in_cookie));
    }
}

