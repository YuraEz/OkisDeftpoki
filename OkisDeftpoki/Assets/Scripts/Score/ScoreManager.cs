using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    public int results;
    public int Score { get { return score; } }

    public Action<int> onBalanceChange;

    private void OnEnable()
    {
        score = PlayerPrefs.GetInt("score", 0);
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    //[Button]
    private void add100()
    {
        ChangeValue(100);
        FinishGame();
    }

    public void ChangeValue(int value, bool inStore = false)
    {
        score += value;
        scoreInGameBfUpt += value;
        if (inStore)
        {
            PlayerPrefs.SetInt("score", score);

            onBalanceChange?.Invoke(score);
        }
        else
        {

            PlayerPrefs.SetInt("score", score);
            onBalanceChange?.Invoke(scoreInGameBfUpt);
        }
    
    }

    

    public void FinishGame()
    {
        results = scoreInGameBfUpt;
        PlayerPrefs.SetInt("score", results + PlayerPrefs.GetInt("score", 0));
        //  if (PlayerPrefs.GetInt("record", 0) < results) PlayerPrefs.SetInt("record", results);
        // PlayerPrefs.SetInt("goal5", PlayerPrefs.GetInt("goal5", 0) + 1);
        // if (results >= 1500) PlayerPrefs.SetInt("goal6", PlayerPrefs.GetInt("goal6", 0) + 1);
        // if (results >= 2000) PlayerPrefs.SetInt("goal7", PlayerPrefs.GetInt("goal7", 0) + 1);
        // if (results >= 5000) PlayerPrefs.SetInt("goal8", PlayerPrefs.GetInt("goal8", 0) + 1);
        PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp", 0) + 100);
    }


    public int scoreInGameBfUpt;
    public void Finish(bool e)
    {

    }
    public void UpdateGame(int few)
    {

    }
    public void UpdateScore(int fw)
    {

    }
}
