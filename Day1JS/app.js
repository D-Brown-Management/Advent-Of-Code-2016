"use strict";
var fs = require("fs");
var inputArray = readInputArray('input.txt');
var currentDirection = 0;
var currentPosition = {
    x: 0,
    y: 0
};
var positionArray = [];
positionArray.push({ x: 0, y: 0 });
var firstCrossFound = false;
for (var i = 0; i < inputArray.length; i++) {
    var dirChar = inputArray[i].trim().charAt(0);
    // set new direction
    switch (dirChar) {
        case "L":
            if (currentDirection > 0) {
                currentDirection = (currentDirection - 1) % 4;
            }
            else {
                currentDirection = 3;
            }
            break;
        case "R":
            currentDirection = (currentDirection + 1) % 4;
            break;
    }
    var spaces = parseInt(inputArray[i].trim().substring(1));
    var _loop_1 = function (i_1) {
        // move spaces
        switch (currentDirection) {
            case 0:
                currentPosition.y += 1;
                break;
            case 1:
                currentPosition.x += 1;
                break;
            case 2:
                currentPosition.y -= 1;
                break;
            case 3:
                currentPosition.x -= 1;
                break;
        }
        var posMap = { x: currentPosition.x, y: currentPosition.y };
        var pospos = positionArray.find(function (elem) {
            return elem.x === posMap.x && elem.y === posMap.y;
        });
        if (!pospos) {
            positionArray.push(posMap);
        }
        else {
            if (!firstCrossFound) {
                var part2Answer = Math.abs(posMap.x) + Math.abs(posMap.y);
                console.log("Part 2 Found:", part2Answer);
                firstCrossFound = true;
            }
        }
    };
    for (var i_1 = 0; i_1 < spaces; i_1++) {
        _loop_1(i_1);
    }
}
console.log("Part1 Location: ", Math.abs(currentPosition.x) + Math.abs(currentPosition.y));
finishApp();
function readInputArray(filename) {
    var file = fs.readFileSync('input.txt', { encoding: 'utf8' });
    var directionArray = file.split(',');
    return directionArray;
}
function finishApp() {
    console.log('Press any key to continue...');
    process.stdin.resume();
    process.stdin.on('data', process.exit.bind(process, 0));
}
