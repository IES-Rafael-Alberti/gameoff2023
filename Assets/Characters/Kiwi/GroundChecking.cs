using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecking : MonoBehaviour
{
    private int collisionCount = 0;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            collisionCount++;
            Debug.Log(collisionCount);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            collisionCount--;
            Debug.Log(collisionCount);
        }
    }

    public bool GroundCheck()
    {
        if (collisionCount > 0)
        {
            return true;
        }
        return false;
    }
}
