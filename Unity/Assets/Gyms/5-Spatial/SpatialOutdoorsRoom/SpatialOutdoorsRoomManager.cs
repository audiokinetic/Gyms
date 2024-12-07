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

public class SpatialOutdoorsRoomManager : MonoBehaviour
{
	// Singleton.
	private static SpatialOutdoorsRoomManager m_instance;
	public static SpatialOutdoorsRoomManager Instance
	{
		get
		{
			return m_instance;
		}
	}

	[SerializeField] protected Text _AuxBusValueText;
	[SerializeField] protected Text _ReverbLevelValueText;
	[SerializeField] protected Text _AuxSendLevelValueText;
	[SerializeField] protected Text _TransmissionLossValueText;

	public AK.Wwise.AuxBus[] auxBuses;

	private int currentAuxBusIndex = 0;

	private void Awake()
	{
		// Singleton instantiation.
		if (m_instance)
		{
			DestroyImmediate(this);
			return;
		}

		m_instance = this;
	}

	private void OnDestroy()
	{
		AkRoom.StopOutdoors();
		ResetOutdoorsRoomParams();
		AkSoundEngine.RemoveRoom(AkRoom.INVALID_ROOM_ID);

		if (m_instance)
		{
			m_instance = null;
		}
	}

	void Start()
	{
		UpdateUI();
	}

	public AK.Wwise.AuxBus GetCurrentAuxBus()
	{
		return auxBuses[currentAuxBusIndex];
	}

	public AK.Wwise.AuxBus GetNextAuxBus()
	{
		currentAuxBusIndex = currentAuxBusIndex + 1;
		currentAuxBusIndex = currentAuxBusIndex % auxBuses.Length;

		return auxBuses[currentAuxBusIndex];
	}

	public void ResetOutdoorsRoomParams()
	{
		currentAuxBusIndex = 0;
		AkRoom.SetOutdoorsRoomParameters(AkRoom.OutdoorsRoomParameters.Default);
	}

	public void UpdateUI()
	{
		_AuxBusValueText.text = AkRoom.currentOutdoorsRoomParameters.reverbAuxBus == null ? "None" : AkRoom.currentOutdoorsRoomParameters.reverbAuxBus.ToString();
		_ReverbLevelValueText.text = AkRoom.currentOutdoorsRoomParameters.reverbLevel.ToString();
		_AuxSendLevelValueText.text = AkRoom.currentOutdoorsRoomParameters.auxSendLevel.ToString();
		_TransmissionLossValueText.text = AkRoom.currentOutdoorsRoomParameters.transmissionLoss.ToString();
	}
}