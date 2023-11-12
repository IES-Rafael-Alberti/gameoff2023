using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

namespace Utilities.GridPostions
{ 
    public static class GridPositions
    {
        public static Vector3 CalculatePosition(Vector2Int gridPosition, Vector2Int gridSize, float grid_spacing, Vector3 grid_center, float zOffset = 0)
        {
            return new Vector3(
                    CalculateX(gridPosition.y, gridSize.y, grid_spacing, grid_center),
                    CalculateY(gridPosition.x, gridSize.x, grid_spacing, grid_center),
                    grid_center.z + zOffset
                );
        }

        public static float CalculateX(int col, int cols, float grid_spacing, Vector3 grid_center)
        {
            return (col - (cols - 1) / 2f) * grid_spacing + grid_center.x;
        }

        public static float CalculateY(int row, int rows, float grid_spacing, Vector3 grid_center)
        {
            return (row - (rows - 1) / 2f) * -grid_spacing + grid_center.y;
        }
    }
}
