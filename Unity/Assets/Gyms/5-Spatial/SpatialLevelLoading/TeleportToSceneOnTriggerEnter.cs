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
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class loads a specified scene when the players enters the trigger component.
/// </summary>
public class TeleportToSceneOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private string _sceneName = DEFAULT_GATE_TEXT;
    [SerializeField] private string _gateNameOverride;
    [SerializeField] private Text _gateText;

    private const string DEFAULT_GATE_TEXT = "<color=yellow>{Insert Scene Name}</color>";

    /// <summary>
    /// Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector.
    /// </summary>
    private void OnValidate()
    {
        if (_gateText)
        {
            if (string.IsNullOrEmpty(_sceneName))
            {
                _sceneName = DEFAULT_GATE_TEXT;
            }

            if (string.IsNullOrEmpty(_gateNameOverride))
            {
                if (_sceneName != _gateText.text) // Update the text of _gateText if the _sceneName field has been edited.
                {
                    _gateText.text = $"Go To\n{_sceneName}";
                }
            }
            else
            {
                if (_gateNameOverride != _gateText.text) // Update the text of _gateText if the _sceneName field has been edited.
                {
                    _gateText.text = $"Go To\n{_gateNameOverride}";
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(_sceneName, LoadSceneMode.Single); // Load the specified scene and closes the current active scene.
        }
    }
}
