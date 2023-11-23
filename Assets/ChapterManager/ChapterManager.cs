using SB.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterManager : MonoBehaviour
{
    // Chapter managing
    [HideInInspector]
    public CMChapterListSO chapterList;

    // State manager
    public static EventManager Events = new EventManager();

    private static ChapterManager _Instance;

    public static ChapterManager Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<ChapterManager>();
                _Instance.name = _Instance.GetType().ToString();
                // chapter manager initialization
                _Instance.chapterList = Resources.Load<CMChapterListSO>("ChapterManager/ChapterList");
                Events.AddListener(EventType.NextChapter, _Instance.OnNextChapter);
                // 
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    private void Awake()
    {

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
        return chapterList.chapters[chapterList.currentChapter].ChapterBoard;
    }

    public void OnNextChapter()
    {
        chapterList.currentChapter++;
        if (chapterList.currentChapter >= chapterList.chapters.Count)
        {
            chapterList.currentChapter = 0;
            //SceneManager.LoadScene(chapterList.menuScene);
            SceneManager.LoadScene(chapterList.endScene);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
