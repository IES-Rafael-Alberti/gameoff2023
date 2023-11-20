using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB.ScriptableObjects
{
    using Utilities.Enum;
    using System;

    [Serializable]
    [CreateAssetMenu(fileName = "NewCubeData", menuName = "ShuffleBoard/Cube")]
    public class SBCubeScriptableObject : ScriptableObject
    {
        [SerializeField] public GameObject map;
        [SerializeField] public bool start;
        [SerializeField] public RotationEnum rotation;
        [SerializeField] public bool masked;
        [SerializeField] public bool locked;
        [SerializeField] public bool empty;


        [SerializeField] public bool doorOnLeft;
        [SerializeField] public bool doorOnRight;
        [SerializeField] public bool doorOnTop;
        [SerializeField] public bool doorOnBottom;
    }
}
