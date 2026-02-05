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

public class SpatialRoomPortalTransform_Interact : ButtonWithTextManager
{
	public enum ButtonParameter
	{
		Object,
		Transform,
		Direction,
		Increase,
		Decrease,
		Reset
	}

	[SerializeField]
	[Tooltip("The parameter this button affects.")]
	ButtonParameter _parameter;

	private float _variation = 0.1f;

	private void OnValidate()
	{
		SpatialRoomPortalTransformManager.RoomPortalTransformObject currentObject = 0;
		SpatialRoomPortalTransformManager.RoomPortalTransformType currentTransform = 0;
		SpatialRoomPortalTransformManager.RoomPortalTransformDirection currentDirection = 0;

		UpdateVariationValue(currentTransform);
		UpdateButtonValue(currentObject, currentTransform, currentDirection);
	}

	public virtual void OnInteract()
	{
		if (SpatialRoomPortalTransformManager.Instance == null)
			return;

		switch (_parameter)
		{
			case ButtonParameter.Object:
				SpatialRoomPortalTransformManager.Instance.ChangeObject();
				break;
			case ButtonParameter.Transform:
				SpatialRoomPortalTransformManager.Instance.ChangeTransform();
				break;
			case ButtonParameter.Direction:
				SpatialRoomPortalTransformManager.Instance.ChangeDirection();
				break;
			case ButtonParameter.Increase:
				SpatialRoomPortalTransformManager.Instance.TransformObject(_variation);
				break;
			case ButtonParameter.Decrease:
				SpatialRoomPortalTransformManager.Instance.TransformObject((-1 * _variation));
				break;
			case ButtonParameter.Reset:
				SpatialRoomPortalTransformManager.Instance.Reset();
				break;
		}
	}

	private void Update()
	{
		if (SpatialRoomPortalTransformManager.Instance == null)
			return;

		UpdateVariationValue(SpatialRoomPortalTransformManager.Instance.currentTransform);
		UpdateButtonValue(
			SpatialRoomPortalTransformManager.Instance.currentObject,
			SpatialRoomPortalTransformManager.Instance.currentTransform,
			SpatialRoomPortalTransformManager.Instance.currentDirection);
	}

	private void UpdateVariationValue(SpatialRoomPortalTransformManager.RoomPortalTransformType in_currentTransform)
	{
		switch (in_currentTransform)
		{
			case SpatialRoomPortalTransformManager.RoomPortalTransformType.Translation:
				_variation = 0.1f;
				break;
			case SpatialRoomPortalTransformManager.RoomPortalTransformType.Rotation:
				_variation = 5f;
				break;
			case SpatialRoomPortalTransformManager.RoomPortalTransformType.Scale:
				_variation = 0.5f;
				break;
		}
	}

	private void UpdateButtonValue(
		SpatialRoomPortalTransformManager.RoomPortalTransformObject in_currentObject,
		SpatialRoomPortalTransformManager.RoomPortalTransformType in_currentTransform,
		SpatialRoomPortalTransformManager.RoomPortalTransformDirection in_currentDirection)
	{
		switch (_parameter)
		{
			case ButtonParameter.Object:
				UpdateDescriptionText("Object");
				UpdateValue(in_currentObject.ToString(), Color.white);
				break;
			case ButtonParameter.Transform:
				UpdateDescriptionText("Transform");
				UpdateValue(in_currentTransform.ToString(), Color.white);
				break;
			case ButtonParameter.Direction:
				UpdateDescriptionText("Direction");
				UpdateValue(in_currentDirection.ToString(), Color.white);
				break;
			case ButtonParameter.Increase:
				UpdateDescriptionText("Increase");
				UpdateValue("+" + _variation, UnityEngine.Color.green);
				break;
			case ButtonParameter.Decrease:
				UpdateDescriptionText("Decrease");
				UpdateValue((-1 * _variation).ToString(), UnityEngine.Color.red);
				break;
			case ButtonParameter.Reset:
				UpdateDescriptionText("Reset");
				UpdateValue("", Color.white);
				break;
		}
	}
}