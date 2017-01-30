#!/bin/python3

import argparse
import logging
import requests
import json
import csv
import sys
import codecs


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


def main():
    # get fake names
    logger.info("Getting {} identities".format(args.count))
    current_preset = "default"
    url = NAMEFAKE_URL.format(args.origin, args.gender)
    fake_names = list()
    count = args.count
    while count > 0:
        try:
            request = requests.get(url)
        except requests.exceptions.ConnectionError as e:
            continue
        count -= 1
        data = json.JSONDecoder().decode(request.text)
        identity = dict()
        for entry in data:
            if entry in ID_PRESETS[current_preset]:
                identity[entry] = toString(data[entry]).replace('\n', '')
        logger.info("Got {} ({}/{})".format(identity["name"],
                                            args.count - count, args.count))
        # concat email
        if "email_u" and "email_d" in ID_PRESETS[current_preset]:
            logger.debug("Concatting email...")
            email = "{}@{}".format(identity["email_u"], identity["email_d"])
            del identity["email_u"]
            del identity["email_d"]
            identity["email"] = email
        # fix display of blood type when \u2212 cannot be understood
        if "blood" in ID_PRESETS[current_preset] and False:
            logger.debug("Fixing \\u2212")
            blood = toString(identity["blood"]).replace(u"\u2212", "-")
            identity["blood"] = blood
        logger.debug(repr(identity))
        fake_names.append(identity)

    # concat email
    ID_PRESETS[current_preset].remove("email_u")
    ID_PRESETS[current_preset].remove("email_d")

    # output fakenames to file
    with open(args.output, 'w',
              encoding=encoding, errors=encoding_mode) as csvfile:
        fieldnames = ID_PRESETS[current_preset]
        writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
        writer.writeheader()
        # writer.writerows(fake_names)
        for identity in fake_names:
            line = str()
            for entry in fieldnames:
                line += identity[entry] + ','
            line = line[:-1] + '\n'
            csvfile.write(line)


if __name__ == '__main__':
    main()
