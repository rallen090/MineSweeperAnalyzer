using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Rerun(int index)
        {
            var game = this._games[index];
            this.RunOne(game, rerun: true);
        }

        private void RunOne(Game game, bool rerun = false)
        {
            using (var solver = this._solverFactory())
            {
                var grid = !rerun
                    ? Sweeper.GenerateGrid(this._xSize, this._ySize, this._mineCount)
                    : game.Steps.First();

                if (rerun)
                {
                    game.Steps.Clear();
                }
                game.Steps.Add(grid.DeepCopy());

                this.UpdateDisplay(() => this._display.InitializeGrid(game.Steps.First()));

                try
                {
                    do
                    {
                        var move = solver.GetNextMove(grid);
                        grid = Sweeper.ExecuteMove(grid, move);
                        var copy = grid.DeepCopy();
                        game.Steps.Add(copy);

                        // draw the new grid as we move
                        this.UpdateDisplay(() => this._display.RedrawGrid(copy));
                    }
                    while (!Sweeper.IsComplete(grid, this._mineCount));

                    game.Success = true;
                }
                catch (Exception ex)
                {
                    game.Exception = ex;
                    var lastMove = grid.DeepCopy();
                    game.Steps.Add(lastMove);
                    this._display.RedrawGrid(lastMove);
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
