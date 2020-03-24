using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Controller> Controllers;

    private void Awake(){
        foreach (Controller c in Controllers)
        {
            c.Init();

            if (c is PlayerController)
            {
                ((PlayerController)c).FirstInputEvent += StartGame;
                ((PlayerController)c).PlayerDeathEvent += GameOver;
            }

            if (c is ProgressController)
            {
                ((ProgressController)c).ProgressUpdateEvent += UpdateProgress;
            }
        }

        SetGameState(GameState.pause);
    }

    private void StartGame()
    {
        SetGameState(GameState.play);
    }

    private void SetGameState(GameState p_state){
        foreach (Controller c in Controllers)
        {
            c.SetControllerState(p_state);
        }
    }

    private void UpdateProgress(float p_progress){
        foreach (Controller c in Controllers)
        {
            if (c is UIController)
            {
                ((UIController)c).SetProgress(p_progress);
            }
        }
    }

    private void GameOver(){
        SetGameState(GameState.gameOver);
    }
}
