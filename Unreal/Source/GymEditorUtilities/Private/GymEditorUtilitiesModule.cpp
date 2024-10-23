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

#include "GymEditorUtilitiesModule.h"
#include "Settings/ProjectPackagingSettings.h"
#include "AssetRegistry/AssetRegistryModule.h"

#include "Editor.h"

#define LOCTEXT_NAMESPACE "GymEditorUtilities"

void FGymEditorUtilitiesModule::StartupModule()
{
	OnPreSaveWorldHandle = FEditorDelegates::PreSaveWorldWithContext.AddRaw(this, &FGymEditorUtilitiesModule::OnPreSaveWorld);
	// Need to wait for the AssetRegistry to finish discovering all assets to run PruneMapsToCook
	auto& AssetRegistryModule = FModuleManager::LoadModuleChecked<FAssetRegistryModule>("AssetRegistry");
	OnAssetRegistryFilesLoadedHandle = AssetRegistryModule.Get().OnFilesLoaded().AddRaw(this, &FGymEditorUtilitiesModule::PruneMapsToCook);
}

void FGymEditorUtilitiesModule::ShutdownModule()
{
	FEditorDelegates::PreSaveWorldWithContext.Remove(OnPreSaveWorldHandle);
	if (FModuleManager::Get().IsModuleLoaded("AssetRegistry"))
	{
		auto& AssetRegistryModule = FModuleManager::GetModuleChecked<FAssetRegistryModule>("AssetRegistry");
		AssetRegistryModule.Get().OnFilesLoaded().Remove(OnAssetRegistryFilesLoadedHandle);
	}
}

void FGymEditorUtilitiesModule::OnPreSaveWorld(UWorld* World, FObjectPreSaveContext ObjectSaveContext)
{
	auto ActiveLevels = World->GetLevels();
	for (auto& ActiveLevel : ActiveLevels)
	{
		FString LevelPath = ActiveLevel->GetOutermost()->GetPathName();
		EnsureLevelIsInPackagingSettings(LevelPath);
	}
}

void FGymEditorUtilitiesModule::EnsureLevelIsInPackagingSettings(const FString& LevelToAdd)
{
	UProjectPackagingSettings* PackagingSettings = GetMutableDefault<UProjectPackagingSettings>();
	if (!PackagingSettings->MapsToCook.ContainsByPredicate([LevelToAdd](FFilePath ItemInArray) { return ItemInArray.FilePath == LevelToAdd; }))
	{
		FFilePath NewPath;
		NewPath.FilePath = LevelToAdd;
		PackagingSettings->MapsToCook.Add(NewPath);
		PackagingSettings->TryUpdateDefaultConfigFile();
	}
}

void FGymEditorUtilitiesModule::PruneMapsToCook()
{
	auto& AssetRegistryModule = FModuleManager::LoadModuleChecked<FAssetRegistryModule>("AssetRegistry");
	UProjectPackagingSettings* PackagingSettings = GetMutableDefault<UProjectPackagingSettings>();
	TArray<FString> MapsToRemove;

	for (auto& MapToCook : PackagingSettings->MapsToCook)
	{
		int32 LastSlashIndex;
		MapToCook.FilePath.FindLastChar('/', LastSlashIndex);
		FString FileName = MapToCook.FilePath.RightChop(LastSlashIndex+1);
		FString ObjectPath = MapToCook.FilePath + TEXT(".") + FileName;
		FAssetData MapAssetData = AssetRegistryModule.Get().GetAssetByObjectPath(FSoftObjectPath(ObjectPath), true);
		if (!MapAssetData.IsValid())
		{
			// Cannot change collection while iterating over it, add to array of things to remove
			MapsToRemove.Add(MapToCook.FilePath);
		}
	}

	if (MapsToRemove.Num() > 0)
	{
		PackagingSettings->MapsToCook.RemoveAll([&](FFilePath ItemInArray) { return MapsToRemove.Contains(ItemInArray.FilePath); });
		PackagingSettings->TryUpdateDefaultConfigFile();
	}
}

IMPLEMENT_MODULE(FGymEditorUtilitiesModule, GymEditorUtilities);

#undef LOCTEXT_NAMESPACE
