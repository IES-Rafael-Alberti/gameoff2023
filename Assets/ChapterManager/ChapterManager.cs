using SB.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterManager : MonoBehaviour
{

    [SerializeField] List<CMChapterSO> chapters = new List<CMChapterSO>();
    private static int _currentChapter;
    private static int _chapterCount;
    private static int _menuScene;
    [SerializeField] GameObject boardPrefab;
    [SerializeField] int menuScene;

    public static ChapterManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Instantiate(boardPrefab);
            Destroy(this);
            return;
        }
        // This part only once in game lifetime
        _currentChapter = 0;
        _chapterCount = chapters.Count;
        _menuScene = menuScene;
        Instance = this;
        Instantiate(boardPrefab);
        /*Método de Inicialización*/
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

    // TODO: remove static
    public static void NextChapter()
    {
        _currentChapter++;
        Debug.Log("Chapter: " + _currentChapter);
        if(_currentChapter>=_chapterCount)
        {
            _currentChapter = 0;
            SceneManager.LoadScene(_menuScene);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
