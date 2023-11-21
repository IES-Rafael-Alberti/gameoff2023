using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.Video;
using System.Diagnostics;

public class EndingController : MonoBehaviour
{

    [SerializeField] GameObject cinematic;
    [SerializeField] int menuScene;
    private VideoPlayer _player;


    // Start is called before the first frame update
    void Start()
    {
        _player = cinematic.GetComponent<VideoPlayer>();

        _player.loopPointReached += LoadScene;


    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(menuScene);
    }

}