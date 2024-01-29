#  FinancialDocumentRetrieval API 

## API Descrioton
This project is an education purpose application, based on Web API .NET Core project with N-tier architecture.
Receive request with three parameters, validate Access token, check if all requested data is whitelisted, and retrieve response with partly anonymizated sensitive data base on configuration.
The focus is on building a maintainable, scalable WEB API using recommended coding practices and principles. Unit and integration tests are also added.

## Instructions for starting/using project
### SQL Server databese cofig

-Use Last migration to create all DB SQL tables

-Insert test data into DB with queries from attached 'SqlServerInsertQueries.txt' file

### Invoking API endpoint config

-Export attached 'FinancialDocumentRetrieval.Api.postman_collection.json' file to create Postmen collection for testing FinancialDocumentRetrieval API 

-Register user with
https://localhost:7264/api/Account/register

-Login user with 
https://localhost:7264/api/Account/login

-Populate Barear token and create json raw body request with data from DB https://localhost:7264/api/FinancialDocument/get-financial-document

## Potential future improvements
     
-Add Anonymization Config to SQL Table or json.conf file
-Insert all initial test data OnModelCreating
-To cover all integration/unit/end-to-end tests
-Change IRepositoryInitUnitOfWork interface to fit Interface Segregation principle
-To upgrade to a new version of .NET
