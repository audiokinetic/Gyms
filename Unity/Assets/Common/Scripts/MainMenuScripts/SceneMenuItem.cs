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
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMenuItem : MenuItem
{
    protected string _sceneName = default;
    public void Init(string sceneName)
    {
        _sceneName = sceneName;
        _name.text = LocalizationSettings.StringDatabase.GetLocalizedString("GymNames", sceneName);
        if (_name.text == sceneName)
        {
            Debug.LogWarning("Couldn't find \"" + _name.text + "\" in the localization table \"GymNames\".");
        }
        gameObject.SetActive(false);
    }

    public override void Interact()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public override int GetNumberOfItems()
    {
        return 0;
    }

    public override float UpdatePosition(float step)
    {
        return step;
    }

    public override bool HasAvailableChildren()
    {
        return false;
    }

    public override bool IsClosed()
    {
        return false;
    }

    public override bool IsAScene()
    {
        return true;
    }

    public override GameObject GetLastAvailableChildren()
    {
        return gameObject;
    }
}
