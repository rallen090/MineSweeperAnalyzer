from subprocess import Popen, PIPE
exePath = "C:\\dev\MineSweeper\\MineSweeper\\bin\\Debug\\MineSweeper.exe"
print "Executing '" + exePath + "'"
p = Popen(exePath, stdin=PIPE)
p.communicate(input=b"EXIT")