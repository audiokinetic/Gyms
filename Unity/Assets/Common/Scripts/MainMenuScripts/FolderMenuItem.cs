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
using UnityEngine.UI;

public class FolderMenuItem : SceneMenuItem
{
    [SerializeField]
    Image _arrow;
    [SerializeField]
    SceneMenuItem _template;
    [SerializeField]
    FolderMenuItem _folderObject;

    bool _isOpen;
    bool _isEmpty = true;
    const float LowerLevelStep = 10;
    bool _containsJustAGym = true;

    protected FolderMenuItem mParent = null;
    protected int mItemCount = 0;

    public void Init(FolderHierarchy hierarchy)
    {
        _isOpen = false;

        //Load Folders
        GameObject verticalLayout = gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        verticalLayout.transform.Translate(new Vector3(LowerLevelStep, 0));
        _sceneName = hierarchy.Name;
        _name.text = LocalizationSettings.StringDatabase.GetLocalizedString("GymNames", hierarchy.Name);
        if (_name.text == hierarchy.Name)
        {
            Debug.LogWarning("Couldn't find \"" + _name.text + "\" in the localization table \"GymNames\".");
        }

        gameObject.name = hierarchy.Name;
        mItemCount = hierarchy.Subfolders.Length;
        foreach (FolderHierarchy subfolder in hierarchy.Subfolders)
        {
            if (!subfolder.IsScene() && subfolder.HasGym())
            {
                _containsJustAGym = false;
                FolderMenuItem folder = Instantiate(_folderObject, verticalLayout.transform);
                folder.Init(subfolder);
                folder.mParent = this;
                folder.gameObject.SetActive(false);
                _isEmpty = false;
            }
            //Only display if the scene has the same name as the folder
            else if(subfolder.IsScene())
            {
                SceneMenuItem fileUI = Instantiate(_template, verticalLayout.transform);
                fileUI.transform.Translate(new Vector3(LowerLevelStep, 0));
                fileUI.Init(hierarchy.Name);
                _isEmpty = false;
            }
        }

        if (_containsJustAGym)
        {
            _arrow.enabled = false;
        }
    }

    public override void Interact()
    {
        if (_containsJustAGym)
        {
            base.Interact();
            return;
        }
        _isOpen = !_isOpen;
        _arrow.transform.Rotate(new Vector3(0, 0, _isOpen ? -90 : 90));

        MainMenu menu = GetComponentInParent<MainMenu>();
        //Highlight the proper folder if it is selected using the mouse.
        menu.SetSelected(this);
        if (!_isEmpty)
        {
            foreach (MenuItem component in gameObject.GetComponentsInChildren<MenuItem>(true))
            {
                //Only display the childrens one level below the interacted object
                if (component.gameObject != gameObject && component.transform.parent.transform.parent == this.transform)
                {
                    component.gameObject.SetActive(_isOpen);
                }
            }
            
            UpdateMyParentItemCount(_isOpen ? mItemCount : -mItemCount);

            menu.UpdatePosition();
        }
    }
    
    public override int GetNumberOfItems()
    {
        return _isOpen ? mItemCount : -mItemCount;
    }

    public override float UpdatePosition(float step)
    {
        //The Height of the Folder is based on the number of visible children (including the recursive ones)
        float height = step * gameObject.GetComponentsInChildren<MenuItem>().Length;

        //Resize the components to update the vertical layout group
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        GetComponentInChildren<VerticalLayoutGroup>().GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height - step);
        return height;
    }

    public override bool HasAvailableChildren()
    {
        return _isOpen && !_isEmpty;
    }

    public override bool IsClosed()
    {
        return !_isOpen;
    }

    public override bool IsAScene()
    {
        return _containsJustAGym;
    }

    public override GameObject GetLastAvailableChildren()
    {
        if(HasAvailableChildren())
        {
            VerticalLayoutGroup subfolder = GetComponentInChildren<VerticalLayoutGroup>();
            return subfolder.transform.GetChild(subfolder.transform.childCount - 1).GetComponent<MenuItem>().GetLastAvailableChildren();
        }
        return gameObject;
    }

    public void UpdateMyParentItemCount(int itemCount)
    {
        if (mParent != null)
        {
            mParent.UpdateMyParentItemCount(itemCount);
            mParent.mItemCount += itemCount;
        }
    }
}
