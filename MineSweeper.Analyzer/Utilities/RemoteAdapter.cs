using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Medallion.Shell;
using MineSweeper.Analyzer.Utilities;

namespace MineSweeper.Utilities
{
    public class RemoteAdapter : IDisposable
    {
        private readonly Command _command;
	    private readonly CommandQueue _commandQueue;

        private RemoteAdapter(Command command)
        {
            this._command = command;
			this._commandQueue = new CommandQueue(this._command);

			this._commandQueue.Start();
        }

        public string SendMessage(string message)
        {
            this._command.StandardInput.WriteLine(message);

	        var nextMessage = this._commandQueue.GetNextMessageAsync().Result;

            if (nextMessage.MessageType == CommandQueue.CommandQueueMessageType.StandardError)
            {
                throw new Exception(nextMessage.Message);
            }

            return nextMessage.Message;
        }

		public void Dispose()
        {
			this._commandQueue.Dispose();
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
