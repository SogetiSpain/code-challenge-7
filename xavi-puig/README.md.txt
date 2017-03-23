# Mynotes

## Available commands

>mynotes new <text for the note>

Creates a new note with the text specified.

>mynotes show [index]

Lists all notes, if index is specified, only the corresponding note is shown.

>mynotes edit <index> <new text for the note>

Updates the text of the note corresponding .to the index.

>mynotes search (date | text | tag) <text, tag or date to search for>

Shows the notes that matches by date, tag or text the specified criteria.

>mynotes tag <index> <tag>

Adds a tag to the note corresponding to the index.

>mynotes untag <index> <tag>

Removes a tag from the note corresponding to the index.

>mynotes xtag <index> <old tag> <new tag>

Changes a tag to another in the note corresponding to the index.

>mynotes delete (<index> | all)

Removes a the note corresponding to the index or all.

>mynotes config [delete]

Shows the current configuration. If delete is specified, the configuration is deleted.

>mynotes help

Shows the list of available commands.


## Developed

The project has been develop using SharpDevelop 5.2.

## Uses

The following third party libraries are used:

* [Json.NET] - Popular high-performance JSON framework for .NET
* [MongoDB.driver] - The official MongoDB C#/.NET Driver provides asynchronous interaction with MongoDB.
* [Fire#] - Firebase REST API wrapper for the .NET & Xamarin.

## Development

The _src_ folder contains the source code, packages and tests developed.

## Execution

Compiled source code can be found in the _dist folder_, just run **mynotes.exe** from the command line. Otherwise you can compile it yourself.

## Configuration

The program will search for _config.text_ in the folder the executable is located, if it can not be found or it is invalid, it will prompt if you want to create a new one.

Three configuration files for storage are currently allowed:

- **Firebase (using REST API)**:

| Format    | Example                         |
|-----------|---------------------------------|
| firebase  | firebase                        |
| url       | https://<yourDB>.firebaseio.com |
| authToken | YourAuthenticationToken         |

- **Firebase (using Fire# third party library)**

| Format    | Example                         |
|-----------|---------------------------------|
| thirdparty| thirdparty                      |
| url       | https://<yourDB>.firebaseio.com |
| authToken | YourAuthenticationToken         |

- **MongoDB (Using the official MongoDB driver)**

| Format        | Example         |
|---------------|-----------------|
| mongo         | mongo           |
| hostName:port | localhost:27017 |
| databaseName  | mynotes         |


[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)


   [Json.NET]: <http://www.newtonsoft.com/json>
   [MongoDB.driver]: <https://docs.mongodb.com/ecosystem/drivers/csharp/>
   [Fire#]: <https://github.com/ziyasal/FireSharp>