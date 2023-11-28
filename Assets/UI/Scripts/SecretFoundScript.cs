using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretFoundScript : MonoBehaviour
{
    public GameObject SecretFoundImage;

    public GameObject[] sImages;

    void Start()
    {
        LockScript.OnLevelComplete += Clear;
        GreekPuzzleTracker.OnStepComplete += RebuildArray;
        
    }

    private void RebuildArray(int stepsComplete)
    {
        if (sImages != null)
        {
            foreach (GameObject image in sImages)
            {
                Destroy(image);
            }
        }
        if (stepsComplete == 0) return;
        sImages = new GameObject[stepsComplete];
        for (int i = 0; i < stepsComplete; i++)
        {
            GameObject newSecretImage = Instantiate(
                SecretFoundImage,
                transform.position + Vector3.right * i * 25,
                Quaternion.identity);
            newSecretImage.transform.parent = transform;
            sImages[i] = newSecretImage;
        }

    }

    private void Clear()
    {
        GreekPuzzleTracker.StepsComplete = 0;
    }
}
