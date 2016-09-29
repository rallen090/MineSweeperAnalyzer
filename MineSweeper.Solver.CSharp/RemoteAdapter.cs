using System;
using Newtonsoft.Json;

namespace MineSweeper.Solver.CSharp
{
    public class RemoteAdapter
    {
        private readonly Solver _solver;

        public RemoteAdapter(Solver solver)
        {
            this._solver = solver;
        }

        public void HandleMessages()
        {
            string message;
            while ((message = Console.ReadLine()) != null)
            {
                var grid = JsonConvert.DeserializeObject<Cell[,]>(message);
                Move move;
                try
                {
                    move = this._solver.GetNextMove(grid);
                }
                catch (Exception ex)
                {
                    throw new SolverImplementationException("Error thrown by Solve() implementation!", innerException: ex);
                }
                Console.WriteLine(JsonConvert.SerializeObject(move));
            }
        }
    }
}
