#!/usr/bin/env python

import settings
from exceptions import MyNotesException
from parsers import MyNotesParser

if __name__ == '__main__':
    print(settings.BANNER)
    print(settings.USAGE)

    while True:
        try:
            command = input('MyNotes> ')
            if not command:
                continue
            else:
                MyNotesParser().parse_command(command)
        except(KeyboardInterrupt, EOFError):
            raise MyNotesException