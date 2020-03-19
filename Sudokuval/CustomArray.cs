using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudokuval
{
    //Generic class to extract columns and rows from main grid
    class CustomArray<T>
    {
        //Extracts columns
        public T[] GetColumn(T[,] matrix, int columnNumber)
        {   
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        //Extracts rows
        public T[] GetRow(T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}
