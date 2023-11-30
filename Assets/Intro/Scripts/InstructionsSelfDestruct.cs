using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsSelfDestruct : MonoBehaviour
{
    private void Start()
    {
        if (GameData.currentLevelIdx != 1)
        {
            DestroySelf();
            return;
        }
        LockScript.OnLevelComplete += DestroySelf;
    }

    private void OnDestroy()
    {
        LockScript.OnLevelComplete -= DestroySelf;
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
