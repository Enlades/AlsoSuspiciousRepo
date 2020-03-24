using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionCheck : MonoBehaviour
{
    public event Action CollisionEvent;

    public string TargetTag;

    private void OnCollisionEnter(Collision col){
        if(col.collider.CompareTag(TargetTag)){
            Debug.Log("Did you ever hear the tragedy of Darth Plagueis the Wise?");

            if(CollisionEvent != null){
                CollisionEvent.Invoke();
            }
        }
    }
}
