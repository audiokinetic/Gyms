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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wwise.API.Runtime.WwiseTypes.WwiseObjectsManagers;

public class StressOpenLevel_LocalizedVoice : OpenLevel_Trigger
{
    [SerializeField]
    string _language;
    
    [SerializeField]
    AkEvent _event;
    
    protected override IEnumerator PreLoadAction()
    {
#if UNITY_ADDRESSABLES && AK_WWISE_ADDRESSABLES
        //Before switching scene, we need to make sure that the prepare event that has started is completed otherwise, we'll get an AkFileNotFound error after changing the language.
        List<string> bankToReload = new List<string>() {_event.data.WwiseObjectReference.DisplayName };
        //This will switch the language and wait for the bank to be loaded
        yield return WwiseEventReferencesManager.Instance.SetLanguageAndReloadLocalizedBanks(_language,bankToReload);
#if UNITY_WEBGL
        yield return _event.data.WwiseObjectReference.CompleteLoadBank();
#else
        yield return new WaitUntil(() => _event.data.WwiseObjectReference.CompleteLoadBank().IsCompleted);
#endif
#endif
        yield return null;
    }
}
