using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SB.Editors
{
    using SB.ScriptableObjects;
    using Utilities.Arrays;
    [InitializeOnLoad]
    [CustomEditor(typeof(SBBoardScriptableObject))]
    public class ShuffleBoardEditor : Editor
    {
        BoardStruct board_data;
        ArrayFlattener<SBCubeScriptableObject> cubeFlattener = new ArrayFlattener<SBCubeScriptableObject>();
        ArrayFlattener<GameObject> gbFlattener = new ArrayFlattener<GameObject>();
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDefaultInspector();

            SBBoardScriptableObject gridComponent = (SBBoardScriptableObject)target;

            int rows = gridComponent.rows;
            int cols = gridComponent.cols;

            if (gridComponent.boardGrid == null || gridComponent.modifierGrid == null)
            {
                Debug.Log("MakingGrid");
                gridComponent.boardGrid = new SBCubeScriptableObject[rows* cols];
                gridComponent.modifierGrid = new GameObject[rows * cols];
            }
            else if (gridComponent.boardGrid.Length != rows * cols || gridComponent.modifierGrid.Length != rows * cols)
            {
                Debug.Log("RemakingGrid");
                gridComponent.boardGrid = new SBCubeScriptableObject[rows * cols];
                gridComponent.modifierGrid = new GameObject[rows * cols];
            }

            SBCubeScriptableObject[,] grid = cubeFlattener.Unflatten(gridComponent.boardGrid, rows, cols);
            GameObject[,] modifierGrid = gbFlattener.Unflatten(gridComponent.modifierGrid, rows, cols);

            EditorGUILayout.LabelField("Room Blocks");
            for (int i = 0; i < rows; i++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int j = 0; j < cols; j++)
                {
                    EditorGUILayout.BeginVertical();
                    grid[i, j] = (SBCubeScriptableObject)EditorGUILayout.ObjectField(grid[i, j], typeof(SBCubeScriptableObject), false);
                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.LabelField("Modifier Blocks");
            for (int i = 0; i < rows; i++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int j = 0; j < cols; j++)
                {
                    EditorGUILayout.BeginVertical();
                    modifierGrid[i, j] = (GameObject)EditorGUILayout.ObjectField(modifierGrid[i, j], typeof(GameObject), false);
                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.EndHorizontal();
            }

            gridComponent.boardGrid = cubeFlattener.Flatten(grid);
            gridComponent.modifierGrid = gbFlattener.Flatten(modifierGrid);

            EditorApplication.playModeStateChanged += (state) =>
            {
                if (state == PlayModeStateChange.ExitingEditMode)
                {
                    ClearSpawnedObjects();
                }
            };
            if (GUILayout.Button("Spawn Preview"))
            {
                ClearSpawnedObjects();
                board_data = gridComponent.Spawn(Vector3.zero, 2f);
            }
            if (GUILayout.Button("Save"))
            {
                gridComponent.boardGrid = cubeFlattener.Flatten(grid);
                gridComponent.modifierGrid = gbFlattener.Flatten(modifierGrid);
                SaveChanges(gridComponent);
            }
        }


        private void SaveChanges(SBBoardScriptableObject target)
        {
            // Mark the asset as dirty.
            EditorUtility.SetDirty(target);

            // Save the changes to the asset file.
            AssetDatabase.SaveAssets();

            // Trigger an asset database refresh to make sure changes are saved.
            AssetDatabase.Refresh();

            Debug.Log("Changes saved.");
        }

        private void OnDisable()
        {
            ClearSpawnedObjects();
        }

        private void ClearSpawnedObjects()
        {
            if(board_data.map_objects == null)
            {
                return;
            }
            int rows = board_data.map_objects.GetLength(0);
            int cols = board_data.map_objects.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    GameObject target = board_data.map_objects[row, col];
                    if (target == null)
                    {
                        continue;
                    }
                    DestroyImmediate(target);
                    target = board_data.modifier_objects[row, col];
                    if (target == null)
                    {
                        continue;
                    }
                    DestroyImmediate(target);
                }
            }
        }
    }
}
