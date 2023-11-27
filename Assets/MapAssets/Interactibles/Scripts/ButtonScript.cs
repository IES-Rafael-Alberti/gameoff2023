using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public delegate void ButtonPress();
    public static event ButtonPress OnButtonPress;

    private AudioSource aSource;
    public AudioClip press;
    public AudioClip release;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!aSource.isPlaying)
        {
            aSource.PlayOneShot(press);
        }
        OnButtonPress?.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        aSource.PlayOneShot(release);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(transform.parent.up * 4f, ForceMode.Force);
    }
}
