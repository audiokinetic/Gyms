using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SmokeTimelineInteractiveMusicSeekInteract : MonoBehaviour
{
	[SerializeField]
	PlayableDirector timeline = default;
	public void Interact()
	{
		timeline.time = 2f;
		timeline.Evaluate();
	}
}
