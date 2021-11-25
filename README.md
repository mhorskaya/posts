## posts

This repository includes a command line application that:
* consumes JSON data from a remote API endpoint which defined in a configuration file. (appSettings.json)
* applies filter to the results specified with the --query flag.
* outputs results in JSON.

Usage:
```
Posts 1.0.0
Copyright (C) 2021 Posts

  -q, --query     Required. The query text (all | <comma_separated_numbers>)

  -o, --output    Writes output to file if specified.

  -f, --format    (Default: false) Formats JSON output.

  --help          Display this help screen.

  --version       Display version information.
```

Sample Start:
```
Posts.exe --query 10,20 --output C:\Users\mhors\Desktop\data.json --format
```
