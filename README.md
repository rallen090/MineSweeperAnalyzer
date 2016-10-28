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
```
// TODO: add here
```

### Java

Project: MineSweeper.Solver.Java
File: Solver.java
```
// TODO: add here
```

### Python

Project: MineSweeper.Solver.Python
File: Solver.py
```
// TODO: add here
```

### JavaScript via NodeJS

Project: MineSweeper.Solver.JavaScript
File: Solver.js
```
// TODO: add here
```

## Setup

#### Installer

#### Source (requires Visual Studio)

## Release notes
- 1.0.0-alpha Pre-release