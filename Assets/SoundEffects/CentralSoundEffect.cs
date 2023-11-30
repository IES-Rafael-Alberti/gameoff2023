using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralSoundEffect : MonoBehaviour
{
    public static AudioSource soundSource;
    public static AudioSource secondChannel;

    public AudioSource secondChannelInput;

    //THIS IS JUST A CHEAT FOR OBJECTS THAT NEED TO DELETE THEMSELVES WHILE PLAYING AUDIO
    public void Start()
    {
        soundSource = GetComponent<AudioSource>();
        secondChannel = secondChannelInput;
    }

    public static void PlaySound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }
    public static void PlaySound2(AudioClip sound)
    {
        secondChannel.PlayOneShot(sound);
    }


}
