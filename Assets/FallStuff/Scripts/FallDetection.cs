using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class FallDetection : MonoBehaviour
{
    public float force = 30;
    private bool destroy = false;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(rigidBody.velocity.y) >= force ) 
        {
            destroy = true;
        }
    }

    void TriggerDestroy() { 
        // Play Animation or Something
        Destroy(gameObject);
     }

    void OnCollisionEnter(Collision collision)    { 
        if (!destroy){ return; }
        TriggerDestroy();
    }
}
