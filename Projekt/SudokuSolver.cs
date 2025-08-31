using System;
using System.Linq;

namespace Projekt;

public class SudokuSolver
{
    private int[][] _sudoku;
    private int[][] _sudokuSolutionFinder;
    private Random _rand;
    private int _solutioncount;
    private int _numberOfPossibleBlankSpaces;
    private Caretaker _caretaker;
    private int _maxNumberOfAttempts;
   
    

    public SudokuSolver(int[][] sudokuBoard)
    {
        _maxNumberOfAttempts = 25000;
        _numberOfPossibleBlankSpaces = 0;
        _rand = new Random();
        _sudoku = sudokuBoard.Select(row => row.ToArray()).ToArray();
        _sudokuSolutionFinder = sudokuBoard.Select(row => row.ToArray()).ToArray();
        _caretaker = new Caretaker();
        NumberOfSudokuAvailableBlindSpotsChecker();
    }

    public void DisplayBlankSudoku()
    {
        for (int i = 0; i < 9; i++)
        {
            Console.WriteLine($"{_sudokuSolutionFinder[i][0]}, {_sudokuSolutionFinder[i][1]} , {_sudokuSolutionFinder[i][2]}, {_sudokuSolutionFinder[i][3]}, {_sudokuSolutionFinder[i][4]}, {_sudokuSolutionFinder[i][5]}, {_sudokuSolutionFinder[i][6]}, {_sudokuSolutionFinder[i][7]}, {_sudokuSolutionFinder[i][8]}");
        }
        Console.WriteLine($"Ilosc blank spaces: {_numberOfPossibleBlankSpaces}");
    }

    


    private void NumberOfSudokuAvailableBlindSpotsChecker()
    {
        while (true)
        {
            int[][] tmpSudoku;
            int x;
            int y;
            if (_maxNumberOfAttempts == 0)
            {
                
                Console.WriteLine("Memento Time!!!!!!!!");
                RestoreStateFromMemento(_caretaker.GetLast());
                break;
            }

            for (int i = 0; i < 81; i++)
            {
                _solutioncount = 0;
                do
                {
                    x = _rand.Next(0, 9);
                    y = _rand.Next(0, 9);
                } while (_sudokuSolutionFinder[x][y] == 0);

                tmpSudoku = _sudokuSolutionFinder.Select(row => row.ToArray()).ToArray();
                _sudokuSolutionFinder[x][y] = 0;
                _caretaker.AddMemento(SaveStateToMemento());

                Solve();

                if (_solutioncount != 1)
                {
                    _sudokuSolutionFinder = tmpSudoku;
                    break;
                }

                _numberOfPossibleBlankSpaces += 1;
            }
            
            if (_numberOfPossibleBlankSpaces < 15)
            {
                _numberOfPossibleBlankSpaces = 0;
                _sudokuSolutionFinder = _sudoku.Select(row => row.ToArray()).ToArray();
                _maxNumberOfAttempts--;
                continue;
            }

            break;
        }
    }


    private void Solve()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (_sudokuSolutionFinder[i][j] == 0)
                {
                    for (int k = 1; k <= 9; k++)
                    {
                        if (IsSafe(i, j, k))
                        {
                            _sudokuSolutionFinder[i][j] = k;
                            Solve();
                            _sudokuSolutionFinder[i][j] = 0;
                            if (_solutioncount > 1) return;
                        }
                    }

                    return;
                }
                
            }
        }

        _solutioncount++;
    }
    
    
    
    private bool IsSafe(int row, int col, int number)
    {
        int startingRow = row / 3 * 3;
        int startingCol = col / 3 * 3;
        for (int i = 0; i < 9; i++)
        {
            if (_sudokuSolutionFinder[row][i] == number)
            {
                return false;
            }
            if(_sudokuSolutionFinder[i][col] == number)
            {
                return false;
            }
        }

        for (int i = startingRow; i < startingRow + 3; i++)
        {
            for (int j = startingCol; j < startingCol + 3; j++)
            {
                if (_sudokuSolutionFinder[i][j] == number)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public Memento SaveStateToMemento()
    {
        return new Memento(_sudokuSolutionFinder, _numberOfPossibleBlankSpaces);
    }

    public void RestoreStateFromMemento(Memento memento)
    {
        _sudokuSolutionFinder = memento.GetSudoku();
        _numberOfPossibleBlankSpaces = memento.GetNumberOfPossibleBlankSpaces();
    }

    public int GetNumberOfBlankSpaces()
    {
        return _numberOfPossibleBlankSpaces;
    }

    public int[][] GetSudokuWithBlanks()
    {
        return _sudokuSolutionFinder;
    }

    public int[][] GetSolvedSudoku()
    {
        return _sudoku;
    }
    
    
}