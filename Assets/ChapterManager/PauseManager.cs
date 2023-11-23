using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    [SerializeField] int gameScene;
    [SerializeField] int menuScene;
    [SerializeField] GameObject canvasMenu;
    [SerializeField] GameObject pauseButton;

    void Start()
    {
       canvasMenu.SetActive(false);
    }

    public void Pause() 
    {
        Time.timeScale = 0f;
        canvasMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        canvasMenu.SetActive(false);
    }


    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameScene);
    }
    
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);
    }

}
