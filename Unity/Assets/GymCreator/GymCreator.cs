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
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
enum SceneTemplate
{
    Empty,
    Button,
    Toggle,
    OpenLevel
}
public class GymCreator : UnityEditor.EditorWindow
{
    const string BasicTestFilePath = "Assets/Tests/TemplateTests.cs";
    const string BasicTestObjectPath = "Assets/GymCreator/GymTemplate/Resources/TestObject_GymTemplate.prefab";

    SceneTemplate _sceneTemplate = SceneTemplate.Empty;
    string _gymName = "";
    string _gymPath = "";
    
    [UnityEditor.MenuItem("Window/Gym Creator", false)]
    public static void InitGymCreatorWindow()
    {
        var window = GetWindow<GymCreator>("Gym Creator", true);

        SetIcon(window);
    }

    private void OnEnable()
    {
        GymCreator window = (GymCreator)EditorWindow.GetWindow(typeof(GymCreator));
        if (window != null)
        {
            SetIcon(window);
        }
    }

    private static void SetIcon(GymCreator window)
    {
        Texture2D originalIcon = EditorGUIUtility.Load("Assets/Icons/Icon-32 Sprite.png") as Texture2D;

        if (originalIcon != null)
        {
            window.titleContent = new GUIContent("Gym Creator", originalIcon);
        }
        else
        {
            window.titleContent = new GUIContent("Gym Creator");
        }
    }

    public void OnGUI()
    {
        using (new UnityEngine.GUILayout.VerticalScope("box"))
        {
            using (new UnityEngine.GUILayout.HorizontalScope())
            {
                UnityEditor.EditorGUILayout.PrefixLabel("Gym Name:");
                _gymName = UnityEngine.GUILayout.TextField(_gymName, TextField, UnityEngine.GUILayout.Height(17));
            }

            using (new UnityEngine.GUILayout.HorizontalScope())
            {
                UnityEditor.EditorGUILayout.PrefixLabel("Gym Path:");
                UnityEditor.EditorGUILayout.SelectableLabel(_gymPath, TextField, UnityEngine.GUILayout.Height(17));

                if (Ellipsis())
                {
                    var GymBasePath = System.IO.Path.Combine(UnityEngine.Application.dataPath, "Gyms/");
                    GymBasePath = GymBasePath.Replace("\\", "/");
                    var GymPathSelected = UnityEditor.EditorUtility.OpenFolderPanel("Select your Gym Path (It must be within Assets/Gyms)", _gymPath, "");

                    if(GymPathSelected.Contains(GymBasePath))
                    {
                        _gymPath = GymPathSelected.Replace(GymBasePath, string.Empty);
                    }
                    else if (GymPathSelected.Length > 0)
                    {
                        UnityEditor.EditorUtility.DisplayDialog("Error", "The Gym Path must be within the Gyms folder (Assets/Gyms)", "Ok");
                    }
                }
            }

            using (new UnityEngine.GUILayout.HorizontalScope())
            {
                UnityEditor.EditorGUILayout.PrefixLabel("Scene Template:");
                _sceneTemplate = (SceneTemplate)UnityEditor.EditorGUILayout.EnumPopup(_sceneTemplate);
            }

            if(GUILayout.Button("Create Gym"))
            {
                if (_gymName.Length == 0)
                {
                    Debug.LogError("Gym Name is empty");
                }
                else
                {
                    Create();
                }
            }
            
            if(GUILayout.Button("Update Scenes in Build"))
            {
                FolderHierarchyUtils.GenerateFolderHierarchy();
            }
        }
    }
    
    private static bool Ellipsis()
    {
        return UnityEngine.GUILayout.Button("...", UnityEngine.GUILayout.Width(30));
    }
    
    private static UnityEngine.GUIStyle textField;
    public static UnityEngine.GUIStyle TextField
    {
        get
        {
            if (textField == null)
                textField = new UnityEngine.GUIStyle("textfield");
            return textField;
        }
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

        string newGymPath = System.IO.Path.Combine(_gymPath, _gymName); 
        newGymPath = newGymPath.Replace("\\", "/");

        testPath = testPath + "/" + newGymPath;
        testPath = testPath.Replace("\\", "/");

        int pathDepth = newGymPath.Count(c => c == '/');

        if(!Directory.Exists(newGymPath) && !Directory.Exists(testPath))
        {

            int index = newGymPath.LastIndexOf("/");
            string name = newGymPath.Substring(index + 1);
            string parentFolder = newGymPath.Substring(0, index);
            CheckForFilesAtPath(parentFolder, pathDepth);

            index = testPath.LastIndexOf("/");
            name = testPath.Substring(index + 1);
            parentFolder = testPath.Substring(0, index);
            CheckForFilesAtPath(parentFolder, pathDepth);

            try
            {
                if (newGymPath.Contains("."))
                {
                    throw new Exception("Invalid Path");
                }
                gymPath = System.IO.Path.Combine(gymPath, newGymPath);
                Directory.CreateDirectory(gymPath);
                Directory.CreateDirectory(testPath + "/Resources");

                string testFileText = File.ReadAllText(BasicTestFilePath);

                testFileText = testFileText.Replace("GymTemplate", name);
                testFileText = testFileText.Replace("Template", name);
                File.WriteAllText(testPath + "/" + name + "Tests.cs", testFileText);

                CopyScene(GetBasicScenePath(_sceneTemplate, 1), gymPath, name, GetBasicSceneName(_sceneTemplate, 1));

                if(ContainsMultipleMaps(_sceneTemplate))
                {
                    CopyScene(GetBasicScenePath(_sceneTemplate, 2), gymPath, name + "_2", GetBasicSceneName(_sceneTemplate, 1));
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
                
                UnityEditor.AssetDatabase.Refresh();
                FolderHierarchyUtils.GenerateFolderHierarchy();
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

    private void CheckForFilesAtPath(string parentFolder, int pathDepth)
    {
        //If not in the root folder and the directory already existed, move it's content
        if(Directory.Exists(parentFolder) && pathDepth > 0)
        {
            int subFolderIndex = parentFolder.LastIndexOf("/");
            string subFolderName = parentFolder.Substring(subFolderIndex);
            //if the folder has files create a sub folder with same name and move the files
            if (Directory.EnumerateFiles(parentFolder).Any(f => !f.EndsWith(".meta")))
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
    }

    private void LogError(Exception e)
    {
        Debug.LogError(e.ToString());
    }
    private void GymAlreadyExistsWarning()
    {
        Debug.LogWarning("Gym Already Exists at Path:" + _gymPath);
    }
}
#endif