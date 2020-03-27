using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Controller
{
    public float MoveSpeed;

    private bool _shouldMove;

    private void FixedUpdate(){
        if(!_shouldMove){
            return;
        }

        transform.Translate(Vector3.forward * MoveSpeed, Space.World);
    }

    public override void SetControllerState(GameState p_state){
        switch(p_state){
            case GameState.debug :{
                break;
            }
            case GameState.play :{
                _shouldMove = true;
                break;
            }
            case GameState.gameIsGoingToBeOver:{
                MoveSpeed /= 2f;
                break;
            }
            case GameState.pause:{
                _shouldMove = false;
                break;
            }
        }
    }

    public override void Init(int p_levelIndex){
        _shouldMove = false;
    }
}
