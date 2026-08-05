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
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Wwise.API.Runtime.WwiseTypes.WwiseObjectsManagers;
using Debug = UnityEngine.Debug;

public class BasicLocalizedVoice : OnOffManager
{
    [SerializeField]
    AK.Wwise.Event _localizedEvent;
    public AK.Wwise.Event LocalizedEvent
    {
        get { return _localizedEvent; }
    }

#if UNITY_WEBGL
    public override void OnAction()
    {
        StartCoroutine(SetLanguage("en_US"));
    }

    public override void OffAction()
    {
        StartCoroutine(SetLanguage("fr_FR"));
    }
#else
    public override async void OnAction()
    {
        try
        {
            await SetLanguage("en_US");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error setting language: {e}");
        }
    }

    public override async void OffAction()
    {
        try
        {
            await SetLanguage("fr_FR");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error setting language: {e}");
        }
    }
#endif

#if UNITY_WEBGL
    public IEnumerator SetLanguage(string language)
    {
        List<string> bankToReload = new List<string>() { _localizedEvent.WwiseObjectReference.DisplayName };
        
        // Wait for language change to complete
        yield return WwiseEventReferencesManager.Instance.SetLanguageAndReloadLocalizedBanks(language, bankToReload);
        
        Debug.Log("Current language: " + AkUnitySoundEngine.GetCurrentLanguage());
        
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
        // Wait for bank to load
        yield return _localizedEvent.WwiseObjectReference.CompleteLoadBank();
#endif
        
        _localizedEvent.Post(gameObject);
    }
#else
    public async Task SetLanguage(string language)
    {
        List<string> bankToReload = new List<string>() { _localizedEvent.WwiseObjectReference.DisplayName };
        
        // Wait for language change to complete
        await WwiseEventReferencesManager.Instance.SetLanguageAndReloadLocalizedBanks(language, bankToReload);
        
        Debug.Log("Current language: " + AkUnitySoundEngine.GetCurrentLanguage());
        
#if AK_WWISE_ADDRESSABLES && UNITY_ADDRESSABLES
        // Wait for bank to load
        await _localizedEvent.WwiseObjectReference.CompleteLoadBank();
#endif
        
        _localizedEvent.Post(gameObject);
    }
#endif
}
