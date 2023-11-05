using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Enum;

public class CubeScript : MonoBehaviour
{
    public RotationEnum rotation;
    public bool masked;
    public bool locked;

    CubeScript(RotationEnum rotation_input, bool masked_input, bool locked_input)
    {
        rotation = rotation_input;
        masked = masked_input;
        locked = locked_input;
    }
}
