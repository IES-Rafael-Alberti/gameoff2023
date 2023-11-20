using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace SB.Runtime
{
    using Utilities.Arrays;
    using Utilities.Enum;
    using SB.ScriptableObjects;
    using Utilities.GridPostions;

    enum BoardState
    {
        Off,
        Drop,
        On
    }
    public class SBShuffleBoardScript : MonoBehaviour
    {
        private ShuffleControls controls;

        public SBBoardScriptableObject initialBoardData;
        private BoardStruct board_data;
        public float gridSpacing;
        private int rows;
        private int cols;

        public float swapPeriod;
        public float zOffset = 25;
        public float kiwiOffset = 3.8f;

        public Camera boardCamera;

        private BoardState boardState = BoardState.Off;
        private int moving_elements = 0;
        private ModifierBase movement_source;

        private ArrayFlattener<SBCubeScriptableObject> flattener = new ArrayFlattener<SBCubeScriptableObject>();

        private void Awake()
        {
            controls = new ShuffleControls();
            CreateBoard();
            boardCamera = gameObject.transform.parent.GetComponent<Camera>();
            TurnOff(false);
            //StartCoroutine(AutoNextChapter());
        }
        IEnumerator AutoNextChapter()
        {
            yield return new WaitForSeconds(15f);
            ChapterManager.Events.InvokeNextChapter();
            yield break;
        }
        
        public void TurnOn()
        {
            controls.Enable();
            boardState = BoardState.On;
            boardCamera.enabled = true;
            controls.BoardControls.Move.performed += OnMovementPerformed;
            controls.BoardControls.Exit.performed += Exit;
            StartCoroutine(TurnOnProcess());
        }
        IEnumerator TurnOnProcess()
        {
            yield return new WaitForSeconds(0.2f);
            MovementClear();
            yield break;
        }

        public void Exit(InputAction.CallbackContext context)
        {
            TurnOff(true);
        }

        public void TurnOff(bool dropKiwi)
        {
            controls.Disable();
            boardState = BoardState.Off;
            MovementStarted(null);
            boardCamera.enabled = false;
            controls.BoardControls.Move.performed -= OnMovementPerformed;
            if (dropKiwi)
            {
                boardState = BoardState.Drop;
                GameObject kiwi = Instantiate(
                    Resources.Load<GameObject>("Character/kiwi"), 
                    transform.position + Vector3.forward*kiwiOffset + Vector3.up * gridSpacing * (cols+1)/2f, 
                    Quaternion.identity);
                PlayerMovementPlatforming pmpScript = kiwi.GetComponent<PlayerMovementPlatforming>();
                pmpScript.DropKiwi();
            }
        }

        private void OnDestroy()
        {
            DestroyBoard();
        }

        public void CreateBoard()
        {
            DestroyBoard();
            InitBoard(ChapterManager.Instance != null ? ChapterManager.Instance.GetChapterBoard() : initialBoardData);
        }

        void InitBoard(SBBoardScriptableObject data)
        {
            rows = data.rows;
            cols = data.cols;
            board_data = data.Spawn(gameObject.transform.position, gridSpacing, this, zOffset);
        }
        public void DestroyBoard()
        {
            if (board_data.map_objects == null)
            {
                return;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (board_data.map_objects[i, j] == null)
                    {
                        continue;
                    }
                    Destroy(board_data.map_objects[i, j]);
                }
            }
            board_data.empty_positions = null;
        }

        public bool MovementAllowed(ModifierBase source)
        {
            if (moving_elements > 0)
            {
                if (movement_source == null) return false;
                if (movement_source != source) return true;
            }
            return true;
        }
        public void MovementStarted(ModifierBase source)
        {
            moving_elements++;
            movement_source = source;
        }
        public void MovementStopped()
        {
            moving_elements--;
            if (moving_elements <= 0)
            {
                MovementClear();
            }
        }
        public void MovementClear()
        {
            moving_elements = 0;
            movement_source = null;
        }
        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            Debug.Log(moving_elements);
            Debug.Log(movement_source);
            if (!MovementAllowed(null))
            {
                return;
            }
            MovementStarted(null);
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
            foreach (Vector2Int emptyPosition in board_data.empty_positions)
            {
                //Check if movement is possible here.
                Vector2Int targetIndex = emptyPosition + shift;
                if (targetIndex.x < 0 || targetIndex.y < 0 || targetIndex.x>=rows || targetIndex.y >= cols)
                {
                    continue;
                }
                sourceMovements.Add(emptyPosition);
                targetMovements.Add(targetIndex);
                startingPositions.Add(
                    GridPositions.CalculatePosition(
                        targetIndex, 
                        new Vector2Int(rows, cols),
                        gridSpacing,
                        transform.position,
                        zOffset));
                finishedPositions.Add(
                    GridPositions.CalculatePosition(
                        emptyPosition,
                        new Vector2Int(rows, cols),
                        gridSpacing,
                        transform.position,
                        zOffset)); 
            }

            if (targetMovements.Count == 0)
            {
                MovementStopped();
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
                    GameObject block = board_data.map_objects[indexes.x, indexes.y];
                    block.transform.position = startingPositions[i] * (1f - percentDone) + finishedPositions[i] * percentDone;
                }
                yield return null;
                timePassed += Time.deltaTime;
            }

            for (int i = 0; i < targetMovements.Count; i++)
            {
                Vector2Int targetIndex = targetMovements[i];
                Vector2Int sourceIndex = sourceMovements[i];
                GameObject sourceBlock = board_data.map_objects[sourceIndex.x, sourceIndex.y];
                GameObject targetBlock = board_data.map_objects[targetIndex.x, targetIndex.y];
                //Swaps modifier targets.
                GameObject modifier_source_block = board_data.modifier_objects[sourceIndex.x, sourceIndex.y];
                if (modifier_source_block != null)
                {
                    ModifierBase modifier_source = modifier_source_block.GetComponent<ModifierBase>();
                    if (modifier_source != null)
                    {
                        modifier_source.BlockExits(sourceBlock);
                        modifier_source.BlockEnters(targetBlock);
                    }
                }
                GameObject modifier_target_block = board_data.modifier_objects[targetIndex.x, targetIndex.y];
                if ( modifier_target_block != null)
                {
                    ModifierBase modifier_target = modifier_target_block.GetComponent<ModifierBase>();
                    if (modifier_target != null)
                    {
                        modifier_target.BlockExits(targetBlock);
                        modifier_target.BlockEnters(sourceBlock);
                    }
                }

                //Swaps blocks.
                board_data.map_objects[sourceIndex.x, sourceIndex.y] = targetBlock;
                board_data.map_objects[targetIndex.x, targetIndex.y] = sourceBlock;
                board_data.empty_positions.Remove(sourceIndex);
                board_data.empty_positions.Add(targetIndex);
            }
            MovementStopped();
            yield break;
        }
    }

}
