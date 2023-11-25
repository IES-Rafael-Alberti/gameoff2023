using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUIScript : MonoBehaviour
{
    public static bool KeyCollected = false;
    public Image keyImage;

    private void Start()
    {
        KeyReset();
        KeyScript.OnCollected += ColectKey;
        SBShuffleBoardScript.OnReturn += KeyReset;
    }
    public void ColectKey()
    {
        KeyCollected = true;
        keyImage.color = new Color(1, 1, 1, 1);
    }
    public void KeyReset()
    {
        keyImage.color = new Color(0, 0, 0, 0.2f);
        KeyCollected = false;
    }
}
