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
    public enum Level
    {
        Egyptian,
        Greek,
        Aztec
    }

    public class SBShuffleBoardScript : MonoBehaviour
    {
        public delegate void KiwiReturn();
        public static event KiwiReturn OnReturn;

        private PlatformingControls controls;

        public bool useDirectBoardData = false;
        public SBBoardScriptableObject initialBoardData;
        public BoardStruct board_data;
        public float gridSpacing;
        private int rows;
        private int cols;

        private GameObject doorPrefab;
        private List<GameObject> doors = new List<GameObject>();

        public GameObject transitionPrefab;
        private List<GameObject> transitions = new List<GameObject>();

        private GameObject arrowPrefab;
        private List<GameObject> arrows = new List<GameObject>();

        private GameObject coverPrefab;
        public float coverDepth = -13.3f;

        public float swapPeriod;
        public float zOffset = 25;
        public float kiwiOffset = 3.8f;

        public Camera boardCamera;
        public Camera characterCamera;

        private int moving_elements = 0;
        private ModifierBase movement_source;

        public AudioSource bgMusicPlayer;
        public AudioSource roomSoundEffects;
        public AudioClip roomMove;
        public AudioClip roomRotate;
        public AudioClip error;

        private void Awake()
        {
            controls = new PlatformingControls();
            controls.Enable();
            boardCamera.enabled = false;
            PlayerMovementPlatforming.OnDeath += Die;
        }

        #region Audio
        public void PlayRoomMove()
        {
            roomSoundEffects.PlayOneShot(roomMove);
        }
        public void PlayRoomRotate()
        {
            roomSoundEffects.PlayOneShot(roomRotate);
        }
        public void PlayError()
        {
            roomSoundEffects.PlayOneShot(error);
        }

        #endregion

        #region Loading
        public void LoadLevel(Level chosenLevel)
        {
            switch (chosenLevel)
            {
                case Level.Egyptian:
                    initialBoardData = Resources.Load<SBBoardScriptableObject>("Levels/EgyptianBoard");
                    Debug.Log(initialBoardData);
                    break;
                case Level.Greek:
                    initialBoardData = Resources.Load<SBBoardScriptableObject>("Levels/Greco-RomanBoard");
                    break;
                case Level.Aztec:
                    initialBoardData = Resources.Load<SBBoardScriptableObject>("Levels/AztecBoard");
                    break;
            }
            InstantiateAfterLoad();
        }
        private void InstantiateAfterLoad()
        {
            DestroyBoard();
            doorPrefab = initialBoardData.doorPrefab;
            arrowPrefab = initialBoardData.arrowPrefab;
            coverPrefab = initialBoardData.coverPrefab;
            bgMusicPlayer.clip = initialBoardData.backgroundMusic;
            bgMusicPlayer.Play();
            CreateBoard();
            boardCamera.orthographicSize = board_data.raw_data.cameraProjectionSize;
            TurnOff(false);
        }

        #endregion

        public void ResetBoard(InputAction.CallbackContext context)
        {
            //This was disabled in place of the menu.
            //if (RoomTransitionScript.enabled) ResetBoard();
        }

        public void ResetBoard()
        {
            PlayerMovementPlatforming.Health = 3;
            TurnOff(false);
            DestroyBoard();
            CreateBoard();
        }
        private void Die()
        {
            ResetBoard();
        }

        public void TurnOn()
        {
            boardCamera.enabled = true;
            controls.BoardControls.Move.performed += OnMovementPerformed;
            controls.BoardControls.Return.performed -= Return;
            controls.BoardControls.Return.performed += Exit;
            StartCoroutine(TurnOnProcess());
            DestoryDoors();
            SpawnArrows();
        }
        IEnumerator TurnOnProcess()
        {
            yield return new WaitForSeconds(0.2f);
            MovementClear();
            yield break;
        }

        public void Exit(InputAction.CallbackContext context)
        {
            if(board_data.spawn_point.transform.rotation.eulerAngles.z > 10f)
            {
                PlayError();
                return;
            }
            if (MovementAllowed(null))
            {
                TurnOff(true);
            }
        }

        private void Return(InputAction.CallbackContext context)
        {
            if (RoomTransitionScript.warpEnabled)
            {
                OnReturn?.Invoke();
                TurnOn();
            }
        }

        public void TurnOff(bool dropKiwi)
        {
            MovementStarted(null);
            boardCamera.enabled = false;
            controls.BoardControls.Move.performed -= OnMovementPerformed;
            controls.BoardControls.Exit.performed -= Exit;
            controls.BoardControls.Return.performed += Return;
            CreateDoors();
            if (dropKiwi)
            {
                SpawnKiwi();
            }
            DeleteArrows();
        }

        private void OnDestroy()
        {
            DestroyBoard();
        }

        private void SpawnKiwi()
        {
            GameObject kiwi = Resources.Load<GameObject>("Character/kiwi");
            GameObject player = Instantiate(kiwi, board_data.spawn_point.transform.position, Quaternion.identity);
            CameraTrackingScript.targetPlayer = player;
            CameraTrackingScript.targetRoom = board_data.spawn_point;
        }

        public void CreateBoard()
        {
            RoomTransitionScript.warpEnabled = true;
            DestroyBoard();
            rows = initialBoardData.rows;
            cols = initialBoardData.cols;
            board_data = initialBoardData.Spawn(gameObject.transform.position, gridSpacing, this, zOffset);
            CreateDoors();
            SpawnCovers();
            SpawnKiwi();
        }
        public void DestroyBoard()
        {
            OnReturn?.Invoke();
            if (board_data.grid != null) Destroy(board_data.grid);
            if (board_data.map_objects == null)
            {
                return;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (board_data.modifier_objects[i, j] != null)
                    {
                        Destroy(board_data.modifier_objects[i, j]);
                    }
                    Destroy(board_data.map_objects[i, j]);
                }
            }
            board_data.empty_positions = null;
            DestoryDoors();
            DeleteArrows();
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

        #region Covers/Masking

        public void SpawnArrows()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    GameObject room = board_data.map_objects[row, col];
                    CubeScript roomCS = room.GetComponent<CubeScript>();
                    if (roomCS.doorOnRight)
                    {
                        GameObject newArrow = Instantiate(
                            arrowPrefab, 
                            room.transform.position + new Vector3(23.03f, 0f, coverDepth), 
                            Quaternion.Euler(0f, 0f, 0f)
                            );
                        arrows.Add(newArrow);
                        newArrow.transform.parent = room.transform;
                    }
                    if (roomCS.doorOnLeft)
                    {
                        GameObject newArrow = Instantiate(
                            arrowPrefab,
                            room.transform.position + new Vector3(-23.03f, 0f, coverDepth),
                            Quaternion.Euler(0f, 0f, 180f)
                            );
                        arrows.Add(newArrow);
                        newArrow.transform.parent = room.transform;
                    }
                    if (roomCS.doorOnTop)
                    {
                        GameObject newArrow = Instantiate(
                            arrowPrefab,
                            room.transform.position + new Vector3(0f, 23.03f, coverDepth),
                            Quaternion.Euler(0f, 0f, 90f)
                            );
                        arrows.Add(newArrow);
                        newArrow.transform.parent = room.transform;
                    }
                    if (roomCS.doorOnBottom)
                    {
                        GameObject newArrow = Instantiate(
                            arrowPrefab,
                            room.transform.position + new Vector3(0, -23.03f, coverDepth),
                            Quaternion.Euler(0f, 0f, -90f)
                            );
                        arrows.Add(newArrow);
                        newArrow.transform.parent = room.transform;
                    }
                }
            }
        }

        public void DeleteArrows()
        {
            foreach (GameObject arrow in arrows)
            {
                Destroy(arrow);
            }
            arrows = new List<GameObject>();
        }

        public void SpawnCovers()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    GameObject room = board_data.map_objects[row, col];
                    CubeScript roomCS = room.GetComponent<CubeScript>();
                    if (roomCS.masked)
                    {
                        GameObject newCover = Instantiate(coverPrefab, room.transform.position + Vector3.forward * coverDepth, Quaternion.identity);
                        newCover.transform.parent = room.transform;
                        roomCS.cover = newCover;
                    }
                }
            }
        }

        #endregion

        #region Shuffling
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
                StartCoroutine(SwapBlocks(DirectionEnum.Left));
                return;
            }
            if (input.x < -0.3)
            {
                StartCoroutine(SwapBlocks(DirectionEnum.Right));
                return;
            }
            if (input.y > 0.3)
            {
                StartCoroutine(SwapBlocks(DirectionEnum.Down));
                return;
            }
            if (input.y < -0.3)
            {
                StartCoroutine(SwapBlocks(DirectionEnum.Up));
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
                GameObject targetBlock = board_data.map_objects[targetIndex.x, targetIndex.y];
                CubeScript targetCS = targetBlock.GetComponent<CubeScript>();
                if (targetCS.locked || targetCS.empty)
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
                PlayError();
                MovementStopped();
                yield break;
            }

            PlayRoomMove();

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
                board_data.map_objects[sourceIndex.x, sourceIndex.y].GetComponent<CubeScript>().position = sourceIndex;
                board_data.map_objects[targetIndex.x, targetIndex.y].GetComponent<CubeScript>().position = targetIndex;
                board_data.empty_positions.Remove(sourceIndex);
                board_data.empty_positions.Add(targetIndex);
            }
            MovementStopped();
            yield break;
        }
        #endregion

        #region Walls
        public void CreateDoors()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    GameObject block1 = board_data.map_objects[row, col];
                    CubeScript block1CS = block1.GetComponent<CubeScript>();
                    //rightcheck
                    if (col < cols - 1)
                    {
                        GameObject block2 = board_data.map_objects[row, col+1];
                        CubeScript block2CS = block2.GetComponent<CubeScript>();
                        if (block1CS.doorOnRight)
                        {
                            if(!block2CS.doorOnLeft)CreateDoorRight(block1);
                            else CreateTransitionRight(block1);
                        }
                        if (block2CS.doorOnLeft)
                        {
                            if(!block1CS.doorOnRight)CreateDoorLeft(block2);
                            else CreateTransitionLeft(block2);
                        }
                    }
                    else if (block1CS.doorOnRight) CreateDoorRight(block1);
                    //downcheck
                    if (row < rows - 1)
                    {
                        GameObject block2 = board_data.map_objects[row + 1, col];
                        CubeScript block2CS = block2.GetComponent<CubeScript>();
                        if (block1CS.doorOnBottom)
                        {
                            if (!block2CS.doorOnTop) CreateDoorBottom(block1);
                            else CreateTransitionBottom(block1);
                        }
                        if (block2CS.doorOnTop)
                        {
                            if(!block1CS.doorOnBottom)CreateDoorTop(block2);
                            else CreateTransitionTop(block2);
                        }
                    }
                    else if(block1CS.doorOnBottom) CreateDoorBottom(block1);
                    if (row == 0 && block1CS.doorOnTop) CreateDoorTop(block1);
                    if (col == 0 && block1CS.doorOnLeft) CreateDoorLeft(block1);
                }
            }
        }

        private void CreateTransitionBottom(GameObject targetBlock)
        {
            GameObject transition = Instantiate(
                transitionPrefab,
                targetBlock.transform.position + Vector3.down * 26f,
                Quaternion.Euler(0, 0, -90)
                );
            transitions.Add(transition);
            transition.GetComponent<RoomTransitionScript>().direction = "Down";
            transition.transform.parent = targetBlock.transform;
        }

        private void CreateTransitionTop(GameObject targetBlock)
        {
            GameObject transition = Instantiate(
                transitionPrefab,
                targetBlock.transform.position + Vector3.up * 26f,
                Quaternion.Euler(0, 0, 90)
                );
            transitions.Add(transition);
            transition.GetComponent<RoomTransitionScript>().direction = "Up";
            transition.transform.parent = targetBlock.transform;
        }

        private void CreateTransitionLeft(GameObject targetBlock)
        {
            GameObject transition = Instantiate(
                transitionPrefab,
                targetBlock.transform.position + Vector3.left * 26f,
                Quaternion.Euler(0, 0, -180)
                );
            transitions.Add(transition);
            transition.GetComponent<RoomTransitionScript>().direction = "Left";
            transition.transform.parent = targetBlock.transform;
        }

        private void CreateTransitionRight(GameObject targetBlock)
        {
            GameObject transition = Instantiate(
                transitionPrefab,
                targetBlock.transform.position + Vector3.right * 26f,
                Quaternion.Euler(0, 0, 0)
                );
            transitions.Add(transition);
            transition.GetComponent<RoomTransitionScript>().direction = "Right";
            transition.transform.parent = targetBlock.transform;
        }

        private void CreateDoorTop(GameObject targetBlock)
        {
            float gap_value = 0;
            float parent_rotation = targetBlock.transform.rotation.eulerAngles.z;
            if ((parent_rotation > 45 && parent_rotation < 135) || (parent_rotation < 315 && parent_rotation > 225))
                gap_value = board_data.raw_data.doorGapHorizontal;
            else
                gap_value = board_data.raw_data.doorGapVertical;
            if (targetBlock.GetComponent<CubeScript>().empty) return;
            doors.Add(Instantiate(
                doorPrefab,
                targetBlock.transform.position + Vector3.up * gap_value + Vector3.forward * board_data.raw_data.doorDepth,
                Quaternion.Euler(0, 0, 90)
                ));
        }
        private void CreateDoorBottom(GameObject targetBlock)
        {
            float gap_value = 0;
            float parent_rotation = targetBlock.transform.rotation.eulerAngles.z;
            if ((parent_rotation > 45 && parent_rotation < 135) || (parent_rotation < 315 && parent_rotation > 225))
                gap_value = board_data.raw_data.doorGapHorizontal;
            else
                gap_value = board_data.raw_data.doorGapVertical;
            if (targetBlock.GetComponent<CubeScript>().empty) return;
            doors.Add(Instantiate(
                doorPrefab,
                targetBlock.transform.position + Vector3.down * gap_value + Vector3.forward * board_data.raw_data.doorDepth,
                Quaternion.Euler(0, 0, -90)
                ));
        }
        private void CreateDoorRight(GameObject targetBlock)
        {
            float gap_value = 0;
            float parent_rotation = targetBlock.transform.rotation.eulerAngles.z;
            if ((parent_rotation > 45 && parent_rotation < 135) || (parent_rotation < 315 && parent_rotation > 225))
                gap_value = board_data.raw_data.doorGapVertical;
            else
                gap_value = board_data.raw_data.doorGapHorizontal;
            if (targetBlock.GetComponent<CubeScript>().empty) return;
            doors.Add(Instantiate(
                doorPrefab,
                targetBlock.transform.position + Vector3.right * gap_value + Vector3.forward * board_data.raw_data.doorDepth,
                Quaternion.Euler(0, 0, 0)
                ));
        }
        private void CreateDoorLeft(GameObject targetBlock)
        {
            float gap_value = 0;
            float parent_rotation = targetBlock.transform.rotation.eulerAngles.z;
            if ((parent_rotation > 45 && parent_rotation < 135) || (parent_rotation < 315 && parent_rotation > 225))
                gap_value = board_data.raw_data.doorGapVertical;
            else
                gap_value = board_data.raw_data.doorGapHorizontal;
            if (targetBlock.GetComponent<CubeScript>().empty) return;
            doors.Add(Instantiate(
                doorPrefab,
                targetBlock.transform.position + Vector3.left * gap_value + Vector3.forward * board_data.raw_data.doorDepth,
                Quaternion.Euler(0, 0, -180)
                ));
        }

        private void DestoryDoors()
        {
            foreach (GameObject door in doors)
            {
                Destroy(door);
            }
            doors = new List<GameObject>();
            foreach (GameObject transition in transitions)
            {
                Destroy(transition);
            }
            transitions = new List<GameObject>();
        }
        #endregion

    }

}
