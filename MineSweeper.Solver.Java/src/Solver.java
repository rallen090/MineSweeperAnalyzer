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
	public Move(MoveType moveType, int x, int y){
		this.X = x;
		this.Y = y;
		this.MoveType = moveType;
	}

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