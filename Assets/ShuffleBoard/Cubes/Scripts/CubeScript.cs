using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Enum;

public class CubeScript : MonoBehaviour
{
    public RotationEnum rotation;
    public bool masked;
    public bool locked;
    public SBShuffleBoardScript source_board;
    public bool empty;

    public bool doorOnLeft;
    public bool doorOnRight;
    public bool doorOnTop;
    public bool doorOnBottom;

    public Vector2Int position;
}
