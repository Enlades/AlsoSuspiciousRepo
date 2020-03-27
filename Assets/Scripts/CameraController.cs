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
                StartCoroutine(CameraShake());
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

    private IEnumerator CameraShake(){
        
        float timer = 0.2f;
        float maxTimer = timer;
        float magnitude = 0.2f;

        Vector3 startPosition = transform.position;

        while(timer > 0){

            transform.Translate(Random.Range(-1f, 1f) * magnitude, Random.Range(-1f, 1f)* magnitude, Random.Range(-1f, 1f)* magnitude);

            timer -= Time.deltaTime;

            yield return new WaitForFixedUpdate();

            yield return null;
        }

        transform.position = startPosition;
    }
}
