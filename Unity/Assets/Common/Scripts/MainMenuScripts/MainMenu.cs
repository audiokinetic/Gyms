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

public class MainMenu : MonoBehaviour
{
    MenuItem _selectedComponent = default;
    float _previousYAxis = 0;
    float _previousXAxis = 0;
    const float _step = 20;
    private int posInSelection = 0;
    private int totalAmountOfItem = 0;
    FolderHierarchy _folderHierarchy = default;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        const string SceneListPath = "FolderHierarchy";
        _folderHierarchy = Resources.Load<FolderHierarchy>(SceneListPath);

        //Load the first level folders
        InstantiateFolders();

        UpdatePosition();
    }

    private void InstantiateFolders()
    {
        FolderMenuItem folderTemplate = gameObject.GetComponentInChildren<FolderMenuItem>(true);
        foreach (FolderHierarchy subFolder in _folderHierarchy.Subfolders)
        {
            if (subFolder.HasGym())
            {
                FolderMenuItem folder = Instantiate(folderTemplate, folderTemplate.gameObject.transform.parent);
                folder.Init(subFolder);
                folder.gameObject.SetActive(true);
                if (_selectedComponent == default)
                {
                    _selectedComponent = folder;
                }
                totalAmountOfItem+=1;
            }
        }

        SetSelected(_selectedComponent);
    }

    private void Update()
    {
        //Down Input
        if ((Input.GetButtonDown("Vertical") || Input.GetAxisRaw("Vertical") < 0) && (_previousYAxis == 0 && Input.GetAxisRaw("Vertical") < 0))
        {
            DownInput();
        }
        //Up Input
        else if ((Input.GetButtonDown("Vertical") || Input.GetAxisRaw("Vertical") > 0) && (_previousYAxis == 0 && Input.GetAxisRaw("Vertical") > 0))
        {
            UpInput();
        }
        //Left Input
        else if ((Input.GetButtonDown("Horizontal") || Input.GetAxisRaw("Horizontal") < 0) && (_previousXAxis == 0 && Input.GetAxisRaw("Horizontal") < 0))
        {
            LeftInput();
        }
        //Right Input
        else if ((Input.GetButtonDown("Horizontal") || Input.GetAxisRaw("Horizontal") > 0) && (_previousXAxis == 0 && Input.GetAxisRaw("Horizontal") > 0))
        {
            RightInput();
        }
        else if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            _selectedComponent.Interact();
            totalAmountOfItem += _selectedComponent.GetNumberOfItems();
        }

        _previousYAxis = Input.GetAxisRaw("Vertical");
        _previousXAxis = Input.GetAxisRaw("Horizontal");
    }

    private void DownInput()
    {
        GameObject next = NextComponent(_selectedComponent.gameObject);
        if (next != null)
        {
            posInSelection += 1;
            SetSelected(next.GetComponent<MenuItem>());
        }
    }

    private void UpInput()
    {
        GameObject previous = PreviousComponent(_selectedComponent.gameObject);
        if (previous != null)
        {
            posInSelection -= 1;


            SetSelected(previous.GetComponent<MenuItem>());
        }
    }

    private void LeftInput()
    {
        GameObject parent = GetParent(_selectedComponent.gameObject);
        if (parent != null)
        {
            FolderMenuItem folder = parent.GetComponent<FolderMenuItem>();
            if (folder != null && !folder.IsClosed())
            {
                folder.Interact();
                SetSelected(folder);
            }
        }
        else if(!_selectedComponent.IsClosed())
        {
            _selectedComponent.Interact();
        }
    }

    private void RightInput()
    {
        if (_selectedComponent.IsClosed())
        {
            _selectedComponent.Interact();
            if(!_selectedComponent.IsAScene())
            {
                GameObject nextObject = NextComponent(_selectedComponent.gameObject);
                if (nextObject != null)
                {
                    SetSelected(nextObject.GetComponent<MenuItem>());
                }
            }
        }
        else
        {
            DownInput();
        }
    }

    public void UpdatePosition()
    {
        foreach(MenuItem component in GetComponentsInChildren<MenuItem>())
        {
            component.UpdatePosition(_step);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.GetComponent<VerticalLayoutGroup>().GetComponent<RectTransform>());
    }

    public void SetSelected(MenuItem component)
    {
        if (component != null)
        {
            Color selected = Color.cyan;
            Color notSelected = Color.black;

            if (_selectedComponent != default)
            {
                _selectedComponent.GetComponentInChildren<Text>().color = notSelected;
            }
            _selectedComponent = component;
            _selectedComponent.GetComponentInChildren<Text>().color = selected;
            CenterAround();
        }
    }

    void CenterAround()
    {
        GetComponentInParent<ScrollRect>().verticalNormalizedPosition =  1.0f - (posInSelection / (float) totalAmountOfItem);
    }

    GameObject NextComponent(GameObject currentObject)
    {
        FolderMenuItem folder = currentObject.GetComponent<FolderMenuItem>();
        //If the object is a folder and is open with children, get the first of his children.
        if(folder != null && folder.HasAvailableChildren())
        {
            return currentObject.GetComponentInChildren<VerticalLayoutGroup>().transform.GetChild(0).gameObject;
        }

        int index = currentObject.transform.GetSiblingIndex();
        if (index < currentObject.transform.parent.childCount - 1)
        {
            return currentObject.transform.parent.GetChild(index + 1).gameObject;
        }

        //Already on last child item, get next item from a parent.
        while(index == currentObject.transform.parent.childCount - 1)
        {
            currentObject = GetParent(currentObject);
            if(currentObject == null)
            {
                return null;
            }
            index = currentObject.transform.GetSiblingIndex();
        }
        return currentObject.transform.parent.GetChild(index + 1).gameObject;
    }

    GameObject PreviousComponent(GameObject currentObject)
    {
        int index = currentObject.transform.GetSiblingIndex();

        //If the object is the first.
        if (currentObject.transform.parent.gameObject == gameObject && index == 2)
        {
            return null;
        }

        //If the object is the first of their parent, return their parent
        if(index == 0)
        {
            return GetParent(currentObject);
        }

        return currentObject.transform.parent.GetChild(index - 1).GetComponent<MenuItem>().GetLastAvailableChildren();
    }

    GameObject GetParent(GameObject gameObject)
    {
        //Returns two level of parent to match the structure of the unity prefab.
        GameObject parent = gameObject.transform.parent.transform.parent.gameObject;
        if(parent.GetComponent<FolderMenuItem>() == null)
        {
            return null;
        }
        return parent;
    }

    public void GenerateFolderHierarchy()
    {
#if UNITY_EDITOR
        FolderHierarchyUtils.GenerateFolderHierarchy();
#endif
    }
}
