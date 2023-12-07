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

#include "GymsBlueprintFunctionLibrary.h"

#include "FunctionalTestBase.h"
#include "Gyms.h"
#include "HAL/FileManager.h"
#include "Kismet/GameplayStatics.h"
#if WITH_EDITOR
#include "Settings/ProjectPackagingSettings.h"
#endif

TArray<UGymsBlueprintFunctionLibrary::FWorldSoftObjectPtr> UGymsBlueprintFunctionLibrary::GetAllGyms(const TArray<FString>& GymNames)
{
	TArray<FString> FoundGymFiles;

	// Find all umap files in the Gyms folder
	FString GymsPath = FPaths::Combine(FPaths::ProjectContentDir(), TEXT("Gyms")) + TEXT("/");;
	IFileManager::Get().FindFilesRecursive(FoundGymFiles, *GymsPath, TEXT("*.umap"), true, false);

	FString ContentDir = FPaths::ProjectContentDir();
	if(!FoundGymFiles.IsEmpty())
	{
		//We're dealing with -Game
		if(FPaths::IsRelative(FoundGymFiles[0]) != FPaths::IsRelative(ContentDir))
		{
			FString BaseDir = FPaths::ConvertRelativePathToFull(FPaths::ProjectDir());
			ContentDir = FPaths::Combine(BaseDir, "Content/");
		}
	}
	FoundGymFiles.Sort();

	const bool CheckGymNames = !GymNames.IsEmpty();
	TArray<FWorldSoftObjectPtr> Gyms;
	// Return only the file name
	for (FString GymFile : FoundGymFiles)
	{
		TArray<FString> PathParts;
		GymFile.RemoveFromStart(ContentDir);
		GymFile.ParseIntoArray(PathParts, TEXT("/"));
		FString Filename = PathParts[PathParts.Num() - 1];
		Filename.RemoveFromEnd(TEXT(".umap"));

		if(!CheckGymNames || GymNames.Contains(Filename))
		{
			FString LastFolder = PathParts[PathParts.Num() - 2];
			if (Filename == LastFolder)
			{
				GymFile.RemoveFromEnd(TEXT(".umap"));
				GymFile = TEXT("/Game/") + GymFile;
				FWorldSoftObjectPtr SoftPtr(GymFile);
				Gyms.Add( SoftPtr );
			}
		}

	}
	return Gyms;
}

bool UGymsBlueprintFunctionLibrary::IsMobilePlatform()
{
	auto PlatformName = UGameplayStatics::GetPlatformName();
	return PlatformName.Compare(TEXT("Android")) == 0 || PlatformName.Compare(TEXT("IOS")) == 0;
}

void UGymsBlueprintFunctionLibrary::UpdateMapsToCook()
{
#if WITH_EDITOR
	TArray<FString> FoundGymFiles;
	FString GymsPath = FPaths::Combine(FPaths::ProjectContentDir(), TEXT("Gyms")) + TEXT("/");;
	IFileManager::Get().FindFilesRecursive(FoundGymFiles, *GymsPath, TEXT("*.umap"), true, false);
	UProjectPackagingSettings* PackagingSettings = GetMutableDefault<UProjectPackagingSettings>();
	PackagingSettings->MapsToCook.Empty();
	FFilePath NewPath;
	NewPath.FilePath = "/Game/MainMenu/MainMenu";
	PackagingSettings->MapsToCook.Add(NewPath);
	for (auto& GymFile : FoundGymFiles)
	{
		GymFile.RemoveFromStart(GymsPath);
		GymFile.RemoveFromEnd(TEXT(".umap"));
		if (!PackagingSettings->MapsToCook.ContainsByPredicate([GymFile](FFilePath ItemInArray) { return ItemInArray.FilePath == GymFile; }))
		{
			NewPath.FilePath = "/Game/Gyms/" + GymFile;
			PackagingSettings->MapsToCook.Add(NewPath);
#if UE_5_0_OR_LATER
			PackagingSettings->TryUpdateDefaultConfigFile();
#else
			PackagingSettings->UpdateDefaultConfigFile();
#endif
        }
    }
#endif
}

void UGymsBlueprintFunctionLibrary::FireEvent(const FGenericCallback& CallbackEvent)
{
	CallbackEvent.ExecuteIfBound();
}

void UGymsBlueprintFunctionLibrary::OpenLevelTestingAdditionalSteps(AFunctionalTest* TestActor)
{
	//In Unreal 5.1.1, Running a test while changing level will abort the test and make it fail. Fake ending the test to prevent it. 
	TestActor->bIsRunning = false;
}

void UGymsBlueprintFunctionLibrary::ForceFinishingTest(AFunctionalTest* TestActor)
{
	FFunctionalTestBase* FunctionalTest = static_cast<FFunctionalTestBase*>(FAutomationTestFramework::Get().GetCurrentTest());
	if (FunctionalTest && TestActor)
	{
#if UE_5_0_OR_LATER
		TestActor->bIsRunning = true;
		FunctionalTest->SetFunctionalTestComplete(TestActor->TestLabel);
#else
		FunctionalTest->SetFunctionalTestComplete(TestActor->GetName());
#endif
	}
}

void UGymsBlueprintFunctionLibrary::ForceGarbageCollection()
{
	CollectGarbage(EObjectFlags::RF_NoFlags);
}

bool UGymsBlueprintFunctionLibrary::IsPIE(UObject* WorldContextObject)
{
	EWorldType::Type WorldType = EWorldType::None;
	if (WorldContextObject)
	{
		UWorld* World = GEngine->GetWorldFromContextObject(WorldContextObject, EGetWorldErrorMode::ReturnNull);
		if(World)
		{
			WorldType = World->WorldType;	
		}
	}

	return WorldType == EWorldType::PIE;
}

FTopLevelAssetPath UGymsBlueprintFunctionLibrary::MakeTopLevelAssetPath(const FString& FullPathOrPackageName, const FString& AssetName)
{
	if (!FullPathOrPackageName.StartsWith(TEXT("/")))
		return FTopLevelAssetPath();

	return AssetName.IsEmpty() ? FTopLevelAssetPath(FullPathOrPackageName) : FTopLevelAssetPath(*FullPathOrPackageName, *AssetName);
}