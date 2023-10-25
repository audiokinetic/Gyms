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

#define LOCTEXT_NAMESPACE "GymEditorUtilities"

void FGymEditorUtilitiesModule::StartupModule()
{
#if UE_5_0_OR_LATER
	OnPreSaveWorldHandle = FEditorDelegates::PreSaveWorldWithContext.AddRaw(this, &FGymEditorUtilitiesModule::OnPreSaveWorld);
#else
	OnPreSaveWorldHandle = FEditorDelegates::PreSaveWorld.AddRaw(this, &FGymEditorUtilitiesModule::OnPreSaveWorld);
#endif
	// Need to wait for the AssetRegistry to finish discovering all assets to run PruneMapsToCook
	auto& AssetRegistryModule = FModuleManager::LoadModuleChecked<FAssetRegistryModule>("AssetRegistry");
	OnAssetRegistryFilesLoadedHandle = AssetRegistryModule.Get().OnFilesLoaded().AddRaw(this, &FGymEditorUtilitiesModule::PruneMapsToCook);
}

void FGymEditorUtilitiesModule::ShutdownModule()
{
#if UE_5_0_OR_LATER
	FEditorDelegates::PreSaveWorldWithContext.Remove(OnPreSaveWorldHandle);
#else
	FEditorDelegates::PreSaveWorld.Remove(OnPreSaveWorldHandle);
#endif
	if (FModuleManager::Get().IsModuleLoaded("AssetRegistry"))
	{
		auto& AssetRegistryModule = FModuleManager::GetModuleChecked<FAssetRegistryModule>("AssetRegistry");
		AssetRegistryModule.Get().OnFilesLoaded().Remove(OnAssetRegistryFilesLoadedHandle);
	}
}

#if UE_5_0_OR_LATER
void FGymEditorUtilitiesModule::OnPreSaveWorld(UWorld* World, FObjectPreSaveContext ObjectSaveContext)
#else
void FGymEditorUtilitiesModule::OnPreSaveWorld(uint32 SaveFlags, UWorld* World)
#endif
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
#if UE_5_0_OR_LATER
		PackagingSettings->TryUpdateDefaultConfigFile();
#else
		PackagingSettings->UpdateDefaultConfigFile();
#endif
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
#if UE_5_1_OR_LATER
		FAssetData MapAssetData = AssetRegistryModule.Get().GetAssetByObjectPath(FSoftObjectPath(ObjectPath), true);
#else
		FAssetData MapAssetData = AssetRegistryModule.Get().GetAssetByObjectPath(FName(ObjectPath), true);
#endif
		if (!MapAssetData.IsValid())
		{
			// Cannot change collection while iterating over it, add to array of things to remove
			MapsToRemove.Add(MapToCook.FilePath);
		}
	}

	if (MapsToRemove.Num() > 0)
	{
		PackagingSettings->MapsToCook.RemoveAll([&](FFilePath ItemInArray) { return MapsToRemove.Contains(ItemInArray.FilePath); });
#if UE_5_0_OR_LATER
		PackagingSettings->TryUpdateDefaultConfigFile();
#else
		PackagingSettings->UpdateDefaultConfigFile();
#endif
	}
}

IMPLEMENT_MODULE(FGymEditorUtilitiesModule, GymEditorUtilities);

#undef LOCTEXT_NAMESPACE
