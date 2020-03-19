using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;

namespace Sudokuval
{
    class sudokuValidator
    {
        //2D matrix to hold sudoku passed to the class
        public int[,] _sudoku = new int[9, 9];

        //Number of rows. This usually is 9
        public int numRows { get; }
        //Number of columns. This usually is 9
        public int numCols { get;  }

        //Makes a copy of sudoku matrix and print it on screen 
        public sudokuValidator(int[,] sudoku)
        {
            
            _sudoku = (int[,])sudoku.Clone();
            numRows = _sudoku.GetLength(0);
            numCols = _sudoku.GetLength(1);

;            Console.WriteLine("Sudoku:");
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    //if (i=0)
                    Console.Write($"| {_sudoku[i, j]} ");
                    if (j == 8)
                    {
                        Console.WriteLine("┐\n");
                    }
                }
                Console.Write("--------------------------------------\n");
            }

        }

        //Apply th 3 validation rules
        //1.- Each column 0f 9 spaces in the grid must contain all of the digits from 1 to 9 exactly once
        //2.- Each row of 9 spaces in the grid must contain all of the digits 1 to 9 exactly once
        //3.- Each 3x3 square in grid(denoted by the thick black lines) must contain the digits 1 to 9 exactly once
        public bool isValid()
        {
            bool result = true;
            CustomArray<int> myArray = new CustomArray<int>();
            if (!isInRange(_sudoku)) result = false;
           
            for (int i = 0; i <numRows; i++)
            {
                int[] myRow = myArray.GetRow(_sudoku, i);
                if (!inspectRows(myRow, i)) result = false;
            }
            for (int j = 0; j < numCols; j++)
            {
                int[] myCol = myArray.GetColumn(_sudoku, j);
                if (!inspectColumns(myCol, j)) result = false;
            }

            if (!validateSubsudoku()) result = false;

            if(result)
                Console.WriteLine($"This sudoku is good");
            else
                Console.WriteLine($"This sudoku has errors");

            return result;
        }

        //Checks if all cells are numeric and in range (1 to 9)
        public bool isInRange(int[,] sudoku)
        {
            Console.Write($"Checking all Cells in range (1 - 9).....");

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (!Enumerable.Range(1, 10).Contains(sudoku[i, j]))
                    {
                        Console.WriteLine($"...Error!!! Cell [{i},{j}] is out of range (1 - 9)");
                        return false;
                    }
                }
            }
            Console.WriteLine($"...All OK!!");
            return true;

        }

        //Checks if each column in the grid contain all of the digits from 1 to 9 exactly once        
        public bool inspectRows(int[] myRow, int numRow)
        {
            Console.Write($"Checking Row    {numRow} ......");
            for (int i = 0; i < numRows; i++)
            {
                for (int ii = 0; ii < numRows; ii++)
                {
                    if (myRow[i].Equals(myRow[ii]) && ii != i)
                    {
                        if(numRow > -1) Console.WriteLine($"...Error!!! Row {numRow}. [{numRow},{i}] = {myRow[i]} is duplicated in  [{numRow},{ii}]");
                        return false;
                    }
                }

            }
            Console.WriteLine($"...OK!!");
            return true;
        }

        //Checks if each row in the grid contain all of the digits 1 to 9 exactly once        
        public bool inspectColumns(int[] myCol, int numCol)
        {
            if (numCol > -1) Console.Write($"Checking Column {numCol} ......");
            for (int i = 0; i < numCols; i++)
            {
                for (int ii = 0; ii < numCols; ii++)
                {

                    if (myCol[i].Equals(myCol[ii]) && ii != i)
                    {
                        if(numCol > -1) Console.WriteLine($"...Error!!! Col {numCol}. [{i},{numCol}] = {myCol[i]} is duplicated in  [{ii},{numCol}]");
                        return false;
                    }

                }
            }
            if (numCol > -1) Console.WriteLine($"...OK!!");
            return true;
        }

        //Checks if each 3x3 square in grid contain the digits 1 to 9 exactly once
        public bool validateSubsudoku()
        {
            int newCol = 0;
            int newRow = 0;
            int quadrantNum = 0;

            
            while (quadrantNum < numCols)
            {
                Console.WriteLine($"Checking Quadrant {quadrantNum + 1} ......");
                int[] parRow1 = Enumerable.Range(newCol, 3).Select(x => _sudoku[newRow, x]).ToArray();
                Console.WriteLine($"\n   {string.Join("",parRow1)} ");
                int[] parRow2 = Enumerable.Range(newCol, 3).Select(y => _sudoku[newRow + 1, y]).ToArray();
                Console.WriteLine($"   {string.Join("", parRow2)} ");
                int[] parRow3 = Enumerable.Range(newCol, 3).Select(z => _sudoku[newRow + 2, z]).ToArray();
                Console.WriteLine($"   {string.Join("", parRow3)} ");
                int[] parRow  = new int[9];
                parRow1.CopyTo(parRow, 0);
                parRow2.CopyTo(parRow, 3);
                parRow3.CopyTo(parRow, 6);
                if (!inspectColumns(parRow, -1))
                {
                    Console.WriteLine($"Error!!! Quadrant {quadrantNum + 1} has duplicates");
                    return false;
                }
                else Console.WriteLine($".................OK!!");
                if (newCol < 6) newCol += 3;
                else
                {
                    newRow += 3;
                    newCol = 0;
                }
                quadrantNum++;
            }
            //Console.WriteLine($"...OK!!");
            return true;
        }


    }
}
