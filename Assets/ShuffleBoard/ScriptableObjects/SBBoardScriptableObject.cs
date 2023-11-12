using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SB.ScriptableObjects
{
    using SB.Runtime;
    using Utilities.Arrays;
    using Utilities.GridPostions;

    public struct BoardStruct
    {
        public List<Vector2Int> empty_positions;
        public GameObject[,] map_objects;
        public GameObject[,] modifier_objects;

        public BoardStruct(int rows, int cols)
        {
            map_objects = new GameObject[rows, cols];
            modifier_objects = new GameObject[rows, cols];
            empty_positions = new List<Vector2Int>();
        }
    }


    [CreateAssetMenu(fileName = "NewBoardData", menuName = "ShuffleBoard/Board")]
    public class SBBoardScriptableObject : ScriptableObject
    {
        [SerializeField] public int rows;
        [SerializeField] public int cols;
        [HideInInspector]
        [SerializeField] public SBCubeScriptableObject[] boardGrid;
        [SerializeField] public GameObject[] modifierGrid;
        public BoardStruct Spawn(Vector3 grid_center, float grid_spacing, SBShuffleBoardScript controller = null, float zOffset = 0)
        {
            ArrayFlattener<SBCubeScriptableObject> flattener = new ArrayFlattener<SBCubeScriptableObject>();
            SBCubeScriptableObject[,] grid = flattener.Unflatten(boardGrid, rows, cols);
            ArrayFlattener<GameObject> gbFlattener = new ArrayFlattener<GameObject>();
            GameObject[,] gbGrid = gbFlattener.Unflatten(modifierGrid, rows, cols);
            BoardStruct board_data = new BoardStruct(rows, cols);

            for (int row = 0; row < rows; row++)
            {
                float y = GridPositions.CalculateY(row, rows, grid_spacing, grid_center);
                for (int col = 0; col < cols; col++)
                {
                    float x = GridPositions.CalculateX(col, cols, grid_spacing, grid_center);
                    SBCubeScriptableObject source = grid[row, col];
                    if (source != null)
                    {
                        //MaintainEmptySlots
                        if (source.empty)
                        {
                            board_data.empty_positions.Add(new Vector2Int(row, col));
                        }
                        //CreateBlocks
                        GameObject sourceObject = source.map;
                        if (sourceObject != null)
                        {
                            if (source.start)
                            {
                                GameObject kiwi = Resources.Load<GameObject>("Character/kiwi");
                                Instantiate(kiwi, new Vector3(x, y, grid_center.z + zOffset), Quaternion.identity);
                            }
                            board_data.map_objects[row, col] = Instantiate(sourceObject, new Vector3(x, y, grid_center.z + zOffset), Quaternion.identity);
                            CubeScript cScript = board_data.map_objects[row, col].AddComponent<CubeScript>();
                            cScript.rotation = source.rotation;
                            cScript.masked = source.masked;
                            cScript.locked = source.locked;
                            cScript.source_board = controller;
                        }
                    }

                    //CreateModifier
                    GameObject sourceModifier = gbGrid[row, col];
                    if (sourceModifier == null)
                    {
                        continue;
                    }
                    GameObject modifier_object = Instantiate(sourceModifier, new Vector3(x, y, grid_center.z + zOffset + 5), Quaternion.identity);
                    ModifierBase mb_script = modifier_object.GetComponent<ModifierBase>();
                    mb_script.BlockEnters(board_data.map_objects[row, col]);
                    mb_script.board_controller = controller;

                    board_data.modifier_objects[row, col] = modifier_object;

                }
            }
            return board_data;
        }
    }
}
