using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MineSweeper.Analyzer.Utilities
{
	public static class AsyncHelpers
	{
		public static Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
		{
			return task.IsCompleted // fast-path optimization
				? task
				: task.ContinueWith(
					completedTask => completedTask.GetAwaiter().GetResult(),
					cancellationToken,
					TaskContinuationOptions.ExecuteSynchronously,
					TaskScheduler.Default);
		}
	}
}
