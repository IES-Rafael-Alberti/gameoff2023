using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralSoundEffect : MonoBehaviour
{
    public static AudioSource soundSource;

    //THIS IS JUST A CHEAT FOR OBJECTS THAT NEED TO DELETE THEMSELVES WHILE PLAYING AUDIO
    public void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }


}
