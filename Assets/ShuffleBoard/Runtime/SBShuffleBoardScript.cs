using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace SB.Runtime
{
    using Utilities.Arrays;
    using Utilities.Enum;
    using SB.ScriptableObjects;

    public class SBShuffleBoardScript : MonoBehaviour
    {
        public float gridSpacing;

        private BoardStruct board_data;

        private int rows;
        private int cols;

        public SBBoardScriptableObject initialBoardData;

        private ArrayFlattener<SBCubeScriptableObject> flattener = new ArrayFlattener<SBCubeScriptableObject>();

        public ShuffleControls controls;
        [HideInInspector]
        public int moving_elements = 0;
        public ModifierBase movement_source;
        public float swapPeriod;

        private void Awake()
        {
            controls = new ShuffleControls();
        }

        IEnumerator WaitChapter()
        {
            yield return new WaitForSeconds(0.1f);
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

        public void CreateBoard() {
            DestroyBoard();
            // I can use this component inside/outside chapter system
            //InitBoard(ChapterManager.Instance.GetChapterBoard());
            InitBoard(ChapterManager.Instance != null ? ChapterManager.Instance.GetChapterBoard() : initialBoardData);
        }

        void InitBoard(SBBoardScriptableObject data)
        {
            rows = data.rows;
            cols = data.cols;
            board_data = data.Spawn(gameObject.transform.position, gridSpacing, this);
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
        }
        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
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
