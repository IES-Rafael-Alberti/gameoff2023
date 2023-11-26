using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using SB.Runtime;

public class PauseManager : MonoBehaviour
{

    [SerializeField] int menuScene;
    [SerializeField] GameObject canvasMenu;
    [SerializeField] GameObject pauseButton;
    public SBShuffleBoardScript sbController;

    void Start()
    {
       canvasMenu.SetActive(false);
    }

    public void Pause() 
    {
        //Time.timeScale = 0f;
        //TODO Change into event
        canvasMenu.SetActive(true);
    }

    public void Resume()
    {
        //Time.timeScale = 1f;
        //TODO Change into event
        canvasMenu.SetActive(false);
    }


    public void RetryLevel()
    {
        //Time.timeScale = 1f;
        sbController.ResetBoard();
        Resume();
    }
    
    public void ReturnToMenu()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);
    }

}
