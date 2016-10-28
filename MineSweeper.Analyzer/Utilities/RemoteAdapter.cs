using System;
using System.IO;
using System.Linq;
using Medallion.Shell;

namespace MineSweeper.Utilities
{
    public class RemoteAdapter : IDisposable
    {
        private readonly Command _command;

        private RemoteAdapter(Command command)
        {
            this._command = command;
        }

        public string SendMessage(string message)
        {
            this._command.StandardInput.WriteLine(message);
            string response = null;
            string error = null;
            while (string.IsNullOrWhiteSpace(response = this._command.StandardOutput.ReadLine()) && string.IsNullOrWhiteSpace(error = this._command.StandardError.ReadLine()))
            {
                // wait for valid response or error
            }

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }

            return response;
        }

        public void Dispose()
        {
            this._command.Kill();
        }

        #region ---- Adapter Types ----

        public static RemoteAdapter CreatePythonAdapter()
        {
            return new RemoteAdapter(Command.Run(@"python", PythonSolverPath.Value));
        }

        public static RemoteAdapter CreateJavaAdapter()
        {
			return new RemoteAdapter(Command.Run("java", JavaSolverPath.Value));
		}

        public static RemoteAdapter CreateCSharpAdapter()
        {
            return new RemoteAdapter(Command.Run(CSharpSolverPath.Value));
        }

		public static RemoteAdapter CreateJavaScriptAdapter()
		{
			return new RemoteAdapter(Command.Run("node", JavaScriptSolverPath.Value));
		}

		#endregion

		#region ---- Folder Locations ----

		private static readonly Lazy<string> PythonSolverPath = new Lazy<string>(
			() => Path.Combine(FileHelpers.FindFolderAbove("MineSweeper.Solver.Python", Directory.GetCurrentDirectory()), "RemoteAdapter.py"));
		private static readonly Lazy<string> JavaSolverPath = new Lazy<string>(
			() => Path.Combine(FileHelpers.FindFolderAbove("MineSweeper.Solver.Java", Directory.GetCurrentDirectory()), @"bin\RemoteSolver.class"));
		private static readonly Lazy<string> CSharpSolverPath = new Lazy<string>(
			() => Path.Combine(FileHelpers.FindFolderAbove("MineSweeper.Solver.CSharp", Directory.GetCurrentDirectory()), @"bin\Debug\MineSweeper.Solver.CSharp.exe"));
		private static readonly Lazy<string> JavaScriptSolverPath = new Lazy<string>(
			() => Path.Combine(FileHelpers.FindFolderAbove("MineSweeper.Solver.JavaScript", Directory.GetCurrentDirectory()), "RemoteSolver.js"));

		#endregion
	}
}
