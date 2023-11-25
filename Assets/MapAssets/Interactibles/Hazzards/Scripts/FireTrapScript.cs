using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireTrapScript : MonoBehaviour
{
    public GameObject DamageTrigger;

    public ParticleSystem p1, p2, p3;

    public float FireTime= 1.5f;
    public float CalmTime = 3f;
    public float WiggleTimeFront = 0.3f;
    public float WiggleTimeBack = 0.1f;

    private void Start()
    {
        StartCoroutine(FlameLoop());
        DamageTrigger.SetActive(false);
    }

    private void ToggleParticleSystemsTo(bool state)
    {
        if (state)
        {
            p1.Play();
            p2.Play();
            p3.Play();
        } else
        {
            p1.Stop();
            p2.Stop();
            p3.Stop();
        }
    }
    IEnumerator FlameLoop()
    {
        while (true)
        {
            ToggleParticleSystemsTo(true);
            yield return new WaitForSeconds(WiggleTimeFront);
            DamageTrigger.SetActive(true);
            yield return new WaitForSeconds(FireTime - WiggleTimeFront - WiggleTimeBack);
            DamageTrigger.SetActive(false);
            yield return new WaitForSeconds(WiggleTimeBack);
            ToggleParticleSystemsTo(false);
            yield return new WaitForSeconds(CalmTime);
        }
    }
}
