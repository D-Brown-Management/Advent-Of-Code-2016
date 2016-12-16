import fs = require("fs");
import readline = require('readline');
const inputArray = readInputArray('input.txt');

let currentDirection = 0;
let currentPosition = <Position>{
    x: 0,
    y: 0    
};

let positionArray = [];
positionArray.push({ x: 0, y: 0 });
let firstCrossFound = false;
for (let i = 0; i < inputArray.length; i++) {
    
    const dirChar = inputArray[i].trim().charAt(0);
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
    
    const spaces = parseInt(inputArray[i].trim().substring(1));

    for (let i = 0; i < spaces; i++) {
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
        const posMap = { x: currentPosition.x, y: currentPosition.y };
        const pospos = positionArray.find(function (elem) {
            return elem.x === posMap.x && elem.y === posMap.y;
        });
        
        if (!pospos) {
            positionArray.push(posMap);
        } else {
            if (!firstCrossFound) {
                let part2Answer = Math.abs(posMap.x) + Math.abs(posMap.y);
                console.log("Part 2 Found:", part2Answer);
                firstCrossFound = true;
            }
            
        }
    }
    
}

console.log("Part1 Location: ", Math.abs(currentPosition.x) + Math.abs(currentPosition.y));

finishApp();

function readInputArray(filename: string): Array<string> {
    var file = fs.readFileSync('input.txt', { encoding: 'utf8' });    
    var directionArray = file.split(',');

    return directionArray;
}

function finishApp(): void {
    console.log('Press any key to continue...');
        
    process.stdin.resume();
    process.stdin.on('data', process.exit.bind(process, 0));
}

interface Position {
    x: number;
    y: number;
}
    