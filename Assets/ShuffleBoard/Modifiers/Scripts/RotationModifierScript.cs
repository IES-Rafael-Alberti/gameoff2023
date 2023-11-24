using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
//using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.Enum;


public class RotationModifierScript : ModifierBase
{
    private ShuffleControls controls;
    public float sink_period;
    public float rotation_period;

    private void Awake()
    {
        controls = new ShuffleControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.BoardControls.ClockwiseRotate.performed += RotateClockwise;
        controls.BoardControls.CounterClockwiseRotate.performed += RotateCounterClockwise;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.BoardControls.ClockwiseRotate.performed -= RotateCounterClockwise;
        controls.BoardControls.CounterClockwiseRotate.performed -= RotateCounterClockwise;
    }

    public void RotateClockwise(InputAction.CallbackContext context)
    {
        RotateBlock(true);
    }
    public void RotateCounterClockwise(InputAction.CallbackContext context)
    {
        RotateBlock(false);
    }
    public void RotateBlock(bool clockwise)
    {
        GameObject target = targets[0];
        if (target == null) return;
        CubeScript target_data = target.GetComponent<CubeScript>();
        if (board_controller.MovementAllowed(this)) 
        {
            if (clockwise)
            {
                (_, target_data.rotation) = RotateEnum.RotateClockwise(target_data.rotation);
                StartCoroutine(RotateBlock(target, -90));
                RotateDoorLabels(true);
                return;
            }
            (_, target_data.rotation) = RotateEnum.RotateCounterclockwise(target_data.rotation);
            StartCoroutine(RotateBlock(target, 90));
            RotateDoorLabels(false);
            return;
        }
    }

    IEnumerator RotateBlock(GameObject block, float rotation_amount)
    {
        board_controller.MovementStarted(this);

        float starting_rotation = block.transform.rotation.eulerAngles.z;
        Vector3 starting_position = block.transform.position;
        float time_passed = 0;
        float progress = 0;

        while (progress < 1)
        {
            progress = time_passed / sink_period;
            if (progress > 1) progress = 1;
            block.transform.position = starting_position + new Vector3(0, 0, progress*0.2f);
            yield return null;
            time_passed += Time.deltaTime;
        }

        time_passed = 0;
        progress = 0;
        while (progress < 1)
        {
            progress = time_passed / rotation_period;
            if (progress > 1) progress = 1;
            block.transform.rotation = Quaternion.Euler(0, 0, starting_rotation + progress * rotation_amount);
            yield return null;
            time_passed += Time.deltaTime;
        }

        time_passed = 0;
        progress = 0;
        while (progress < 1)
        {
            progress = time_passed / sink_period;
            if (progress > 1) progress = 1;
            block.transform.position = starting_position + new Vector3(0, 0, (1-progress) * 0.2f);
            yield return null;
            time_passed += Time.deltaTime;
        }


        board_controller.MovementStopped();
        yield break;
    }

    private void RotateDoorLabels(bool clockwise)
    {
        CubeScript cubeInfo = targets[0].GetComponent<CubeScript>();
        bool oldLeft = cubeInfo.doorOnLeft;
        bool oldRight = cubeInfo.doorOnRight;
        bool oldTop = cubeInfo.doorOnTop;
        bool oldBottom = cubeInfo.doorOnBottom;
        if (clockwise)
        {
            cubeInfo.doorOnLeft = oldBottom;
            cubeInfo.doorOnRight = oldTop;
            cubeInfo.doorOnTop = oldLeft;
            cubeInfo.doorOnBottom = oldRight;
            return;
        }
        cubeInfo.doorOnLeft = oldTop;
        cubeInfo.doorOnRight = oldBottom;
        cubeInfo.doorOnTop = oldRight;
        cubeInfo.doorOnBottom = oldLeft;

    }
}
