using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatform : MonoBehaviour
{
    private float y;

    // Update is called once per frame
    void FixedUpdate()
    {
        y += Time.deltaTime * 20f;

        if (y > 360f)
        {
            y = 0f;
        }
    transform.localRotation = Quaternion.Euler(-90, y, 0);
    }
}
