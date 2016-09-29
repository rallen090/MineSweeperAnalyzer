using System;
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
            return new RemoteAdapter(Command.Run(@"python", @"C:\dev\MineSweeper\Scripts\InputWrapperPython.py"));
        }

        public static RemoteAdapter CreateJavaAdapter()
        {
            throw new NotImplementedException();
        }

        public static RemoteAdapter CreateCSharpAdapter()
        {
            return new RemoteAdapter(Command.Run(@"C:\dev\MineSweeper\MineSweeper.Solver.CSharp\bin\Debug\MineSweeper.Solver.CSharp.exe"));
        }

        #endregion
    }
}
