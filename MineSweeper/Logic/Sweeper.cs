using System;
using System.Collections.Generic;
using System.Linq;
using MineSweeper.Models;

namespace MineSweeper.Logic
{
    public class Sweeper
    {
        public static int MaxX;
        public static int MaxY;

        public static Cell[,] GenerateGrid(int xSize, int ySize, int mineCount)
        {
            var grid = new Cell[ySize, xSize];

            // set mines first
            var random = new Random();
            var mineLocations = GetRandomNumberSet(random, mineCount, min: 0, xMax: xSize - 1, yMax: ySize - 1);
            foreach(var location in mineLocations)
            {
                var xMineIndex = location.Item1;
                var yMineIndex = location.Item2;
                grid[yMineIndex, xMineIndex] = new Cell(xMineIndex, yMineIndex, isMine: true);
            }
            
            // then populate rest of map with adjacent mine values
            for (var y = 0; y < ySize; y++)
            {
                for (var x = 0; x < xSize; x++)
                {
                    // not a mine since it has not been set yet
                    if (grid[y, x] == null)
                    {
                        var adjacentMines = GetAdjacentCells(grid, x, y).Count(c => c != null && c.IsMine);
                        var cell = new Cell(x, y) { Value = adjacentMines };
                        grid[y, x] = cell;
                    }
                }
            }

            return grid;
        }

        public static Cell[,] ExecuteMove(Cell[,] grid, Move move)
        {
            var cell = grid[move.Y, move.X];
            switch (move.MoveType)
            {
                case MoveType.Click:
                    cell.State = CellState.Revealed;
                    if (cell.IsMine)
                    {
                        throw new MineException("You've hit a mine!");
                    }
                    else
                    {
                        RevealEmptyAdjacentCells(grid, move.X, move.Y);
                    }
                    break;
                case MoveType.Flag:
                    cell.State = CellState.Flagged;
                    break;
            }
            return grid;
        }

        public static bool IsComplete(Cell[,] grid, int mineCount)
        {
            var flagCount = 0;
            var hiddenCount = 0;
            foreach (var cell in grid)
            {
                // not complete if there are still hidden cells
                if (cell.State == CellState.Hidden)
                {
                    hiddenCount++;
                }

                if (cell.State == CellState.Flagged)
                {
                    flagCount++;
                }
            }

            return hiddenCount + flagCount <= mineCount;
        }

        private static void RevealEmptyAdjacentCells(Cell[,] grid, int x, int y)
        {
            var adjacentCells = GetAdjacentCells(grid, x, y);
            foreach (var cell in adjacentCells)
            {
                // if you are a hidden and empty cell, then we will reveal you
                if (cell.State != CellState.Revealed && !cell.IsMine && cell.Value == 0)
                {
                    // reveal the cell
                    cell.State = CellState.Revealed;

                    // then recurse
                    RevealEmptyAdjacentCells(grid, cell.X, cell.Y);
                }
            }
        }

        private static List<Cell> GetAdjacentCells(Cell[,] grid, int x, int y)
        {
            var xOptions = new [] { x - 1, x, x + 1 }.Where(v => v >= 0 && v <= MaxX);
            var yOptions = new [] { y - 1, y, y + 1 }.Where(v => v >= 0 && v <= MaxY);
            var allPairs = xOptions.SelectMany(xValue => yOptions.Select(yValue => new { xValue, yValue })).Where(s => s.xValue != x || s.yValue != y);
            return allPairs.Select(p => grid[p.yValue, p.xValue]).ToList();
        } 

        public static ISet<Tuple<int, int>> GetRandomNumberSet(Random random, int count, int min, int xMax, int yMax)
        {
            var set = new HashSet<Tuple<int, int>>();
            while (set.Count < count)
            {
                set.Add(Tuple.Create(random.Next(min, xMax), random.Next(min, yMax)));
            }
            return set;
        }
    }

    public class Move
    {
        public MoveType MoveType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public enum MoveType
    {
        Click = 0,
        Flag = 1
    }

    public class MineException : Exception
    {
        public MineException(string message) : base(message)
        {
        }
    }
}
