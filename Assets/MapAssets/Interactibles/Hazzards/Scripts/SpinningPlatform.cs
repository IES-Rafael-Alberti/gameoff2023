using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatform : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.parent = transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            collision.gameObject.transform.parent = null;
        }
    }
}
