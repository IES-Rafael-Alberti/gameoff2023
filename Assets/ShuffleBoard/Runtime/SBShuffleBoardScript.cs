using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace SB.Runtime
{
    using Utilities.Arrays;
    using Utilities.Enum;
    using SB.ScriptableObjects;
    using static UnityEngine.Rendering.DebugUI.Table;
    using Unity.VisualScripting;

    public class SBShuffleBoardScript : MonoBehaviour
    {
        public float gridSpacing;

        private List<Vector2Int> emptyPositions;

        private int rows;
        private int cols;

        public SBBoardScriptableObject initialBoardData;
        private GameObject[,] board;

        private ArrayFlattener<SBCubeScriptableObject> flattener = new ArrayFlattener<SBCubeScriptableObject>();

        public ShuffleControls controls;
        private bool movementEnabled = true;
        public float swapPeriod;

        private void Awake()
        {
            controls = new ShuffleControls();
        }

        private void OnEnable()
        {
            CreateBoard();
            controls.Enable();
            controls.BoardControls.Move.performed += OnMovementPerformed;
        }

        private void OnDisable()
        {
            DestroyBoard();
            controls.Disable();
            controls.BoardControls.Move.performed -= OnMovementPerformed;
        }

        private void OnDestroy()
        {
            DestroyBoard();
        }

        public void CreateBoard()
        {
            DestroyBoard();
            rows = initialBoardData.rows;
            cols = initialBoardData.cols;
            (board, emptyPositions) = initialBoardData.Spawn(gameObject.transform.position, gridSpacing);
        }
        public void DestroyBoard()
        {
            if (board == null)
            {
                return;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (board[i, j] == null)
                    {
                        continue;
                    }
                    Destroy(board[i, j]);
                }
            }
            emptyPositions = null;
        }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            if (!movementEnabled)
            {
                return;
            }
            Vector2 input = context.ReadValue<Vector2>();

            if (input.x > 0.3)
            {
                StartCoroutine(SwapBlocks(DirectionEnum.Right));
                return;
            }
            if (input.x < -0.3)
            {
                StartCoroutine(SwapBlocks(DirectionEnum.Left));
                return;
            }
            if (input.y > 0.3)
            {
                StartCoroutine(SwapBlocks(DirectionEnum.Up));
                return;
            }
            if (input.y < -0.3)
            {
                StartCoroutine(SwapBlocks(DirectionEnum.Down));
                return;
            }
        }
        IEnumerator SwapBlocks(DirectionEnum direction)
        {
            movementEnabled = false;
            Vector2Int shift = Vector2Int.zero;
            switch (direction)
            {
                case DirectionEnum.Left:
                    shift = new Vector2Int(0, -1);
                    break;
                case DirectionEnum.Right:
                    shift = new Vector2Int(0, 1);
                    break;
                case DirectionEnum.Up:
                    shift = new Vector2Int(-1, 0);
                    break;
                case DirectionEnum.Down:
                    shift = new Vector2Int(1, 0);
                    break;
            }

            float timePassed = 0f;
            List<Vector2Int> sourceMovements = new List<Vector2Int>();
            List<Vector2Int> targetMovements = new List<Vector2Int>();
            List<Vector3> startingPositions = new List<Vector3>();
            List<Vector3> finishedPositions = new List<Vector3>();
            foreach (Vector2Int emptyPosition in emptyPositions)
            {
                //Check if movement is possible here.
                Vector2Int targetIndex = emptyPosition + shift;
                if (targetIndex.x < 0 || targetIndex.y < 0 || targetIndex.x>=rows || targetIndex.y >= cols)
                {
                    continue;
                }
                sourceMovements.Add(emptyPosition);
                targetMovements.Add(targetIndex);
                startingPositions.Add(new Vector3(
                        (targetIndex.y - (cols - 1) / 2f) * gridSpacing + transform.position.x,
                        (targetIndex.x - (rows - 1) / 2f) * -gridSpacing + transform.position.y,
                        transform.position.z
                        ));
                finishedPositions.Add(new Vector3(
                        (emptyPosition.y - (cols - 1) / 2f) * gridSpacing + transform.position.x,
                        (emptyPosition.x - (rows - 1) / 2f) * -gridSpacing + transform.position.y,
                        transform.position.z
                        ));
            }

            if (targetMovements.Count == 0)
            {
                movementEnabled = true;
                yield break;
            }

            bool movementFinished = false;
            while (!movementFinished)
            {
                float percentDone = timePassed / swapPeriod;
                if (timePassed > swapPeriod)
                {
                    percentDone = 1f;
                    movementFinished = true;
                }
                for (int i = 0; i < targetMovements.Count; i++)
                {
                    Vector2Int indexes = targetMovements[i];
                    GameObject block = board[indexes.x, indexes.y];
                    block.transform.position = startingPositions[i] * (1f - percentDone) + finishedPositions[i] * percentDone;
                }
                yield return null;
                timePassed += Time.deltaTime;
            }

            for (int i = 0; i < targetMovements.Count; i++)
            {
                Vector2Int targetIndex = targetMovements[i];
                Vector2Int sourceIndex = sourceMovements[i];
                GameObject sourceBlock = board[sourceIndex.x, sourceIndex.y];
                board[sourceIndex.x, sourceIndex.y] = board[targetIndex.x, targetIndex.y];
                board[targetIndex.x, targetIndex.y] = sourceBlock;
                emptyPositions.Remove(sourceIndex);
                emptyPositions.Add(targetIndex);
            }
            movementEnabled = true;
            yield break;
        }
    }

}
