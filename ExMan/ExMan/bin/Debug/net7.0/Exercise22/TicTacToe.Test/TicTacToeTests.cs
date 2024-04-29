using Avalonia.Media;
using FluentAssertions;
using LeoBoard;
using Xunit;

namespace TicTacToe.Test;

public sealed class TicTacToeTests
{
    public static TheoryData<int[][], int> CheckWinnerData =>
        new()
        {
            { new[] { new[] { 0, 0, 1 }, new[] { 1, 1, 1 }, new[] { 2, 2, 1 } }, 1 },
            { new[] { new[] { 0, 2, 1 }, new[] { 1, 1, 1 }, new[] { 2, 0, 1 } }, 1 },
            { new[] { new[] { 0, 2, 0 }, new[] { 1, 1, 0 }, new[] { 2, 0, 0 } }, 0 },
            { new[] { new[] { 0, 0, 0 }, new[] { 1, 1, 0 }, new[] { 2, 2, 0 } }, 0 },
            { new[] { new[] { 0, 0, 1 }, new[] { 1, 0, 1 }, new[] { 2, 0, 1 } }, 1 },
            { new[] { new[] { 0, 2, 0 }, new[] { 1, 2, 0 }, new[] { 2, 2, 0 } }, 0 },
            { new[] { new[] { 0, 0, 1 }, new[] { 0, 1, 1 }, new[] { 0, 2, 1 } }, 1 },
            { new[] { new[] { 2, 0, 0 }, new[] { 2, 1, 0 }, new[] { 2, 2, 0 } }, 0 },
            { new[] { new[] { 0, 0, 1 }, new[] { 1, 1, 1 }, new[] { 2, 0, 1 } }, -1 },
            { new[] { new[] { 0, 2, 0 }, new[] { 1, 1, 0 }, new[] { 2, 2, 0 } }, -1 },
            { new[] { new[] { 0, 2, 0 }, new[] { 1, 1, 0 }, new[] { 2, 2, 0 } }, -1 },
            {
                new[]
                {
                    new[] { 0, 1, 0 }, new[] { 1, 0, 1 }, new[] { 1, 1, 0 }, new[] { 2, 2, 1 }, new[] { 2, 1, 0 },
                    new[] { 0, 2, 1 }
                },
                0
            },
            {
                new[]
                {
                    new[] { 0, 1, 0 }, new[] { 1, 0, 1 }, new[] { 1, 2, 0 }, new[] { 2, 2, 1 }, new[] { 2, 1, 0 },
                    new[] { 0, 2, 1 }
                },
                -1
            }
        };

    [Theory]
    [MemberData(nameof(CheckWinnerData))]
    public void CheckWinner(int[][] fieldsToSet, int expectedIndex)
    {
        InitBoard(fieldsToSet);

        var winnerIdx = TicTacToe.CheckWinner();

        winnerIdx.Should().Be(expectedIndex);
    }

    [Fact]
    public void GetFieldValues()
    {
        var e = string.Empty;
        string[,] expected =
        {
            { e, "O", e },
            { "O", e, e },
            { e, e, "X" }
        };
        InitBoard();

        string?[,] fieldValues = TicTacToe.GetFieldValues();

        fieldValues.Should()
                   .BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(0, 1, 0)]
    [InlineData(0, 0, -1)]
    [InlineData(2, 1, -1)]
    [InlineData(2, 2, 1)]
    [InlineData(1, 0, 0)]
    public void GetPlayerIndexFromCell(int row, int col, int expectedIndex)
    {
        InitBoard();

        var playerIndex = TicTacToe.GetPlayerIndexFromCell(row, col);

        playerIndex.Should().Be(expectedIndex);
    }

    [Theory]
    [InlineData(9, 0, 2)]
    [InlineData(5, 1, 1)]
    [InlineData(1, 2, 0)]
    [InlineData(-1, 2, -1)]
    [InlineData(1, 3, -1)]
    [InlineData(1, -1, -1)]
    public void GetColFromPosition(int position, int row, int expectedCol)
    {
        var column = TicTacToe.GetColFromPosition(position, row);

        column.Should().Be(expectedCol);
    }

    [Theory]
    [InlineData(7, 0)]
    [InlineData(5, 1)]
    [InlineData(3, 2)]
    [InlineData(10, -1)]
    public void GetRowFromPosition(int position, int expectedRow)
    {
        var row = TicTacToe.GetRowFromPosition(position);

        row.Should().Be(expectedRow);
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(3, true)]
    [InlineData(5, true)]
    [InlineData(7, true)]
    [InlineData(9, true)]
    [InlineData(10, false)]
    public void CheckPositionValid(int position, bool expected)
    {
        var isValid = TicTacToe.CheckPositionValid(position);

        isValid.Should().Be(expected);
    }

    [Fact]
    public void InitPositions()
    {
        Board.InitializeForTest(3, 3);
        TicTacToe.InitPositions();

        int[,] numbers =
        {
            { 7, 8, 9 },
            { 4, 5, 6 },
            { 1, 2, 3 }
        };
        for (var row = 0; row < numbers.GetLength(0); row++)
        {
            for (var col = 0; col < numbers.GetLength(1); col++)
            {
                Board.GetCellContent(row, col).Should().BeEquivalentTo(numbers[row, col].ToString());
            }
        }
    }

    [Theory]
    [InlineData(3, true)]
    [InlineData(1, true)]
    [InlineData(7, true)]
    [InlineData(5, true)]
    [InlineData(0, false)]
    [InlineData(10, false)]
    [InlineData(11, false)]
    [InlineData(-3, false)]
    public void GetPosition_Range(int position, bool expected)
    {
        Board.InitializeForTest(3, 3);

        var posValid = TicTacToe.CheckPosition(position);

        posValid.Should().Be(expected);
    }

    [Theory]
    [InlineData(3, true)]
    [InlineData(1, true)]
    public void GetPosition_Free(int position, bool expected)
    {
        Board.InitializeForTest(3, 3);

        var isFree = TicTacToe.CheckPosition(position);

        isFree.Should().Be(expected);
    }

    [Theory]
    [InlineData(3, false)]
    [InlineData(5, true)]
    [InlineData(7, true)]
    [InlineData(8, false)]
    [InlineData(4, false)]
    [InlineData(6, true)]
    public void GetPosition_Mixed(int position, bool expected)
    {
        Board.InitializeForTest(3, 3);
        int[,] fieldsToSet =
        {
            { 0, 1 },
            { 2, 2 },
            { 1, 0 }
        };
        for (var i = 0; i < fieldsToSet.GetLength(0); i++)
        {
            Board.SetCellContent(fieldsToSet[i, 0], fieldsToSet[i, 1], "X", Brushes.Red);
        }

        var isFree = TicTacToe.CheckPosition(position);

        isFree.Should().Be(expected);
    }

    private static void InitBoard(int[][]? fieldsToSet = null)
    {
        Board.InitializeForTest(3, 3);
        fieldsToSet ??= new[]
        {
            new[] { 0, 1, 0 },
            new[] { 2, 2, 1 },
            new[] { 1, 0, 0 }
        };
        foreach (var cellDef in fieldsToSet)
        {
            Board.SetCellContent(cellDef[0], cellDef[1],
                                 cellDef[2] == 0 ? "O" : "X", Brushes.Red);
        }
    }
}
