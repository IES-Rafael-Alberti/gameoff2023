using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Arrays{
    public class ArrayFlattener<T>
    {
        public T[] Flatten(T[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);
            T[] flattenedArray = new T[rows * columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int index = i * columns + j;
                    flattenedArray[index] = array[i, j];
                }
            }

            return flattenedArray;
        }

        public T[,] Unflatten(T[] flattenedArray, int rows, int columns)
        {
            if (flattenedArray.Length != rows * columns)
            {
                Debug.LogError("Flattened array size doesn't match the specified dimensions.");
                return null;
            }

            T[,] array = new T[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int index = i * columns + j;
                    array[i, j] = flattenedArray[index];
                }
            }

            return array;
        }

    }
}
