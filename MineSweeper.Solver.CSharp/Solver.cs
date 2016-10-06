﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper.Solver.CSharp
{
    public class Solver
    {
        private readonly Queue<Move> _moves = new Queue<Move>();

        public Move GetNextMove(Cell[,] grid, bool recursivelyExplore = false, int? nestLevel = null)
        {
            // process moves we already have planned
            if (this._moves.Count > 0)
            {
                return this._moves.Dequeue();
            }

            // get all revealed values
            var revealedValues = grid.Cast<Cell>()
                .Where(cell => cell.State == CellState.Revealed && cell.Value > 0)
                .ToList();

            // iterate the revealed value, giving priority to smaller values first
            var orderedRevealedValues = revealedValues.OrderBy(v => v.Value);
            var hiddenCells = new HashSet<Cell>();
            foreach (var cell in orderedRevealedValues)
            {
                var adjacentCells = this.GetAdjacentCells(grid, cell.X, cell.Y);
                var flagCount = adjacentCells.Count(c => c.State == CellState.Flagged);
                var hidden = adjacentCells.Where(c => c.State == CellState.Hidden).ToList();

                // save off hidden cells for performance if we recurse
                hidden.ForEach(h => hiddenCells.Add(h));

                var relativeValue = cell.Value - flagCount;

                // if while exploring, we set a flag that then causes a conflict, then bail
                if (recursivelyExplore && relativeValue < 0)
                {
                    throw new Exception("Failed path");
                }

                // click if the value we have is already accounted for with flags
                if (relativeValue == 0 && hidden.Count > 0 && !recursivelyExplore /* cannot test clicks since we don't know values */)
                {
                    // click all adjacent cells that are applicable (i.e. not clicked and not flagged)
                    return this.EnqueueExtraMoves(hidden, MoveType.Click);
                }
                // flag if the value is not accounted for by adjacent flags
                if (relativeValue >= hidden.Count && hidden.Count > 0)
                {
                    return this.EnqueueExtraMoves(hidden, MoveType.Flag);
                }
            }

            // recurse on a subsection and test flags
            if (!nestLevel.HasValue || nestLevel < 2)
            {
                foreach (var hiddenCell in hiddenCells)
                {
                    hiddenCell.State = CellState.Flagged;
                    try
                    {
                        this.GetNextMove(grid, recursivelyExplore: true, nestLevel: nestLevel.HasValue ? nestLevel + 1 : 1);
                    }
                    catch
                    {
                        // if we found a conflict when recursing with the test flag, then we know that this cell can be clicked
                        return new Move { MoveType = MoveType.Click, X = hiddenCell.X, Y = hiddenCell.Y };
                    }
                    hiddenCell.State = CellState.Hidden;
                }
            }
            
            // now just take the first open cell if we couldn't determine an actual good move
            foreach (var cell in grid)
            {
                if (cell.State == CellState.Hidden)
                {
                    return new Move
                    {
                        MoveType = MoveType.Click,
                        X = cell.X,
                        Y = cell.Y
                    };
                }
            }
            return null;
        }

        private List<Cell> GetAdjacentCells(Cell[,] grid, int x, int y)
        {
            var xOptions = new[] { x - 1, x, x + 1 }.Where(v => v >= 0 && v < grid.GetLength(1));
            var yOptions = new[] { y - 1, y, y + 1 }.Where(v => v >= 0 && v < grid.GetLength(0));
            var allPairs = xOptions.SelectMany(xValue => yOptions.Select(yValue => new { xValue, yValue })).Where(s => s.xValue != x || s.yValue != y);
            return allPairs.Select(p => grid[p.yValue, p.xValue]).ToList();
        }

        private Move EnqueueExtraMoves(List<Cell> cells, MoveType type)
        {
            if (cells.Count == 1)
            {
                return new Move
                {
                    MoveType = type,
                    X = cells.Single().X,
                    Y = cells.Single().Y
                };
            }
            var moves = cells.Select(c => new Move
            {
                MoveType = type,
                X = c.X,
                Y = c.Y
            }).ToList();
            moves.Skip(1).ToList().ForEach(this._moves.Enqueue);
            return moves.First();
        }
    }

    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int? Value { get; set; }
        public CellState State { get; set; }
    }

    public enum CellState
    {
        Hidden = 0,
        Flagged = 1,
        Revealed = 2
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
}