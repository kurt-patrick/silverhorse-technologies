## Installation instructions:

git clone https://github.com/kurt-patrick/silverhorse-technologies.git  
Navigate to silverhorse-technologies\dotnet-backend\SilverHorseTech.Net.BackEnd  
Open SilverHorseTech.Net.BackEnd.sln  
In visual studio in Solution Explorer right click on SilverHorseTech.Net.BackEnd and Restore NuGet Packages  
Then click on Build > Build Solution  
2 ways to run the webapi  
1. Self hosted  
The WebApi itself can be run from the console project SilverHorseTech.WebApi.SelfHost  
Make sure the console project has been set as the startup project  
Press the Start button or F5 to load the console project (you should see a console window)  
2. Using IIS Express and your web browser  
If you make SilverHorseTech.WebApi the startup project you can Press the Start button or F5 to load  
If you use the browser and iis then you will need to wait for the browser to load so you can see which port to run on  

## Postman Testing  

Add the headers:  
- Content-Type=application/json  
- Authorization=Bearer af24353tdsfw  
  
NOTE: If the header Authorization is not added, or contains the wrong value a 501 error will be returned  
  
Perform a GET against any of the following to confirm the system is working  
http://localhost:12345/api/posts  
http://localhost:12345/api/users  
http://localhost:12345/api/albums  
http://localhost:12345/api/collection  
  
/posts also supports PUT, POST, DELETE  
  
  
## Unit Testing  
Unit tests have been added for all controllers (posts, users, albums, collection)  
As well as Authorization header validation  
To run unit tests, make sure the project SilverHorseTech.WebApi.Tests has been built  
In Test Explorer you should see the unit tests, right click and Run  
You can also open a test class and right click Run Test(s)  
