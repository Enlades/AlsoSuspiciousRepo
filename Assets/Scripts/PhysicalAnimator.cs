using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalAnimator : MonoBehaviour
{
    protected Rigidbody _rb;

    private bool _shouldAnimate;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _shouldAnimate = true;
    }

    private void FixedUpdate()
    {
        if (!_shouldAnimate)
        {
            return;
        }

        FixedUpdateStuff();
    }

    protected abstract void FixedUpdateStuff();

    protected virtual void OnCollisionEnter(Collision col)
    {
        if (!_shouldAnimate)
        {
            return;
        }

        _shouldAnimate = false;
        _rb.useGravity = true;
    }
}
