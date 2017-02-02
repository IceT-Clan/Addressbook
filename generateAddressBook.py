#!/bin/python3

import argparse
import logging
import requests
import json
import csv
import sys
import codecs
import time
import threading
from queue import Queue
import colorama


# -------------- Constant Variables ------------- #
ID_PRESETS = {
    "default": [
        "name",
        "address",
        "birth_data",
        "phone_h",
        "phone_w",
        "email_u",
        "email_d",
        "email",
        "color",
        "height",
        "weight",
        "blood",
        "eye",
        "hair"
    ]
}

NAMEFAKE_URL = "http://api.namefake.com/{}/{}"
lock = threading.Lock()
q = Queue()
count = 0


# -------------- Arguments ------------- #
parser = argparse.ArgumentParser()
group = parser.add_mutually_exclusive_group()
group.add_argument("-d", "--debug", action="store_true",
                   help="enable debugging")
group.add_argument("-q", "--quiet", action="store_true",
                   help="be more quiet")
parser.add_argument("-n", "--simulate", action="store_true",
                    help="simulate, don't do anything")
parser.add_argument("-O", "--output", help="Output file")
parser.add_argument("-o", "--origin", default="random",
                    help="Country for the generated name")
parser.add_argument("-g", "--gender", default="random",
                    help="Gender for the generated name")
parser.add_argument("-c", "--count", help="How many names do you want",
                    type=int)
parser.add_argument("-t", "--threads", default=404, type=int
                    help="Num of threats to use")

args = parser.parse_args()

if args.debug:
    loglevel = logging.DEBUG
elif args.quiet:
    loglevel = logging.WARN
else:
    loglevel = logging.INFO


# -------------- Logger -------------- #
# create logger
logger = logging.getLogger("chronos")
logger.setLevel(loglevel)

# create console handler and set level to debug
ch = logging.StreamHandler()
ch.setLevel(loglevel)

# create formatter
formatter = logging.Formatter(fmt="[%(asctime)s][%(levelname)s] %(message)s",
                              datefmt="%H:%M:%S")

# add formatter to ch
ch.setFormatter(formatter)

# add ch to logger
logger.addHandler(ch)

encoding = "utf-8"
encoding_mode = "replace"
if sys.stdout.encoding != encoding:
    sys.stdout = codecs.getwriter(encoding)(sys.stdout.buffer, encoding_mode)
if sys.stderr.encoding != encoding:
    sys.stderr = codecs.getwriter(encoding)(sys.stderr.buffer, encoding_mode)


# -------------- Functions -------------- #
def toString(var):
    if isinstance(var, str):
        string = var
    elif isinstance(var, (int, float)):
        string = str(var)
    return string


def getIdentityFromServer(url, fieldnames):
    try:
        request = requests.get(url)
    except requests.exceptions.ConnectionError as e:
        return None
    data = json.JSONDecoder().decode(request.text)
    identity = dict()
    for entry in data:
        if entry in fieldnames:
            identity[entry] = toString(data[entry]).replace('\n', '')

    # concat email
    if "email_u" and "email_d" in fieldnames:
        logger.debug("Concatting email...")
        email = "{}@{}".format(identity["email_u"], identity["email_d"])
        del identity["email_u"]
        del identity["email_d"]
        identity["email"] = email

    return identity


def workIdentity(url, fieldnames, fake_names):
    while True:
        identity = getIdentityFromServer(url, fieldnames)
        if not identity:
            continue
        global count
        count += 1
        logger.info("Got {} IDs of {} IDs {}%: {}".
                    format(count,
                           args.count,
                           str(count / args.count * 100)[:5],
                           identity["name"][:40]))
        logger.debug(repr(identity))
        break
    fake_names.append(identity)


def worker():
    while True:
        url, fieldnames, fake_names = q.get()
        workIdentity(url, fieldnames, fake_names)
        q.task_done()


def main():
    # get fake names
    logger.info("Getting {} identities".format(args.count))
    current_preset = "default"
    url = NAMEFAKE_URL.format(args.origin, args.gender)
    fake_names = list()
    # count = args.count
    fieldnames = ID_PRESETS[current_preset]

    for i in range(404):
        t = threading.Thread(target=worker)
        t.daemon = True
        t.start()

    time.perf_counter()

    for i in range(args.count):
        q.put((url, fieldnames, fake_names))

    q.join()
    time_needed = time.perf_counter()

    # for count in range(args.count):
    #     workIdentity(url, fieldnames, fake_names, count)

    # concat email
    ID_PRESETS[current_preset].remove("email_u")
    ID_PRESETS[current_preset].remove("email_d")

    # output fakenames to file
    with open(args.output, 'w',
              encoding=encoding, errors=encoding_mode) as csvfile:
        writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
        writer.writeheader()
        # writer.writerows(fake_names)
        for identity in fake_names:
            line = str()
            for entry in fieldnames:
                line += identity[entry] + ','
            line = line[:-1] + '\n'
            csvfile.write(line)

    logger.info("""Statistics:
                   Time needed: {}
                   Time per identity: {} s""".
                format(time.strftime("%Hh %Mm %Ss", time.gmtime(time_needed)),
                       str(time_needed / args.count)[:6]))


if __name__ == '__main__':
    colorama.init()
    main()
