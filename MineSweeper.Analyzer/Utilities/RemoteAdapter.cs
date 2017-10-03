using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

			var cancellationSource = new CancellationTokenSource();
	        var readTasks = new List<Task<string>>
	        {
				this.ReadMessageAsync(this._command, cancellationSource.Token),
				this.ReadErrorAsync(this._command, cancellationSource.Token)
			};
	        var firstResponseTask = Task.WhenAny(readTasks).Result;
	        var response = firstResponseTask.Result;
	        var isError = firstResponseTask == readTasks[1];
			cancellationSource.Cancel();

            if (isError)
            {
                throw new Exception(response);
            }

            return response;
        }

	    private async Task<string> ReadMessageAsync(Command command, CancellationToken cancellationToken)
	    {
		    string response;
		    while (string.IsNullOrWhiteSpace(response = await command.StandardOutput.ReadLineAsync().ConfigureAwait(false)))
		    {
			    if (cancellationToken.IsCancellationRequested)
			    {
					cancellationToken.ThrowIfCancellationRequested();
				}
		    }
		    return response;
	    }

		private async Task<string> ReadErrorAsync(Command command, CancellationToken cancellationToken)
		{
			string error;
			while (string.IsNullOrWhiteSpace(error = await command.StandardError.ReadLineAsync().ConfigureAwait(false)))
			{
				if (cancellationToken.IsCancellationRequested)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
			}
			return error;
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
			() => Path.Combine(FileHelpers.FindFolderAbove("MineSweeper.Solver.JavaScript", Directory.GetCurrentDirectory()), "RemoteAdapter.js"));

		#endregion
	}
}
