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
using UnityEngine.Scripting;

public class StressMediaLoad_Script : MonoBehaviour
{
    bool _stopTest = false;
    float _time = 0;

    string switchMap = "";

    const float MaxDuration = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MediaMapHandle());
    }

    void Update()
    {
        if(!_stopTest)
        {
            StartCoroutine(FrameUpdate());
        }
    }

    IEnumerator FrameUpdate()
    {
        Debug.Log(_time);
        System.GC.Collect();
        _time += Time.deltaTime;
        if(_time > MaxDuration)
        {
            _stopTest = true;
            yield return null;
        }
        else
        {
            if(switchMap.Length > 0)
            {
                SceneManager.UnloadSceneAsync(switchMap);
            }
            switchMap = "StressMediaLoad_Switch_" + (char)('1' + Random.Range(0, 2));
            SceneManager.LoadScene(switchMap, LoadSceneMode.Additive);
        }
    }

    IEnumerator MediaMapHandle()
    {
        yield return new WaitForSeconds(0.2f);
        while (!_stopTest)
        {
            string mediaMap = (Random.Range(0, 2) == 0 ? "StressMediaLoad_Streaming" : "StressMediaLoad_Memory") + "Media";
            SceneManager.LoadScene(mediaMap, LoadSceneMode.Additive);
            yield return new WaitForSeconds(Random.Range(0f, 0.1f));
            SceneManager.UnloadSceneAsync(mediaMap);
        }
    }
}
