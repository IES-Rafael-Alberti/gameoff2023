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
        GameObject[,] spawned_objects;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDefaultInspector();

            SBBoardScriptableObject gridComponent = (SBBoardScriptableObject)target;
            ArrayFlattener<SBCubeScriptableObject> flattener = new ArrayFlattener<SBCubeScriptableObject>();

            int rows = gridComponent.rows;
            int cols = gridComponent.cols;

            SBCubeScriptableObject[,] grid = flattener.Unflatten(gridComponent.boardGrid, rows, cols);

            EditorGUILayout.LabelField("2D Array Generator");

            if (grid == null)
            {
                Debug.Log("MakingGrid");
                grid = new SBCubeScriptableObject[rows, cols];
            }
            else if (grid.GetLength(0) != rows || grid.GetLength(1) != cols)
            {
                Debug.Log("RemakingGrid");
                grid = new SBCubeScriptableObject[rows, cols];
            }

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

            gridComponent.boardGrid = flattener.Flatten(grid);

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
                (spawned_objects, _) = gridComponent.Spawn(Vector3.zero, 2f);
            }
            if (GUILayout.Button("Save"))
            {
                gridComponent.boardGrid = flattener.Flatten(grid);
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
            if(spawned_objects == null)
            {
                return;
            }
            int rows = spawned_objects.GetLength(0);
            int cols = spawned_objects.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    GameObject target = spawned_objects[row, col];
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
