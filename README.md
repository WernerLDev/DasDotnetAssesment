# Assesment DAS

The implementation for the assesment given by Das recruitment

## prerequisites

In order to run this project you need the following tools:

- dotnet CLI with .NET8 runtime
- docker (to run the message queue)

I am using SQLite so no need to run a database server. In real projects I would choose PostgreSQL but in this case SQLite was a bit more convenient as it allows me to quickly re-create the database during development.

## Getting started

It is a basic .NET application so all you need to do is run the following commands:

Start RabbitMQ with the following shell script. This will spin up a docker container that contains RabbitMQ.

```
./rabbitmq.sh
```

Create the database:

```bash
dotnet-ef database update --project GoTApiDas
```

Start project:

```bash
dotnet run --project GoTApiDas
```

### Unit tests
I have added 2 unit tests to test the service class. These tests can be executed with the following command:

```bash
dotnet test
```

## Use the endpoints

### Fetch all characters from the database:

```
GET http://localhost:5129/Api/Characters
```

### Find characters by alias:

```
GET http://localhost:5129/Api/FindByAlias?alias=Daughter&page=0
```

### Import characters from Ice of Fire API

```
POST http://localhost:5129/Api/ImportCharacters
```

### Create new character manually:

```
POST http://localhost:5129/Api/Characters
```

body:

```json
{
  "name": "Test character",
  "gender": "Female",
  "culture": "Braavosi",
  "born": "",
  "died": "",
  "characterAliases": [
    {
      "alias": "The alis "
    },
    {
      "alias": "Another alias"
    }
  ]
}
```
