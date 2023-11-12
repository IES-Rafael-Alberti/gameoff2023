using SB.Runtime;
using SB.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public class BalconyScript : MonoBehaviour
{
    public GameObject parentCube;

    private PlatformingControls controls;
    private GameObject target;
    private SBShuffleBoardScript controller;

    private void Awake()
    {
        controls = new PlatformingControls();
    }
    private void Start()
    {
        controller = parentCube.GetComponent<CubeScript>().source_board;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
            controls.Player.Use.performed += Activate;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
            controls.Player.Use.performed -= Activate;
        }
    }

    private void Activate(InputAction.CallbackContext context)
    {
        target.SetActive(false);
        controller.TurnOn();
        controls.Player.Use.performed -= Activate;
    }
}
