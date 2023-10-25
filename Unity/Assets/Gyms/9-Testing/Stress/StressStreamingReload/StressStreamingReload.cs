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

public class StressStreamingReload : MonoBehaviour
{
    uint _testIterations = 0;
    const uint TestMaxIteration = 150;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReloadHandle());
        StartCoroutine(GarbageCollectorCall());
    }

    IEnumerator ReloadHandle()
    {
        while (_testIterations <= TestMaxIteration)
        {
            string sceneName = "StressStreamingReload_1";
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            yield return new WaitForEndOfFrame();
            SceneManager.UnloadSceneAsync(sceneName);
            
            sceneName = "StressStreamingReload_2";
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            yield return new WaitForEndOfFrame();
            SceneManager.UnloadSceneAsync(sceneName);
            _testIterations++;
        }
    }

    IEnumerator GarbageCollectorCall()
    {
        while (_testIterations <= TestMaxIteration)
        {
            System.GC.Collect();
            yield return new WaitForSeconds(Random.Range(0f, 0.001f));
        }
    }
}
