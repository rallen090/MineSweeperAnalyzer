var solver = require('./Solver.js');

var readline = require('readline');
var read = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
  terminal: false
});

function handleMessages(){
	read.on('line', function(line){
			//console.log(line);
			var grid = JSON.parse(line);
			var nextMove = solver.getNextMove(grid);
			console.log(JSON.stringify(nextMove));
		});
};

handleMessages();