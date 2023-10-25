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
using UnityEngine.SceneManagement;

public class StressOpenLevel_ExternalSources : StressOpenLevel_Trigger
{
    [SerializeField]
    string[] externalSourcesChange;

    [SerializeField]
    string[] externalSourcesBase;

    [SerializeField]
    AK.Wwise.Event _event;

    AkExternalSourceInfoArray _externalSourceInfoArray = new AkExternalSourceInfoArray(3);

    private void Start()
    {
        _externalSourceInfoArray[0].iExternalSrcCookie = AkSoundEngine.GetIDFromString("One");
        _externalSourceInfoArray[0].szFile = externalSourcesBase[0];
        _externalSourceInfoArray[0].idCodec = 2;

        _externalSourceInfoArray[1].iExternalSrcCookie = AkSoundEngine.GetIDFromString("Two");
        _externalSourceInfoArray[1].szFile = externalSourcesBase[1];
        _externalSourceInfoArray[1].idCodec = 2;

        _externalSourceInfoArray[2].iExternalSrcCookie = AkSoundEngine.GetIDFromString("Three");
        _externalSourceInfoArray[2].szFile = externalSourcesBase[2];
        _externalSourceInfoArray[2].idCodec = 2;

        AkSoundEngine.PostEvent(_event.Id, gameObject, 0, null, 0, 3, _externalSourceInfoArray);
    }

    protected override void PostLoadAction()
    {
        for(int i = 0; i < 3; i++)
        {
            if(externalSourcesChange.Length < i)
            {
                _externalSourceInfoArray[i].szFile = externalSourcesChange[i];
            }
        }
    }
}
