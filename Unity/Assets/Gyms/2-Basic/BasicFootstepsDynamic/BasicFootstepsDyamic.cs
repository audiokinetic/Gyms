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
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicFootstepsDyamic : MonoBehaviour
{
    [SerializeField] AK.Wwise.Switch[] _switchFootsteps;

    private Dictionary<string, AK.Wwise.Switch> _switch = new Dictionary<string, AK.Wwise.Switch>();
    private AK.Wwise.Switch _swithInPlay;
    private Ray _ray;
    private RaycastHit _hit;

    void Awake() {
        SetDictionary();
    }

    void FixedUpdate() {
        RaycastDown();
    }

    public AK.Wwise.Switch RaycastDown() {
        _ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(_ray, out _hit, 3f)) {
            MeshRenderer mesh = _hit.transform.gameObject.GetComponent<MeshRenderer>();
            string material = mesh.material.name;
            foreach (var switchFoot in _switch)
            {
                if(material.Contains(switchFoot.Key)) {
                    if(_swithInPlay == null)  
                        SetValueSwitch(switchFoot.Value);
                    else if(_swithInPlay.Id != switchFoot.Value.Id) 
                        SetValueSwitch(switchFoot.Value);
                    return switchFoot.Value;
                }
            }
        }
        return null;
    }

    private void SetDictionary() {
        for (int x = 0; x < _switchFootsteps.Length; x++)
        {
            string key = GetString(_switchFootsteps[x].Name);
            _switch.Add(key, _switchFootsteps[x]);
        }
    }

    private void SetValueSwitch(AK.Wwise.Switch s) {
        s.SetValue(gameObject);
        _swithInPlay = s;
    }

    /// <summary>
    /// Get the name of the switch as a string
    /// </summary>
    /// <param name="name">Ak.Wwise.Switch Name</param>
    /// <returns>name of the switch given</returns>
    private string GetString(string name) {
        string[] arrayString = name.Split("/ ");
        return arrayString[arrayString.Length - 1];
    }

}
