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
            switch(direction){
                case "Up":
                    CameraTrackingScript.targetRoom = map[currentPosition.x-1, currentPosition.y];
                    break;
                case "Down":
                    CameraTrackingScript.targetRoom = map[currentPosition.x+1, currentPosition.y];
                    break;
                case "Left":
                    CameraTrackingScript.targetRoom = map[currentPosition.x, currentPosition.y - 1];
                    break;
                case "Right":
                    CameraTrackingScript.targetRoom = map[currentPosition.x, currentPosition.y+1];
                    break;
                case "default":
                    break;
            }
        }
    }
}
