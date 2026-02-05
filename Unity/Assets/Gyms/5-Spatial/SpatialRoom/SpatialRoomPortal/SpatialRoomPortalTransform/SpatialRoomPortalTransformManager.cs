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

public class SpatialRoomPortalTransformManager : MonoBehaviour
{
	public enum RoomPortalTransformObject
	{
		Room,
		Portal,
	}

	public enum RoomPortalTransformType
	{
		Translation,
		Rotation,
		Scale,
	}

	public enum RoomPortalTransformDirection
	{
		X,
		Y,
		Z,
	}

	private struct Transform
	{
		public Vector3 Position;
		public Vector3 Rotation;
		public Vector3 Scale;
	}

	// Singleton.
	private static SpatialRoomPortalTransformManager m_instance;
	public static SpatialRoomPortalTransformManager Instance
	{
		get
		{
			return m_instance;
		}
	}

	[SerializeField] protected Text _ObjectValueText;
	[SerializeField] protected Text _TransformValueText;
	[SerializeField] protected Text _DirectionValueText;

	[SerializeField] protected Text _RoomPositionValueText;
	[SerializeField] protected Text _RoomRotationValueText;
	[SerializeField] protected Text _RoomScaleValueText;

	[SerializeField] protected Text _PortalPositionValueText;
	[SerializeField] protected Text _PortalRotationValueText;
	[SerializeField] protected Text _PortalScaleValueText;

	[SerializeField] protected Text _PortalPlacementValueText;

	[SerializeField] protected AkRoom roomComponent;
	[SerializeField] protected AkRoomPortal portalComponent;
	[SerializeField] protected MeshRenderer VisualMaterial;

	private RoomPortalTransformObject _currentObject = 0;
	private RoomPortalTransformType _currentTransform = 0;
	private RoomPortalTransformDirection _currentDirection = 0; 
	
	public RoomPortalTransformObject currentObject { get { return _currentObject; } }
	public RoomPortalTransformType currentTransform { get { return _currentTransform; } }
	public RoomPortalTransformDirection currentDirection { get { return _currentDirection; } }

	private Transform _defaultRoomTransform;
	private Transform _defaultPortalTransform;
	private Transform _currentRoomTransform;
	private Transform _currentPortalTransform;

	private bool _isPortalValid = false;

