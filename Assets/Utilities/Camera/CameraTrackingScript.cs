using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackingScript : MonoBehaviour
{
    private Vector3 startingOffset;

    // Start is called before the first frame update
    void Start()
    {
        startingOffset = transform.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distance_multiplier = 1;
        if (transform.parent.GetComponent<PlayerMovementPlatforming>().gliding) distance_multiplier = 2;
        transform.position = transform.parent.position + startingOffset * distance_multiplier;
        transform.LookAt(transform.parent.position);
    }
}
