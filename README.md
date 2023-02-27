# Prezzie APIs

Provides all APIS from POST, PUT, GET for users  
POST /api/users : create user with email, password, and displayName properties  
PUT /api/users/{userID} : update specific user DisplayName & password based on user id  
GET /api/users : List all users & filter based on email and pagination  

## 🔨 Development and Testing
  
This project is build using ASP.NET Core 6  
It require mySQL to store all data in database
  
##  Assumptions
ApiSettings to access databas should be stored in Key vault for security reason. 
On this demo, we are using app settings due to server limitation (only for local environment)


## ⚙️ Deploying
1. Download & Unzip your zip file
2. Run your Visual Studio 2022  
3. Create your sql database in your local env the creds in appsettings.json, you can modify it to suit what you need
4. Install-Package Microsoft.EntityFrameworkCore.Tools  
5. Open your Package Manager Console  
6. Run this "Add-Migration InitialCreate"
7. Run this "Update-Database"  
8. Run IIS Express  

## 🔨 What can be improve
1. Add filter by ascending and descending  
2. Add user check based on email before adding more user to avoid duplication assume email is unique
3. Implement HMAC validation for security reason
4. Make a generic ErrorHandling for Apis
5. Add more unit tests to cover all scenarios

## ✏️ Authors / Maintainers
 Ivan Tjokrodinata (ivantjokro@gmail.com)
