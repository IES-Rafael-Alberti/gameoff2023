using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzle : MonoBehaviour
{
    private bool triggered = false;

    private AudioSource aSource;
    public AudioClip success;
    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        GreekPuzzleTracker.StepsComplete = 0;
    }
    private void OnTriggerExit(Collider other)
    {
        if (triggered) return;
        if(other.CompareTag("Statue")){
            GreekPuzzleTracker.StepsComplete++;
            triggered = true;
            aSource.PlayOneShot(success);
        }
    }
}
