using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int currentLevelIdx = 1;
    public static Level currentLevel = Level.Egyptian;
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Save an integer to a binary file
    public static void SaveLevel()
    {
        string fileName = "KiwiSaveData";
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + fileName;

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, currentLevelIdx);
        }

        Debug.Log("Integer saved to: " + path);
    }

    public static bool DoesFileExist()
    {
        string fileName = "KiwiSaveData";
        string path = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(path))
        {
            return true;
        }
        return false;
    }

    // Load an integer from a binary file
    public static void LoadLevel()
    {
        string fileName = "KiwiSaveData";
        string path = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                int loadedValue = (int)formatter.Deserialize(stream);
                Debug.Log("Integer loaded: " + loadedValue);
                currentLevelIdx = loadedValue;
            }
        }
        else
        {
            Debug.LogWarning("Save file not found: " + path);
            currentLevelIdx = 1; // Default value if the file doesn't exist
        }
        IdxToLevel();
    }

    private static void IdxToLevel()
    {
        switch (currentLevelIdx)
        {
            case 1:
                {
                    currentLevel = Level.Egyptian;
                    break;
                }
            case 2:
                {
                    currentLevel = Level.Greek;
                    break;
                }
            case 3:
                {
                    currentLevel = Level.Aztec;
                    break;
                }
            default:
                {
                    currentLevel = Level.Egyptian;
                    break;
                }
        }
    }
}
