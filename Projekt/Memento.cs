using System.Linq;

namespace Projekt;

public class Memento
{
    private int[][] _sudoku;
    private int _numberOfPossibleBlankSpaces;
    private static int _minNumberOfBlankSpaces = 0;


    public Memento(int[][] sudoku, int numberOfPossibleBlankSpaces)
    {
        _sudoku = sudoku.Select(row => row.ToArray()).ToArray();
        _numberOfPossibleBlankSpaces = numberOfPossibleBlankSpaces;
    }

    public int[][] GetSudoku()
    {
        return _sudoku;
    }

    public int GetNumberOfPossibleBlankSpaces()
    {
        return _numberOfPossibleBlankSpaces;
    }

    public static int GetMinNumberOfBlankSpaces()
    {
        return _minNumberOfBlankSpaces;
    }

    public static void SetMinNumberOfBlankSpaces(int number)
    {
        _minNumberOfBlankSpaces = number;
    }

    public static void ResetMinNumberOfBlankSpaces()
    {
        _minNumberOfBlankSpaces = 0;
    }
}