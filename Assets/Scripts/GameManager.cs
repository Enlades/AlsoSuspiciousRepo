using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public List<Controller> Controllers;

    private void Awake(){
        foreach (Controller c in Controllers)
        {
            c.Init(SceneManager.GetActiveScene().buildIndex);
            
            c.HandleEvents(new Action(() => { StartGame(); })
            , new Action(() => { GameOver(); })
            , new Action<float>((float p_float) => { BroadcastProgress(p_float); })
            , new Action<Vector3>((Vector3 p_vector) => { BroadcastPlayerPosition(p_vector); }));
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

    private void BroadcastPlayerPosition(Vector3 p_position){
        foreach (Controller c in Controllers)
        {
            c.EvaluatePlayerPosition(p_position);
        }
    }

    private void BroadcastProgress(float p_progress){
        foreach (Controller c in Controllers)
        {
            c.EvaluateProgress(p_progress);
        }

        if(p_progress >= 1f){
            LevelComplete();
        }
    }

    private void GameOver(){
        SetGameState(GameState.gameOver);
    }

    private void LevelComplete(){
        SetGameState(GameState.win);

        LoadNextLevel();
    }

    private void LoadNextLevel(){
        int currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentLevelBuildIndex < SceneManager.sceneCountInBuildSettings - 1){
            SceneManager.LoadScene(currentLevelBuildIndex + 1);
        }else{
            SceneManager.LoadScene(currentLevelBuildIndex);
        }
    }
}
