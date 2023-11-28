using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePuzzleScript : MonoBehaviour
{

    public GameObject reward;

    private bool triggered = false;
    private bool active = false;
    private float timer = 0;

    private AudioSource aSource;
    public AudioClip error;
    public AudioClip success;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;

            if (timer > 2f)
            {
                if (GreekPuzzleTracker.StepsComplete >= 2)
                {
                    triggered = true;
                    reward.SetActive(true);
                    aSource.PlayOneShot(success);
                } else
                {
                    aSource.PlayOneShot(error);
                }
                timer = 0;
                active = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.CompareTag("Player")){
            active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = false;
            timer = 0;
        }

    }

}
