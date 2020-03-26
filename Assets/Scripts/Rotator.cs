using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : PhysicalAnimator
{
    public Vector3 Axis;
    public float Amount;

    protected override void FixedUpdateStuff(){
        _rb.MoveRotation(Quaternion.AngleAxis(Amount * Time.time, Axis));
    }
}
