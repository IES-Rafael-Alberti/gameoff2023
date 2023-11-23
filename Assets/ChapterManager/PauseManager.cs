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

        canvasMenu.SetActive(true);

        // inhabilitar el input system
        // activar el canvas del menu

    }

    public void OnContinue()
    {
        SceneManager.LoadScene(gameScene);
    }


    public void OnRetryLevel()
    {
        SceneManager.LoadScene(gameScene);
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(gameScene);
    }

}
