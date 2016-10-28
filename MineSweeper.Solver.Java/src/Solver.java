// TODO: implement Solver
public class Solver {
	// returns a Move given a grid, which is a 2D array of Cell objects
	public Move GetNextMove(Cell[][] grid)
    {
		return null;
    }
}

//region ---- Models (DO NOT MODIFY) ----

class Cell
{
    public int X;
    public int Y;
    public Integer Value;
    public CellState State;
}

enum CellState
{
    Hidden,
    Flagged,
    Revealed
}

class Move
{
    public MoveType MoveType;
    public int X;
    public int Y;
}

enum MoveType
{
    Click,
    Flag
}

//endregion