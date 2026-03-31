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
		ReverbZone,
		ParentRoom,
		TransitionRegionWidth,
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
	[SerializeField] protected Text _ReverbZoneEnabledText;
	[SerializeField] protected Text _ReverbZoneEnabledValueText;
	[SerializeField] protected Text _ParentRoomText;
	[SerializeField] protected Text _ParentRoomValueText;
	[SerializeField] protected Text _TransitionRegionWidthText;
	[SerializeField] protected Text _TransitionRegionWidthValueText;

	[SerializeField] protected AK.Wwise.AuxBus[] auxBusses;
	[SerializeField] protected Color[] auxBusColors;

	[SerializeField] protected AkRoom roomComponent;
	[SerializeField] protected MeshRenderer roomVisualMaterial;
	[SerializeField] protected AkRoom parentRoomComponent;
	[SerializeField] protected AkReverbZone reverbZoneComponent;

	[SerializeField] protected float maxTransitionRegionWidth;

	private bool defaultRoomEnabledValue = true;
	private float defaultTransmissionLossValue = 0.5f;
	private int defaultAuxBusIndex = 0;
	private float defaultReverbLevelValue = 1f;
	private bool defaultRoomToneEnabledValue = false;
	private float defaultAuxSendLevelValue = 0f;
	private bool defaultReverbZoneEnabledValue = false;
	private bool defaultParentRoomEnabledValue = false;
	private float defaultTransitionRegionWidthValue = 0f;

	private bool currentRoomEnabledValue = true;
	private float currentTransmissionLossValue = 0.5f;
	private int currentAuxBusIndex = 0;
	private float currentReverbLevelValue = 1f;
	private bool currentRoomToneEnabledValue = false;
	private float currentAuxSendLevelValue = 0f;
	private bool currentReverbZoneEnabledValue = false;
	private bool currentParentRoomEnabledValue = false;
	private float currentTransitionRegionWidthValue = 0f;

	public bool CurrentRoomEnabled
	{
		get
		{
			return currentRoomEnabledValue;
		}
	}
	public bool CurrentParentRoomEnabled
	{
		get
		{
			return currentParentRoomEnabledValue;
		}
	}
	public bool CurrentReverbZoneEnabled
	{
		get
		{
			return currentReverbZoneEnabledValue;
		}
	}
	public bool CurrentRoomToneEnabled
	{
		get
		{
			return currentRoomToneEnabledValue;
		}
	}
	public string CurrentAuxBus
	{
		get
		{
			return auxBusses[currentAuxBusIndex].ToString();
		}
	}
	public Color CurrentAuxBusColor
	{
		get
		{
			return auxBusColors[currentAuxBusIndex];
		}
	}

	private bool RoomTonePosted = false;

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
				if (roomComponent.reverbAuxBus.Id == auxBusses[i].Id)
				{
					defaultAuxBusIndex = i;
					break;
				}
			}
			defaultReverbLevelValue = roomComponent.reverbLevel;
			defaultRoomToneEnabledValue = roomComponent.roomToneEvent.IsValid();
			defaultAuxSendLevelValue = roomComponent.roomToneAuxSend;

			if (reverbZoneComponent != null)
			{
				defaultReverbZoneEnabledValue = reverbZoneComponent.ReverbZone.GetID() == roomComponent.GetID();
				defaultParentRoomEnabledValue = reverbZoneComponent.ParentRoom.GetID() != AkRoom.INVALID_ROOM_ID;
				defaultTransitionRegionWidthValue = reverbZoneComponent.TransitionRegionWidth;
			}
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
		currentReverbZoneEnabledValue = defaultReverbZoneEnabledValue;
		currentParentRoomEnabledValue = defaultParentRoomEnabledValue;
		currentTransitionRegionWidthValue = defaultTransitionRegionWidthValue;

		UpdateComponents();
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
					currentTransmissionLossValue = Mathf.Floor(Random.value * 10f) / 10f;
					break;
				case RoomParameters.ReverbAuxBus:
					currentAuxBusIndex = Random.Range(0, auxBusses.Length);
					break;
				case RoomParameters.ReverbLevel:
					currentReverbLevelValue = Mathf.Floor(Random.value * 10f) / 10f;
					break;
				case RoomParameters.RoomTone:
					currentRoomToneEnabledValue = Random.value < 0.5f;
					break;
				case RoomParameters.AuxSendLevel:
					currentAuxSendLevelValue = Mathf.Floor(Random.value * 10f) / 10f;
					break;
				case RoomParameters.ReverbZone:
					currentReverbZoneEnabledValue = Random.value < 0.5f;
					break;
				case RoomParameters.ParentRoom:
					currentParentRoomEnabledValue = Random.value < 0.5f;
					break;
				case RoomParameters.TransitionRegionWidth:
					currentTransitionRegionWidthValue = (Mathf.Floor(Random.value * 10f) / 10f) * Random.Range(0, maxTransitionRegionWidth); ;
					break;
			}
		}

		UpdateComponents();
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
			case RoomParameters.ReverbZone:
				currentReverbZoneEnabledValue = !currentReverbZoneEnabledValue;
				break;
			case RoomParameters.ParentRoom:
				currentParentRoomEnabledValue = !currentParentRoomEnabledValue;
				break;
		}

		UpdateComponents();
		UpdateUI();
	}

	public void ChangeAuxBus()
	{
		currentAuxBusIndex++;
		currentAuxBusIndex %= auxBusses.Length;

		UpdateComponents();
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
			case RoomParameters.TransitionRegionWidth:
				currentTransitionRegionWidthValue += in_variation;
				currentTransitionRegionWidthValue = Mathf.Clamp(currentTransitionRegionWidthValue, 0f, maxTransitionRegionWidth);
				break;
		}

		UpdateComponents();
		UpdateUI();
	}

	public void UpdateComponents()
	{
		roomComponent.enabled = currentRoomEnabledValue;
		if (!currentRoomEnabledValue)
		{
			// disabling the room will stop all sounds posted on the room
			RoomTonePosted = false;
			currentRoomToneEnabledValue = false;
			currentReverbZoneEnabledValue = false;
		}

		roomComponent.transmissionLoss = currentTransmissionLossValue;
		roomComponent.reverbAuxBus = auxBusses[currentAuxBusIndex];
		roomComponent.reverbLevel = currentReverbLevelValue;
		roomComponent.roomToneAuxSend = currentAuxSendLevelValue;

		if (currentRoomEnabledValue)
		{
			// post/stop room tone
			if (currentRoomToneEnabledValue && !RoomTonePosted)
			{
				roomComponent.PostRoomTone();
				RoomTonePosted = true;
			}
			else if (!currentRoomToneEnabledValue && RoomTonePosted)
			{
				if (roomComponent.roomToneEvent.IsValid())
				{
					AkSoundEngine.StopAll(roomComponent.GetID());
					RoomTonePosted = false;
				}
			}
		}

		if (reverbZoneComponent == null)
			return;

		reverbZoneComponent.enabled = currentReverbZoneEnabledValue;

		var parentRoom = currentParentRoomEnabledValue ? parentRoomComponent : null;
		parentRoomComponent.enabled = currentParentRoomEnabledValue;
		var parentRoomMeshRenderer = parentRoomComponent.gameObject.GetComponent<MeshRenderer>();
		if (parentRoomMeshRenderer)
		{
			parentRoomMeshRenderer.enabled = currentParentRoomEnabledValue;
		}

		if (reverbZoneComponent.ParentRoom != parentRoom || reverbZoneComponent.TransitionRegionWidth != currentTransitionRegionWidthValue)
		{
			reverbZoneComponent.ParentRoom = parentRoom;
			reverbZoneComponent.TransitionRegionWidth = currentTransitionRegionWidthValue;
			if (currentReverbZoneEnabledValue)
				reverbZoneComponent.SetReverbZone();
		}
	}

	public void UpdateUI()
	{
		var currentAuxBus = auxBusses[currentAuxBusIndex];
		var currentAuxBusColor = auxBusColors[currentAuxBusIndex];

		// Panel Values
		_RoomEnabledValueText.text = currentRoomEnabledValue ? "Enabled" : "Disabled";
		_TransmissionLossValueText.text = string.Format("{0:0.00}", currentTransmissionLossValue);
		_ReverbAuxBusValueText.text = currentAuxBus.IsValid() ? currentAuxBus.ToString() : "None";
		_ReverbLevelValueText.text = string.Format("{0:0.00}", currentReverbLevelValue);
		_RoomToneEnabledValueText.text = currentRoomToneEnabledValue ? "Playing" : "Stopped";
		_AuxSendLevelValueText.text = string.Format("{0:0.00}", currentAuxSendLevelValue);
		_ReverbZoneEnabledValueText.text = currentReverbZoneEnabledValue ? "Enabled" : "Disabled";
		_ParentRoomValueText.text = currentParentRoomEnabledValue ? parentRoomComponent.name : "Outdoors";
		_TransitionRegionWidthValueText.text = string.Format("{0:0.00}", currentTransitionRegionWidthValue);

		// Panel text color
		_ReverbAuxBusValueText.color = new UnityEngine.Color(currentAuxBusColor.r, currentAuxBusColor.g, currentAuxBusColor.b, 1);

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
			_ReverbZoneEnabledText.color = _enabledColor;
			_ReverbZoneEnabledValueText.color = _enabledColor;

			if (currentReverbZoneEnabledValue)
			{
				_ParentRoomText.color = _enabledColor;
				_ParentRoomValueText.color = _enabledColor;
				_TransitionRegionWidthText.color = _enabledColor;
				_TransitionRegionWidthValueText.color = _enabledColor;
			}
			else
			{
				_ParentRoomText.color = _disabledColor;
				_ParentRoomValueText.color = _disabledColor;
				_TransitionRegionWidthText.color = _disabledColor;
				_TransitionRegionWidthValueText.color = _disabledColor;
			}
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
			_ReverbZoneEnabledText.color = _disabledColor;
			_ReverbZoneEnabledValueText.color = _disabledColor;
			_ParentRoomText.color = _disabledColor;
			_ParentRoomValueText.color = _disabledColor;
			_TransitionRegionWidthText.color = _disabledColor;
			_TransitionRegionWidthValueText.color = _disabledColor;
		}

		// Visual mesh material colors
		UnityEngine.Color color = auxBusColors[currentAuxBusIndex];
		var alpha = 0f;
		if (currentRoomEnabledValue)
		{
			alpha = currentTransmissionLossValue;
		}
		roomVisualMaterial.material.color = new UnityEngine.Color(color.r, color.g, color.b, alpha);
	}
}