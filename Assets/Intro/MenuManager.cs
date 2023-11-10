using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject cinematic;
    [SerializeField] GameObject mainMenu;

    private VideoPlayer _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = cinematic.GetComponent<VideoPlayer>();
        _player.loopPointReached += DisableCinematic;
    }

    // Update is called once per frame
    void Update()
    {
                 
    }

    void DisableCinematic(VideoPlayer vp)
    {
        cinematic.SetActive(false);
        mainMenu.SetActive(true);
    }


    public void NewGame() { 
        Debug.Log("NewGame"); 
        SceneManager.LoadScene(2); 
    }

    public void ExitGame() { Application.Quit(); }

}
