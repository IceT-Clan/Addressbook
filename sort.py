#!/bin/python3
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

def usage():
	print("Usage: " + sys.argv[0] + " <file> [-s]")
	print("file\twich file to sort")
	print("-s\tsort file")


if len(sys.argv) <= 1:
	usage()
	exit(0)

if sys.argv[1] in ["-h", "--help"]:
	usage()

workingfile = sys.argv[1]
if len(sys.argv) > 2 and sys.argv[2] in ["-s", "--sort"]:
	sort = True
else:
	sort = False


if not os.path.exists(workingfile):
	logging.error(workingfile + " does not exits")
	exit(127)

lines = set()
linecounter = 0
with open(workingfile, 'r', encoding=encoding) as infile:
	for line in infile:
		linecounter += 1
		lines.add(line)
if sort:
	lines = sorted(list(lines))
with open(workingfile, 'w', encoding=encoding) as outfile:
	outfile.writelines(lines)
logging.info("Sorted {} lines resulting in {} lines. Diff: {}".
			 format(linecounter, len(lines), len(lines) - linecounter))
