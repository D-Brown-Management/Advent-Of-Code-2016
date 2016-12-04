var fs = require('fs');
var readline = require('readline');

console.log('Easter HQ Search');

var inputArray = readInputArray('input.txt');

console.log(inputArray);





finishApp();

function readInputArray(filename) {
  var file = fs.readFileSync('input.txt', { encoding: 'utf8' });
  var directionArray = file.split(', ');

  return directionArray;
}

function finishApp() {
    console.log('Press any key to continue...');

    process.stdin.setRawMode(true);
    process.stdin.resume();
    process.stdin.on('data', process.exit.bind(process, 0));
}