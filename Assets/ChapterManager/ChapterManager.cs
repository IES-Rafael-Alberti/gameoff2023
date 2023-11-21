using SB.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.Video;

public class ChapterManager : MonoBehaviour
{

    // These atributes are static
    public static ChapterManager Instance;
    public static EventManager Events;

    [SerializeField] List<CMChapterSO> chapters = new List<CMChapterSO>();
    [SerializeField] GameObject boardPrefab;
    [SerializeField] int endingScene;
    [SerializeField] int gameOverScene;

    private int _currentChapter;

    private void Awake()
    {
        if (Instance != null)
        {
            Instantiate(boardPrefab);
            Destroy(this); //Why destroy?
            return;
        }
        // This part only once in game lifetime
        _currentChapter = 0;
        Events = new EventManager();
        Instance = this;
        // Subscribe to next chapter event
        Events.NextChapter.AddListener(OnNextChapter);
        Events.NextChapter.AddListener(EventTest);
        // Subscribe to game over event?
        
        // Init Method
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SBBoardScriptableObject GetChapterBoard()
    {
        return chapters[_currentChapter].ChapterBoard;
    }

    public void OnNextChapter()
    {
        _currentChapter++;
        Debug.Log("Chapter: " + _currentChapter);
        if(_currentChapter >= chapters.Count)
        {
            _currentChapter = 0;
            SceneManager.LoadScene(endingScene); 
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restarts the Treasure Room scene, not the current chapter
        }
    }

    public int GetCurrentChapter()
    {
        return _currentChapter;
    }


    private void EventTest()
    {
        Debug.Log("Event received");
    }

    public static void InvokeNextChapter()
    {
        Events.InvokeNextChapter();
    }


    public void GameOver()
    {
        SceneManager.LoadScene(gameOverScene);
        GetCurrentChapter();
        Debug.Log("Game Over: " + _currentChapter);
    }

}
