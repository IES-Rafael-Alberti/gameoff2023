using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryManager : MonoBehaviour
{

    [SerializeField] int gameScene;
    [SerializeField] int menuScene;

    public void Retry()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

}
