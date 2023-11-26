using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartProjectileScript : MonoBehaviour
{
    public float speed = 4f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
