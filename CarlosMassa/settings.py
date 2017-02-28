#!/usr/bin/env python

from enums import Color

BANNER = Color.YELLOW + """
  __  __         _   _       _                        __
 |  \/  |       | \ | |     | |                      /_/
 | \  / |_   _  |  \| | ___ | |_ ___  ___           / /
 | |\/| | | | | | . ` |/ _ \| __/ _ \/ __|         / /
 | |  | | |_| | | |\  | (_) | ||  __/\__ |        / /
 |_|  |_|\__, | |_| \_|\___/ \__\___||___/       / /
 ######## __/ | ##########################      | /
         |___/                                  |/__/\____/\_______________
""" + Color.END_COLOR


USAGE = """
    Once the terminal shows '>' prompt write the command you want to execute

    Syntax: [datasource | *] [command] [additional]

        *: all datasources (only works for search and show)

    Basic commands:
        - new message [-t|--tag tags]       \x1B[3m add a note with a message \x1B[23m
        - show                              \x1B[3m shows stored notes \x1B[23m
        - search [-t|--tag] text|regex      \x1B[3m search note by text or regular expression\x1B[23m
        - help                              \x1B[3m shows help screen \x1B[23m
        - datasources                       \x1B[3m shows data sources registered\x1B[23m
        - exit                              \x1B[3m exit the program \x1B[23m

    Examples:
        - mydatasource new note message
        - mydatasource new message --tags important foo
        - mydatasource show
        - mydatasource search text
        - mydatasource search [a-zA-Z0-9]*
"""