#!/usr/bin/env python

import json
import re

import configparser
import requests
import firebase
from pymongo import *
from note import Note
from firebase import firebase


class ConnectorFactory:

    @staticmethod
    def get_connector(ds_name):
        datasources = configparser.ConfigParser()
        datasources.read('configuration/datasources.conf')

        db_type = datasources[ds_name]['type']
        url = datasources[ds_name]['url']
        collection = "notes"

        if db_type == "firebase":
            config = configparser.ConfigParser()
            config.read('configuration/app.conf')
            if config['config']['firebase_uses_restapi'] == '1':
                return FirebaseConnector(url,
                                         datasources[ds_name]['api_key'],
                                         collection)
            else:
                return FirebaseClientConnector(url,
                                               datasources[ds_name]['api_key'],
                                               collection,
                                               datasources[ds_name]['email'])
        elif db_type == "mongodb":
            return MongoDBConnector(url,
                                    datasources[ds_name]['port'],
                                    datasources[ds_name]['db_name'],
                                    datasources[ds_name]['user'],
                                    datasources[ds_name]['password'],
                                    collection)


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
        res = self.__firebase.post('/' + self.__collection, note.__dict__,
                     params={'print': 'pretty'})
        return res != None

    def find_all(self):
        res = self.__firebase.get('/' + self.__collection, None)
        jsoncontent = json.loads(str(res).replace("\'","\""))
        notes = []
        for note_id, value in res.items():
            n = Note("")
            n.from_json(jsoncontent[note_id])
            notes.append(n)

        return notes

    def search(self, text_or_regex):
        return super().search(text_or_regex)


class MongoDBConnector(Connector):

    def __init__(self, url, port, db_name, user, password, collection):
        self.__url = url;
        self.__collection = collection
        client = MongoClient(url, int(port))
        self.__db = client[db_name]
        if user and password:
            self.__db.authenticate(user, password)

    def new(self, note):
        try:
            res = self.__db[self.__collection].insert_one(note.__dict__).inserted_id
            return True
        except(Exception):
            return False

    def find_all(self):
        res = self.__db[self.__collection].find()
        notes = []
        for note in res:
            n = Note("")
            n.__dict__.update(note)
            notes.append(n)

        return notes

    def search(self, text_or_regex):
        reg = re.compile(text_or_regex)
        res = self.__db[self.__collection].find({'_Note__description': {'$regex': reg}})
        notes = []
        for note in res:
            n = Note("")
            n.__dict__.update(note)
            notes.append(n)

        return notes
        #return super().search(text_or_regex)
