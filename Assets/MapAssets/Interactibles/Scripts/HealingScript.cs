using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingScript : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("A");
            if (PlayerMovementPlatforming.Health >= 3) return;
            Debug.Log("Hello?");
            other.gameObject.GetComponent<PlayerMovementPlatforming>().Heal();
            Destroy(transform.parent.gameObject);
        }
    }
}
