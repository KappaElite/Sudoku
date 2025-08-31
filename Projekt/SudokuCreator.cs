using System;
using System.Globalization;
using Avalonia.Platform;
using HarfBuzzSharp;

namespace Projekt;

public class SudokuCreator
{
    private int[][] _sudoku;
    private Random _numberGenerator;
    private int _maxNumberOfBlankSpaces;
    private SudokuSolver _test;

    public SudokuCreator()
    {
        _numberGenerator = new Random();
        _sudoku = new int[9][];
        _sudoku[0] = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        _sudoku[1] = [4, 5, 6, 7, 8, 9, 1, 2, 3];
        _sudoku[2] = [7, 8, 9, 1, 2, 3, 4, 5, 6];
        _sudoku[3] = [2, 3, 1, 5, 6, 4, 8, 9, 7];
        _sudoku[4] = [5, 6, 4, 8, 9, 7, 2, 3, 1];
        _sudoku[5] = [8, 9, 7, 2, 3, 1, 5, 6, 4];
        _sudoku[6] = [3, 1, 2, 6, 4, 5, 9, 7, 8];
        _sudoku[7] = [6, 4, 5, 9, 7, 8, 3, 1, 2];
        _sudoku[8] = [9, 7, 8, 3, 1, 2, 6, 4, 5];
        
        Memento.ResetMinNumberOfBlankSpaces();
        CreateSudoku();
        
    }

    private void CreateSudoku()
    {
        ShuffleNumbers();
        ShuffleRows();
        ShuffleColumns();
        Shuffle3X9Rows();
        Shuffle3X9Columns();
    }

    private void ShuffleNumbers()
    {
        for (int i = 1; i <= 9; i++)
        {
            int randNum = _numberGenerator.Next(1, 10);

            for (int j = 0; j < 9; j++)
            {
                for (int k = 0; k < 9; k++)
                {
                    if (_sudoku[j][k] == i)
                    {
                        _sudoku[j][k] = randNum;
                    }
                    else if (_sudoku[j][k] == randNum)
                    {
                        _sudoku[j][k] = i;
                    }
                }
            }
        }
    }

    private void ShuffleRows()
    {
        int blockNumber;
        for (int i = 0; i < 9; i++)
        {
            int randNum = _numberGenerator.Next(0, 3);
            blockNumber = i / 3;
            (_sudoku[randNum + blockNumber * 3], _sudoku[i]) = (_sudoku[i], _sudoku[randNum + blockNumber * 3]);
        }
    }

    private void ShuffleColumns()
    {
        int blockNumber;
        for (int i = 0; i < 9; i++)
        {
            int randNum = _numberGenerator.Next(0, 3);
            blockNumber = i / 3;
            int tmpValue;
            for (int j = 0; j < 9; j++)
            {
                tmpValue = _sudoku[j][randNum + blockNumber * 3];
                _sudoku[j][randNum + blockNumber * 3] = _sudoku[j][i];
                _sudoku[j][i] = tmpValue;
            }
        }
    }

    private void Shuffle3X9Rows()
    {
        for (int i = 0; i < 3; i++)
        {
            int randNum = _numberGenerator.Next(0, 3);
            
            for (int j = 0; j < 3; j++)
            {
                (_sudoku[i * 3 + j], _sudoku[randNum * 3 + j]) = (_sudoku[randNum * 3 + j], _sudoku[i * 3 + j]);
            }
        }
    }


    private void Shuffle3X9Columns()
    {
        for (int i = 0; i < 3; i++)
        {
            int randNum = _numberGenerator.Next(0, 3);

            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int col1 = i * 3 + j;
                    int col2 = randNum * 3 + j;
                    (_sudoku[k][col1], _sudoku[k][col2]) = (_sudoku[k][col2], _sudoku[k][col1]);
                }
            }
        }
    }
    
    public void DisplaySudoku()
    {
        
        for (int i = 0; i < 9; i++)
        {
            Console.WriteLine($"{_sudoku[i][0]}, {_sudoku[i][1]} , {_sudoku[i][2]}, {_sudoku[i][3]}, {_sudoku[i][4]}, {_sudoku[i][5]}, {_sudoku[i][6]}, {_sudoku[i][7]}, {_sudoku[i][8]}");
        }

        Console.WriteLine("------------------------------------------------------------------------");
        
        _test.DisplayBlankSudoku();
    }


    public int TestSudoku()
    {
        
        _test = new SudokuSolver(_sudoku);
        return _test.GetNumberOfBlankSpaces();
    }


    public int[][] GetSudokuWithBlanks()
    {
        return _test.GetSudokuWithBlanks();
    }

    public int[][] GetSudoku()
    {
        return _sudoku;
    }
}