using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RoomTransitionScript : MonoBehaviour
{
    public string direction;
    private static bool enabled = true;
    GameObject target;

    private void Update()
    {
        if (enabled && target != null)
        {
            enabled = false;
            StartCoroutine(Renable());

            float shift_magnitude = 9f;
            if (Mathf.Abs(transform.right.y) > Mathf.Abs(transform.right.x))
            {
                shift_magnitude = 15f;
            }

            Vector3 warpPosition = transform.position + transform.right * shift_magnitude;

            target.transform.position = new Vector3(warpPosition.x, target.transform.position.y, warpPosition.z);

            CubeScript parentData = transform.parent.GetComponent<CubeScript>();
            Vector2Int currentPosition = parentData.position;
            GameObject[,] map = parentData.source_board.board_data.map_objects;
            GameObject next_room;
            switch (direction)
            {
                case "Up":
                    next_room = map[currentPosition.x - 1, currentPosition.y];
                    CameraTrackingScript.targetRoom = next_room;
                    next_room.GetComponent<CubeScript>().EnterRoom();
                    break;
                case "Down":
                    next_room = map[currentPosition.x + 1, currentPosition.y];
                    CameraTrackingScript.targetRoom = next_room;
                    next_room.GetComponent<CubeScript>().EnterRoom();
                    break;
                case "Left":
                    next_room = map[currentPosition.x, currentPosition.y - 1];
                    CameraTrackingScript.targetRoom = next_room;
                    next_room.GetComponent<CubeScript>().EnterRoom();
                    break;
                case "Right":
                    next_room = map[currentPosition.x, currentPosition.y + 1];
                    CameraTrackingScript.targetRoom = next_room;
                    next_room.GetComponent<CubeScript>().EnterRoom();
                    break;
                case "default":
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = null;
        }
    }
    IEnumerator Renable()
    {
        yield return new WaitForSeconds(0.5f);
        enabled = true;
        yield break;
    }
}
