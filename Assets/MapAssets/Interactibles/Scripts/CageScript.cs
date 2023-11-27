using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageScript : MonoBehaviour
{
    public float raiseLength = 5f;
    public bool raise = false;

    private AudioSource aSource;
    public AudioClip countdown;
    public AudioClip close;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
        aSource.clip = countdown;
        aSource.Stop();
    }

    private void Start()
    {
        ButtonScript.OnButtonPress += RaiseCage;
    }
    private void RaiseCage()
    {
        if (!raise)
        {
            raise = true;
            StartCoroutine(RaiseProcess());
        }
    }
    IEnumerator RaiseProcess()
    {
        aSource.pitch = 1f;
        aSource.Play();
        transform.localPosition = Vector3.up * 10f;
        yield return new WaitForSeconds(raiseLength - 2);
        aSource.pitch = 1.5f;
        yield return new WaitForSeconds(2);
        transform.localPosition = Vector3.zero;
        raise = false;
        aSource.Stop();
        aSource.PlayOneShot(close);
        yield break;
    }
}
