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
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
public class GymCreator : MonoBehaviour
{
    [SerializeField]
    InputField _path;

    [SerializeField]
    Dropdown _dropdown;

    [SerializeField]
    Text _message;

    enum SceneTemplate
    {
        Empty,
        Button,
        Toggle,
        OpenLevel
    }

    const string BasicTestFilePath = "Assets/Tests/TemplateTests.cs";
    const string BasicTestObjectPath = "Assets/GymCreator/GymTemplate/Resources/TestObject_GymTemplate.prefab";

    private void Start()
    {
        UnityEditor.EditorApplication.playModeStateChanged += UpdateMaps;
    }
    bool ContainsMultipleMaps(SceneTemplate scene)
    {
        switch (scene)
        {
        case (SceneTemplate.OpenLevel):
            return true;
        }
        return false;
    }

    string GetBasicScenePath(SceneTemplate scene, int mapNumber)
    {
        string path = "Assets/GymCreator/GymTemplate/";
        path += GetBasicSceneName(scene, mapNumber);
        return path + ".unity";
    }

    string GetBasicSceneName(SceneTemplate scene, int mapNumber)
    {
        string name = "";
        switch (scene)
        {
        case (SceneTemplate.Empty):
            name += "GymTemplate";
            break;
        case (SceneTemplate.Button):
            name += "GymTemplateButton";
            break;
        case (SceneTemplate.Toggle):
            name += "GymTemplateToggle";
            break;
        case (SceneTemplate.OpenLevel):
            name += "GymTemplateOpenLevel";
            break;
        }
        if(mapNumber > 1)
        {
            name += "_2";
        }
        return name;
    }

    void CopyScene(string sceneToCopyPath, string destinationPath, string name, string sceneToCopyName)
    {
        string sceneContent = File.ReadAllText(sceneToCopyPath);
        sceneContent = sceneContent.Replace("TemplateGym", name);
        sceneContent = sceneContent.Replace(sceneToCopyName, name);
        string newScenePath = destinationPath + "/" + name + ".unity";
        File.WriteAllText(newScenePath, sceneContent);
    }

    public void Create()
    {
        string gymPath = Directory.GetCurrentDirectory() + "/Assets/Gyms";
        string testPath = Directory.GetCurrentDirectory() + "/Assets/Tests";

        SceneTemplate templateType = (SceneTemplate)_dropdown.value;

        gymPath = gymPath + "/" + _path.text;
        gymPath.Replace("\\", "/");

        testPath = testPath + "/" + _path.text;
        testPath.Replace("\\", "/");

        int pathDepth = _path.text.Count(c => c == '/');

        Debug.Log(gymPath);
        if(!Directory.Exists(gymPath) && !Directory.Exists(testPath))
        {

            int index = gymPath.LastIndexOf("/");
            string name = gymPath.Substring(index + 1);
            string parentFolder = gymPath.Substring(0, index);

            //If not in the root folder and the directory already existed, move it's content
            if(Directory.Exists(parentFolder) && pathDepth > 0)
            {
                int subFolderIndex = parentFolder.LastIndexOf("/");
                string subFolderName = parentFolder.Substring(subFolderIndex);
                //if the folder has files create a sub folder with same name and move the files
                if (Directory.EnumerateFiles(parentFolder).Any())
                {
                    try
                    {
                        string newFolderName = parentFolder + subFolderName;
                        if(!Directory.Exists(newFolderName))
                        {
                            Directory.CreateDirectory(newFolderName);
                        }

                        string[] files = Directory.GetFiles(parentFolder);

                        // Move each file to the destination directory
                        foreach (string file in files)
                        {
                            // Check if it is a file (not a directory)
                            if (File.Exists(file))
                            {
                                string fileName = Path.GetFileName(file);
                                string destinationPath = Path.Combine(newFolderName, fileName);
                                File.Move(file, destinationPath);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        LogError(e);
                    }
                }
            }

            try
            {
                if (_path.text.Contains("."))
                {
                    throw new Exception("Invalid Path");
                }
                Directory.CreateDirectory(gymPath);
                Directory.CreateDirectory(testPath + "/Resources");

                string testFileText = File.ReadAllText(BasicTestFilePath);

                testFileText = testFileText.Replace("GymTemplate", name);
                testFileText = testFileText.Replace("Template", name);
                File.WriteAllText(testPath + "/" + name + "Tests.cs", testFileText);

                CopyScene(GetBasicScenePath(templateType, 1), gymPath, name, GetBasicSceneName(templateType, 1));

                if(ContainsMultipleMaps(templateType))
                {
                    CopyScene(GetBasicScenePath(templateType, 2), gymPath, name + "_2", GetBasicSceneName(templateType, 1));
                    string secondScenePath = gymPath + "/" + name + "_2.unity";
                    string sceneText = File.ReadAllText(secondScenePath);
                    sceneText = sceneText.Replace(name + "_2", name);
                    File.WriteAllText(secondScenePath, sceneText);
                }

                string relativePath = "Assets/Gyms" + "/" + name + "/" + name + ".unity";
                FolderHierarchyUtils.AddSceneToBuild(relativePath);

                string testObjectContent = File.ReadAllText(BasicTestObjectPath);
                testObjectContent = testObjectContent.Replace("TemplateGym", name);
                File.WriteAllText(testPath + "/Resources/" + "TestObject_" + name + ".prefab", testObjectContent);

                _message.color = Color.white;
                _message.text = "Gym " + name + " successfully created";
            }
            catch (Exception e)
            {
                LogError(e);
            }

        }
        else
        {
            GymAlreadyExistsWarning();
        }

    }

    private void LogError(Exception e)
    {
        Debug.LogWarning(e.ToString());
        _message.color = Color.red;
        _message.text = "Failed Creating Gym: " + e.ToString();
    }

    private void GymAlreadyExistsWarning()
    {
        _message.color = Color.red;
        _message.text = _path.text + " Gym already exists";
    }

    private void UpdateMaps(UnityEditor.PlayModeStateChange state)
    {
        if(state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
        {
            UnityEditor.EditorApplication.playModeStateChanged -= UpdateMaps;
            UnityEditor.AssetDatabase.Refresh();

            FolderHierarchyUtils.GenerateFolderHierarchy();
        }
    }

}
#endif