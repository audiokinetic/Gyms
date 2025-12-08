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
using UnityEngine;
using Wwise.API.Runtime;

public class AdvancedMultiplePositions_Interact : OnOffManager
{
    // Left speaker
    public GameObject ambientLeft;

    // Right speaker
    public GameObject ambientRight;

    // Play event
    public AK.Wwise.Event @event = new AK.Wwise.Event();

    // Exists for testing the meter on the AkChannelEmitter bus
    public AK.Wwise.RTPC gameParameter = new AK.Wwise.RTPC();

    public override void OffAction()
    {
        AkSoundEngine.StopAll();
    }

    public override void OnAction()
    {
        var akChannelEmitters = new AkChannelEmitterArray(2);

        // Assign L to the left emitter, R to the right emitter

        akChannelEmitters.Add(ambientLeft.transform.position, ambientLeft.transform.forward, ambientLeft.transform.up, new AkChannelConfig(1, AkSoundEngine.AK_SPEAKER_FRONT_LEFT).uChannelMask);

        akChannelEmitters.Add(ambientRight.transform.position, ambientRight.transform.forward, ambientRight.transform.up, new AkChannelConfig(1, AkSoundEngine.AK_SPEAKER_FRONT_RIGHT).uChannelMask);

 
        // Set our positions
 
        AkSoundEngine.SetMultiplePositions(ambientRight.gameObject, akChannelEmitters, (ushort)akChannelEmitters.Count, AkMultiPositionType.MultiPositionType_MultiDirections);

        @event.Post(ambientRight.gameObject);
    }
}
