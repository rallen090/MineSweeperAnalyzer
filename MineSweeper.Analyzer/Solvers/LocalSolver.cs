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
			var remainingRevealedValues = new List<CellInfo>();
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
				// save off revealed cells which we have not exhausted the flags for yet
				if (relativeValue > 0)
				{
					remainingRevealedValues.Add(new CellInfo
					{
						Cell = cell,
						AdjacentCells = adjacentCells,
						RemainingValue = relativeValue,
						FlagCount = flagCount
					});
				}
			}

			// choose on inference
			var orderedSubsets = remainingRevealedValues.Select(c => new
			{
				cell = c,
				eligibleNeighbors = this.GetAdjacentCells(grid, c.Cell.X, c.Cell.Y).Where(e => e.State == CellState.Hidden).ToList()
			})
			.OrderByDescending(c => c.eligibleNeighbors.Count)
			.ToList();

			for (var i = 0; i < orderedSubsets.Count - 1; i++)
			{
				var current = orderedSubsets[i];
				var currentSet = orderedSubsets[i].eligibleNeighbors;
				var type = MoveType.Click;
				var firstSuperset = orderedSubsets.Skip(1).FirstOrDefault(s =>
				{
					// subset if (a) the super is > in size to the sub, (b) the remaining bomb counts match, and (c) subset is contained in superset
					var isSubset = currentSet.Count < s.eligibleNeighbors.Count
						&& current.cell.RemainingValue <= s.cell.RemainingValue
						//&& current.cell.RemainingValue == s.cell.RemainingValue
						&& currentSet.All(s.eligibleNeighbors.Contains);

					if (current.cell.RemainingValue != s.cell.RemainingValue)
					{
						type = MoveType.Flag;
					}

					return isSubset;
				});
				if (firstSuperset != null)
				{
					var excluded = new HashSet<Tuple<int, int>>(currentSet.Select(p => Tuple.Create(p.X, p.Y)));
					var symmetricDifference = firstSuperset.eligibleNeighbors.Where(p => !excluded.Contains(Tuple.Create(p.X, p.Y))).ToList();

					if (symmetricDifference.Any(s => s.IsMine))
					{
						Console.WriteLine("Symmetric difference incorrect");
					}

					return this.EnqueueExtraMoves(symmetricDifference, type);
				}
			}

			// choose on cell probabilities
			var remainingHiddenCells = grid.Cast<Cell>()
				.Where(cell => cell.State == CellState.Hidden)
				.Select(c => new CellProbability { Cell = c, Probability = 0.0 })
				.ToList();
			foreach (var cell in remainingRevealedValues)
			{
				var hiddenCells = cell.AdjacentCells.Where(h => h.State == CellState.Hidden).ToList();
				var incrementalProbability = (double)cell.RemainingValue / hiddenCells.Count;
				hiddenCells.ForEach(c => remainingHiddenCells.Single(r => r.Cell.X == c.X && r.Cell.Y == c.Y).Probability += incrementalProbability);
			}
			// assign untouched cell probability based on bomb count
			//var remainingBombCount = 10 - grid.Cast<Cell>().Count(c => c.State == CellState.Flagged);
			//var cellsWithoutProbability = remainingHiddenCells.Where(c => c.Probability == null).ToList();
			//var globalProbability = (double)remainingBombCount / cellsWithoutProbability.Count;
			//cellsWithoutProbability.ForEach(c => c.Probability = globalProbability);

			var leastLikelyCellToClick = remainingHiddenCells.OrderBy(r => r.Probability).Select(s => s.Cell).FirstOrDefault();
			if (leastLikelyCellToClick != null)
			{
				return this.EnqueueExtraMoves(new List<Cell> { leastLikelyCellToClick }, MoveType.Click);
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

	    private class CellInfo
	    {
		    public Cell Cell { get; set; }
			public IReadOnlyList<Cell> AdjacentCells { get; set; }
			public int RemainingValue { get; set; }
			public int FlagCount { get; set; }
		}

	    public class CellProbability
	    {
		    public Cell Cell { get; set; }
			public double? Probability { get; set; }
	    }
    }
}
