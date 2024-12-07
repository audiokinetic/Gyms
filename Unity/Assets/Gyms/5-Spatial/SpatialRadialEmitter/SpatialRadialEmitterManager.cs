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

using UnityEngine;
using UnityEngine.UI;

public class SpatialRadialEmitterManager : MonoBehaviour
{
    private static SpatialRadialEmitterManager _instance;
    public static SpatialRadialEmitterManager instance { get { return _instance; } }

    [SerializeField] private AkRadialEmitter _radialEmitter;
    [SerializeField] protected Text _outerRadiusText;
    [SerializeField] protected Text _innerRadiusText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    public virtual void UpdateUI()
    {
        _innerRadiusText.text = _radialEmitter.innerRadius.ToString();
        _outerRadiusText.text = _radialEmitter.outerRadius.ToString();
    }
}
