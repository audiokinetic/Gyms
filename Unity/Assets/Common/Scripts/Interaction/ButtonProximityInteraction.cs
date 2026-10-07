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

public class ButtonProximityInteraction: MonoBehaviour
{
	const float AngleThreshold = 90;

	public float delayTime = 0.5f; // Set the time required between each valid interaction
	private float timer = 0f;
	private bool m_PlayerInTrigger = false;
	public UnityEngine.Events.UnityEvent OnButtonPressed;

	void OnTriggerEnter(Collider in_other)
	{
		var controller = SetPlayerController.GetCurrentPlayerController();
		if (controller && in_other == controller.GetComponent<CapsuleCollider>())
		{
			m_PlayerInTrigger = true;
		}
	}

	void OnTriggerExit(Collider in_other)
	{
		var controller = SetPlayerController.GetCurrentPlayerController();
		if (in_other == controller.GetComponent<CapsuleCollider>())
		{
			m_PlayerInTrigger = false;
		}
	}

	bool IsLookingAtButton()
	{
		GameObject controller = SetPlayerController.GetCurrentPlayerController();
		Vector3 playerForward = controller.transform.forward;
		Vector3 playerForwardXZ = new Vector2(playerForward.x, playerForward.z);

		Vector3 playerToButton = gameObject.transform.position - controller.transform.position;
		Vector2 playerToButtonXZ = new Vector2(playerToButton.x, playerToButton.z);

		float angle = Vector2.Angle(playerForwardXZ, playerToButtonXZ);
		return angle <= AngleThreshold;
	}


	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;

		bool playerInteracting = Input.GetButtonDown("Interact") || (Input.touchCount == 3) && timer >= delayTime;

		if (m_PlayerInTrigger && playerInteracting && IsLookingAtButton())
		{
			if (OnButtonPressed != null)
			{
				OnButtonPressed.Invoke();
			}
			timer = 0f;
		}
	}
}
