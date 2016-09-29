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
            var input = JsonConvert.SerializeObject(grid);
            var output = this._adapter.SendMessage(input);

            Move move;
            if(!TryParseMove(output, out move))
            {
                throw new Exception($"Could not parse adapter response: '{output}' received from sending '{input}'");
            }
            return move;
        } 

        private bool TryParseMove(string line, out Move move)
        {
            try
            {
                // move format is [X, Y, MoveType]
                move = JsonConvert.DeserializeObject<Move>(line);
                return true;
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
