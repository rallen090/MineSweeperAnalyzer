#region ---- Models ----

class Cell(object):
	def __init__(self, x, y, value, state):
		self.X = x
		self.Y = y
		self.Value = value
		self.State = state

class CellState:
	HIDDEN = 0
	FLAGGED = 1
	REVEALED = 2

class Move(object):
	def __init__(self, x, y, moveType):
		self.X = x
		self.Y = y
		self.MoveType = moveType

class MoveType:
	CLICK = 0
	FLAG = 1

#endregion

class Solver(object):
    # returns a Move given a grid, which is a 2D array of Cell objects 
    def GetNextMove(self, grid):
        return Move(1,2,0)