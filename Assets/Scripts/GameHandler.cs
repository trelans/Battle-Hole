using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject level;
    [SerializeField] private TextMeshProUGUI levelTMP;
    
    private int levelNo;
    private int levelIndex;
    private void Start()
    {
        
        levelNo = PlayerPrefs.GetInt("level", 1);
        levelIndex = PlayerPrefs.GetInt("levelIndex", 0);
        level.transform.GetChild(levelIndex).gameObject.SetActive(true);
        levelTMP.SetText("LEVEL " + levelNo);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        
        Debug.Log("GAME OVER!");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        
        levelNo++;
        levelIndex++;

        if (levelIndex == level.transform.childCount)
        {
            levelIndex = 0;
        }

        PlayerPrefs.SetInt("levelIndex" , levelIndex);
        PlayerPrefs.SetInt("level", levelNo);
        Time.timeScale = 1;
        RestartLevel();
    }

    public void OpenWin()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }
}
