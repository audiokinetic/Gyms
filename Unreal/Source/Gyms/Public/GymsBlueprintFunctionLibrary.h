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

#pragma once

#include "CoreMinimal.h"
#include "UObject/ObjectMacros.h"
#include "FunctionalTest.h"
#include "Engine/EngineTypes.h"

#include "Kismet/BlueprintFunctionLibrary.h"
#include "GymsBlueprintFunctionLibrary.generated.h"


DECLARE_DYNAMIC_DELEGATE(FGenericCallback);

/**
 * 
 */
UCLASS()
class GYMS_API UGymsBlueprintFunctionLibrary : public UBlueprintFunctionLibrary
{
	GENERATED_BODY()

	using FWorldSoftObjectPtr = TSoftObjectPtr<UWorld>;

	UFUNCTION(BlueprintCallable, Category="Gym Blueprint Helpers")
	static TArray<TSoftObjectPtr<UWorld>> GetAllGyms();

	UFUNCTION(BlueprintCallable, Category = "Gym Blueprint Helpers")
	static bool IsMobilePlatform();

	UFUNCTION(BlueprintCallable, Category = "Gym Blueprint Helpers")
	static void UpdateMapsToCook();

	UFUNCTION(BlueprintCallable, Category = "Gym Blueprint Utilities")
	static void FireEvent(const FGenericCallback& CallbackEvent);

	UFUNCTION(BlueprintCallable, Category = "Gym Blueprint Helpers")
	static void OpenLevelTestingAdditionalSteps(AFunctionalTest* TestActor);

	UFUNCTION(BlueprintCallable, Category = "Gym Blueprint Helpers")
	static void ForceFinishingTest(AFunctionalTest* TestActor);

	UFUNCTION(BlueprintCallable, Category = "Gym Blueprint Helpers")
	static void ForceGarbageCollection();
	
	UFUNCTION(BlueprintCallable, Category = "Gym Blueprint Helpers")
	static bool IsPIE(UObject* WorldContextObject);

	UFUNCTION(BlueprintCallable, Category = "Gym Blueprint Helpers")
	static FTopLevelAssetPath MakeTopLevelAssetPath(const FString& FullPathOrPackageName, const FString& AssetName);
	
};
