# GenerateAddressBook.py

Author:
* [Nikolai Zimmermann](https://github.com/Chronophylos)

Language:
* Python 3.6

Platforms:
* Windows
* Linux (untested)
* MacOS (untested)


# Description
Generate a addressbook filled with fake indentities. Identities are pulled from the [namefake.com](http://namefake.com) [API](http://namefake.com/api).

# Usage
```Shell
usage: generateAddressBook.py [-h] [-d | -q] [-n] -O OUTPUT [-o ORIGIN]
                              [-g GENDER] -c COUNT [-t THREADS]

optional arguments:
  -h, --help            show this help message and exit
  -d, --debug           enable debugging
  -q, --quiet           be more quiet
  -n, --simulate        simulate, don't do anything
  -O OUTPUT, --output OUTPUT
                        Output file
  -o ORIGIN, --origin ORIGIN
                        Country for the generated identities
  -g GENDER, --gender GENDER
                        Gender for the generated identities
  -c COUNT, --count COUNT
                        How many identities do you want
  -t THREADS, --threads THREADS
                        Num of threats to use
```

# Future
This script needs a lot of rework and can be opimized.
