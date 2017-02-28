#!/usr/bin/env python

import json
import re

import configparser
import requests
import firebase
from pymongo import MongoClient
from note import Note


class ConnectorFactory:

    def get_connector(self, ds_name):
        config = configparser.ConfigParser()
        config.read('configuration/datasources.conf')

        db_type = config[ds_name]['type']
        url = config[ds_name]['url']
        collection = "notes"

        if db_type == "firebase":
            return FirebaseConnector(url, config[ds_name]['api_key'], collection)
        elif db_type == "mongodb":
            return MongoDBConnector(url, config[ds_name]['port'], config[ds_name]['dbname'], collection)


class Connector:

    def search(self, text_or_regex):
        notes = self.find_all()
        found = []
        for note in notes:
            regex = re.compile(text_or_regex)
            if re.search(regex, note.get_description()):
                found.append(note)

        return found

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
        return super().search(text_or_regex)


class FirebaseClientConnector(Connector):

    def __init__(self, url, api_key, collection, email):
        authentication = firebase.FirebaseAuthentication(api_key, email, True, True)
        self.__firebase = firebase.FirebaseApplication(url, authentication)
        self.__collection = collection

    def new(self, note):
        self.__firebase.post('/' + self.__collection, note.to_json(),
                     params={'print': 'pretty'},
                     headers={'X_FANCY_HEADER': 'very fancy'})

    def find_all(self):
        self.__firebase.get('/' + self.__collection, {'print': 'pretty'})

    def search(self, text_or_regex):
        return super().search(text_or_regex)


class MongoDBConnector(Connector):

    def __init__(self, url, port, db_name, collection):
        self.__url = url;
        self.__collection = collection
        client = MongoClient(url, port)
        self.__db = client[db_name]

    def new(self, note):
        self.__db[self.__collection].insert(note.to_json())

    def find_all(self):
        self.__db[self.__collection].find()

    def search(self, text_or_regex):
        return super().search(text_or_regex)