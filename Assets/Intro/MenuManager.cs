using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject cinematic;
    [SerializeField] GameObject mainMenu;
    [SerializeField] int gameScene;
    [SerializeField] GameObject viewer;
    [SerializeField] GameObject continueButton;

    public static MenuManager Instance;

    public GameObject highlightnedPlaceholder;
    private VideoPlayer _player;




    private void Awake()
    {
        Instance = this;
        PrepareContinueButton();
    }


    // Start is called before the first frame update
    void Start()
    {
        
        enableMenu(true);
        _player = cinematic.GetComponent<VideoPlayer>();

    }

    void PrepareContinueButton()
    {
        var button = continueButton.GetComponent<Button>();
        var isFirstChapter = ChapterManager.Instance.chapterList.currentChapter == 0;
        button.interactable = !isFirstChapter;
        var colors = button.colors;
        colors.normalColor = isFirstChapter ? Color.gray : Color.white;
        button.colors = colors;
    }


    void OnCinematicFinished(VideoPlayer vp)
    {
        if (ChapterManager.Instance.chapterList.firstPlay)
        {
            ChapterManager.Instance.chapterList.firstPlay = false;
            SceneManager.LoadScene(gameScene);
        }
        else
        {
            enableMenu(true);
        }

    }

    public void NewGame() {
        if (ChapterManager.Instance.chapterList.firstPlay)
        {
            ViewCinematic();
            ChapterManager.Instance.chapterList.currentChapter = 0;
        }
        else SceneManager.LoadScene(gameScene);
     }

    public void ContinueGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void ExitGame() 
    { 
        Application.Quit(); 
    }

    void enableMenu(bool enable)
    {
        mainMenu.SetActive(enable);
        cinematic.SetActive(!enable);
        viewer.SetActive(!ChapterManager.Instance.chapterList.firstPlay);
        highlightnedPlaceholder.SetActive(false);
        //continueButton.SetActive(false);
    }

    public void ViewCinematic()
    {
        enableMenu(false);
        _player.Play();
        _player.loopPointReached += OnCinematicFinished;
    }

}
