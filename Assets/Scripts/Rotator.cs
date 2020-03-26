using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 Axis;
    public float Amount;

    private Rigidbody _rb;
    
    private bool _shouldAnimate;

    private void Awake(){
        _rb = GetComponent<Rigidbody>();

        _shouldAnimate = true;
    }

    private void FixedUpdate(){
        if(!_shouldAnimate){
            return;
        }

        _rb.MoveRotation(Quaternion.AngleAxis(Amount * Time.time, Axis));
    }

    private void OnCollisionEnter(Collision col){
        if (!_shouldAnimate)
        {
            return;
        }
        
        _shouldAnimate = false;
        _rb.useGravity = true;
    }
}
