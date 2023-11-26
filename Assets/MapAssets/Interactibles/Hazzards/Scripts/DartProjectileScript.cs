using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DartProjectileScript : MonoBehaviour
{
    public float speed = 4f;

    // Update is called once per frame
    void Start()
    {
        SBShuffleBoardScript.OnReturn += DestroyThis;
        StartCoroutine(Lifespan());
    }
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.right * speed;
    }

    private void OnDestroy()
    {
        SBShuffleBoardScript.OnReturn -= DestroyThis;
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("A");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovementPlatforming>().Damage();
        }
        DestroyThis();
    }

    IEnumerator Lifespan()
    {
        yield return new WaitForSeconds(5f);
        DestroyThis();
    }
}
