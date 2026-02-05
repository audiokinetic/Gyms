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

public class SpatialRoomPortalParams_Interact : ButtonWithTextManager
{
	[SerializeField]
	[Tooltip("The parameter this button affects.")]
	SpatialRoomPortalManager.PortalParameters _parameter;

	[SerializeField]
	[Tooltip("If this button affects a float, enter the variation here.")]
	float _variation = 0.1f;

	private void OnValidate()
	{
		switch (_parameter)
		{
			case SpatialRoomPortalManager.PortalParameters.PortalEnabled:
				UpdateDescriptionText("Toggle Portal");
				UpdateValue("", Color.white);
				break;
			case SpatialRoomPortalManager.PortalParameters.Occlusion:
				UpdateDescriptionText("Portal Occlusion");
				UpdateValue(
					_variation >= 0 ? "+" + _variation : _variation.ToString(),
					_variation >= 0 ? UnityEngine.Color.green : UnityEngine.Color.red
				);
				break;
			case SpatialRoomPortalManager.PortalParameters.Reset:
				UpdateDescriptionText("Reset");
				UpdateValue("", Color.white);
				break;
			case SpatialRoomPortalManager.PortalParameters.Randomize:
				UpdateDescriptionText("Randomize");
				UpdateValue("", Color.white);
				break;
		}
	}

	public virtual void OnInteract()
	{
		if (SpatialRoomPortalManager.Instance == null)
			return;

		switch (_parameter)
		{
			case SpatialRoomPortalManager.PortalParameters.PortalEnabled:
				SpatialRoomPortalManager.Instance.TogglePortal();
				break;
			case SpatialRoomPortalManager.PortalParameters.Occlusion:
				SpatialRoomPortalManager.Instance.IncreaseOcclusionValue(_variation);
				break;
			case SpatialRoomPortalManager.PortalParameters.Reset:
				SpatialRoomPortalManager.Instance.ResetParams();
				break;
			case SpatialRoomPortalManager.PortalParameters.Randomize:
				SpatialRoomPortalManager.Instance.RandomizeParams();
				break;
		}
	}
}