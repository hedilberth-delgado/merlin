# Umbraco CMS, Morganas Web API

This solution consists of two projects:

1. **Umbraco CMS**: The Morganas Umbraco CMS prohect it will be the custom implementation of Umbraco CMS Backoffice the  that uses SQLite as its database. This project exposes various backoffice services for all the CMS magament API. 

2. **Morganas Web API**: Is a Web API project that provides an interface for accessing the Umbraco API Management Backoffice API. This project interacts with the Umbraco CMS application to perform create or delete operations on document types and also to check the health of the CMS.

## Projects Overview

### 1. MorganasUmbraco

- **Description**: This project will allow to exposes services that the Umbraco CMS Backoffice. This are accesible with an access token generated using client credentials. (See appsettings.Development.json file)
- **Database**: Utilizes SQLite for data storage.
- **User**: the admin user is hedilberth.delgado@globant.com and the password is Globant2025.
- **Key Features**:
  - **Backoffice services**: We can access all the endpoints that the backoffice API has avaible for external access.
  - **Is Ok**: Provides a custom endpoint on Backoffice API in the Umbraco service, that it has to receive a boolean, isOk. It has to return an
status code if isOk is true, otherwise a BadRequest.

### 2. Morganas Web API

- **Description**: A Web API that interacts with the Umbraco backoffice API. Acts as a bridge to access some of the API Management endpoints from Umbraco CMS.
- **Key Features**:
  - **Create Document Type**: Exposes an API endpoint to create new document types in Umbraco.
  - **Delete Document Type**: Exposes an API endpoint to delete existing document types by key.
  - **Check CMS Health**: Provides an endpoint to check the health status of the Umbraco CMS.
 
 ### 3. Aspire .NET
 - **Description**: .NET Aspire is a set of powerful tools, templates, and packages for building observable, production ready apps. 
 - **Key Features**: 
   - **Apps Orchestration**: It will allow to have a single place to start/stop the Morganas and Umbraco projects respectivily.
   - **Apps Observability**:  It will allow the monitor the apps logs and other features. 

### Setup Instructions

1. **Install Dependencies**:
   Make sure you have the required .NET 9 SDK installed. You can download it from [the official .NET website](https://dotnet.microsoft.com/download).

2. **Make sure to have Windows trusted certificate**: To be able to run the application is necesary to have a trusted SSL Certificate in the computer that is going to host the application. 
   ```bash
   dotnet dev-certs https --clean
   dotnet dev-certs https --trust
   ```
3. **Clone the Repository**:
   ```bash
   git clone https://github.com/hedilberth-delgado/merlin.git
   ```
4. **Run the Application**: in a terminal do the following
   ```bash
   cd Merlin/Aspire/AspireApp.AppHost
   dotnet run
   ```
5. **Access Aspire dashboard**: Navigate to `https://localhost:17141/` to access to the .NET Aspire dashboard to see and manage the added resources.

6. **Access the Backoffice**:
   Navigate to `http://localhost:44306/umbraco` to access the Umbraco backoffice.

7. **Use the API**:
   Use a tool like Postman or similar to access the Web API endpoints:
   - **Create Document Type**: `POST http://localhost:5147/document-types` 
   Sample payload: 
   ```{
      "alias": "This is a note",
      "name": "This is a note",
      "description": "This is the description",
      "icon": "icon-notepad",
      "allowedAsRoot": true,
      "variesByCulture": false,
      "variesBySegment": false,
      "collection": null,
      "isElement": true
      }
      ```
   - **Delete Document Type**: `DELETE http://localhost:5147/document-types/4ec86f73-a448-45d5-b113-b737cb279cf2`. If the resource exists then it will return a 204 status code, otherwise it will return 404

   - **Check CMS Health**: `GET http://localhost:5147/healthcheck` the response should look like this: 
   ```{
      "items": [
         {
            "name": "Configuration"
         },
         {
            "name": "Data Integrity"
         },
         {
            "name": "Live Environment"
         },
         {
            "name": "Permissions"
         },
         {
            "name": "Security"
         },
         {
            "name": "Services"
         }
      ],
      "total": 6
      }
   ```

## Development

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download) (version compatible with your 
projects)
- [Valid SSL Certificade](https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-9.0&tabs=visual-studio%2Clinux-sles#troubleshoot-certificate-problems-such-as-certificate-not-trusted) Refer to this page to fix any SSL issue

## Acknowledgments

- [Umbraco CMS](https://umbraco.com/)
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
- [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire)
