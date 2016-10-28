using System;
using System.Collections.Generic;
using System.Linq;
using MineSweeper.Logic;
using MineSweeper.Models;

namespace MineSweeper.Solvers
{
    public class RandomSolver : ISolver
    {
        private readonly Queue<Move> _moveQueue = new Queue<Move>();
        private readonly Random _random = new Random();

        public Move GetNextMove(Cell[,] grid)
        {
            if (this._moveQueue.Count > 0)
            {
                return this._moveQueue.Dequeue();
            }
            var moves = new List<Move>(grid.Length);
            moves.AddRange(from Cell cell in grid select new Move {MoveType = MoveType.Click, X = cell.X, Y = cell.Y});
            this.Shuffle(moves);
            moves.ForEach(this._moveQueue.Enqueue);
            return this._moveQueue.Dequeue();
        }

        public void Shuffle<T>(IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = this._random.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public void Dispose()
        {
        }
    }
}
