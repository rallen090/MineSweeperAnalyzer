
public class Solver {
	public Move GetNextMove(Cell[][] grid)
    {
		// TODO: implement solution here
		return null;
    }
}

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
