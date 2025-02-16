# TempTake Server

## About

A .NET Core server that is used to access the data from the database (later it will manage the users that will use the system).

## Endpoints

- `GET /api/entry/worker`
    Body:
    ```json
    {
        "WorkerMAC": "string",
        "From": "2022-01-01T00:00:00", // Optional
        "To": "2022-01-01T00:00:00" // Optional
    }
    ```
- `GET /api/entry/worker/all`
    Body:
    ```json
    {
        "WorkerMAC": "string"
    }
    ```
- `GET /api/entry/manger`
    Body:
    ```json
    {
        "ManagerMAC": "string",
        "From": "2022-01-01T00:00:00", // Optional
        "To": "2022-01-01T00:00:00" // Optional
    }
    ```
- `GET /api/entry/manger/all`
    Body:
    ```json
    {
        "ManagerMAC": "string"
    }
    ```

## Installation and setup

The MQTT service will start in the docker container of the project.

