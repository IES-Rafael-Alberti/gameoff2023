using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScript : MonoBehaviour
{
    public delegate void LevelCompleteCallback();
    public static event LevelCompleteCallback OnLevelComplete;

    public AudioClip unlock;

    private bool triggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            if (KeyUIScript.KeyCollected)
            {
                triggered = true;
                GetComponent<AudioSource>().PlayOneShot(unlock);
                Invoke("CompleteLevel", unlock.length + 1f);
            }
        }
    }

    private void CompleteLevel()
    {
        OnLevelComplete?.Invoke();
    }
}
