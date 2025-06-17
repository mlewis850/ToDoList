### Todo List
To run the frontend locally, run `ng serve` while in the /frontend folder

To run the frontend in a docker container run the following commands
docker build -t todo-angular ./frontend
docker run -p 4201:4200 todo-angular

### Backend
Use this to build the backend
docker build -t todo-backend .

You can use this command to run the backend container by itself, but it won't be able to connect to the sql container this way
docker run -p 8080:8080 todo-backend

### Start MySQL container
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password1!" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   mcr.microsoft.com/mssql/server:2025-latest
(add -d to the command to let it run in the background)

Then run one of these commands to start up the backend with a connection to the sql container
docker-compose up

OR you can run
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password1!' -p 8080:1433 todo-backend

### Connect to MySQL container to see the dbs
docker exec -it sql1 "bash"
/opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -P "Password1!" -C




### Run C# unit tests
dotnet test

### Run Angular unit tests
ng test


### Setting up database with data
CREATE DATABASE Todo;

USE Todo;

CREATE TABLE TodoList (
    id VARCHAR(36),
    title NVARCHAR(150),
    checked BIT,
    ordering INT,
);

INSERT INTO TodoList
VALUES ("1", "Create a todo list app", 0, 0);



Extend DBContext from entity framwork
virtual DBSet<TodoItem> Todos;
[Key] specifies primary key
make migration script
Tim Corey on youtube
Neil cummings on udemy building .net from scratch