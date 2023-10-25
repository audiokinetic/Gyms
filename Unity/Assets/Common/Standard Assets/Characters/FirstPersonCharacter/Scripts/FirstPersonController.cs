using UnityEngine;

#pragma warning disable 618, 649
namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool _isWalking;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] [Range(0f, 1f)] private float _runStepLength;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _stickToGroundForce;
        [SerializeField] private float _gravityMultiplier;
        [SerializeField] private MouseLook _mouseLook;
        [SerializeField] private bool _useFovKick;
        [SerializeField] private bool _useHeadBob;
        [SerializeField] private float _stepInterval;
        [SerializeField] AK.Wwise.Event _footStepEvent;
        [SerializeField] bool _useFootSteps = false;

        private Camera _camera;
        private bool _jump;
        private Vector2 _input;
        private Vector3 _moveDir = Vector3.zero;
        private CharacterController _characterController;
        private CollisionFlags _collisionFlags;
        private bool _previouslyGrounded;
        private float _stepCycle;
        private float _nextStep;
        private bool _jumping;
        private bool _firstStep = true;

        // Use this for initialization
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _camera = Camera.main;
            _stepCycle = 0f;
            _nextStep = _stepCycle/2f;
            _jumping = false;
			_mouseLook.Init(transform , _camera.transform);
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!_jump)
            {
                _jump = Input.GetButtonDown("Jump");
            }

            if (!_previouslyGrounded && _characterController.isGrounded)
            {
                _moveDir.y = 0f;
                _jumping = false;
            }
            if (!_characterController.isGrounded && !_jumping && _previouslyGrounded)
            {
                _moveDir.y = 0f;
            }

            _previouslyGrounded = _characterController.isGrounded;
        }

        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*_input.y + transform.right*_input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, _characterController.radius, Vector3.down, out hitInfo,
                               _characterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            _moveDir.x = desiredMove.x*speed;
            _moveDir.z = desiredMove.z*speed;


            if (_characterController.isGrounded)
            {
                _moveDir.y = -_stickToGroundForce;

                if (_jump)
                {
                    _moveDir.y = _jumpSpeed;
                    _jump = false;
                    _jumping = true;
                }
            }
            else
            {
                _moveDir += Physics.gravity*_gravityMultiplier*Time.fixedDeltaTime;
            }
            _collisionFlags = _characterController.Move(_moveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            _mouseLook.UpdateCursorLock();
        }

        private void ProgressStepCycle(float speed)
        {
            if (_characterController.velocity.sqrMagnitude > 0 && (_input.x != 0 || _input.y != 0))
            {
                _stepCycle += (_characterController.velocity.magnitude + (speed*(_isWalking ? 1f : _runStepLength)))*
                             Time.fixedDeltaTime;
            }

            if (!(_stepCycle > _nextStep))
            {
                return;
            }

            _nextStep = _stepCycle + _stepInterval;

            if (_useFootSteps && !_jumping && !_firstStep)
            {
                _footStepEvent.Post(gameObject);
            }
            else if(_useFootSteps && !_jumping && _firstStep)
            {
                _firstStep = false;
            }
        }

        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!_useHeadBob)
            {
                return;
            }
            if (_characterController.velocity.magnitude > 0 && _characterController.isGrounded)
            {
                newCameraPosition = _camera.transform.localPosition;
            }
            else
            {
                newCameraPosition = _camera.transform.localPosition;
            }
            _camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            bool waswalking = _isWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            _isWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = _isWalking ? _walkSpeed : _runSpeed;
            _input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (_input.sqrMagnitude > 1)
            {
                _input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (_isWalking != waswalking && _useFovKick && _characterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
            }
        }


        private void RotateView()
        {
            _mouseLook.LookRotation (transform, _camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (_collisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(_characterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
