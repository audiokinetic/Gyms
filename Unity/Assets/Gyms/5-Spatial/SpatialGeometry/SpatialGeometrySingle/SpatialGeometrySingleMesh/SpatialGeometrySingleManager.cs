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

public class SpatialGeometrySingleManager : MonoBehaviour
{
	public enum GeometryParameters
	{
		Diffraction = 1 << 0,
		BoundaryEdges = 1 << 1,
		AcousticTexture = 1 << 2,
		TransmissionLoss = 1 << 3,
		GeometryEnabled = 1 << 4,
		RandomizeAll = 0x11111,
		ResetAll = 0x00000,
	}

	// Singleton.
	private static SpatialGeometrySingleManager m_instance;
	public static SpatialGeometrySingleManager Instance
	{
		get
		{
			return m_instance;
		}
	}

	[SerializeField] protected Text _GeometryEnabledValueText;
	[SerializeField] protected Text _DiffractionText;
	[SerializeField] protected Text _DiffractionValueText;
	[SerializeField] protected Text _BoundaryEdgesText;
	[SerializeField] protected Text _BoundaryEdgesValueText;
	[SerializeField] protected Text _AcousticTextureLeftText;
	[SerializeField] protected Text _AcousticTextureLeftValueText;
	[SerializeField] protected Text _TransmissionLossLeftText;
	[SerializeField] protected Text _TransmissionLossLeftValueText;
	[SerializeField] protected Text _AcousticTextureRightText;
	[SerializeField] protected Text _AcousticTextureRightValueText;
	[SerializeField] protected Text _TransmissionLossRightText;
	[SerializeField] protected Text _TransmissionLossRightValueText;

	[SerializeField] protected AK.Wwise.AcousticTexture[] acousticTextures;
	[SerializeField] protected Color[] acousticTextureColors;
	[SerializeField] protected Material[] acousticTextureMaterials;

	[SerializeField] protected MeshRenderer VisualMaterialLeft;
	[SerializeField] protected MeshRenderer VisualMaterialRight;
	[SerializeField] protected MeshRenderer VisualMaterialGeometry;

	[SerializeField] protected AkSurfaceReflector geometryComponent;

	private bool currentGeometryEnabledValue = true;

	private bool currentDiffractionValue = true;
	private bool currentBoundaryEdgesValue = false;

	private int[] currentAcousticTextureIndex = new int[2] { 0, 1 };
	private float[] currentTransmissionLossValue = new float[2] { 1f, 1f };

	private Color _disabledColor = new Color(0.5f, 0.5f, 0.5f, 1f);
	private Color _enabledColor = new Color(1f, 1f, 1f, 1f);

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
		currentGeometryEnabledValue = true;

		currentDiffractionValue = true;
		currentBoundaryEdgesValue = false;

		currentAcousticTextureIndex[0] = 0;
		currentAcousticTextureIndex[1] = 1;
		currentTransmissionLossValue[0] = 1f;
		currentTransmissionLossValue[1] = 1f;

