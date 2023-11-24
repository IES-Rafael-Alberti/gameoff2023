using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackingScript : MonoBehaviour
{
    public static GameObject targetRoom;
    public static GameObject targetPlayer;
    private Vector3 startingOffset;

    // Start is called before the first frame update
    void Start()
    {
        startingOffset = transform.localPosition;
        transform.parent = null;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (targetPlayer.GetComponent<PlayerMovementPlatforming>().gliding)
        {
            transform.position = targetPlayer.transform.position + startingOffset * 2f;
            transform.LookAt(targetPlayer.transform.position);
        } else
        {
            transform.position = targetRoom.transform.position + Vector3.back * 35f + Vector3.up * 5;
            transform.LookAt(targetPlayer.transform.position);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        }
    }
}
