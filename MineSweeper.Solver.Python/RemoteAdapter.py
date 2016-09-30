import sys, json
from Solver import Cell, CellState, Move, MoveType, Solver

def HandleMessage(line):
    jsonMap = json.loads(line)
    grid = [[None]*len(jsonMap[0]) for _ in range(len(jsonMap))]
    for y in range(len(jsonMap)):
        for x in range(len(jsonMap[0])):
            grid[y][x] = Cell(jsonMap[y][x]["X"], jsonMap[y][x]["Y"], jsonMap[y][x]["Value"], jsonMap[y][x]["State"])
    return grid;
solver = Solver();

while True:
  line = sys.stdin.readline()
  grid = HandleMessage(line)
  move = solver.GetNextMove(grid)
  sys.stdout.write(json.dumps(move.__dict__) + "\r\n")
  # normal processes seem to write empty lines to stderr so that a ReadLine call doesn't indefinitely hang when trying to read errors, but python
  # does not seem to do this by default. we get around this by manually writing empty lines here.
  sys.stderr.write("\r\n")
  # flushing is required to actually commit the writes
  sys.stdout.flush()
  sys.stderr.flush()
