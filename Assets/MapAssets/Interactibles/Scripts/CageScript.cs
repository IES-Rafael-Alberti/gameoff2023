using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageScript : MonoBehaviour
{
    public float raiseLength = 5f;
    public bool raise = false;

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
        transform.localPosition = Vector3.up * 10f;
        yield return new WaitForSeconds(raiseLength);
        transform.localPosition = Vector3.zero;
        raise = false;
        yield break;
    }
}
