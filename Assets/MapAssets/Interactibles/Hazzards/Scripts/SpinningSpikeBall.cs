using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSpikeBall : MonoBehaviour
{
    private float y;
    private float x;

    void Start()
    {
        x = transform.localRotation.eulerAngles.x;
        y = transform.localRotation.eulerAngles.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        y += Time.deltaTime * 20f;

        if (y > 360f)
        {
            y = 0f;
        }
    transform.localRotation = Quaternion.Euler(x, y, 0);
    }
}