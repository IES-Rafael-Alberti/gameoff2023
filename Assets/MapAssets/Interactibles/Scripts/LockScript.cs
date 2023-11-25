using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    public delegate void LevelCompleteCallback();
    public static event LevelCompleteCallback OnLevelComplete;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (KeyUIScript.KeyCollected) OnLevelComplete?.Invoke();
        }
    }
}
