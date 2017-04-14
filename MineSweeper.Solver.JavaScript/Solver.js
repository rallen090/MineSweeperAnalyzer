module.exports = {
	// returns a Move provided a MineSweeper grid, which is a 2D array of Cells
	getNextMove: function (grid){
		// solver algorithm here...

		// return next move
		return new Move(MoveType.CLICK, /* X */ 1, /* Y */ 2);
	}
};

//region ---- Models (DO NOT MODIFY) ----

function Cell(x, y, value, state)
{
    this.x = x;
    this.y = y;
	this.value = value;
	this.state = state;
};

var CellState =
{
    HIDDEN: 0,
    FLAGGED: 1,
    REVEALED: 2
};

function Move(moveType, x, y)
{
    this.moveType = moveType;
    this.x = x;
	this.y = y;
}

var MoveType =
{
    CLICK: 0,
    FLAG: 1
};

//endregion