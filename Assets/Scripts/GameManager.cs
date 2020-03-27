using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public List<Controller> Controllers; 

    public LevelColors LevelColors;

    public Material FaceColorMat;
    public Material OppositeColorMat;
    public Material GroundColorMat;

    private void Awake(){
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;

        int randomColorPaletteIndex = UnityEngine.Random.Range(1, LevelColors.ColorPalettes.Length);
        int lastRandom = PlayerPrefs.GetInt("LastRandomColorPaletteIndex", -1);

        while(randomColorPaletteIndex == lastRandom){
            randomColorPaletteIndex = UnityEngine.Random.Range(1, LevelColors.ColorPalettes.Length);
        }

        PlayerPrefs.SetInt("LastRandomColorPaletteIndex", randomColorPaletteIndex);

        AssignLevelColors(randomColorPaletteIndex);

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
        SetGameState(GameState.gameIsGoingToBeOver);

        StartCoroutine(DelayedAction(1f, () =>
        {
            SetGameState(GameState.gameOver);
        }));
    }

    private void LevelComplete(){
        SetGameState(GameState.win);

        LoadNextLevel();
    }

    private void LoadNextLevel(){
        int currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;

        StartCoroutine(DelayedAction(1f, ()=>{
            if (currentLevelBuildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(currentLevelBuildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(currentLevelBuildIndex);
            }
        }));
    }

    private void AssignLevelColors(int p_index){
        FaceColorMat.color = LevelColors.ColorPalettes[p_index].FaceColor;
        OppositeColorMat.color = LevelColors.ColorPalettes[p_index].OppositeColor;
        GroundColorMat.color = LevelColors.ColorPalettes[p_index].GroundColor;
        Camera.main.backgroundColor = LevelColors.ColorPalettes[p_index].CameraBGColor;
    }

    private IEnumerator DelayedAction(float p_waitTime, Action p_callback){
        yield return new WaitForSeconds(p_waitTime);

        if(p_callback != null){
            p_callback.Invoke();
        }
    }
}
