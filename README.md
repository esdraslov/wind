# Wind

a new way to program websites

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
