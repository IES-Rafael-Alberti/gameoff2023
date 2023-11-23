using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    [SerializeField] int gameScene;
    [SerializeField] GameObject canvasMenu;

    void Start()
    {
        canvasMenu.SetActive(false);
    }


    public void PauseMenu() { 
    
        // inhabilitar el input system
        // activar el canvas del menu

    }

    public void Continue()
    {
        //SceneManager.LoadScene(gameScene);
    }


    public void RetryLevel()
    {
        SceneManager.LoadScene(gameScene);
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(gameScene);
    }

}
