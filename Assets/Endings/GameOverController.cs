using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    [SerializeField] int gameScene;
    [SerializeField] int menuScene;

    public static ChapterManager Instance;


        

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // StartCoroutine(WaitDelay());

    public void Retry()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void Return()
    {
        SceneManager.LoadScene(menuScene);
    }


}