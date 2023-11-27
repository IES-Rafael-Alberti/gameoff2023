using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI;

public class KeyScript : MonoBehaviour
{
    public delegate void KeyCallback();
    public static event KeyCallback OnCollected;

    public AudioClip pickupSound;

    // Start is called before the first frame update
    private GameObject startingEmpty;
    void Start()
    {
        startingEmpty = new GameObject("KeyPosition");
        startingEmpty.transform.position = transform.parent.position;
        startingEmpty.transform.rotation = transform.parent.rotation;
        startingEmpty.transform.parent = transform.parent.parent;

        SBShuffleBoardScript.OnReturn += Reset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            CentralSoundEffect.PlaySound(pickupSound);
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        transform.parent.gameObject.SetActive(true);
        transform.parent.position = startingEmpty.transform.position;
        transform.parent.rotation = startingEmpty.transform.rotation;
    }

    private void OnDestroy()
    {
        SBShuffleBoardScript.OnReturn -= Reset;
    }
}
