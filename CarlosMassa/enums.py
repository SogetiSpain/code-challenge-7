#!/usr/bin/env python


class Color:
    YELLOW = '\033[01;33m'
    BLUE = '\033[94m'
    GREEN = '\033[92m'
    LIGHT_YELLOW = '\033[93m'
    RED = '\033[91m'
    END_COLOR = '\033[0m'


class Style:
    ITALIC = '\x1B[3m'
    BOLD = '\033[1m'
    UNDERLINE = '\033[4m'