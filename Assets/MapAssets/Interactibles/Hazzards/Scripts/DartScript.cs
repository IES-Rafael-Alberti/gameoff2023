using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartScript : MonoBehaviour
{

    public GameObject DartPrefab;
    public float FirePeriod = 4f;

    public void Start()
    {
        StartCoroutine(DartSpawnLoop());
    }

    IEnumerator DartSpawnLoop()
    {
        while(true)
        {
            Instantiate(DartPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(FirePeriod);
        }
    }
}
