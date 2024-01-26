 			
	---SQL Server databese cofig---

-Use Last migration to create all DB SQL tables

-Insert test data into DB with queries from attached 'SqlServerInsertQueries.txt' file

	---Invoking API endpoint config---

-Export attached 'FinancialDocumentRetrieval.Api.postman_collection.json' file to create Postmen collection for testing FinancialDocumentRetrieval API 

Register user with
https://localhost:7264/api/Account/register

Login user with 
https://localhost:7264/api/Account/login

Populate Barear token and create json raw body request with data from DB https://localhost:7264/api/FinancialDocument/get-financial-document


      ---Potential future improvements---
     
-Add Anonymization Config to SQL Table or json.conf file
-Insert all test data OnModelCreating
-To cover all integration/unit/end-to-end tests
-To upgrade to a new version of .NET
