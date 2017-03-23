#!/usr/bin/env python

import shlex
import sys
import threading

from settings import *
from connectors import *
from doc import NotesDocument
from config import ConfigSwitcher
from enums import Color


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
            print(Color.GREEN + "Your note was saved" + Color.END_COLOR)
        else:
            print(Color.RED + "Error saving your note" + Color.END_COLOR)

    def __parse_search(self, connector, text):
        found_notes = connector.search(' '.join(text))
        if len(found_notes) > 0:
            print("There are " + Color.YELLOW + str(len(found_notes)) + Color.END_COLOR + " notes that match your search")
            [print(str(note)) for note in found_notes]
        else:
            print(Color.RED + "There are no notes matching your search" + Color.END_COLOR)

    def __execute_action(self, datasource, action, additional):
        connector = ConnectorFactory().get_connector(datasource)
        if action == "new":
            self.__parse_new(connector, additional)
        elif action == "search":
            self.__parse_search(connector, additional)
        elif action == "show":
            [print(str(note)) for note in connector.find_all()]
        elif action == "doc":
            name = input('File name (Default: MyNotes) # ')
            path = input('File path (Default: .) # ')
            document = NotesDocument(name, path)
            document.add_paragraph(connector.find_all())
            document.save()
        else:
            print(Color.RED + 'Invalid command action' + Color.END_COLOR)

    # Threaded version of execute action
    def __execute_action_worker(self, datasource, action, additional):
        print('Results from datasource: ' + Color.YELLOW + datasource + Color.END_COLOR)
        self.__execute_action(datasource, action, additional)

    def parse_command(self, command):

        config = configparser.ConfigParser()
        config.read('configuration/datasources.conf')
        datasources = config.sections()

        if command.lower() in ("exit", "quit", "close", "q"):
            sys.exit()
        elif command.lower() == "datasources":
            print(datasources)
        elif command.lower() == "help":
            print(USAGE)
        elif command.lower() == "config":
            ConfigSwitcher().show_config()
        elif len(shlex.split(command)) > 1:
            args = shlex.split(command)
            if args[0] == "set":
                ConfigSwitcher().set(args[1], args[2])
            else:
                datasource = args[0]
                action = args[1]       # base
                additional = args[2:]  # additional

                if datasource in datasources:
                    self.__execute_action(datasource, action, additional)
                elif datasource == '*' and action != "new":
                    for ds_name in datasources:
                        t = threading.Thread(target=self.__execute_action_worker, args=(ds_name, action, additional))
                        t.start()
                        t.join(timeout=5)
        else:
            print(Color.RED + 'Invalid command syntax' + Color.END_COLOR)
