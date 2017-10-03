using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Medallion.Shell;
using Medallion.Shell.Streams;

namespace MineSweeper.Analyzer.Utilities
{
	public class CommandQueue : IDisposable
	{
		private readonly Command _command;
		private readonly Queue<Tuple<string, DateTime>> _standardOutputMessages = new Queue<Tuple<string, DateTime>>();
		private readonly Queue<Tuple<string, DateTime>> _standardErrorMessages = new Queue<Tuple<string, DateTime>>();

		private Task _standardOutputReaderTask;
		private Task _standardErrorReaderTask;

		private readonly object _lock = new object();
		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

		public CommandQueue(Command command)
		{
			this._command = command;
		}

		public void Start()
		{
			lock (this._lock)
			{
				if (this._standardOutputReaderTask == null)
				{
					this._standardOutputReaderTask = this.EnqueueStreamAsync(this._command.StandardOutput, this._standardOutputMessages, this._cancellationTokenSource.Token);
				}
				if (this._standardErrorReaderTask == null)
				{
					this._standardErrorReaderTask = this.EnqueueStreamAsync(this._command.StandardError, this._standardErrorMessages, this._cancellationTokenSource.Token);
				}
			}
		}

		public async Task<CommandQueueMessage> GetNextMessageAsync()
		{
			while (!this._cancellationTokenSource.Token.IsCancellationRequested)
			{
				Tuple<string, DateTime> nextMessage = null;
				CommandQueueMessageType type = CommandQueueMessageType.StandardOutput;
				lock (this._standardOutputMessages)
				lock (this._standardErrorMessages)
				{
					var hasBoth = this._standardOutputMessages.Any() && this._standardErrorMessages.Any();
					if (this._standardOutputMessages.Any() || (hasBoth && this._standardOutputMessages.Peek()?.Item2 < this._standardErrorMessages.Peek()?.Item2))
					{
						nextMessage = this._standardOutputMessages.Dequeue();
						type = CommandQueueMessageType.StandardOutput;
					}
					else if (this._standardErrorMessages.Any() || hasBoth)
					{
						nextMessage = this._standardErrorMessages.Dequeue();
						type = CommandQueueMessageType.StandardError;
					}
				}

				// return next valid message
				if (nextMessage != null)
				{
					return new CommandQueueMessage(nextMessage.Item1, nextMessage.Item2, type);
				}

				await Task.Delay(TimeSpan.FromMilliseconds(250), this._cancellationTokenSource.Token);
			}

			return null;
		}

		private async Task EnqueueStreamAsync(ProcessStreamReader reader, Queue<Tuple<string, DateTime>> queue, CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				string message;
				while (string.IsNullOrWhiteSpace(message = await reader.ReadLineAsync().WithCancellation(cancellationToken).ConfigureAwait(false)))
				{
					if (cancellationToken.IsCancellationRequested) { break; }
				}

				lock (queue)
				{
					queue.Enqueue(Tuple.Create(message, DateTime.UtcNow));
				}
			}
		}

		public void Dispose()
		{
			this._cancellationTokenSource.Cancel();
			Task.WhenAll(new[]
			{
				// wrap with continuations to swallow exceptions - closing streams / task canceled exception are likely
				this._standardOutputReaderTask.ContinueWith(t => t.Dispose()),
				this._standardErrorReaderTask.ContinueWith(t => t.Dispose())
			}).Wait();
		}

		public class CommandQueueMessage
		{
			public CommandQueueMessage(string message, DateTime timestamp, CommandQueueMessageType messageType)
			{
				this.Message = message;
				this.TimeStamp = timestamp;
				this.MessageType = messageType;
			}

			public string Message { get; }
			public DateTime TimeStamp { get; }
			public CommandQueueMessageType MessageType { get; }
		}

		public enum CommandQueueMessageType
		{
			StandardOutput = 1,
			StandardError = 2,
		}
	}
}
