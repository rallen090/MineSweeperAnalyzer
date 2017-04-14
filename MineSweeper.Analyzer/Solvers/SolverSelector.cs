using System;
using MineSweeper.Utilities;

namespace MineSweeper.Solvers
{
    public static class SolverSelector
    {
        public static Func<ISolver> GetSolverFactory(string selectedSolverString)
        {
            switch (selectedSolverString)
            {
                case "LocalCustom":
                    return () => new LocalSolver();
                case "LocalRandom":
                    return () => new RandomSolver();
                case "Python":
                    return () => new RemoteSolver(RemoteAdapter.CreatePythonAdapter());
                case "Java":
                    return () => new RemoteSolver(RemoteAdapter.CreateJavaAdapter());
                case "CSharp":
                    return () => new RemoteSolver(RemoteAdapter.CreateCSharpAdapter());
				case "JavaScript (NodeJS)":
					return () => new RemoteSolver(RemoteAdapter.CreateJavaScriptAdapter());
				default:
                    throw new NotImplementedException($"Solver '{selectedSolverString}' is not implemented");
            }
        }
    }
}
