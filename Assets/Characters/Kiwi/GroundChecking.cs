using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecking : MonoBehaviour
{
    private int collisionCount = 0;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Statue"))
        {
            collisionCount++;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Statue"))
        {
            collisionCount--;
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
