using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public Button ContinueButton;

    public void Start()
    {
        if (GameData.DoesFileExist()) return;
        ContinueButton.interactable = false;
    }
    public void NewGame()
    {
        SceneManager.LoadScene("TreasureRoom");
    }

    public void Continue()
    {
        GameData.LoadLevel();
        SceneManager.LoadScene("TreasureRoom");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
