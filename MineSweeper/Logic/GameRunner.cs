using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MineSweeper.Models;
using MineSweeper.Solvers;

namespace MineSweeper.Logic
{
    public class GameRunner
    {
        private readonly int _xSize;
        private readonly int _ySize;
        private readonly int _mineCount;
        private readonly int _trials;
        private readonly List<Game> _games;
        private readonly MineSweeperBoard _display;
        private readonly Func<ISolver> _solverFactory;

        public GameRunner(int xSize, int ySize, int mineCount, int trials, MineSweeperBoard display, Func<ISolver> solverFactory)
        {
            this._xSize = xSize;
            this._ySize = ySize;
            this._mineCount = mineCount;
            this._trials = trials;
            this._games = new List<Game>(trials);
            this._display = display;
            this._solverFactory = solverFactory;
        }

        public void RunAll()
        {
            for (var i = 0; i < this._trials; i++)
            {
                var game = new Game();
                this._games.Add(game);
                this.RunOne(game);
            }
            this.UpdateDisplay(() => this._display.CompleteRunInfo());
        }

        private void RunOne(Game game)
        {
            using (var solver = this._solverFactory())
            {
                var grid = Sweeper.GenerateGrid(this._xSize, this._ySize, this._mineCount);
                game.Steps.Add(grid.DeepCopy());
                this.UpdateDisplay(() => this._display.InitializeGrid(grid));

                try
                {
                    do
                    {
                        var move = solver.GetNextMove(grid);
                        grid = Sweeper.ExecuteMove(grid, move);
                        game.Steps.Add(grid.DeepCopy());

                        // draw the new grid as we move
                        this.UpdateDisplay(() => this._display.RedrawGrid(grid));
                    }
                    while (!Sweeper.IsComplete(grid, this._mineCount));

                    game.Success = true;
                }
                catch (Exception ex)
                {
                    game.Exception = ex;
                    game.Steps.Add(grid.DeepCopy());
                    this.UpdateDisplay(() =>
                    {
                        this._display.LogError(ex.ToString());

                        // add one more step with the board revealed for debugging
                        var revealedGrid = grid.DeepCopy();
                        foreach (var cell in revealedGrid)
                        {
                            cell.State = CellState.Revealed;
                        }
                        game.Steps.Add(revealedGrid);

                        this._display.RedrawGrid(revealedGrid);
                    });
                }
                this.UpdateDisplay(() => this._display.UpdateRunInfo(this._games));
            }
        }

        private void UpdateDisplay(Action action)
        {
            this._display.Invoke((MethodInvoker)delegate { action(); });
        }

        public List<Game> GetGames()
        {
            return this._games;
        } 
    }
}
