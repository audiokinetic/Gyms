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

using UnityEngine;

public class SpatialRoomSingleParams_Interact : ButtonWithTextManager
{
	[SerializeField]
	[Tooltip("The parameter this button affects.")]
	SpatialRoomSingleManager.RoomParameters _parameter;

	[SerializeField]
	[Tooltip("If this button affects a float, enter the variation here.")]
	float _variation = 0.1f;

	[SerializeField]
	[Tooltip("The buttons that this button affects.")]
	SpatialRoomSingleParams_Interact[] _buttonsToUpdate;

	private void OnValidate()
	{
		switch (_parameter)
		{
			case SpatialRoomSingleManager.RoomParameters.RoomEnabled:
				UpdateDescriptionText("Toggle Room");
				UpdateValue("Enabled/Disabled", Color.white);
				break;
			case SpatialRoomSingleManager.RoomParameters.RoomTone:
				UpdateDescriptionText("Toggle Room Tone");
				UpdateValue("Playing/Stopped", Color.white);
				break;
			case SpatialRoomSingleManager.RoomParameters.ReverbZone:
				UpdateDescriptionText("Toggle Reverb Zone");
				UpdateValue("Enabled/Disabled", Color.white);
				break;
			case SpatialRoomSingleManager.RoomParameters.ParentRoom:
				UpdateDescriptionText("Toggle Parent Room");
				UpdateValue("Enabled/Disabled", Color.white);
				break;
			case SpatialRoomSingleManager.RoomParameters.ReverbAuxBus:
				UpdateDescriptionText("Reverb Aux Bus");
				UpdateValue("", Color.white);
				break;
			case SpatialRoomSingleManager.RoomParameters.TransmissionLoss:
				UpdateDescriptionText("Transmission Loss");
				UpdateValue(
					_variation >= 0 ? "+" + _variation : _variation.ToString(),
					_variation >= 0 ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialRoomSingleManager.RoomParameters.ReverbLevel:
				UpdateDescriptionText("Reverb Level");
				UpdateValue(
					_variation >= 0 ? "+" + _variation : _variation.ToString(),
					_variation >= 0 ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialRoomSingleManager.RoomParameters.AuxSendLevel:
				UpdateDescriptionText("Aux Send Level");
				UpdateValue(
					_variation >= 0 ? "+" + _variation : _variation.ToString(),
					_variation >= 0 ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialRoomSingleManager.RoomParameters.TransitionRegionWidth:
				UpdateDescriptionText("Transition Region Width");
				UpdateValue(
					_variation >= 0 ? "+" + _variation : _variation.ToString(),
					_variation >= 0 ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
		}
	}

	private void Start()
	{
		UpdateValues();
	}

	public virtual void OnInteract()
	{
		if (SpatialRoomSingleManager.Instance == null)
			return;

		switch (_parameter)
		{
			case SpatialRoomSingleManager.RoomParameters.RoomEnabled:
			case SpatialRoomSingleManager.RoomParameters.RoomTone:
			case SpatialRoomSingleManager.RoomParameters.ReverbZone:
			case SpatialRoomSingleManager.RoomParameters.ParentRoom:
				SpatialRoomSingleManager.Instance.ToggleParam(_parameter);
				break;
			case SpatialRoomSingleManager.RoomParameters.TransmissionLoss:
			case SpatialRoomSingleManager.RoomParameters.ReverbLevel:
			case SpatialRoomSingleManager.RoomParameters.AuxSendLevel:
			case SpatialRoomSingleManager.RoomParameters.TransitionRegionWidth:
				SpatialRoomSingleManager.Instance.IncreaseFloatValue(_parameter, _variation);
				break;
			case SpatialRoomSingleManager.RoomParameters.ReverbAuxBus:
				SpatialRoomSingleManager.Instance.ChangeAuxBus();
				break;
		}

		UpdateValues();
		foreach (var button in _buttonsToUpdate)
		{
			if (button)
				button.UpdateValues();
		}
	}

	public void UpdateValues()
	{
		if (SpatialRoomSingleManager.Instance == null)
			return;

		switch (_parameter)
		{
			case SpatialRoomSingleManager.RoomParameters.RoomEnabled:
				UpdateValue(
					SpatialRoomSingleManager.Instance.CurrentRoomEnabled ? "Enabled" : "Disabled",
					SpatialRoomSingleManager.Instance.CurrentRoomEnabled ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialRoomSingleManager.RoomParameters.RoomTone:
				UpdateValue(
					SpatialRoomSingleManager.Instance.CurrentRoomToneEnabled ? "Playing" : "Stopped",
					SpatialRoomSingleManager.Instance.CurrentRoomToneEnabled ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialRoomSingleManager.RoomParameters.ReverbZone:
				UpdateValue(
					SpatialRoomSingleManager.Instance.CurrentReverbZoneEnabled ? "Enabled" : "Disabled",
					SpatialRoomSingleManager.Instance.CurrentReverbZoneEnabled ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialRoomSingleManager.RoomParameters.ParentRoom:
				UpdateValue(
					SpatialRoomSingleManager.Instance.CurrentParentRoomEnabled ? "Enabled" : "Disabled",
					SpatialRoomSingleManager.Instance.CurrentParentRoomEnabled ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialRoomSingleManager.RoomParameters.ReverbAuxBus:
				var auxBusColor = SpatialRoomSingleManager.Instance.CurrentAuxBusColor;
				UpdateValue(
					SpatialRoomSingleManager.Instance.CurrentAuxBus,
					new Color (auxBusColor.r, auxBusColor.g, auxBusColor.b, 1f)
				);
				break;
		}
	}
}