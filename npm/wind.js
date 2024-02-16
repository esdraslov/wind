let parameters = process.argv.slice(2);

// temporally
//                   start in 2 (why?)
//console.log(parameters[2])

if (parameters[0] === undefined) {
    throw new Error('Missing parameter: mode/input file');
} else if (parameters[1] === undefined && (parameters[0].includes('-') && parameters[0] !== "--open")) {
    throw new Error('Missing parameter: input file');
}
