#!/bin/bash
import sys
import os
import logging
import codecs

encoding = "utf-8"
encoding_mode = "replace"
if sys.stdout.encoding != encoding:
    sys.stdout = codecs.getwriter(encoding)(sys.stdout.buffer, encoding_mode)
if sys.stderr.encoding != encoding:
    sys.stderr = codecs.getwriter(encoding)(sys.stderr.buffer, encoding_mode)


logging.basicConfig(format='[%(levelname)s] %(message)s',
                    level=logging.INFO)

if len(sys.argv) < 3:
    print("Usage: " + sys.argv[0] + " <output> <files>...")
    exit(0)

output = sys.argv[1]

files = sys.argv[2:]
for file in files:
    if not os.path.exists(file):
        files.remove(file)
        logging.warn(file + " does not exist")

if len(files) == 1:
    logging.error("only one file exists")
elif len(files) == 0:
    logging.critical("no existing files found")
    exit(127)

with open(output, 'a', encoding=encoding) as outfile:
    for file in files:
        logging.info("Getting content of {}".format(file))
        with open(file, encoding=encoding) as infile:
            outfile.writelines(infile)
logging.info("Concatted {} files".format(len(files)))
