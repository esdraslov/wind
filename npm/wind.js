let { exec } = require("child_process")
let fs = require("fs")

let parameters = process.argv.slice(2);

// temporally
//                   start in 2 (why?)
//console.log(parameters[2])

if (parameters[0] === undefined) {
    throw new Error('Missing parameter: mode/input file');
} else if (parameters[1] === undefined && (parameters[0].includes('-') && parameters[0] !== "--open")) {
    throw new Error('Missing parameter: input file');
}

const mode = parameters[0].includes("-") ? parameters[0] : null;

const inputFile = parameters[0].includes("-") ? parameters[1] : parameters[0];

if (mode === "--open") {
    exec(`start http://localhost:8000`, (error, stdout, stderr) => {
        if (error || stderr) {
            throw new Error(`exec error: ${error ? error : stderr}`);
        } else {
            console.log(`opened\n${stdout}`);
        }
    });
} else if (!mode) {
    // compile the wind code to node.js
    fs.readFile(inputFile, "utf8", (err, data) => {
        if (err) {
            throw new Error(`readFile error: ${err}`);
        } else {
            console.log(data);
        }
    })
} else if (mode === "--server") {
    // after
} else if (!mode && parameters[2] === "--wvm") {
    // java
    // after
}
