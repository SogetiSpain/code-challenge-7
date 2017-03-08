#!/usr/bin/env python

import configparser
from enums import Color

class ConfigSwitcher:

    @staticmethod
    def set(key, boolean):
        config_file = "configuration/app.conf"
        config = configparser.ConfigParser()
        config.read(config_file)
        config.set('config', key, str(boolean))
        with open(config_file, 'w') as configfile:
            config.write(configfile)
            print("Changed value of property " + Color.YELLOW + "'" + key + Color.END_COLOR +
                   "' to " + Color.YELLOW + boolean + Color.END_COLOR)


    @staticmethod
    def show_config():
        config_file = "configuration/app.conf"
        config = configparser.ConfigParser()
        config.read(config_file)
        for key, val in config['config'].items():
            print("- " + key + ": " + Color.YELLOW + val + Color.END_COLOR)