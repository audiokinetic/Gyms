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

public class SpatialRoomPortalManager : MonoBehaviour
{
	public enum PortalParameters
	{
		PortalEnabled,
		Occlusion,
		Reset,
		Randomize,
	}

	// Singleton.
	private static SpatialRoomPortalManager m_instance;
	public static SpatialRoomPortalManager Instance
	{
		get
		{
			return m_instance;
		}
	}

	[SerializeField] protected Text _PortalEnabledValueText;
	[SerializeField] protected Text _PortalOcclusionText;
	[SerializeField] protected Text _PortalOcclusionValueText;

	[SerializeField] protected AkRoomPortal portalComponent;
	[SerializeField] protected MeshRenderer VisualMaterial;

	private bool defaultPortalEnabledValue = true;
	private float defaultOcclusionValue = 0f;

	private bool currentPortalEnabledValue = true;
	private float currentOcclusionValue = 0f;

	private void InitializeParams()
	{
		if (portalComponent != null)
		{
			defaultPortalEnabledValue = portalComponent.initialState == AkRoomPortal.State.Open;
		}
		defaultOcclusionValue = 0f;

		if (VisualMaterial != null)
		{
			VisualMaterial.material.color = new UnityEngine.Color(0f, 1f, 0f, defaultOcclusionValue);
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
		currentPortalEnabledValue = defaultPortalEnabledValue;
		currentOcclusionValue = defaultOcclusionValue;

		UpdatePortalComponent();
		UpdateUI();
	}

	public void RandomizeParams()
	{
		currentPortalEnabledValue = Random.value < 0.5f;
		currentOcclusionValue = Random.value;

		UpdatePortalComponent();
		UpdatePortalOcclusion();
		UpdateUI();
	}

	public void TogglePortal()
	{
		currentPortalEnabledValue = !currentPortalEnabledValue;

		UpdatePortalComponent();
		UpdateUI();
	}

	public void IncreaseOcclusionValue(float in_variation)
	{
		currentOcclusionValue += in_variation;
		currentOcclusionValue = Mathf.Clamp(currentOcclusionValue, 0f, 1f);

		UpdatePortalOcclusion();
		UpdateUI();
	}

	public void UpdatePortalComponent()
	{
		if (currentPortalEnabledValue)
			portalComponent.Open();
		else
			portalComponent.Close();
	}

	public void UpdatePortalOcclusion()
	{
		AkSoundEngine.SetPortalObstructionAndOcclusion(portalComponent.GetID(), 0f, currentOcclusionValue);
	}

	public void UpdateUI()
	{
		// Panel Values
		_PortalEnabledValueText.text = currentPortalEnabledValue ? "Enabled" : "Disabled";
		_PortalOcclusionValueText.text = string.Format("{0:0.00}", currentOcclusionValue);

		// Panel text color
		if (currentPortalEnabledValue)
		{
			_PortalOcclusionText.color = Color.white;
			_PortalOcclusionValueText.color = Color.white;
		}
		else
		{
			_PortalOcclusionText.color = Color.gray;
			_PortalOcclusionValueText.color = Color.gray;
		}

		// Visual mesh material colors
		var newAlpha = currentOcclusionValue;
		if (!currentPortalEnabledValue)
		{
			newAlpha = 1;

		}
		VisualMaterial.material.color = new UnityEngine.Color(0f, 1f, 0f, newAlpha);
	}
}