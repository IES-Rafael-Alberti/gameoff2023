using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChapterList", menuName = "Chapter List")]

public class CMChapterListSO : ScriptableObject
{
    public int currentChapter;
    public List<CMChapterSO> chapters;
    public int menuScene;
    public int endScene;
    public bool firstPlay;
}