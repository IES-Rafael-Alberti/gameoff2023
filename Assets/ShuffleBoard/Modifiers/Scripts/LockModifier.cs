using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockModifier : ModifierBase
{
    public override void BlockEnters(GameObject block)
    {
        block.GetComponent<CubeScript>().locked = true;
    }

    public override void BlockExits(GameObject block)
    {
        block.GetComponent<CubeScript>().locked = false;
    }
}
