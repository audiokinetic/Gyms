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

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FolderHierarchy : ScriptableObject
{
    public string Path = "";
    public string Name = "";
    public string AssetName = "";
    public FolderHierarchy[] Subfolders;
    public bool HasGym()
    {
        foreach(FolderHierarchy subfolder in Subfolders)
        {
            if(subfolder.IsScene() && subfolder.Name == Name)
            {
                return true;
            }
            if(subfolder.HasGym())
            {
                return true;
            }
        }
        return false;
    }

    public bool IsScene()
    {
		//When it is a scene, the path will be empty.
        return Path.Length == 0;
    }

#if UNITY_EDITOR
    public void CreateAsset(string gymFolderPath, string pathToSave)
    {
        GenerateHierarchy(gymFolderPath, gymFolderPath);
        SaveAsset(pathToSave);
    }

    public void GenerateHierarchy(string path, string previousPath)
    {
        Name = path.Replace(previousPath + "/", "");
        AssetName = path.Replace("Assets/Gyms/", "");
        AssetName = AssetName.Replace("/", "_");
        if (path == previousPath)
        {
            Name = "FolderHierarchy";
            AssetName = "FolderHierarchy";
        }
        Path = path;
        string[] directories = AssetDatabase.GetSubFolders(path);
        string[] files = AssetDatabase.FindAssets(Name, new[] { path });

        List<string> specificFiles = new List<string>();
        for(int i = 0; i < files.Length; i++)
        {
            string fileName = AssetDatabase.GUIDToAssetPath(files[i]);
            if(fileName.Contains(".unity"))
            {
                fileName = fileName.Replace(Path + "/", "").Replace(".unity", "");
                if (fileName == Name)
                {
                    specificFiles.Add(fileName);
                }
            }

        }
        Subfolders = new FolderHierarchy[directories.Length + specificFiles.Count];
        for (int i = 0; i < directories.Length; i++)
        {
            Subfolders[i] = CreateInstance<FolderHierarchy>();
            Subfolders[i].GenerateHierarchy(directories[i], Path);
        }

        //Load Scenes
        for (int i = 0; i < specificFiles.Count; i++)
        {
            FolderHierarchy scene = CreateInstance<FolderHierarchy>();
            scene.Name = specificFiles[i];
            string scenePath = path + "/" + specificFiles[i] + ".unity";
            Subfolders[i + directories.Length] = scene;
        }
    }

    public void SaveAsset(string pathToSave)
    {
        if (Subfolders != null)
        {
            foreach (FolderHierarchy subfolder in Subfolders)
            {
                subfolder.SaveAsset(pathToSave);
            }
        }
        if(AssetName == "")
        {
            AssetDatabase.CreateAsset(this, pathToSave + Name + (IsScene() ? "Gym" : "") + ".asset");
        }
        else
        {
            AssetDatabase.CreateAsset(this, pathToSave + AssetName + (IsScene() ? "Gym" : "") + ".asset");
        }
    }
#endif
}
