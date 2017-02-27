#!/usr/bin/env python

import json
import re

import requests
from note import Note


class ConnectorFactory:

    @staticmethod
    def getConnector(self, type):
        if type == "firebase":
            return FirebaseConnector()
        elif type == "mongodb":
            return MongoDBConnector()


class Connector:

    def __init__(self):
        self.__type = "undefined"


class FirebaseConnector(Connector):

    def __init__(self, url, api_key, collection):
        self.__url = url + collection + ".json?auth="+api_key

    def new(self, note):
        response = requests.post(self.__url, data=note.to_json())
        if response.status_code == 200:
            return True
        else:
            return False

    def find_all(self):
        response = requests.get(self.__url)
        jsoncontent = json.loads(response.content)
        notes = []
        if response.json():
            for note_id in response.json():
                n = Note("")
                n.from_json(jsoncontent[note_id])
                notes.append(n)

        return notes

    def search(self, text_or_regex):
        notes = self.find_all()
        found = []
        for note in notes:
            regex = re.compile(text_or_regex)
            if re.search(regex, note.get_description()):
                found.append(note)

        return found

class MongoDBConnector(Connector):

    def __init__(self):
        self.__type == "undefined"
