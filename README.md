# Blogging Platform API

## Overview

This project is a blogging platform API developed with .NET Core, MS SQL, and Entity Framework. It supports user account creation, blog post management, commenting, and user following.


## Technologies
- **Backend**: .NET Core
- **Database**: MS SQL
- **ORM**: Entity Framework Core
- **Architecture**: Clean Architecture
- **CQRS**: Command Query Responsibility Segregation
- **Authentication**: JWT (JSON Web Tokens)


## Getting Started

### Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 8.0)
- [MS SQL Server](https://www.microsoft.com/en-us/sql-serversql-server-downloads) or compatible database

### Setup

1. **Clone the Repository:**
    ```bash
    git clone https://github.com/nevein25/Blogging
    cd blogging
    ```

2. **Configure the Connection String**
    - Update the `appsettings.json` file with your database connection string.

3. **Run the Application:**
    - The database is automatically **migrated** and **seeded** when the project runs.
      ```bash
      dotnet run
      ```
    - The API will be available at `https://localhost:7125`

## Testing the API
- You can test the API using the following test user:

- **Username**: JohnDoe
- **Password**: TEST@test123

Use these credentials to log in and test various endpoints. The test user will have some sample data associated with it due to the automatic seeding process.