using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool ClampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public float MinimumY = -60F;
        public float MaximumY = 60F;
        public bool Smooth;
        public float SmoothTime = 5f;
        public bool LockCursor = true;


        private Quaternion _characterTargetRot;
        private Quaternion _cameraTargetRot;
        private bool _cursorIsLocked = true;

        public void Init(Transform character, Transform camera)
        {
            _characterTargetRot = character.localRotation;
            _cameraTargetRot = camera.localRotation;
        }


        public void LookRotation(Transform character, Transform camera)
        {
            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

            _characterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            _cameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            if(ClampVerticalRotation)
                _cameraTargetRot = ClampRotationAroundXAxis (_cameraTargetRot);

            if(Smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, _characterTargetRot,
                    SmoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, _cameraTargetRot,
                    SmoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = _characterTargetRot;
                camera.localRotation = _cameraTargetRot;
            }

            UpdateCursorLock();
        }

        public void SetCursorLock(bool value)
        {
            LockCursor = value;
            if(!LockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
            if (LockCursor)
                InternalLockUpdate();
        }

        private void InternalLockUpdate()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                _cursorIsLocked = false;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                _cursorIsLocked = true;
            }

            if (_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }



    }
}
