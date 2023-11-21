using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject cinematic;
    [SerializeField] GameObject mainMenu;
    [SerializeField] int gameScene;
    public GameObject highlightnedPlaceholder;
    private VideoPlayer _player;


    public static MenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(ChapterManager.Instance != null);
        cinematic.SetActive(ChapterManager.Instance == null);
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
        SceneManager.LoadScene(gameScene); 
    }

    public void ExitGame() { Application.Quit(); }

}
