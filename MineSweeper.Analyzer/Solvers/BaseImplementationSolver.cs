using System;
using System.Collections.Generic;
using System.Linq;
using MineSweeper.Logic;
using MineSweeper.Models;

namespace MineSweeper.Solvers
{
    public class BaseImplementationSolver : ISolver
    {
        public Move GetNextMove(Cell[,] grid)
        {
	        for (var x = 0; x < grid.GetLength(1); x++)
	        {
		        for (var y = 0; y < grid.GetLength(0); y++)
		        {
			        if (grid[y, x].State == CellState.Revealed)
			        {
				        var value = grid[y, x].Value;
				        var neighbors = this.GetAdjacentCells(grid, x, y);
				        var possibleMineCount = 0;
						var flaggedMineCount = 0;

				        foreach (var neighbor in neighbors)
				        {
					        if (neighbor.State == CellState.Hidden)
					        {
						        possibleMineCount++;
					        }
							else if (neighbor.State == CellState.Flagged)
							{
								flaggedMineCount++;
								possibleMineCount++;
							}
				        }

				        if (possibleMineCount == value)
				        {
					        foreach (var neighbor in neighbors)
					        {
						        if (neighbor.State == CellState.Hidden)
						        {
							        return new Move {MoveType = MoveType.Flag, X = neighbor.X, Y = neighbor.Y};
						        }
					        }
				        }

						if (flaggedMineCount == value && possibleMineCount > flaggedMineCount)
						{
							foreach (var neighbor in neighbors)
							{
								if (neighbor.State == CellState.Hidden)
								{
									return new Move { MoveType = MoveType.Click, X = neighbor.X, Y = neighbor.Y };
								}
							}
						}
					}
		        }
			}

	        for (var x = 0; x < grid.GetLength(1); x++)
	        {
		        for (var y = 0; y < grid.GetLength(0); y++)
		        {
			        var cell = grid[y, x];
			        if (cell.State == CellState.Hidden)
			        {
						return new Move { MoveType = MoveType.Click, X = cell.X, Y = cell.Y };
					}
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

        public void Dispose()
        {
        }
    }
}
