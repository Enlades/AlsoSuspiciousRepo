using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : Controller
{
    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI NextLevelText;

    public Image ProgressBarFill;

    public GameObject RestartPanel;
    public GameObject TouchHand;
    public Button RestartButton;

    public override void SetControllerState(GameState p_state)
    {
        switch (p_state)
        {
            case GameState.play:
                {
                    TouchHand.SetActive(false);
                    break;
                }
            case GameState.pause:
                {
                    TouchHand.SetActive(true);
                    break;
                }
            case GameState.gameOver:
                {
                    RestartPanel.SetActive(true);
                    break;
                }
        }
    }

    public override void Init()
    {
        SetProgress(0f);
        SetLevel(1);

        RestartButton.onClick.AddListener(()=>{RestartLevel();});
    }

    public void SetLevel(int p_currentLevel){
        CurrentLevelText.text = p_currentLevel.ToString();
        NextLevelText.text = (p_currentLevel + 1).ToString();
    }

    public void SetProgress(float p_amount){
        ProgressBarFill.fillAmount = p_amount;
    }

    private void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
