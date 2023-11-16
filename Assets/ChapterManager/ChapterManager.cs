using SB.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterManager : MonoBehaviour
{

    // These atributes are static
    public static ChapterManager Instance;
    public static EventManager Events;

    [SerializeField] List<CMChapterSO> chapters = new List<CMChapterSO>();
    [SerializeField] GameObject boardPrefab;
    [SerializeField] int menuScene;

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
            SceneManager.LoadScene(menuScene);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void EventTest()
    {
        Debug.Log("Event received");
    }

    public static void InvokeNextChapter()
    {
        Events.InvokeNextChapter();
    }

}
