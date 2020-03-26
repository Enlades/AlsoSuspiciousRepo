using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : Controller
{
    public event Action FirstInputEvent;
    public event Action PlayerDeathEvent;
    public event Action<Vector3> PlayerPositionEvent;
    
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

                if(PlayerPositionEvent != null){
                    PlayerPositionEvent.Invoke(PlayerObject.transform.position);
                }
            }
        }
    }

    public override void SetControllerState(GameState p_state)
    {
        switch (p_state)
        {
            case GameState.play:
            case GameState.pause:
                {
                    _shouldTakeInput = true;
                    _firsInput = false;
                    break;
                }
            case GameState.gameOver:{
                _shouldTakeInput = false;
                _firsInput = false;
                break;
            }
            case GameState.win:{
                _shouldTakeInput = false;
                _firsInput = false;
                break;
            }
        }
    }

    public override void Init(int p_levelIndex)
    {
        _shouldTakeInput = false;
        _firsInput = false;

        _playerRb = PlayerObject.GetComponent<Rigidbody>();

        PlayerObject.GetComponent<CollisionCheck>().CollisionEvent += PlayerDeath;
    }

    public override void HandleEvents(params Delegate[] p_delegates)
    {
        FirstInputEvent += (Action)p_delegates[0];
        PlayerDeathEvent += (Action)p_delegates[1];
        PlayerPositionEvent += (Action<Vector3>)p_delegates[3];
    }

    private void PlayerDeath(){
        Debug.Log("Did you ever hear the tragedy of Darth Plagueis the Wise?");

        if(PlayerDeathEvent != null){
            PlayerDeathEvent.Invoke();
        }
    }
}
