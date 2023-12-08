# Running the projects

The main folders contains 2 other folders which contains the solution for each challenge provided.


#### Payout combinator

To run the payout combinator, simply run payout-combinator.sln on visual studio or at ../payout-combinator/payout-combinator/payout-combinator with dotnet run:
```` 
dotnet run -c r 
````

#### Rest server

To run the customer-manager-api rest server, we need a MySQL 8 database. Here are the steps to get a running instance of the database:
** if you dont have docker installed: https://docs.docker.com/get-docker/

1. pull mysql image from docker
````
docker pull mysql:8.0
````
2. get a running mysql container
````
docker run --name customer_manager -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=customer_manager -d mysql:8.0
````

3. Go to the web project's folder ../dotnet-challenge/customer-manager-api/customer-manager-api, 
````
    dotnet run -c r
````

You can now access it on https://localhost:7203/swagger/index.html, or in your preferable API client (Insomnia, Postman, etc..)  http://localhost:5000

Also, there's a insomnia collection at dotnet-challenge/customer-manager-api/customer-manager-api if you want to import it.

