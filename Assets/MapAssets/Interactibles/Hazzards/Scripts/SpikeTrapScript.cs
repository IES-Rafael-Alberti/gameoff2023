using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
    public GameObject damageTrigger;
    public GameObject spike;

    public float upPeriod = 1f;
    public float downPeriod = 3f;

    public AudioClip slice;

    private void Start()
    {
        StartCoroutine(StabLoop());
    }

    IEnumerator StabLoop()
    {
        float timePassed = 0;
        while (true)
        {
            GetComponent<AudioSource>().PlayOneShot(slice);
            while (timePassed < upPeriod) {
                damageTrigger.SetActive(true);
                spike.transform.localPosition = Vector3.MoveTowards(spike.transform.localPosition, Vector3.zero, Time.deltaTime*75f);
                yield return null;
                timePassed += Time.deltaTime;
            }
            timePassed = 0;
            while (timePassed < downPeriod) {
                damageTrigger.SetActive(false);
                spike.transform.localPosition = Vector3.MoveTowards(spike.transform.localPosition, -Vector3.up * 19f, Time.deltaTime * 50f);
                yield return null;
                timePassed += Time.deltaTime;
            }
            timePassed = 0;
        }
    }
}
