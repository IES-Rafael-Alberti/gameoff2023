using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RoomTransitionScript : MonoBehaviour
{
    public string direction;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject target = other.gameObject;
            target.transform.position = transform.position + transform.right * 9f;

            CubeScript parentData = transform.parent.GetComponent<CubeScript>();
            Vector2Int currentPosition = parentData.position;
            GameObject[,] map = parentData.source_board.board_data.map_objects;
            GameObject next_room;
            switch (direction){
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
}
