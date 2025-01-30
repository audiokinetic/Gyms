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

/// <summary>
/// This class loads a specified scene when the players enters the trigger component.
/// The GameObject must have two children named Towards and Away
/// </summary>
public class AddSceneOnTriggerExit : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private Color _colorLoaded;
    [SerializeField] private Color _colorUnloaded;

    private Transform _pointTowardsScene;
    private Transform _pointAwayFromScene;
    private Material _material;
    private bool _isSceneLoaded = false;

    void Start()
    {
        _pointTowardsScene = transform.Find("Towards");
        _pointAwayFromScene = transform.Find("Away");
        _material = GetComponent<MeshRenderer>().material;
        OnSceneStatusChange(SceneManager.GetSceneByName(_sceneName).isLoaded);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool shouldUnloadScene = IsPlayerGoingAway(other.transform); // Should unload scene if the player is going away from it.
            if (_isSceneLoaded && shouldUnloadScene)
            {
                // Unload the specified scene.
                SceneManager.UnloadSceneAsync(_sceneName);
                OnSceneStatusChange(false);
            }
            else if (!_isSceneLoaded && !shouldUnloadScene)
            {
                // Load the specified scene and add it to the hierarchy.
                SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
                OnSceneStatusChange(true);
            }
        }
    }

    /// <summary>
    /// Checks if the players is going away from or towards the scene.
    /// </summary>
    /// <param name="inPlayer">The Transform component of the player.</param>
    /// <returns>True if the player is going away from the scene.</returns>
    private bool IsPlayerGoingAway(Transform inPlayer)
    {
        return Vector3.Distance(inPlayer.position, _pointAwayFromScene.position) <= Vector3.Distance(inPlayer.position, _pointTowardsScene.position);
    }

    /// <summary>
    /// Updates the appearance and the variables of the gameObject when the specified scene is loaded or unloaded.
    /// </summary>
    /// <param name="loadState">Is the scene loaded?</param>
    private void OnSceneStatusChange(bool loadState)
    {
        _isSceneLoaded = loadState;
        if (loadState)
        {
            _material.color = _colorLoaded;
            return;
        }
        else
        {
            _material.color = _colorUnloaded;
            return;
        }
    }
}
