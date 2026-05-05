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

#if !UE_BUILD_SHIPPING
#include "FunctionalTest.h"
#include "FunctionalTestBase.h"
#include "AutomationBlueprintFunctionLibrary.h"
#endif
#include "Gyms.h"
#include "AssetRegistry/AssetData.h"
#include "AssetRegistry/AssetRegistryModule.h"
#include "Engine/Engine.h"
#include "Engine/World.h"
#include "HAL/FileManager.h"
#include "Kismet/GameplayStatics.h"

#include "Wwise/API/WwisePlatformAPI.h"

#if WITH_EDITOR
#include "Settings/ProjectPackagingSettings.h"
#endif

TArray<UGymsBlueprintFunctionLibrary::FWorldSoftObjectPtr> UGymsBlueprintFunctionLibrary::GetAllGyms(const TArray<FString>& GymNames)
{
	const IAssetRegistry& AssetRegistry = FAssetRegistryModule::GetRegistry();

	TArray<FAssetData> AssetDataList;
	AssetRegistry.GetAssetsByPath(FName(TEXT("/Game/Gyms")), AssetDataList, /*bRecursive=*/ true);

	const FTopLevelAssetPath WorldClassPath = UWorld::StaticClass()->GetClassPathName();
	const bool bCheckGymNames = !GymNames.IsEmpty();

	TArray<FWorldSoftObjectPtr> Gyms;
	for (const FAssetData& AssetData : AssetDataList)
	{
		if (AssetData.AssetClassPath != WorldClassPath)
		{
			continue;
		}

		const FString AssetName = AssetData.AssetName.ToString();
		if (bCheckGymNames && !GymNames.Contains(AssetName))
		{
			continue;
		}

		const FString PackagePath = AssetData.PackagePath.ToString();
		const int32 LastSlashIndex = PackagePath.Find(TEXT("/"), ESearchCase::IgnoreCase, ESearchDir::FromEnd);
		const FString ParentFolderName = (LastSlashIndex != INDEX_NONE) ? PackagePath.RightChop(LastSlashIndex + 1) : PackagePath;

		if (AssetName == ParentFolderName)
		{
			Gyms.Emplace(FSoftObjectPath(AssetData.PackageName.ToString()));
		}
	}

	Gyms.Sort([](const FWorldSoftObjectPtr& Left, const FWorldSoftObjectPtr& Right)
	{
		return Left.GetUniqueID().GetLongPackageName() < Right.GetUniqueID().GetLongPackageName();
	});

	return Gyms;
}

bool UGymsBlueprintFunctionLibrary::IsMobilePlatform()
{
	auto PlatformName = UGameplayStatics::GetPlatformName();
	return PlatformName.Compare(TEXT("Android")) == 0 || PlatformName.Compare(TEXT("IOS")) == 0;
}

void UGymsBlueprintFunctionLibrary::FireEvent(const FGenericCallback& CallbackEvent)
{
	CallbackEvent.ExecuteIfBound();
}

void UGymsBlueprintFunctionLibrary::OpenLevelTestingAdditionalSteps(AActor* TestActor)
{
#if !UE_BUILD_SHIPPING
	if (AFunctionalTest* FunctionalTestActor = Cast<AFunctionalTest>(TestActor))
	{
		FunctionalTestActor->bIsRunning = false;
	}
#endif
}

void UGymsBlueprintFunctionLibrary::ForceFinishingTest(AActor* TestActor)
{
#if !UE_BUILD_SHIPPING
	AFunctionalTest* FunctionalTestActor = Cast<AFunctionalTest>(TestActor);
	FFunctionalTestBase* FunctionalTest = static_cast<FFunctionalTestBase*>(FAutomationTestFramework::Get().GetCurrentTest());
	if (FunctionalTest && FunctionalTestActor)
	{
		FunctionalTestActor->bIsRunning = true;
		FunctionalTest->SetFunctionalTestComplete(FunctionalTestActor->TestLabel);
	}
#endif
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

int32 UGymsBlueprintFunctionLibrary::GetOutputDeviceId(const FString& DeviceName)
{
#if defined(PLATFORM_MICROSOFT) && PLATFORM_MICROSOFT && !(defined(PLATFORM_XB1) && PLATFORM_XB1) && !(defined(PLATFORM_XBOXONE) && PLATFORM_XBOXONE)
	const auto Platform = IWwisePlatformAPI::Get();
	return Platform->GetDeviceIDFromName((wchar_t*)*DeviceName);
#else
	return -1;
#endif
}

void UGymsBlueprintFunctionLibrary::IgnoreErrorMessages(const FString& IgnoredError)
{
#if !UE_BUILD_SHIPPING
	UAutomationBlueprintFunctionLibrary::AddExpectedLogError(IgnoredError, 0);
#endif
	UE_LOG(LogTemp, Error, TEXT("%s"), *IgnoredError);
}
