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
#include "Kismet/BlueprintFunctionLibrary.h"
#include "GymCreator.generated.h"

UENUM()
enum class ETemplateMap
{
	Empty,
	Button,
	ToggleButton,
	OpenLevel
};

/**
 * 
 */
UCLASS()
class GYMS_API UGymCreator : public UBlueprintFunctionLibrary
{
	GENERATED_BODY()

	static bool ContainsMultipleMaps(ETemplateMap TemplateIndex);

	static FString GetMapName(ETemplateMap TemplateIndex, int MapNumber);

	UFUNCTION(BlueprintCallable, Category = "Gym Creator Helpers")
	static FString CreateGym(FString CommonPath, FString Path, int TemplateIndex);
};
