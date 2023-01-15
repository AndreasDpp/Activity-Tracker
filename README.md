# **Activity-Tracker**

This application is split into a frontend application in Blazor and a backend application as a FaaS solution in Azure Functions. 

The blazor application is just a simple representation of the data stored in a MongoDB. It's also possible to access the API through the Postman collection in the backend folder. 

## **Frontend**

The frontend is a simple Blazor application build with webassembly, it dosen't need to run the code on the web-server because we have a seperate backend API.

I haven't used any specific patterns in the frontend it's just a simple project for showing some data from the backend API.

## **Backend**

The backend can be accessed through the Azure functions that are all exposed without need of any authtentication. 

The architecture of the solution is a 3 layer architure with a prestenation layer which is the Azure functions project. All the business logic is located in the Service project as a .NET library, and the last project is of course the data-access layer where we have the code for accessing the mongo DB with a generic repository. 

## **Setup to run it locally**

The backend solution can be accessed through Postman, I have placed a collection with the calls in the "Backend" folder. But if the frontend has to access the Azure Functions it has to have the following code in the local.settings.json.

```
  "Host": {
    "LocalHttpPort": 7296,
    "CORS": "*",
    "CORSCredentials": false
  }
```

For the MongoDB under Values section in the same file add the following.

```
    "MongoDB:DatabaseName": "<Database name>",
    "MongoDB:ConnectionString": "<Connection string>"
```
