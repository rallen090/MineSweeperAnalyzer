using System;
using MineSweeper.Logic;
using MineSweeper.Models;

namespace MineSweeper.Solvers
{
    public interface ISolver : IDisposable
    {
        Move GetNextMove(Cell[,] grid);
    }
}
