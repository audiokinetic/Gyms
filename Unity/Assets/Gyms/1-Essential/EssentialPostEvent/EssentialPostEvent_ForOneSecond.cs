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
using UnityEngine;

public class EssentialPostEvent_ForOneSecond : MonoBehaviour
{
    [SerializeField]
    AK.Wwise.Event _event;
    [SerializeField]
    AK.Wwise.Bank _bank;
    [SerializeField]
    GameObject _gameObject;
    [SerializeField]
    bool _onPlayerGameObject;
    // Start is called before the first frame update
    public void Post()
    {
        StartCoroutine(PostTimer());
    }

    IEnumerator PostTimer()
    {
        _bank.Load();
        GameObject gameObjectToPost = _gameObject ? _gameObject : gameObject;
        if(_onPlayerGameObject)
        {
            gameObjectToPost = GameObject.FindGameObjectWithTag("MainCamera");
        }
        _event.Post(gameObjectToPost);
        yield return new WaitForSeconds(1);
        _event.Stop(gameObjectToPost);
    }
}