using UnityEngine;
using System.Collections;

[System.Serializable]
//////////////////////////////////////////////////////////////
// FirstPersonControl.cs
//
// FirstPersonControl creates a control scheme where the camera 
// location and controls directly map to being in the first person.
// The left pad is used to move the character, and the
// right pad is used to rotate the character. A quick double-tap
// on the right joystick will make the character jump.
//
// If no right pad is assigned, then tilt is used for rotation
// you double tap the left pad to jump
//////////////////////////////////////////////////////////////
[UnityEngine.RequireComponent(typeof(CharacterController))]
public partial class FirstPersonControl : MonoBehaviour
{
    // This script must be attached to a GameObject that has a CharacterController
    public Joystick moveTouchPad;
    public Joystick rotateTouchPad; // If unassigned, tilt is used
    public Transform cameraPivot; // The transform used for camera rotation
    public float forwardSpeed;
    public float backwardSpeed;
    public float sidestepSpeed;
    public float jumpSpeed;
    public float inAirMultiplier; // Limiter for ground speed while jumping
    public Vector2 rotationSpeed; // Camera rotation speed for each axis
    public float tiltPositiveYAxis;
    public float tiltNegativeYAxis;
    public float tiltXAxisMinimum;
    private Transform thisTransform;
    private CharacterController character;
    private Vector3 cameraVelocity;
    private Vector3 velocity; // Used for continuing momentum while in air
    private bool canJump;
    public virtual void Start()
    {
         // Cache component lookup at startup instead of doing this every frame		
        this.thisTransform = (Transform) this.GetComponent(typeof(Transform));
        this.character = (CharacterController) this.GetComponent(typeof(CharacterController));
        // Move the character to the correct start position in the level, if one exists
        GameObject spawn = GameObject.Find("PlayerSpawn");
        if (spawn)
        {
            this.thisTransform.position = spawn.transform.position;
        }
    }

    public virtual void OnEndGame()
    {
         // Disable joystick when the game ends	
        this.moveTouchPad.Disable();
        if (this.rotateTouchPad)
        {
            this.rotateTouchPad.Disable();
        }
        // Don't allow any more control changes when the game ends
        this.enabled = false;
    }

    public virtual void Update()
    {
        Vector3 movement = this.thisTransform.TransformDirection(new Vector3(this.moveTouchPad.position.x, 0, this.moveTouchPad.position.y));
        // We only want horizontal movement
        movement.y = 0;
        movement.Normalize();
        // Apply movement from move joystick
        Vector2 absJoyPos = new Vector2(Mathf.Abs(this.moveTouchPad.position.x), Mathf.Abs(this.moveTouchPad.position.y));
        if (absJoyPos.y > absJoyPos.x)
        {
            if (this.moveTouchPad.position.y > 0)
            {
                movement = movement * (this.forwardSpeed * absJoyPos.y);
            }
            else
            {
                movement = movement * (this.backwardSpeed * absJoyPos.y);
            }
        }
        else
        {
            movement = movement * (this.sidestepSpeed * absJoyPos.x);
        }
        // Check for jump
        if (this.character.isGrounded)
        {
            bool jump = false;
            Joystick touchPad = null;
            if (this.rotateTouchPad)
            {
                touchPad = this.rotateTouchPad;
            }
            else
            {
                touchPad = this.moveTouchPad;
            }
            if (!touchPad.IsFingerDown())
            {
                this.canJump = true;
            }
            if (this.canJump && (touchPad.tapCount >= 2))
            {
                jump = true;
                this.canJump = false;
            }
            if (jump)
            {
                 // Apply the current movement to launch velocity		
                this.velocity = this.character.velocity;
                this.velocity.y = this.jumpSpeed;
            }
        }
        else
        {
             // Apply gravity to our velocity to diminish it over time
            this.velocity.y = this.velocity.y + (Physics.gravity.y * Time.deltaTime);
            // Adjust additional movement while in-air
            movement.x = movement.x * this.inAirMultiplier;
            movement.z = movement.z * this.inAirMultiplier;
        }
        movement = movement + this.velocity;
        movement = movement + Physics.gravity;
        movement = movement * Time.deltaTime;
        // Actually move the character	
        this.character.Move(movement);
        if (this.character.isGrounded)
        {
            // Remove any persistent velocity after landing	
            this.velocity = Vector3.zero;
        }
        // Apply rotation from rotation joystick
        if (this.character.isGrounded)
        {
            Vector2 camRotation = Vector2.zero;
            if (this.rotateTouchPad)
            {
                camRotation = this.rotateTouchPad.position;
            }
            else
            {
                 // Use tilt instead
                //			print( iPhoneInput.acceleration );
                Vector3 acceleration = Input.acceleration;
                float absTiltX = Mathf.Abs(acceleration.x);
                if ((acceleration.z < 0) && (acceleration.x < 0))
                {
                    if (absTiltX >= this.tiltPositiveYAxis)
                    {
                        camRotation.y = (absTiltX - this.tiltPositiveYAxis) / (1 - this.tiltPositiveYAxis);
                    }
                    else
                    {
                        if (absTiltX <= this.tiltNegativeYAxis)
                        {
                            camRotation.y = -(this.tiltNegativeYAxis - absTiltX) / this.tiltNegativeYAxis;
                        }
                    }
                }
                if (Mathf.Abs(acceleration.y) >= this.tiltXAxisMinimum)
                {
                    camRotation.x = -(acceleration.y - this.tiltXAxisMinimum) / (1 - this.tiltXAxisMinimum);
                }
            }
            camRotation.x = camRotation.x * this.rotationSpeed.x;
            camRotation.y = camRotation.y * this.rotationSpeed.y;
            camRotation = camRotation * Time.deltaTime;
            // Rotate the character around world-y using x-axis of joystick
            this.thisTransform.Rotate(0, camRotation.x, 0, Space.World);
            // Rotate only the camera with y-axis input
            this.cameraPivot.Rotate(-camRotation.y, 0, 0);
        }
    }

    public FirstPersonControl()
    {
        this.forwardSpeed = 4;
        this.backwardSpeed = 1;
        this.sidestepSpeed = 1;
        this.jumpSpeed = 8;
        this.inAirMultiplier = 0.25f;
        this.rotationSpeed = new Vector2(50, 25);
        this.tiltPositiveYAxis = 0.6f;
        this.tiltNegativeYAxis = 0.4f;
        this.tiltXAxisMinimum = 0.1f;
        this.canJump = true;
    }

}