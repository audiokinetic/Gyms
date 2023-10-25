using UnityEngine;
using System.Collections;

//////////////////////////////////////////////////////////////
// RotationConstraint.js
// Penelope iPhone Tutorial
//
// RotationConstraint constrains the relative rotation of a 
// Transform. You select the constraint axis in the editor and 
// specify a min and max amount of rotation that is allowed 
// from the default rotation
//////////////////////////////////////////////////////////////
public enum ConstraintAxis
{
    X = 0,
    Y = 1,
    Z = 2
}

[System.Serializable]
public partial class RotationConstraint : MonoBehaviour
{
    public ConstraintAxis axis; // Rotation around this axis is constrained
    public float min; // Relative value in degrees
    public float max; // Relative value in degrees
    private Transform thisTransform;
    private Vector3 rotateAround;
    private Quaternion minQuaternion;
    private Quaternion maxQuaternion;
    private float range;
    public virtual void Start()
    {
        this.thisTransform = this.transform;
        switch (this.axis)
        {
            case ConstraintAxis.X:
                this.rotateAround = Vector3.right;
                break;
            case ConstraintAxis.Y:
                this.rotateAround = Vector3.up;
                break;
            case ConstraintAxis.Z:
                this.rotateAround = Vector3.forward;
                break;
        }
        // Set the min and max rotations in quaternion space
        Quaternion axisRotation = Quaternion.AngleAxis(this.thisTransform.localRotation.eulerAngles[((int) this.axis)], this.rotateAround);
        this.minQuaternion = axisRotation * Quaternion.AngleAxis(this.min, this.rotateAround);
        this.maxQuaternion = axisRotation * Quaternion.AngleAxis(this.max, this.rotateAround);
        this.range = this.max - this.min;
    }

    // We use LateUpdate to grab the rotation from the Transform after all Updates from
    // other scripts have occured
    public virtual void LateUpdate()
    {
         // We use quaternions here, so we don't have to adjust for euler angle range [ 0, 360 ]
        Quaternion localRotation = this.thisTransform.localRotation;
        Quaternion axisRotation = Quaternion.AngleAxis(localRotation.eulerAngles[((int) this.axis)], this.rotateAround);
        float angleFromMin = Quaternion.Angle(axisRotation, this.minQuaternion);
        float angleFromMax = Quaternion.Angle(axisRotation, this.maxQuaternion);
        if ((angleFromMin <= this.range) && (angleFromMax <= this.range))
        {
            return; // within range
        }
        else
        {
             // Let's keep the current rotations around other axes and only
             // correct the axis that has fallen out of range.
            Vector3 euler = localRotation.eulerAngles;
            if (angleFromMin > angleFromMax)
            {
                euler[((int) this.axis)] = this.maxQuaternion.eulerAngles[((int) this.axis)];
            }
            else
            {
                euler[((int) this.axis)] = this.minQuaternion.eulerAngles[((int) this.axis)];
            }
            this.thisTransform.localEulerAngles = euler;
        }
    }

}