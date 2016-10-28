using System;
using System.IO;
using System.Linq;

namespace MineSweeper.Utilities
{
	public static class FileHelpers
	{
		public static string FindFolderAbove(string targetFolder, string path)
		{
			var directories = Directory.GetDirectories(path).ToList();
			foreach (var directory in directories)
			{
				if (string.Equals(directory.Split('\\').LastOrDefault(), targetFolder, StringComparison.InvariantCultureIgnoreCase))
				{
					return directory;
				}
			}
			var parentPath = Path.GetFullPath(Path.Combine(path, @"..\"));

			if (Path.GetDirectoryName(parentPath) == null)
			{
				return null;
			}

			return FindFolderAbove(targetFolder, parentPath);
		}
	}
}
