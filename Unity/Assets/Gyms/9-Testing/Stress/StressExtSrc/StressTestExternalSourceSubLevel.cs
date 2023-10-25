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

public class StressTestExternalSourceSubLevel : MonoBehaviour
{
    AkExternalSourceInfoArray _externalSourceInfoArray = new AkExternalSourceInfoArray(2);

    [SerializeField]
    string[] externalSourcesNames;

    [SerializeField]
    string[] mediaNames;

    [SerializeField]
    AK.Wwise.Event[] _events;

    // Start is called before the first frame update
    void Start()
    {
        if(mediaNames.Length == 0)
        {
            return;
        }
        _externalSourceInfoArray[0].iExternalSrcCookie = AkSoundEngine.GetIDFromString(externalSourcesNames[0]);
        _externalSourceInfoArray[0].szFile = mediaNames[0];
        _externalSourceInfoArray[0].idCodec = 2;

        _externalSourceInfoArray[1].iExternalSrcCookie = AkSoundEngine.GetIDFromString(externalSourcesNames[1]);
        _externalSourceInfoArray[1].szFile = mediaNames[0];
        _externalSourceInfoArray[1].idCodec = 2;

        StartCoroutine(SetRandomExternalSourcesWithDelay(0));
        StartCoroutine(SetRandomExternalSourcesWithDelay(1));
        StartCoroutine(PostEvents());
    }

    IEnumerator SetRandomExternalSourcesWithDelay(int id)
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 0.01f));
            SetRandomExternalSource(id);
        }
    }

    IEnumerator PostEvents()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 0.01f));
            for (int i = 0; i < 2; i++)
            {
                AkSoundEngine.PostEvent(_events[0].Id, gameObject, 0, null, 0, 1, _externalSourceInfoArray);
            }
        }
    }    

    void SetRandomExternalSource(int id)
    {
        _externalSourceInfoArray[id].szFile = mediaNames[Random.Range(0, mediaNames.Length)];
    }

}
