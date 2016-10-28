using System;
using System.Collections.Generic;
using System.Linq;
using MineSweeper.Logic;
using MineSweeper.Models;

namespace MineSweeper.Solvers
{
    public class LocalSolver : ISolver
    {
		private readonly Queue<Move> _moves = new Queue<Move>();

		public Move GetNextMove(Cell[,] grid)
		{
			var savedMove = this.DequeueValidSavedMove(grid);
			if (savedMove != null)
			{
				return savedMove;
			}

			// get all revealed values
			var revealedValues = grid.Cast<Cell>()
				.Where(cell => cell.State == CellState.Revealed)
				.ToList();

			// iterate the revealed value, giving priority to smaller values first
			var orderedRevealedValues = revealedValues;
			foreach (var cell in orderedRevealedValues)
			{
				var adjacentCells = this.GetAdjacentCells(grid, cell.X, cell.Y);
				var flagCount = adjacentCells.Count(c => c.State == CellState.Flagged);
				var hidden = adjacentCells.Where(c => c.State == CellState.Hidden).ToList();

				var relativeValue = cell.Value - flagCount;

				// click if the value we have is already accounted for with flags
				if (relativeValue == 0 && hidden.Count > 0 /*&& !recursivelyExplore  cannot test clicks since we don't know values */)
				{
					// click all adjacent cells that are applicable (i.e. not clicked and not flagged)
					return this.EnqueueExtraMoves(hidden, MoveType.Click);
				}
				// flag if the value is not accounted for by adjacent flags
				if (relativeValue == hidden.Count && hidden.Count > 0)
				{
					return this.EnqueueExtraMoves(hidden, MoveType.Flag);
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

	    private Move DequeueValidSavedMove(Cell[,] grid)
	    {
			// process moves we already have planned
			if (this._moves.Count > 0)
			{
				while (this._moves.Count > 0)
				{
					var move = this._moves.Dequeue();

					// make sure the move we make has not been revealed (our last move could have revlealed this by opening up adjacent cells)
					if (grid[move.Y, move.X].State == CellState.Hidden)
					{
						return move;
					}
				}
			}
		    return null;
	    }

		public void Dispose()
        {
        }
    }
}