		UpdateGeometryComponent();
		UpdateUI();
	}

	public void RandomizeParams()
    {
		currentGeometryEnabledValue = Random.value < 0.5f;

		currentDiffractionValue = Random.value < 0.5f;
		currentBoundaryEdgesValue = Random.value < 0.5f;

		currentAcousticTextureIndex[0] = Random.Range(0, acousticTextures.Length);
		currentAcousticTextureIndex[1] = Random.Range(0, acousticTextures.Length);

		currentTransmissionLossValue[0] = Random.value;
		currentTransmissionLossValue[1] = Random.value;

		UpdateGeometryComponent();
		UpdateUI();
    }

	public void ToggleParam(GeometryParameters in_param)
    {
		switch (in_param)
		{
			case GeometryParameters.Diffraction:
				currentDiffractionValue = !currentDiffractionValue;
				break;
			case GeometryParameters.BoundaryEdges:
				currentBoundaryEdgesValue = !currentBoundaryEdgesValue;
				break;
			case GeometryParameters.GeometryEnabled:
				currentGeometryEnabledValue = !currentGeometryEnabledValue;
				break;
		}

		UpdateGeometryComponent();
		UpdateUI();
	}

	public void ChangeAcousticTexture(int in_submesh)
    {
		currentAcousticTextureIndex[in_submesh]++;
		currentAcousticTextureIndex[in_submesh] %= acousticTextures.Length;

		UpdateGeometryComponent();
		UpdateUI();
	}

	public void IncreaseTransmissionLoss(int in_submesh, float in_variation)
    {
		currentTransmissionLossValue[in_submesh] += in_variation;
		currentTransmissionLossValue[in_submesh] = Mathf.Clamp(currentTransmissionLossValue[in_submesh], 0f, 1f);

		UpdateGeometryComponent();
		UpdateUI();
	}

	public void UpdateGeometryComponent()
	{
		geometryComponent.enabled = currentGeometryEnabledValue;

		geometryComponent.EnableDiffraction = currentDiffractionValue;
		geometryComponent.EnableDiffractionOnBoundaryEdges = currentBoundaryEdgesValue;

		geometryComponent.AcousticTextures[0] = acousticTextures[currentAcousticTextureIndex[0]];
		geometryComponent.AcousticTextures[1] = acousticTextures[currentAcousticTextureIndex[1]];

		geometryComponent.TransmissionLossValues[0] = currentTransmissionLossValue[0];
		geometryComponent.TransmissionLossValues[1] = currentTransmissionLossValue[1];
	}

	public void UpdateUI()
	{
		// Panel Values
		_GeometryEnabledValueText.text = currentGeometryEnabledValue ? "Enabled" : "Disabled";

		_DiffractionValueText.text = currentDiffractionValue ? "Enabled" : "Disabled";
		_BoundaryEdgesValueText.text = currentBoundaryEdgesValue ? "Enabled" : "Disabled";

		var currentAcousticTextureLeft = acousticTextures[currentAcousticTextureIndex[0]];
		_AcousticTextureLeftValueText.text = currentAcousticTextureLeft.IsValid() ? currentAcousticTextureLeft.ToString() : "None";
		_AcousticTextureLeftValueText.color = acousticTextureColors[currentAcousticTextureIndex[0]];
		_TransmissionLossLeftValueText.text = string.Format("{0:0.00}", currentTransmissionLossValue[0]);

		var currentAcousticTextureRight = acousticTextures[currentAcousticTextureIndex[1]];
		_AcousticTextureRightValueText.text = currentAcousticTextureRight.IsValid() ? currentAcousticTextureRight.ToString() : "None";
		_AcousticTextureRightValueText.color = acousticTextureColors[currentAcousticTextureIndex[1]];
		_TransmissionLossRightValueText.text = string.Format("{0:0.00}", currentTransmissionLossValue[1]);

		// Panel text color
		if (currentGeometryEnabledValue)
		{
			_DiffractionText.color = _enabledColor;
			_DiffractionValueText.color = _enabledColor;

			if (currentDiffractionValue)
			{
				_BoundaryEdgesText.color = _enabledColor;
				_BoundaryEdgesValueText.color = _enabledColor;
			}
			else
			{
				_BoundaryEdgesText.color = _disabledColor;
				_BoundaryEdgesValueText.color = _disabledColor;
			}

			_AcousticTextureLeftText.color = _enabledColor;
			_AcousticTextureRightText.color = _enabledColor;

			_TransmissionLossLeftText.color = _enabledColor;
			_TransmissionLossLeftValueText.color = _enabledColor;
			_TransmissionLossRightText.color = _enabledColor;
			_TransmissionLossRightValueText.color = _enabledColor;
		}
		else
		{
			_DiffractionText.color = _disabledColor;
			_DiffractionValueText.color = _disabledColor;

			_BoundaryEdgesText.color = _disabledColor;
			_BoundaryEdgesValueText.color = _disabledColor;

			_AcousticTextureLeftText.color = _disabledColor;
			_AcousticTextureRightText.color = _disabledColor;

			_TransmissionLossLeftText.color = _disabledColor;
			_TransmissionLossLeftValueText.color = _disabledColor;
			_TransmissionLossRightText.color = _disabledColor;
			_TransmissionLossRightValueText.color = _disabledColor;
		}

		// Visual mesh material colors
		Material[] LeftRegionMaterials = VisualMaterialLeft.materials;
		LeftRegionMaterials[0] = acousticTextureMaterials[currentAcousticTextureIndex[0]];
		VisualMaterialLeft.materials = LeftRegionMaterials;

		Material[] RightRegionMaterials = VisualMaterialRight.materials;
		RightRegionMaterials[0] = acousticTextureMaterials[currentAcousticTextureIndex[1]];
		VisualMaterialRight.materials = RightRegionMaterials;

		if (currentGeometryEnabledValue)
		{
			VisualMaterialGeometry.enabled = true;
			Material[] GeometryMaterials = VisualMaterialGeometry.materials;
			GeometryMaterials[0] = acousticTextureMaterials[currentAcousticTextureIndex[0]];
			GeometryMaterials[1] = acousticTextureMaterials[currentAcousticTextureIndex[1]];
			VisualMaterialGeometry.materials = GeometryMaterials;
		}
        else
        {
			VisualMaterialGeometry.enabled = false;
		}
	}
}