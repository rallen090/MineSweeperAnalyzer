using System;
using MineSweeper.Logic;
using MineSweeper.Models;
using MineSweeper.Utilities;
using Newtonsoft.Json;

namespace MineSweeper.Solvers
{
    public class RemoteSolver : ISolver
    {
        private readonly RemoteAdapter _adapter;

        public RemoteSolver(RemoteAdapter adapter)
        {
            this._adapter = adapter;
        }

        public Move GetNextMove(Cell[,] grid)
        {
            var input = JsonConvert.SerializeObject(this.CopyToRemoteCellGrid(grid));
            var output = this._adapter.SendMessage(input);

            Move move;
            if(!TryParseMove(output, out move))
            {
                throw new Exception($"Could not parse adapter response: '{output}' received from sending '{input}'");
            }
            return move;
        }

        private RemoteCell[,] CopyToRemoteCellGrid(Cell[,] grid)
        {
            var newGrid = new RemoteCell[grid.GetLength(0), grid.GetLength(1)];
            for (var y = 0; y < grid.GetLength(0); y++)
            {
                for (var x = 0; x < grid.GetLength(1); x++)
                {
                    newGrid[y, x] = new RemoteCell
                    {
                        State = grid[y, x].State,
                        X = grid[y, x].X,
                        Y = grid[y, x].Y,
                        Value = grid[y, x].State == CellState.Revealed ? grid[y, x].Value : default(int?)
                    };
                }
            }
            return newGrid;
        }

        private bool TryParseMove(string line, out Move move)
        {
            try
            {
                // move format is [X, Y, MoveType]
                move = JsonConvert.DeserializeObject<Move>(line);

				// validate enum value since deserializing allows for the enum value to not be bounded
                return Enum.IsDefined(typeof(MoveType), move.MoveType);
            }
            catch
            {
                move = null;
                return false;
            }
        }

        public void Dispose()
        {
            this._adapter.Dispose();
        }
    }
}
