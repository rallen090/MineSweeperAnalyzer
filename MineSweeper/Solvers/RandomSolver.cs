using System.Collections.Generic;
using MineSweeper.Logic;
using MineSweeper.Models;

namespace MineSweeper.Solvers
{
    public class RandomSolver : ISolver
    {
        private readonly Queue<Move> _moveQueue = new Queue<Move>();

        public Move GetNextMove(Cell[,] grid)
        {
            if (this._moveQueue.Count > 0)
            {
                return this._moveQueue.Dequeue();
            }

            foreach (var cell in grid)
            {
                this._moveQueue.Enqueue(new Move {MoveType = MoveType.Click, X = cell.X, Y = cell.Y});
            }

            //var random = new Random();
            //var moves = Sweeper.GetRandomNumberSet(random, count: 15, min: 0, xMax: grid.GetLength(1), yMax: grid.GetLength(0)).ToList();
            //moves.ForEach(m => this._moveQueue.Enqueue(new Move {MoveType = MoveType.Click, X = m.Item1, Y = m.Item2}));
            return this._moveQueue.Dequeue();
        }

        public void Dispose()
        {
        }
    }
}
