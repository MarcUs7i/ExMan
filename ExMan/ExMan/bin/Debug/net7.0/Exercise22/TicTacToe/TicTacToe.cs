using Avalonia.Media;
using LeoBoard;

namespace TicTacToe;

public static class TicTacToe
{
    private const string Game = "TicTacToe";
    private const int Size = 3;
    private const int MinNum = 1;
    private const int MaxNum = 9;
    private const int MaxTurns = MaxNum;
    private const string X = "X";
    private const string O = "O";
    private static readonly Random random = Random.Shared;

    /// <summary>
    ///     Executes the Tic Tac Toe program
    /// </summary>
    public static async Task Run()
    {
        Console.WriteLine($"{Game}{Environment.NewLine}=========");
        await Board.Initialize("TicTacToe", Size, Size, cellSize: 60, fontSize: 36);

        var playerIndex = random.Next(0, 2); // randomly decide who starts
        
        InitPositions(); // initialize playing field

        int counter = 0;
        int winnerIndex = -1; // either player 1 (0) or player 2 (1) won, -1 indicates a draw
        do
        {
            int position = GetStonePosition(playerIndex);
            int row = GetRowFromPosition(position);
            int col = GetColFromPosition(position, row);
            if (playerIndex == 0)
            {
                Board.SetCellContent(row, col, O, Brushes.Red);
            }
            else
            {
                Board.SetCellContent(row, col, X, Brushes.Green);
            }

            counter++;
            if (counter >= 5) // first turn in which there can be a winner
            {
                winnerIndex = CheckWinner();
            }

            playerIndex = 1 - playerIndex;
        } while (counter < MaxTurns && winnerIndex == -1);

        Console.WriteLine(winnerIndex != -1
            ? $"The player with number {winnerIndex} has won!"
            : "The game ended in a draw");
    }

    /// <summary>
    ///     Checks which player has won the game or if it ended in a draw.
    /// </summary>
    /// <returns>Index of the winning player or -1 if it is a draw</returns>
    public static int CheckWinner()
    {
        // Check rows and columns
        for (int i = 0; i < Size; i++)
        {
            // Check rows
            if (Board.GetCellContent(i, 0) == Board.GetCellContent(i, 1) && Board.GetCellContent(i, 1) == Board.GetCellContent(i, 2))
            {
                if (Board.GetCellContent(i, 0) == X) return 1;
                if (Board.GetCellContent(i, 0) == O) return 0;
            }

            // Check columns
            if (Board.GetCellContent(0, i) == Board.GetCellContent(1, i) && Board.GetCellContent(1, i) == Board.GetCellContent(2, i))
            {
                if (Board.GetCellContent(0, i) == X) return 1;
                if (Board.GetCellContent(0, i) == O) return 0;
            }
        }

        // Check diagonal 1
        if (Board.GetCellContent(0, 0) == Board.GetCellContent(1, 1) && Board.GetCellContent(1, 1) == Board.GetCellContent(2, 2))
        {
            if (Board.GetCellContent(1, 1) == X) return 1;
            if (Board.GetCellContent(1, 1) == O) return 0;
        }
        // Check diagonal 2
        if (Board.GetCellContent(0, 2) == Board.GetCellContent(1, 1) && Board.GetCellContent(1, 1) == Board.GetCellContent(2, 0))
        {
            if (Board.GetCellContent(1, 1) == X) return 1;
            if (Board.GetCellContent(1, 1) == O) return 0;
        }
        
        return -1;
    }
    
    public static string?[,] GetFieldValues()
    {
        string?[,] fieldText = new string? [Size,Size];

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                fieldText[i, j] = Board.GetCellContent(i, j);
            }
        }

        return fieldText;
    }

    /// <summary>
    ///     Based on the supplied row and column (both 0 index based)
    ///     the index (0 for 'O' and 1 for 'X') of the player 'stone' placed in the
    ///     identified cell is returned.
    ///     If the cell is empty or the parameters are out of range (0-2) -1 is returned.
    /// </summary>
    /// <param name="row">Row index of the field</param>
    /// <param name="col">Column index of the field</param>
    /// <returns>Index of the player stone or -1 if invalid/no stone</returns>
    public static int GetPlayerIndexFromCell(int row, int col)
    {
        if (row is < 0 or >= Size || col is < 0 or >= Size) return -1;

        if (Board.GetCellContent(row, col) == O) return 0;
        if (Board.GetCellContent(row, col) == X) return 1;
        
        return -1;
    }

    /// <summary>
    ///     Calculates the col index (0 based) for the supplied position.
    ///     If an invalid position or row is supplied -1 is returned.
    /// </summary>
    /// <param name="position">Position (range 1-9)</param>
    /// <param name="row">Row index (range 0-2)</param>
    /// <returns>The index of the positions column (0 based) if valid; -1 otherwise</returns>
    public static int GetColFromPosition(int position, int row)
    {
        if (!CheckPositionValid(position) || row is < 0 or >= Size)
        {
            return -1;
        }
        
        return (position - 1) % Size;
    }

    /// <summary>
    ///     Calculates the row index (0 based) for the supplied position.
    ///     If an invalid position is supplied -1 is returned.
    /// </summary>
    /// <param name="position">Position (range 1-9)</param>
    /// <returns>The index of the positions row (0 based) if valid; -1 otherwise</returns>
    public static int GetRowFromPosition(int position)
    {
        if (!CheckPositionValid(position)) return -1;
        return 2 - (position - 1) / Size;
    }

    /// <summary>
    ///     The player with the supplied <see cref="playerIndex"/> is asked to enter
    ///     a valid position for placing their next 'stone'.
    ///     A position is only valid if it is within range and still empty.
    ///     Repeats the input until a valid position is entered.
    /// </summary>
    /// <param name="playerIndex">Index of the player whose turn it is</param>
    /// <returns>The selected position (1-9)</returns>
    private static int GetStonePosition(int playerIndex)
    {
        while (true)
        {
            Console.Write($"Player {playerIndex}, Position [{MinNum}-{MaxNum}]: ");
            string userInput = Console.ReadLine()!;
            if (int.TryParse(userInput, out int position))
            {
                return position;
            }
        }
    }

    /// <summary>
    ///     Checks if the supplied position is valid (within the defined range)
    ///     and if that is the case, also if the field position is empty (= neither X nor O).
    /// </summary>
    /// <param name="position">Field position to check (1-9)</param>
    /// <returns>True if the position is valid and empty; false otherwise</returns>
    public static bool CheckPosition(int position)
    {
        int row = GetRowFromPosition(position); 
        int col = GetColFromPosition(position, row);
        if (col == -1) return false;
        
        if (Board.GetCellContent(row, col) is X or O) return false;

        return true;
    }

    /// <summary>
    ///     Checks if the supplied position is valid (within the defined range).
    /// </summary>
    /// <param name="position">Field position to check (1-9)</param>
    /// <returns>True if the position is valid; false otherwise</returns>
    public static bool CheckPositionValid(int position)
    {
        return position is >= MinNum and <= MaxNum;
    }

    /// <summary>
    ///     Initially puts the numbers 1-9 (arranged like on a num block) in the fields
    ///     to indicate which number corresponds to which field.
    /// </summary>
    public static void InitPositions()
    {
        // TODO
        int[] numbers = { 7, 8, 9, 4, 5, 6, 1, 2, 3 };
        int iterations = 0;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Board.SetCellContent(i,j, $"{numbers[iterations]}", Brushes.Gray);
                iterations++;
            }
        }
    }
}