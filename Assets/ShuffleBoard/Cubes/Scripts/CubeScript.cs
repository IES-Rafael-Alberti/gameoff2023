using SB.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameObject cover;

    public Vector2Int position;

    public void EnterRoom()
    {
        if (cover != null)
        {
            StartCoroutine(DissolveCover());
        }
    }

    IEnumerator DissolveCover()
    {
        float timePassed = 0;
        MeshRenderer meshRenderer = cover.GetComponentInChildren<MeshRenderer>();
        Material[] coverMaterials = meshRenderer.materials;

        while (timePassed < 1) {
            foreach (Material mat in coverMaterials)
            {
                if (mat.HasProperty("_DissolveAmount")) mat.SetFloat("_DissolveAmount", timePassed);
                yield return null;
                timePassed += Time.deltaTime;
            }
        }

        Destroy(cover);
        yield break;
    }
}
