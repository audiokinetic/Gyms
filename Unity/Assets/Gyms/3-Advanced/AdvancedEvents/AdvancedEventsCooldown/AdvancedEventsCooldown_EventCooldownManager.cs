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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedEventsCooldown_EventCooldownManager : MonoBehaviour
{
    [SerializeField]
    AdvancedEventsCooldown_AkWwiseEventCooldownData cooldownData;
    readonly Dictionary<uint, bool> eventCooldownStates = new Dictionary<uint, bool>();

    void Awake()
    {
        foreach (var eventCooldownData in cooldownData.data)
        {
            eventCooldownStates[eventCooldownData._event.Id] = false;
        }
    }

    public bool CanPostEvent(AkEvent in_akEvent, out bool out_isCooldownEvent)
    {
        uint eventId = in_akEvent.data.Id;

        if (eventCooldownStates.TryGetValue(eventId, out bool out_cooldownActive))
        {
            out_isCooldownEvent = true;

            if (out_cooldownActive)
            {
                Debug.Log($"{in_akEvent.data.Name} cannot be posted due to active cooldown state.");
                return false;
            }
        }
        else
        {
            out_isCooldownEvent = false;
        }

        return true;
    }

    public void ActivateCooldownForEvent(AkEvent in_akEvent)
    {
        uint eventId = in_akEvent.data.Id;

        var cooldownEvent = cooldownData.data.Find(eventData => eventData._event.Id == eventId);

        StartCoroutine(CooldownRoutine(eventId, cooldownEvent.cooldownTimeMs));
    }

    IEnumerator CooldownRoutine(uint in_eventId, int in_cooldownTimeMs)
    {
        eventCooldownStates[in_eventId] = true;
        yield return new WaitForSeconds(in_cooldownTimeMs * 0.001f);
        eventCooldownStates[in_eventId] = false;
    }
}