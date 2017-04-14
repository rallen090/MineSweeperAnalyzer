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
	def __init__(self, moveType, x, y):
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
                              
import random
 
# TODO: implement Solver
class Solver(object):
	# returns a Move given a grid, which is a 2D array of Cell objects
	def GetNextMove(self, grid):
		n = len(grid)

		for x in range(n):
			for y in range(n):
				if grid[y][x].State == CellState.REVEALED:
					value = grid[y][x].Value
					neighbors = list(getNeighbors(x, y, grid))
					possible_mine_count = 0
					flagged_mine_count = 0

					for neighbor in neighbors:
						if neighbor.State == CellState.HIDDEN:
							possible_mine_count += 1
						elif neighbor.State == CellState.FLAGGED:
							possible_mine_count += 1
							flagged_mine_count += 1

					if possible_mine_count == value:
						for neighbor in neighbors:
							if neighbor.State == CellState.HIDDEN:
								return Move(MoveType.FLAG, neighbor.X, neighbor.Y)

					if flagged_mine_count == value and possible_mine_count > flagged_mine_count:
						for neighbor in neighbors:
							if neighbor.State == CellState.HIDDEN:
								return Move(MoveType.CLICK, neighbor.X, neighbor.Y)

		for x in range(n):
			for y in range(n):
				cell = grid[y][x]
				if cell.State == CellState.HIDDEN:
					return Move(MoveType.CLICK, cell.X, cell.Y)
		# poss = [(x, y) for x in range(len(grid)) for y in range(len(grid[0])) if grid[y][x].State == CellState.HIDDEN]
		# random.shuffle(poss)
		# return Move(poss[0][0], poss[0][1], MoveType.CLICK)