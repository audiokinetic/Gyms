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

	private void OnValidate()
	{
		switch (_parameter)
		{
			case SpatialRoomSingleManager.RoomParameters.RoomEnabled:
				UpdateDescriptionText("Toggle Room");
				UpdateValue("", Color.white);
				break;
			case SpatialRoomSingleManager.RoomParameters.TransmissionLoss:
				UpdateDescriptionText("Transmission Loss");
				UpdateValue(
					_variation >= 0 ? "+" + _variation : _variation.ToString(),
					_variation >= 0 ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialRoomSingleManager.RoomParameters.ReverbAuxBus:
				UpdateDescriptionText("Reverb Aux Bus");
				UpdateValue("", Color.white);
				break;
			case SpatialRoomSingleManager.RoomParameters.ReverbLevel:
				UpdateDescriptionText("Reverb Level");
				UpdateValue(
					_variation >= 0 ? "+" + _variation : _variation.ToString(),
					_variation >= 0 ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialRoomSingleManager.RoomParameters.RoomTone:
				UpdateDescriptionText("Toggle Room Tone");
				UpdateValue("", Color.white);
				break;
		}
	}

	public virtual void OnInteract()
	{
		if (SpatialRoomSingleManager.Instance == null)
			return;

		switch (_parameter)
		{
			case SpatialRoomSingleManager.RoomParameters.RoomEnabled:
			case SpatialRoomSingleManager.RoomParameters.RoomTone:
				SpatialRoomSingleManager.Instance.ToggleParam(_parameter);
				break;
			case SpatialRoomSingleManager.RoomParameters.TransmissionLoss:
			case SpatialRoomSingleManager.RoomParameters.ReverbLevel:
			case SpatialRoomSingleManager.RoomParameters.AuxSendLevel:
				SpatialRoomSingleManager.Instance.IncreaseFloatValue(_parameter, _variation);
				break;
			case SpatialRoomSingleManager.RoomParameters.ReverbAuxBus:
				SpatialRoomSingleManager.Instance.ChangeAuxBus();
				break;
		}
	}
}