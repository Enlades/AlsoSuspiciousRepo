using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : PhysicalAnimator
{
    public Vector3 MoveDirection;
    public float MoveSpeed;
    public float MoveDistance;

    private Vector3 _startPosition;

    protected override void Awake(){
        base.Awake();

        _startPosition = transform.position;
    }

    protected override void FixedUpdateStuff()
    {
        _rb.MovePosition(transform.position + MoveDirection * MoveSpeed);

        if (Vector3.Distance(transform.position, _startPosition + MoveDirection * MoveSpeed) > MoveDistance)
        {
            _startPosition = transform.position;
            MoveDirection *= -1;
        }
    }
}