	private void InitializeParams()
	{
		if (roomComponent != null)
		{
			_defaultRoomTransform.Position = roomComponent.transform.position;
			_defaultRoomTransform.Rotation = roomComponent.transform.eulerAngles;
			_defaultRoomTransform.Scale = roomComponent.transform.lossyScale;
		}

		if (portalComponent != null)
		{
			_defaultPortalTransform.Position = portalComponent.transform.position;
			_defaultPortalTransform.Rotation = portalComponent.transform.eulerAngles;
			_defaultPortalTransform.Scale = portalComponent.transform.lossyScale;

			_isPortalValid = portalComponent.IsValid;
		}

		if (VisualMaterial != null)
		{
			VisualMaterial.material.color = _isPortalValid ? Color.green : Color.red;
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
		Reset();
	}

	public void Reset()
	{
		_currentObject = 0;
		_currentTransform = 0;
		_currentDirection = 0;

		_currentRoomTransform.Position = _defaultRoomTransform.Position;
		_currentRoomTransform.Rotation = _defaultRoomTransform.Rotation;
		_currentRoomTransform.Scale = _defaultRoomTransform.Scale;

		_currentPortalTransform.Position = _defaultPortalTransform.Position;
		_currentPortalTransform.Rotation = _defaultPortalTransform.Rotation;
		_currentPortalTransform.Scale = _defaultPortalTransform.Scale;

		UpdateComponents();
		UpdateUI();
	}

	public void ChangeObject()
	{
		switch(_currentObject)
		{
			case RoomPortalTransformObject.Room:
				_currentObject = RoomPortalTransformObject.Portal;
				break;
			case RoomPortalTransformObject.Portal:
				_currentObject = RoomPortalTransformObject.Room;
				break;
		}

		UpdateUI();
	}

	public void ChangeTransform()
	{
		switch (_currentTransform)
		{
			case RoomPortalTransformType.Translation:
				_currentTransform = RoomPortalTransformType.Rotation;
				break;
			case RoomPortalTransformType.Rotation:
				_currentTransform = RoomPortalTransformType.Scale;
				break;
			case RoomPortalTransformType.Scale:
				_currentTransform = RoomPortalTransformType.Translation;
				break;
		}

		UpdateUI();
	}

	public void ChangeDirection()
	{
		switch (_currentDirection)
		{
			case RoomPortalTransformDirection.X:
				_currentDirection = RoomPortalTransformDirection.Y;
				break;
			case RoomPortalTransformDirection.Y:
				_currentDirection = RoomPortalTransformDirection.Z;
				break;
			case RoomPortalTransformDirection.Z:
				_currentDirection = RoomPortalTransformDirection.X;
				break;
		}

		UpdateUI();
	}

	public void TransformObject(float in_variation)
	{
		Transform transform;

		if (_currentObject == RoomPortalTransformObject.Room)
		{
			transform.Position = _currentRoomTransform.Position;
			transform.Rotation = _currentRoomTransform.Rotation;
			transform.Scale = _currentRoomTransform.Scale;
		}
		else
		{
			transform.Position = _currentPortalTransform.Position;
			transform.Rotation = _currentPortalTransform.Rotation;
			transform.Scale = _currentPortalTransform.Scale;
		}

		if (_currentTransform == RoomPortalTransformType.Translation)
		{
			if (_currentDirection == RoomPortalTransformDirection.X)
				transform.Position.x += in_variation;
			else if (_currentDirection == RoomPortalTransformDirection.Y)
				transform.Position.y += in_variation;
			else if (_currentDirection == RoomPortalTransformDirection.Z)
				transform.Position.z += in_variation;
		}
		else if (_currentTransform == RoomPortalTransformType.Rotation)
		{
			if (_currentDirection == RoomPortalTransformDirection.X)
				transform.Rotation.x += in_variation;
			else if (_currentDirection == RoomPortalTransformDirection.Y)
				transform.Rotation.y += in_variation;
			else if (_currentDirection == RoomPortalTransformDirection.Z)
				transform.Rotation.z += in_variation;
		}
		else if (_currentTransform == RoomPortalTransformType.Scale)
		{
			if (_currentDirection == RoomPortalTransformDirection.X)
				transform.Scale.x += in_variation;
			else if (_currentDirection == RoomPortalTransformDirection.Y)
				transform.Scale.y += in_variation;
			else if (_currentDirection == RoomPortalTransformDirection.Z)
				transform.Scale.z += in_variation;
		}

		if (_currentObject == RoomPortalTransformObject.Room)
		{
			_currentRoomTransform = transform;
		}
		else
		{
			_currentPortalTransform = transform;
		}

		UpdateComponents();
		UpdateUI();
	}

	public void UpdateComponents()
	{
		roomComponent.transform.position = _currentRoomTransform.Position;
		roomComponent.transform.eulerAngles = _currentRoomTransform.Rotation;
		roomComponent.transform.localScale = _currentRoomTransform.Scale;

		portalComponent.transform.position = _currentPortalTransform.Position;
		portalComponent.transform.eulerAngles = _currentPortalTransform.Rotation;
		portalComponent.transform.localScale = _currentPortalTransform.Scale;

		Physics.SyncTransforms();
	}

	public void UpdateUI()
	{
		// Panel Values
		_ObjectValueText.text = _currentObject.ToString();
		_TransformValueText.text = _currentTransform.ToString();
		_DirectionValueText.text = _currentDirection.ToString();

		_RoomPositionValueText.text = _currentRoomTransform.Position.ToString();
		_RoomRotationValueText.text = _currentRoomTransform.Rotation.ToString();
		_RoomScaleValueText.text = _currentRoomTransform.Scale.ToString();

		_PortalPositionValueText.text = _currentPortalTransform.Position.ToString();
		_PortalRotationValueText.text = _currentPortalTransform.Rotation.ToString();
		_PortalScaleValueText.text = _currentPortalTransform.Scale.ToString();

		UpdatePortalPlacementUI();
	}

	private void UpdatePortalPlacementUI()
	{
		_PortalPlacementValueText.text = _isPortalValid ? "Valid" : "Invalid";

		// Panel text color
		_PortalPlacementValueText.color = _isPortalValid ? Color.green : Color.red;

		// Visual mesh material colors
		VisualMaterial.material.color = _isPortalValid ? new Color(Color.green.r, Color.green.g, Color.green.b, 0.5f) : new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
	}

	private void Update()
	{
		_isPortalValid = portalComponent.IsValid;
		UpdatePortalPlacementUI();
	}
}