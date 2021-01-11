
This project was bootstrapped with [Create React App](https://github.com/facebookincubator/create-react-app).

Below you will find some information on how to perform common tasks.<br>
You can find the most recent version of this guide [here](https://github.com/facebookincubator/create-react-app/blob/master/packages/react-scripts/template/README.md).

## Table of Contents

- [Table of Contents](#table-of-contents)
- [Prerequisites](#prerequisites)
- [Create a Database](#create-a-database)
- [Update Connection string](#update-connection-string)
- [Run update database command to apply migrations to your database](#run-update-database-command-to-apply-migrations-to-your-database)
- [Run Application](#run-application)
- [Login with](#login-with)
- [Swagger APIs](#swagger-apis)
- [Sending Feedback](#sending-feedback)

## Prerequisites

To run the application on my local dev machine, you are require to install following things:

* Updated Visual Studio 2019 with asp.net core 2.1
* Latest Node js
* Sql Server 2018 at least

## Create a Database
Create a database named **PromoDb** in Sql server

## Update Connection string
Update connection string in **appsetting.json** and **appsetting.development.json** in PromoCodeProject as per your sql server url, user id, and password.

## Run update database command to apply migrations to your database
To open console package manager, go to Tools > Nuget package Manager > console package manager. Run following command in console package manager 

```powershell

update-database -context PromoDbContext

``` 

## Run Application

When you have already completed apply migrations via **update-database** command, run your applicatoin locally from your visual studio. when you run first time, it may take time because required .net libraries and npm packages are restored. finally, your application runs successfully at the url **https://localhost:44392/**
You almost never need to update `create-react-app` itself: it delegates all the setup to `react-scripts`.

## Login with
``` json

"Email": "waseembt10029@hotmail.com"
"Password": "waseem"

```
## Swagger APIs


```json
{
  "swaggerUrl":"https://localhost:44392/resources/explorer/index.html"
}
```


```
## Sending Feedback

We are always open to [your feedback](https://github.com/waseemsaleem).
