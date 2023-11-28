using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSoundEffects : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip open;
    public AudioClip slide;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOpen()
    {
        audioSource.PlayOneShot(open);
    }
    public void PlaySlide()
    {
        audioSource.PlayOneShot(slide);
    }
}
