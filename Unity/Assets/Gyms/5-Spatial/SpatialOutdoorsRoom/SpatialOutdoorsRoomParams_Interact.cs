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

public class SpatialOutdoorsRoomParams_Interact : ButtonWithTextManager
{
	public enum OutdoorsParameters
	{
		ReverbAuxBus = 1 << 0,
		ReverbLevel = 1 << 1,
		TransmissionLoss = 1 << 2,
		AuxSendLevel = 1 << 3,
		RandomizeAll = 0x1111,
		ResetAll = 0x0000,
	}
	
	[SerializeField]
	[Tooltip("The parameter this button affects.")]
	OutdoorsParameters _parameter = new OutdoorsParameters();

	[SerializeField]
	[Tooltip("If this button affects ReverbLevel, TransmissionLoss or AuxSendLevel, enter the variation here.")]
	float _variation = 0.1f;

	private Color _defaultDescriptionColor = new Color(1f, 0.6f, 0f, 1f);

	private void OnValidate()
	{
		UpdateDescriptionColor(_defaultDescriptionColor);

		if (_parameter == OutdoorsParameters.RandomizeAll)
		{
			UpdateDescriptionText("Randomize All\nOutdoors");
			UpdateValue("", Color.white);
		}
		else if (_parameter == OutdoorsParameters.ResetAll)
		{
			UpdateDescriptionText("Reset All\nOutdoors");
			UpdateValue("", Color.white);
		}
		else if (_parameter == OutdoorsParameters.ReverbAuxBus)
		{
			UpdateDescriptionText("Aux Bus\nOutdoors");
			UpdateValue("", Color.white);
		}
		else if (_parameter == OutdoorsParameters.ReverbLevel ||
			_parameter == OutdoorsParameters.TransmissionLoss ||
			_parameter == OutdoorsParameters.AuxSendLevel)
		{
			UpdateValue(
				_variation >= 0 ? "+" + _variation : _variation.ToString(),
				_variation >= 0 ? UnityEngine.Color.green : UnityEngine.Color.red
			);

			if (_parameter == OutdoorsParameters.ReverbLevel)
			{
				UpdateDescriptionText("Reverb Level\nOutdoors");
			}
			else if (_parameter == OutdoorsParameters.TransmissionLoss)
			{
				UpdateDescriptionText("Transmission Loss\nOutdoors");
			}
			else if (_parameter == OutdoorsParameters.AuxSendLevel)
			{
				UpdateDescriptionText("Aux Send Level\nOutdoors");
			}
		}
	}

	public virtual void OnInteract()
	{
		switch (_parameter)
		{
			case OutdoorsParameters.RandomizeAll:
				RandomizeAllParameters();
				break;
			case OutdoorsParameters.ResetAll:
				ResetAllParameters();
				break;
			case OutdoorsParameters.ReverbAuxBus:
				ChangeAuxBus();
				break;
			case OutdoorsParameters.ReverbLevel:
			case OutdoorsParameters.TransmissionLoss:
			case OutdoorsParameters.AuxSendLevel:
				ChangeFloatParameter();
				break;
		}
	}

	public void RandomizeAllParameters()
	{
		if (SpatialOutdoorsRoomManager.Instance == null)
			return;

		int auxBusIndex = UnityEngine.Random.Range(0, SpatialOutdoorsRoomManager.Instance.auxBuses.Length);
		AkRoom.OutdoorsRoomParameters outdoorsRoomParameters = new AkRoom.OutdoorsRoomParameters();

		outdoorsRoomParameters.reverbAuxBus = SpatialOutdoorsRoomManager.Instance.auxBuses[auxBusIndex];
		outdoorsRoomParameters.reverbLevel = UnityEngine.Random.Range(0, 101) / 100f;
		outdoorsRoomParameters.transmissionLoss = UnityEngine.Random.Range(0, 101) / 100f;
		outdoorsRoomParameters.auxSendLevel = UnityEngine.Random.Range(0, 101) / 100f;

		AkRoom.SetOutdoorsRoomParameters(outdoorsRoomParameters); // Update in Wwise.

		SpatialOutdoorsRoomManager.Instance.UpdateUI();
	}

	public void ResetAllParameters()
	{
		if (SpatialOutdoorsRoomManager.Instance == null)
			return;

		SpatialOutdoorsRoomManager.Instance.ResetOutdoorsRoomParams();
		SpatialOutdoorsRoomManager.Instance.UpdateUI();
	}

	public void ChangeAuxBus()
	{
		if (SpatialOutdoorsRoomManager.Instance == null)
			return;

		AkRoom.OutdoorsRoomParameters outdoorsRoomParameters = AkRoom.currentOutdoorsRoomParameters;
		outdoorsRoomParameters.reverbAuxBus = SpatialOutdoorsRoomManager.Instance.GetNextAuxBus();

		AkRoom.SetOutdoorsRoomParameters(outdoorsRoomParameters); // Update in Wwise.

		SpatialOutdoorsRoomManager.Instance.UpdateUI();
	}

	public void ChangeFloatParameter()
	{
		if (SpatialOutdoorsRoomManager.Instance == null)
			return;

		AkRoom.OutdoorsRoomParameters outdoorsRoomParameters = AkRoom.currentOutdoorsRoomParameters;

		switch (_parameter)
		{
			case OutdoorsParameters.ReverbLevel:
				outdoorsRoomParameters.reverbLevel = Mathf.Round(Mathf.Clamp(outdoorsRoomParameters.reverbLevel + _variation, .0f, 1.0f) * 100) / 100;
				break;
			case OutdoorsParameters.TransmissionLoss:
				outdoorsRoomParameters.transmissionLoss = Mathf.Round(Mathf.Clamp(outdoorsRoomParameters.transmissionLoss + _variation, .0f, 1.0f) * 100) / 100;
				break;
			case OutdoorsParameters.AuxSendLevel:
				outdoorsRoomParameters.auxSendLevel = Mathf.Round(Mathf.Clamp(outdoorsRoomParameters.auxSendLevel + _variation, .0f, 1.0f) * 100) / 100;
				break;
		}

		AkRoom.SetOutdoorsRoomParameters(outdoorsRoomParameters); // Update in Wwise.

		SpatialOutdoorsRoomManager.Instance.UpdateUI();
	}
}