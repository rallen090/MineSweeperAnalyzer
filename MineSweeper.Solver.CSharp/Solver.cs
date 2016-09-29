using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Solver.CSharp
{
    public class Solver
    {
        //public Move GetNextMove(Cell[,] grid)
        //{
        //    throw new NotImplementedException();
        //}

        private readonly Queue<Move> _moveQueue = new Queue<Move>();

        public Move GetNextMove(Cell[,] grid)
        {
            if (this._moveQueue.Count > 0)
            {
                return this._moveQueue.Dequeue();
            }

            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    this._moveQueue.Enqueue(new Move { MoveType = MoveType.Click, X = i, Y = j });
                }
            }

            return this._moveQueue.Dequeue();
        }
    }

    public class Cell
    {
        public int Value { get; set; }
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
