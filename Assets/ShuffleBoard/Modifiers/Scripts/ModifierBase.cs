using SB.Runtime;
using SB.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class ModifierBase : MonoBehaviour
{
    public List<GameObject> targets;
    public Vector2Int position; //row col
    public SBShuffleBoardScript board_controller;
    public virtual void Trigger()
    {

    }

    public virtual void BlockEnters(GameObject block)
    {
        Debug.Log(block);
        targets.Add(block);
    }

    public virtual void BlockExits(GameObject block)
    {
        Debug.Log(block);
        targets.Remove(block);
    }
}
