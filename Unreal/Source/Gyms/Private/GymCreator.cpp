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


#include "GymCreator.h"

#if WITH_EDITOR
#include "FileHelpers.h"
#include "ObjectTools.h"
#endif

bool UGymCreator::ContainsMultipleMaps(ETemplateMap TemplateIndex)
{
    switch (TemplateIndex)
    {
    case(ETemplateMap::OpenLevel):
        return true;
    }
    return false;
}

FString UGymCreator::GetMapName(ETemplateMap TemplateIndex, int MapNumber)
{
    FString Name;
    switch (TemplateIndex)
    {
    case(ETemplateMap::Empty):
        Name = "GymTemplate";
        break;
    case(ETemplateMap::Button):
        Name = "GymTemplateButton";
        break;
    case(ETemplateMap::ToggleButton):
        Name = "GymTemplateToggle";
        break;
    case(ETemplateMap::OpenLevel):
        Name = "GymTemplateOpenLevel";
        break;
    }
    if (MapNumber > 1)
    {
        Name += "_";
        Name.AppendInt(MapNumber);
    }
    return Name;
}

FString UGymCreator::CreateGym(FString CommonPath, FString Path, int TemplateIndex)
{
    ETemplateMap TemplateMap = (ETemplateMap)TemplateIndex;
    const FString TemplatePath =  FString("/Game") / "GymTemplate/";
    const FString TemplateGymPath = TemplatePath + GetMapName(TemplateMap, 1) + "." + GetMapName(TemplateMap, 1);

    FString TemplateGymPath2;

    if (ContainsMultipleMaps(TemplateMap))
    {
        TemplateGymPath2 = TemplatePath + GetMapName(TemplateMap, 2) + "." + GetMapName(TemplateMap, 2);
    }

    IPlatformFile& FileManager = FPlatformFileManager::Get().GetPlatformFile();

    //Unreal Levels can't contain white spaces. We replace them with _ instead.
    Path = Path.Replace(TEXT(" "), TEXT("_"));

    int32 PathDepth = Path.Len() - Path.Replace(TEXT("/"), TEXT("")).Len() - 1;

    if (Path.Contains(".") || Path.IsEmpty())
    {
        return "Invalid Name";
    }

    if (!FileManager.CreateDirectoryTree(*(CommonPath + Path)))
    {
        return "Couldn't Create Gym";
    }
    int ParentFolderIndex = -1;
    FString Name = Path;
    FString ParentFolder = CommonPath;
    if (Path.FindLastChar('/', ParentFolderIndex))
    {
        Name = Path.RightChop(ParentFolderIndex + 1);
        ParentFolder += Path.Mid(0, ParentFolderIndex);
        //If not in the root folder and the directory already existed, move it's content
        if (FileManager.DirectoryExists(*ParentFolder) && PathDepth > 0)
        {
            FString SubFolderName = ParentFolder;
            int SubFolderIndex = -1;
            if (ParentFolder.FindLastChar('/', SubFolderIndex))
            {
                SubFolderName = ParentFolder.RightChop(SubFolderIndex + 1);
                //If the folder has files create a sub folder with same name and move the files
                TArray<FString> FoundFiles;
                FileManager.FindFiles(FoundFiles, *ParentFolder, nullptr);
                if (FoundFiles.Num() > 0)
                {
                    //Create new Folder if needed
                    FString NewFolderName = ParentFolder + "/" + SubFolderName + "/";
                    if (!FileManager.DirectoryExists(*NewFolderName))
                    {
                        FileManager.CreateDirectoryTree(*NewFolderName);
                    }
                    //Moved the file to the new folder
                    for(FString File :FoundFiles)
                    {
                        int FileNameIndex = -1;
                        File.FindLastChar('/', FileNameIndex);
                        FString FileName = File.RightChop(FileNameIndex + 1);
                        FString NewFileName = NewFolderName + FileName;
                        FileManager.MoveFile(*NewFileName, *File);                        ;
                    }
                }
            }

        }
    }

#if WITH_EDITOR
    UObject* TemplateMapObject = LoadObject<UWorld>(nullptr, *(TemplateGymPath + "." + GetMapName(TemplateMap, 1)));

    TArray<UPackage*> PackagesToSave;
    ObjectTools::FPackageGroupName NewGymName;
    NewGymName.PackageName = "/Game/Gyms/" / Name / Name; 
    NewGymName.ObjectName = Name;
    TSet<UPackage*> PackagesNotDuplicated;
    UObject* NewMap = ObjectTools::DuplicateSingleObject(TemplateMapObject, NewGymName, PackagesNotDuplicated);
    if (NewMap)
    {
        PackagesToSave.Add(NewMap->GetOutermost());
    }

    if (TemplateGymPath2 != FString())
    {
        ObjectTools::FPackageGroupName NewGymName2;
        NewGymName2.PackageName = "/Game/Gyms/" / Name / Name + "_2"; 
        NewGymName2.ObjectName = Name;
        
        UObject* TemplateMapObject2 = LoadObject<UWorld>(nullptr, *(TemplateGymPath2 + "." + GetMapName(TemplateMap, 2)));
        UObject* NewMap2 = ObjectTools::DuplicateSingleObject(TemplateMapObject2, NewGymName2, PackagesNotDuplicated);
        if (NewMap2)
        {
            PackagesToSave.Add(NewMap2->GetOutermost());
        }
    }

    if (PackagesToSave.Num() > 0)
    {
        FEditorFileUtils::PromptForCheckoutAndSave(PackagesToSave, false, false);
    }
#endif
    return Name + " Gym Created";
}