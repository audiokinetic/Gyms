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

#include "AWwiseAPIExample.h"
#include "AkComponent.h" 
#include "Wwise/API/WwiseSoundEngineAPI.h"

// Sets default values
AAWwiseAPIExample::AAWwiseAPIExample()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = false	;

}

void AAWwiseAPIExample::PostEventSoundEngineWwiseAPIExample(UAkAudioEvent* Event, AActor* GameObject)
{
	if (auto* SoundEngine = IWwiseSoundEngineAPI::Get())
	{
		if(Event && GameObject)
		{
			TArray<UAkComponent*> Array;
			GameObject->GetComponents<UAkComponent>(Array);
			//The Actor doesn't have an AkComponent attached. This component is needed if we want sound to be emitted from this actor.
			if (Array.Num() == 0)
			{
				//PostEvent doesn't load the event. The Event needs to be loaded beforehand.
				if (!Event->IsLoaded())
				{
					Event->LoadData();
				}
 
				auto* AkAudioComponent = NewObject<UAkComponent>(GameObject);
				AkAudioComponent->SetupAttachment(GameObject->GetDefaultAttachComponent());
				Array.Add(AkAudioComponent);
				AkAudioComponent->RegisterComponent();
			}
 
			SoundEngine->PostEvent(Event->GetWwiseShortID(), Array[0]->GetAkGameObjectID());
		}
	}
}

void AAWwiseAPIExample::StopAllWwiseAPIExample()
{
	if (auto* SoundEngine = IWwiseSoundEngineAPI::Get())
	{
		SoundEngine->StopAll();
	}
}