#!/bin/python3
import sys
import os
import logging
import codecs
import operator

encoding = "utf-8"
encoding_mode = "replace"
if sys.stdout.encoding != encoding:
	sys.stdout = codecs.getwriter(encoding)(sys.stdout.buffer, encoding_mode)
if sys.stderr.encoding != encoding:
	sys.stderr = codecs.getwriter(encoding)(sys.stderr.buffer, encoding_mode)


logging.basicConfig(format='[%(levelname)s] %(message)s',
					level=logging.INFO)

def usage():
	print("Usage: " + sys.argv[0] + " <file>")

if len(sys.argv) <= 1:
	usage()
	exit(0)

if sys.argv[1] in ["-h", "--help"]:
	usage()

workingfile = sys.argv[1]

characters = dict()

with open(workingfile, 'r', encoding=encoding) as infile:
	for line in infile:
		for char in line:
			if char in characters:
				characters[char] += 1
			else:
				characters[char] = 1
for key in characters:
	print("{}: {}".format(key, characters[key]))
print(repr(characters))