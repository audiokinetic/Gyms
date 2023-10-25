using UnityEngine;

public class Joystick : MonoBehaviour
{
	private static Joystick[] joysticks; // A static collection of all joysticks
	private const float tapTimeDelta = 0.3f; // Time allowed between taps
	public Vector2 deadZone; // Control when position is output
	public bool normalize; // Normalize output after the dead-zone?
	public Vector2 position; // [-1, 1] in x,y
	public int tapCount; // Current tap count
	private int lastFingerId = -1; // Finger last used for this joystick
	private float tapTimeWindow; // How much time there is left for a tap to occur
	private Vector2 fingerDownPos;
	public UnityEngine.UI.RawImage RawImage;
	private Rect touchZone; // Default position / extents of the joystick graphic
	private Vector2 guiTouchOffset; // Offset to apply to touch input
	private Vector2 guiCenter; // Center of joystick

	public void Start()
	{
		// Collect all joysticks in the game, so we can relay finger latching messages
		joysticks = FindObjectsOfType<Joystick>();

		if (RawImage)
		{
			touchZone = RawImage.GetPixelAdjustedRect();
			touchZone.position = RawImage.rectTransform.TransformPoint(touchZone.position);
		}
	}

	public void Disable()
	{
		gameObject.SetActive(false);
	}

	private void ResetJoystick()
	{
		// Release the finger control and set the joystick back to the default position
		lastFingerId = -1;
		position = Vector2.zero;
		fingerDownPos = Vector2.zero;

		if (RawImage)
		{
			float _14 = 0.025f;
			Color _15 = RawImage.color;
			_15.a = _14;
			RawImage.color = _15;
		}
	}

	public bool IsFingerDown()
	{
		return lastFingerId != -1;
	}

	private void LatchedFinger(int fingerId)
	{
		// If another joystick has latched this finger, then we must release it
		if (lastFingerId == fingerId)
			ResetJoystick();
	}

	private void Update()
	{

		int count = Input.touchCount;

		// Adjust the tap time window while it still available
		if (tapTimeWindow > 0)
		{
			tapTimeWindow -= Time.deltaTime;
		}
		else
		{
			tapCount = 0;
		}
		if (count == 0)
		{
			ResetJoystick();
		}
		else
		{
			for (int i = 0; i < count; ++i)
			{
				var touch = Input.GetTouch(i);

				if (touchZone.Contains(touch.position) && ((lastFingerId == -1) || (lastFingerId != touch.fingerId)))
				{
					if (RawImage)
					{
						float _16 = 0.15f;
						Color _17 = RawImage.color;
						_17.a = _16;
						RawImage.color = _17;
					}
					lastFingerId = touch.fingerId;
					fingerDownPos = touch.position;

					// Accumulate taps if it is within the time window
					if (tapTimeWindow > 0)
					{
						tapCount++;
					}
					else
					{
						tapCount = 1;
						tapTimeWindow = tapTimeDelta;
					}
					// Tell other joysticks we've latched this finger
					foreach (var j in joysticks)
						if (j != this)
							j.LatchedFinger(touch.fingerId);
				}

				if (lastFingerId == touch.fingerId)
				{
					// Override the tap count with what the iPhone SDK reports if it is greater
					// This is a workaround, since the iPhone SDK does not currently track taps
					// for multiple touches
					if (touch.tapCount > tapCount)
						tapCount = touch.tapCount;

					position.x = Mathf.Clamp((touch.position.x - fingerDownPos.x) / (touchZone.width / 2), -1, 1);
					position.y = Mathf.Clamp((touch.position.y - fingerDownPos.y) / (touchZone.height / 2), -1, 1);

					if ((touch.phase == TouchPhase.Ended) || (touch.phase == TouchPhase.Canceled))
						ResetJoystick();
				}
			}
		}

		// Adjust for dead zone
		float absoluteX = Mathf.Abs(position.x);
		float absoluteY = Mathf.Abs(position.y);
		if (absoluteX < deadZone.x)
		{
			// Report the joystick as being at the center if it is within the dead zone
			position.x = 0;
		}
		else if (normalize)
		{
			// Rescale the output after taking the dead zone into account
			position.x = (Mathf.Sign(position.x) * (absoluteX - deadZone.x)) / (1 - deadZone.x);
		}

		if (absoluteY < deadZone.y)
		{
			// Report the joystick as being at the center if it is within the dead zone
			position.y = 0;
		}
		else if (normalize)
		{
			// Rescale the output after taking the dead zone into account
			position.y = (Mathf.Sign(position.y) * (absoluteY - deadZone.y)) / (1 - deadZone.y);
		}
	}
}
