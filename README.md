### Todo List

### Docker Commands
docker build -t todo-angular /frontend
docker run -p 4201:4200 todo-angular

docker build -t todo-backend .
docker run -p 8080:8080 todo-backend


### Start MySQL container
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password1!" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2025-latest

   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password1!" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   mcr.microsoft.com/mssql/server:2025-latest

docker-compose up

### Connect to MySQL container
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