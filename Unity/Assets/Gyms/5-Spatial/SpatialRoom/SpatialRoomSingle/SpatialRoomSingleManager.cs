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
using UnityEngine.UI;

public class SpatialRoomSingleManager : MonoBehaviour
{
	public enum RoomParameters
	{
		RoomEnabled,
		TransmissionLoss,
		ReverbAuxBus,
		ReverbLevel,
		RoomTone,
		AuxSendLevel,
	}

	// Singleton.
	private static SpatialRoomSingleManager m_instance;
	public static SpatialRoomSingleManager Instance
	{
		get
		{
			return m_instance;
		}
	}

	[SerializeField] protected Text _RoomEnabledValueText;
	[SerializeField] protected Text _TransmissionLossText;
	[SerializeField] protected Text _TransmissionLossValueText;
	[SerializeField] protected Text _ReverbAuxBusText;
	[SerializeField] protected Text _ReverbAuxBusValueText;
	[SerializeField] protected Text _ReverbLevelText;
	[SerializeField] protected Text _ReverbLevelValueText;
	[SerializeField] protected Text _RoomToneEnabledText;
	[SerializeField] protected Text _RoomToneEnabledValueText;
	[SerializeField] protected Text _AuxSendLevelText;
	[SerializeField] protected Text _AuxSendLevelValueText;

	[SerializeField] protected AK.Wwise.AuxBus[] auxBusses;
	[SerializeField] protected Color[] auxBusColors;

	[SerializeField] protected AkRoom roomComponent;
	[SerializeField] protected MeshRenderer VisualMaterial;

	private bool defaultRoomEnabledValue = true;
	private float defaultTransmissionLossValue = 0.5f;
	private int defaultAuxBusIndex = 0;
	private float defaultReverbLevelValue = 1f;
	private bool defaultRoomToneEnabledValue = false;
	private float defaultAuxSendLevelValue = 0f;

	private bool currentRoomEnabledValue = true;
	private float currentTransmissionLossValue = 0.5f;
	private int currentAuxBusIndex = 0;
	private float currentReverbLevelValue = 1f;
	private bool currentRoomToneEnabledValue = false;
	private float currentAuxSendLevelValue = 0f;

	private Color _disabledColor = new Color(0.5f, 0.5f, 0.5f, 1f);
	private Color _enabledColor = new Color(1f, 1f, 1f, 1f);

	private void InitializeParams()
	{
		if (roomComponent != null)
		{
			defaultRoomEnabledValue = roomComponent.enabled;
			defaultTransmissionLossValue = roomComponent.transmissionLoss;
			defaultAuxBusIndex = 0;
			for (int i = 0; i < auxBusses.Length; i++)
			{
				if (roomComponent.reverbAuxBus == auxBusses[i])
				{
					defaultAuxBusIndex = i;
				}
			}
			defaultReverbLevelValue = roomComponent.reverbLevel;
			defaultRoomToneEnabledValue = roomComponent.roomToneEvent.IsValid();
			defaultAuxSendLevelValue = roomComponent.roomToneAuxSend;
		}

		if (VisualMaterial != null)
		{
			UnityEngine.Color oldColor = VisualMaterial.material.color;
			VisualMaterial.material.color = new UnityEngine.Color(oldColor.r, oldColor.g, oldColor.b, defaultTransmissionLossValue);
		}
	}

	private void Awake()
	{
		// Singleton instantiation.
		if (m_instance)
		{
			DestroyImmediate(this);
			return;
		}

		m_instance = this;

		InitializeParams();
	}

	private void OnDestroy()
	{
		if (m_instance)
		{
			m_instance = null;
		}
	}

	void Start()
	{
		ResetParams();
	}

	public void ResetParams()
	{
		currentRoomEnabledValue = defaultRoomEnabledValue;
		currentTransmissionLossValue = defaultTransmissionLossValue;
		currentAuxBusIndex = defaultAuxBusIndex;
		currentReverbLevelValue = defaultReverbLevelValue;
		currentRoomToneEnabledValue = defaultRoomToneEnabledValue;
		currentAuxSendLevelValue = defaultAuxSendLevelValue;

		UpdateRoomComponent();
		UpdateUI();
	}

	public void RandomizeParams(RoomParameters[] in_params)
	{
		foreach (var param in in_params)
		{
			switch (param)
			{
				case RoomParameters.RoomEnabled:
					currentRoomEnabledValue = Random.value < 0.5f;
					break;
				case RoomParameters.TransmissionLoss:
					currentTransmissionLossValue = Random.value;
					break;
				case RoomParameters.ReverbAuxBus:
					currentAuxBusIndex = Random.Range(0, auxBusses.Length);
					break;
				case RoomParameters.ReverbLevel:
					currentReverbLevelValue = Random.value;
					break;
				case RoomParameters.RoomTone:
					currentRoomToneEnabledValue = Random.value < 0.5f;
					break;
				case RoomParameters.AuxSendLevel:
					currentAuxSendLevelValue = Random.value;
					break;
			}
		}

		UpdateRoomComponent();
		UpdateUI();
	}

