using System;

namespace MineSweeper.Solver.CSharp
{
    public class SolverImplementationException : Exception
    {
        public SolverImplementationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
