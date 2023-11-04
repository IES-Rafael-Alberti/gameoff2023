using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SB.ScriptableObjects
{
    using Unity.VisualScripting;
    using Utilities.Arrays;
    [CreateAssetMenu(fileName = "NewBoardData", menuName = "ShuffleBoard/Board")]
    public class SBBoardScriptableObject : ScriptableObject
    {
        [SerializeField] public int rows;
        [SerializeField] public int cols;
        [HideInInspector]
        [SerializeField] public SBCubeScriptableObject[] boardGrid;
        public (GameObject[,], List<Vector2Int>) Spawn(Vector3 grid_center, float grid_spacing)
        {
            ArrayFlattener<SBCubeScriptableObject> flattener = new ArrayFlattener<SBCubeScriptableObject>();
            SBCubeScriptableObject[,] grid = flattener.Unflatten(boardGrid, rows, cols);
            GameObject[,] new_objects = new GameObject[rows, cols];

            List<Vector2Int> empty_positions = new List<Vector2Int>();

            for (int row = 0; row < rows; row++)
            {
                float y = (row - (rows - 1) / 2f) * -grid_spacing + grid_center.y;
                for (int col = 0; col < cols; col++)
                {
                    float x = (col - (cols - 1) / 2f) * grid_spacing + grid_center.x;
                    SBCubeScriptableObject source = grid[row, col];
                    if (source == null)
                    {
                        new_objects[row, col] = null;
                        continue;
                    }
                    if (source.empty)
                    {
                        empty_positions.Add(new Vector2Int(row, col));
                    }
                    GameObject sourceObject = source.map;
                    if (sourceObject == null)
                    {
                        continue;
                    }
                    new_objects[row, col] = MonoBehaviour.Instantiate(sourceObject, new Vector3(x, y, grid_center.z), Quaternion.identity);
                }
            }
            return (new_objects, empty_positions);
        }
    }
}
