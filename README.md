# MineSweeper Solution Analyzer

MineSweeper Solution Analyzer is a native desktop application for analyzing and visualizing programmatic solutions to the game MineSweeper.

![alt tag](https://github.com/rallen090/Solver/blob/master/Content/VisualizerDemo.JPG)

The app allows for Solver implementations in various languages which all pipe into the same analyzer. Trials can be run in batch to analyze performance and accuracy of different Solver algorithms. The app also optionally can visualize algorithms work through trials in real-time.

## Implementing a Solver

The app runs code from implementations of a Solver component. The general API and skeleton are as follows:

```
// any custom code within the Solver is valid
class Solver
{
	// Called by the app for every move that it makes. 
	// Takes in a 2D array of Cells representing a MineSweeper board.
	// Returns the next Move to be made
	Move GetNextMove(Cell[][] grid)
	{
		// implement a solution here...
	}
}
```

Cell and Move are set members of the API which cannot be modified since they are how the app and custom Solvers can communicate. Cell and Move are defined as follows:

```
class Cell
{
	// the X coordinate of the cell on the grid
	int X;
	// the Y coordinate of the cell on the grid
	int Y;
	// the state of the cell (i.e. Hidden, Revealed, or Flagged)
	CellState State;
	// the number value of the cell representing adjacent number of mines (NULL/NONE if the cell is Hidden)
	int? Value;
}

enum CellState
{
	Hidden,
	Revealed,
	Flagged
}

class Move
{
	// the X coordinate of the cell for the move
	int X;
	// the Y coordinate of the cell for the move
	int Y;
	// the type of move
	MoveType MoveType;
}

enum MoveType
{
	Click,
	Flag
}
```

## Language support

### C\# 

Project: MineSweeper.Solver.CSharp
File: Solver.cs
```csharp
	// TODO: implement Solver
    public class Solver
    {
		/// <summary>
		/// Returns a <see cref="Move"/> provided a MineSweeper <paramref name="grid"/>, which is a 2D array of <see cref="Cell"/>s
		/// </summary>
		public Move GetNextMove(Cell[,] grid)
		{
			// solver algorithm here...

			// return next move
			return new Move { MoveType = MoveType.Click, X = 1, Y = 2 };
		}
	}
```

### Java

Project: MineSweeper.Solver.Java
File: Solver.java
```java
	// TODO: implement Solver
	public class Solver {
		/**
		 * Returns a Move given a grid, which is a 2D array of Cell objects
		 */
		public Move GetNextMove(Cell[][] grid)
		{
			// solver algorithm here...
			
			// return next move
			return new Move(MoveType.Click, 1, 2);
		}
	}
```

### Python

Project: MineSweeper.Solver.Python
File: Solver.py
```python
	# TODO: implement Solver
	class Solver(object):
		# returns a Move provided a MineSweeper grid, which is a 2D array of Cells
		def GetNextMove(self, grid):
			# solver algorithm here...

			# return next move
			return Move(MoveType.CLICK, 1, 2);
```

### JavaScript via NodeJS

Project: MineSweeper.Solver.JavaScript
File: Solver.js
```javascript
	module.exports = {
		// returns a Move provided a MineSweeper grid, which is a 2D array of Cells
		getNextMove: function (grid){
			// solver algorithm here...

			// return next move
			return new Move(MoveType.CLICK, /* X */ 1, /* Y */ 2);
		}
	};
```

## Setup

#### Requirements

- [.NET](https://www.microsoft.com/net/download/framework) required to run the Analyzer (4.6)
- [Visual Studio](https://www.visualstudio.com/thank-you-downloading-visual-studio/?sku=community) (recommended for C# solver support)
- [Java](http://www.oracle.com/technetwork/java/javase/downloads/index.html) (required for Java solver support)
- [Python](https://www.python.org/downloads/) (required for Python solver support)
- [NodeJS](https://nodejs.org/en/download/) (required for JavaScript solver support)

#### Manual

- Clone this repository
- Run Analyzer executable (MineSweeper\MineSweeper.Analyzer\bin\Debug\MineSweeper.Analyzer.exe) OR open solution in Visual Studio (MineSweeper\MineSweeper.sln)
- To edit the solvers and run them, you must edit the the appropriate Solver file as described above which corresponds to a particular languages
	- C# (requires compilation)
	- Java (requires compilation)
	- JavaScript with NodeJS (interpreted, no build required)
	- Python (interpreted, no build required)
	
NOTE: feature for compiling solvers from the app itself is in development

#### Installer

In development - adding an installation project which can be downloaded and run to perform a native installation of all required components.

## Release notes
- 1.1.0-alpha Pre-release with all languages supported
- 1.0.0-alpha Pre-release