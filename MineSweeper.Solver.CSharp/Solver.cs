using System;
using System.Collections.Generic;
using System.Linq;

namespace MineSweeper.Solver.CSharp
{
	// TODO: implement Solver
    public class Solver
    {
		/// <summary>
		/// Returns a <see cref="Move"/> provided a MineSweeper <paramref name="grid"/>, which is a 2D array of <see cref="Cell"/>s
		/// </summary>
		public Move GetNextMove(Cell[,] grid)
		{
			// solver algorithm here...

			// return next move
			return new Move { MoveType = MoveType.Click, X = 1, Y = 2 };
		}
	}

	#region ---- Models (DO NOT MODIFY) ----

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

	#endregion
}
