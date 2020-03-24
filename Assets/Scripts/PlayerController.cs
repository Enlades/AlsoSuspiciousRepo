using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : Controller
{
    public event Action FirstInputEvent;
    
    public GameObject PlayerObject;

    private Rigidbody _playerRb;

    private bool _shouldTakeInput;
    private bool _firsInput;

    private void FixedUpdate(){
        if(!_shouldTakeInput){
            return;
        }

        if(Input.GetMouseButton(0)){
            if (!_firsInput)
            {
                if(FirstInputEvent != null){
                    FirstInputEvent.Invoke();
                }
                _firsInput = true;
            }
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("Ground"))){
                _playerRb.MovePosition(hit.point + Vector3.up * 0.5f);
            }
        }
    }

    public override void SetControllerState(GameState p_state){
        switch(p_state){
            case GameState.play :
            case GameState.pause :{
                _shouldTakeInput = true;
                _firsInput = false;
                break;
            }
        }
    }

    public override void Init(){
        _shouldTakeInput = false;
        _firsInput = false;

        _playerRb = PlayerObject.GetComponent<Rigidbody>();
    }
}
