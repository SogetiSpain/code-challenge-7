#!/usr/bin/env python

import configparser
import sys
import shlex
import settings
from connectors import *


class MyNotesParser:

    def __parse_new(self, connector, text):
        tags = []
        for idx, content in enumerate(text):
            if any(flag in content for flag in ["-t", "--tag"]):
                tags = text[idx+1:]
                text = text[:idx]
                break

        note = Note(' '.join(text), tags)
        added = connector.new(note)
        if added:
            print("Your note was saved")
        else:
            print("Error saving your note")

    def __parse_search(self, connector, text):
        found_notes = connector.search(' '.join(text))
        if len(found_notes) > 0:
            print("There are " + str(len(found_notes)) + " notes that match your search")
            [print(str(note)) for note in found_notes]
        else:
            print("There are no notes matching your search")

    def parse_command(self, command):

        config = configparser.ConfigParser()
        config.read('configuration/datasources.conf')
        datasources = config.sections()

        if command.lower() in ("exit", "quit", "close", "q"):
            sys.exit()
        elif command.lower() == "datasources":
            print(datasources)
        elif command.lower() == "help":
            print(settings.USAGE)
        elif len(shlex.split(command)) > 1:
            args = shlex.split(command)
            datasource = args[0]
            action = args[1] # base
            additional = args[2:] #' '.join(str(part) for part in args[2:]) # additional

            if datasource in datasources:
                #ConnectorFactory.getConnector(datasource, config[datasource]['url'], config[datasource]['api_key'], "notes") # Check
                connector = FirebaseConnector(config[datasource]['url'], config[datasource]['api_key'], "notes")
                if action == "new":
                    self.__parse_new(connector, additional)
                elif action == "search":
                    self.__parse_search(connector, additional)
                elif action == "show":
                    [print(str(note)) for note in connector.find_all()]
                else:
                    print('Invalid command action')
        else:
            print('Invalid command syntax')
