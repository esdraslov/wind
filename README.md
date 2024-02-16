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

# Back-end only usage

back-end only usage make fully access to computer, and possible creating and destroying files or operating system and commands
without any requesting permissions
also for safer usage, add a `--wvm` for execute in a Wind Virtual Machine or `--sandboxed` to allow only access to not critial folders like `C:\Windows\System32`

## pc.backend.file.get

get a file from the computer

## pc.backend.file.get(... sync: boolean)

get a file from the computer and sync the result

## pc.backend.file.get(... sync: boolean, encoding: string)

get a file from the computer and sync the result with the given encoding

## pc.backend.file.set

write or create a file on the computer

## pc.backend.file.set(... administratorOnly: boolean)

write or create a file on the computer but making accessible with administrator permission

## pc.backend.file.set(... administratorOnly: boolean, attributes: array[json[string]])

write or create a file on the computer but making accessible with administrator permission and with the given attributes

## pc.backend.file.exists

check if a file exists on the computer

## pc.backend.os.boot

changes the boot of the computer (for tests we recommends to use `--sandboxed` or in a wvm)

## pc.backend.os.filesystem

changes the filesystem of the computer (for tests we recommends to use `--sandboxed` or in a wvm)

## pc.backend.os.language

changes the language of the computer (for tests we recommends to use `--sandboxed` or in a wvm)

## pc.backend.os.settings.create

create new settings to the os

## pc.backend.os.settings.create(... type: string)

create new settings to the os with the given type

## pc.backend.os.settings.get

get the settings of the os

## pc.backend.os.settings.get(... type: string)

get the settings of the os with the given type

## pc.backend.os.settings.set

set the settings of the os

## pc.backend.os.executable

change the executable of the os (for tests we recommends to use `--sandboxed` or in a wvm; default is `linux.sh` or `mac.exe`)

## pc.backend.os.version

change the version of the os (for tests we recommends to use `--sandboxed` or in a wvm; default is `latest` or `1.0.0`; changes the version of the executable and browser useragent)
