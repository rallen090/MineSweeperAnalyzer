module.exports = {
	getNextMove: function (grid){
		return new Move(MoveType.CLICK, Math.floor((Math.random() * 10) + 0), Math.floor((Math.random() * 10) + 0));
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