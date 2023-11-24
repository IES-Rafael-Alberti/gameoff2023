using System.Collections;
using System.Collections.Generic;
using SB.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChapter", menuName = "Chapter")]


public class CMChapterSO : ScriptableObject
{

    public string ChapterName;
    public SBBoardScriptableObject ChapterBoard;
}
