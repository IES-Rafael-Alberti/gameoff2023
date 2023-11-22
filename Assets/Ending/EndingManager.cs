using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Diagnostics;

public class EndingManager : MonoBehaviour
{

    [SerializeField] GameObject cinematicED;
    [SerializeField] int menuScene;
    private VideoPlayer _player;
    public static EndingManager Instance;

    private void Awake()
    {
        Instance = this;
        _player = cinematicED.GetComponent<VideoPlayer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        ViewCinematic();
    }

    void OnCinematicFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(menuScene);
    }

    public void ViewCinematic()
    {
        _player.Play();
        _player.loopPointReached += OnCinematicFinished;
    }

}