	public void ToggleParam(RoomParameters in_param)
	{
		switch (in_param)
		{
			case RoomParameters.RoomEnabled:
				currentRoomEnabledValue = !currentRoomEnabledValue;
				break;
			case RoomParameters.RoomTone:
				currentRoomToneEnabledValue = !currentRoomToneEnabledValue;
				break;
		}

		UpdateRoomComponent();
		UpdateUI();
	}

	public void ChangeAuxBus()
	{
		currentAuxBusIndex++;

		UpdateRoomComponent();
		UpdateUI();
	}

	public void IncreaseFloatValue(RoomParameters in_param, float in_variation)
	{
		switch (in_param)
		{
			case RoomParameters.TransmissionLoss:
				currentTransmissionLossValue += in_variation;
				currentTransmissionLossValue = Mathf.Clamp(currentTransmissionLossValue, 0f, 1f);
				break;
			case RoomParameters.ReverbLevel:
				currentReverbLevelValue += in_variation;
				currentReverbLevelValue = Mathf.Clamp(currentReverbLevelValue, 0f, 1f);
				break;
			case RoomParameters.AuxSendLevel:
				currentAuxSendLevelValue += in_variation;
				currentAuxSendLevelValue = Mathf.Clamp(currentAuxSendLevelValue, 0f, 1f);
				break;
		}

		UpdateRoomComponent();
		UpdateUI();
	}

	public void UpdateRoomComponent()
	{
		roomComponent.enabled = currentRoomEnabledValue;
		roomComponent.transmissionLoss = currentTransmissionLossValue;
		roomComponent.reverbAuxBus = auxBusses[currentAuxBusIndex];
		roomComponent.reverbLevel = currentReverbLevelValue;
		if (currentRoomToneEnabledValue)
		{
			roomComponent.PostRoomTone();
		}
		else
		{
			if (roomComponent.roomToneEvent.IsValid())
			{
				AkSoundEngine.StopAll(roomComponent.GetID());
			}
		}
		roomComponent.roomToneAuxSend = currentAuxSendLevelValue;
	}

	public void UpdateUI()
	{
		// Panel Values
		_RoomEnabledValueText.text = currentRoomEnabledValue ? "Enabled" : "Disabled";
		_TransmissionLossValueText.text = string.Format("{0:0.00}", currentTransmissionLossValue);
		var currentAuxBus = auxBusses[currentAuxBusIndex];
		_ReverbAuxBusValueText.text = currentAuxBus.IsValid() ? currentAuxBus.ToString() : "None";
		_ReverbLevelValueText.text = string.Format("{0:0.00}", currentReverbLevelValue);
		_RoomToneEnabledValueText.text = currentRoomToneEnabledValue ? "Playing" : "Stopped";
		_AuxSendLevelValueText.text = string.Format("{0:0.00}", currentAuxSendLevelValue);

		// Panel text color
		if (currentRoomEnabledValue)
		{
			_TransmissionLossText.color = _enabledColor;
			_TransmissionLossValueText.color = _enabledColor;
			_ReverbAuxBusText.color = _enabledColor;
			_ReverbLevelText.color = _enabledColor;
			_ReverbLevelValueText.color = _enabledColor;
			_RoomToneEnabledText.color = _enabledColor;
			_RoomToneEnabledValueText.color = _enabledColor;
			_AuxSendLevelText.color = _enabledColor;
			_AuxSendLevelValueText.color = _enabledColor;
		}
		else
		{
			_TransmissionLossText.color = _disabledColor;
			_TransmissionLossValueText.color = _disabledColor;
			_ReverbAuxBusText.color = _disabledColor;
			_ReverbLevelText.color = _disabledColor;
			_ReverbLevelValueText.color = _disabledColor;
			_RoomToneEnabledText.color = _disabledColor;
			_RoomToneEnabledValueText.color = _disabledColor;
			_AuxSendLevelText.color = _disabledColor;
			_AuxSendLevelValueText.color = _disabledColor;
		}

		// Visual mesh material colors
		UnityEngine.Color oldColor = VisualMaterial.material.color;
		var newAlpha = currentTransmissionLossValue;
		if (!currentRoomEnabledValue)
		{
			newAlpha = 0;

		}
		VisualMaterial.material.color = new UnityEngine.Color(oldColor.r, oldColor.g, oldColor.b, newAlpha);
	}
}