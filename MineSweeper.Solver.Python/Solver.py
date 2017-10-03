#region ---- Models (DO NOT MODIFY) ----

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

def getNeighbors(x, y, grid):
	for nx in xrange(max(0, x - 1), min(x + 2, len(grid))):
		for ny in xrange(max(0, y - 1), min(y + 2, len(grid[0]))):
			if nx != 0 or ny != 0:
				yield grid[ny][nx]
 
# TODO: implement Solver
class Solver(object):
	def __init__(self):
		self.queue = []
	def GetNextMove(self, grid):
		if self.queue:
			return self.queue.pop()
		
		# search for decidable tile
		for row in grid:
			for cell in row:
				if cell.State == CellState.REVEALED:
					empties = 0
					emptyCells = []
					bombs = 0
					for n in getNeighbors(cell.X, cell.Y, grid):
						if n.State == CellState.FLAGGED:
							bombs += 1
						elif n.State == CellState.HIDDEN:
							empties += 1
							emptyCells.append(n)
					if empties and bombs == cell.Value:
						# all safe
						self.queue += [Move(c.X, c.Y, MoveType.CLICK) for c in emptyCells]
					elif empties and empties == cell.Value - bombs:
						# all bombs
						self.queue += [Move(c.X, c.Y, MoveType.FLAG) for c in emptyCells]
		# generated all possible sure (1-ply) moves
		if self.queue:
			return self.queue.pop()
		
		n = len(grid)
		for x in range(n):
			for y in range(n):
				cell = grid[y][x]
				if cell.State == CellState.HIDDEN:
					return Move(cell.X, cell.Y, MoveType.CLICK)
					
		return Move(0, 0, MoveType.CLICK)