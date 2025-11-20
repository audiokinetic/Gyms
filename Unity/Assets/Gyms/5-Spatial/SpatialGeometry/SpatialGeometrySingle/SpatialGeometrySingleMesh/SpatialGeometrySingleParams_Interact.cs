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

public class SpatialGeometrySingleParams_Interact : ButtonWithTextManager
{	
	[SerializeField]
	[Tooltip("The parameter this button affects.")]
	SpatialGeometrySingleManager.GeometryParameters _parameter;

	[SerializeField]
	[Tooltip("If this button affects TransmissionLoss, enter the variation here.")]
	float _variation = 0.1f;

	[SerializeField]
	[Tooltip("If this button affects Surface Properties, enter the submesh here.")]
	[Range(0,1)]
	int _subMesh = 0;

	private void OnValidate()
	{
		switch (_parameter)
		{
			case SpatialGeometrySingleManager.GeometryParameters.GeometryEnabled:
				UpdateDescriptionText("Toggle Geometry");
				UpdateValue("", Color.white);
				break;
			case SpatialGeometrySingleManager.GeometryParameters.Diffraction:
				UpdateDescriptionText("Toggle Diffraction");
				UpdateValue("", Color.white);
				break;
			case SpatialGeometrySingleManager.GeometryParameters.BoundaryEdges:
				UpdateDescriptionText("Toggle Diffraction on Boundary Edges");
				UpdateValue("", Color.white);
				break;
			case SpatialGeometrySingleManager.GeometryParameters.AcousticTexture:
				UpdateDescriptionText("Acoustic Texture");
				UpdateValue("", Color.white);
				break;
			case SpatialGeometrySingleManager.GeometryParameters.TransmissionLoss:
				UpdateDescriptionText("Transmission Loss");
				UpdateValue(
					_variation >= 0 ? "+" + _variation : _variation.ToString(),
					_variation >= 0 ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialGeometrySingleManager.GeometryParameters.RandomizeAll:
				UpdateDescriptionText("Randomize All");
				UpdateValue("", Color.white);
				break;
			case SpatialGeometrySingleManager.GeometryParameters.ResetAll:
				UpdateDescriptionText("Reset All");
				UpdateValue("", Color.white);
				break;
		}
	}

	public virtual void OnInteract()
	{
		if (SpatialGeometrySingleManager.Instance == null)
			return;

		switch (_parameter)
		{
			case SpatialGeometrySingleManager.GeometryParameters.GeometryEnabled:
			case SpatialGeometrySingleManager.GeometryParameters.Diffraction:
			case SpatialGeometrySingleManager.GeometryParameters.BoundaryEdges:
				SpatialGeometrySingleManager.Instance.ToggleParam(_parameter);
				break;
			case SpatialGeometrySingleManager.GeometryParameters.AcousticTexture:
				SpatialGeometrySingleManager.Instance.ChangeAcousticTexture(_subMesh);
				break;
			case SpatialGeometrySingleManager.GeometryParameters.TransmissionLoss:
				SpatialGeometrySingleManager.Instance.IncreaseTransmissionLoss(_subMesh, _variation);
				break;
			case SpatialGeometrySingleManager.GeometryParameters.RandomizeAll:
				SpatialGeometrySingleManager.Instance.RandomizeParams();
				break;
			case SpatialGeometrySingleManager.GeometryParameters.ResetAll:
				SpatialGeometrySingleManager.Instance.ResetParams();
				break;
		}
	}
}