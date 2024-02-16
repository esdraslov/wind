# Wind

a new way to program

# Installation

## Bash / command line

to install via command line, run:
```bash
npm install -g wind
```

### MacOS
```bash
sudo npm install -g wind
```

## HTML

append the following to your `<head>`:
```html
<script src="https://esdraslov.github.com/public/wind.min.js>
<!-- also have wind.js (unminimised version)  -->
```

# Usage

firstly, you need to create a `.wind` file in your project root.
then, you can use the `wind` command to start the server:
```bash
wind --server <.wind file>
```
or using the `.html` with `<wscript>` (servers can be executed via `wind --server --html <.html file>`)
or using `--open` to open the browser automatically (no `--server` needed)

# Documentation

[https://esdraslov.github.io/wind/](https://esdraslov.github.io/wind/)

# Examples

## HTML way

button click event example:
```html
<wscript src="./script.wind"></wscript>
<button id="btn">click me!</button>
```
```wind
var btn = document.getElementById('btn');
btn.event.click = function() {
                    //  new text, element structure/root, css color
    btn.context.change('Hello, World!', btn.context.main, css.color.red)
}
```

## Back-end way

```wind
// new element                                                                  element number
var newElement = server.create('div', {id: 'newElement', style: {color: 'red'}}, 0);
newElement.text = 'Hello, World!';
// append
server.append(newElement, server.root);
// add event
//            can be used to get the element (0)
server.elements[0].event.click = function() {
                    //  new text, element structure/root, css color
    server.elements[0].context.change('Hello, World!', server.elements[0].context.main, css.color.red)
}
server.response({browser: "200"})
```

### start server

```bash
wind --server <.wind file> [--port <port>]
wind --open
```

## Dangereous functions

### server.destruct

close the server imediatly

### server.killELement

kill the element by id (cannot be reversed after)

### document.close

close the tab/document

### pc.download

download a file without verify
recommended to use `pc.safe.download`

## gdf (Glitch Debug Frame) module

makes debugging easier

### gdf.log

logs the message to the console

### gdf.error

logs the message to the console as error

### gdf.breakpoint

pauses the execution of the script

### gdf.throw `<type>`

throws an error of the given type

### gdf.stop

stop gdf frame from execute
<hr>
also to start again uses `gdf.start` function

### gdf.clear

clear the console
