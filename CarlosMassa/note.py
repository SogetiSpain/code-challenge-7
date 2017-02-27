#!/usr/bin/env python

from json import JSONEncoder
from datetime import datetime
import json
from enums import Color

class Note:

    def __init__(self, description, tags = []):
        self.__description = description
        self.__tags = tags
        self.__creationDate = datetime.now().strftime('%d-%m-%Y')

    def __str__(self):
        content = self.__creationDate + ' - ' + self.__description
        tags = ' '.join([Color.RED + "["+tag+"]" + Color.END_COLOR for tag in self.__tags])
        return content + " " + tags

    def add_tag(self, tag):
        self.__tags.append(tag)

    def add_tags(self, *args):
        for count, tag in enumerate(args):
            self.add_tag(tag)

    def to_json(self):
        return json.dumps(self, default=lambda o: o.__dict__, sort_keys=True, indent=4)

    def from_json(self, jsonstr):
        self.__dict__.update(jsonstr)

    def get_description(self):
        return self.__description

