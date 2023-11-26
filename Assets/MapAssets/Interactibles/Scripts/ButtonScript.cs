using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public delegate void ButtonPress();
    public static event ButtonPress OnButtonPress;
    private void OnCollisionEnter(Collision collision)
    {
        OnButtonPress?.Invoke();
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(transform.parent.up * 4f, ForceMode.Force);
    }
}
