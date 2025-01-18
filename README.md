How to Setup and Run the Project

Follow the steps below to set up and run the project:

Prerequisites

Node.js: Install version 22.13.0 or compatible.

.NET SDK: Ensure you have the latest .NET SDK installed.

Backend Setup

Navigate to the solution directory:

cd path/to/solution

Clean, restore, and build the backend:

dotnet clean hahn.sln
dotnet restore hahn.sln
dotnet build hahn.sln

Ensure the database connection string in appsettings.json is correctly configured for your environment.

Run the script to create or update the database:

build-and-update.bat

This script will apply migrations and set up the database structure.

Running the Worker and API

Start the Worker service:

cd src/Hahn.Worker
dotnet run

In a new terminal, start the API service:

cd src/Hahn.WebApi
dotnet run

Frontend Setup

Navigate to the frontend directory:

cd src/Hahn.Frontend

Install dependencies:

npm install

Run the frontend:

npm run serve

Access the application in your browser at:

http://localhost:8080

Notes

Ensure all required dependencies are installed before running the project.

If any issues occur, verify the configurations in appsettings.json and ensure your database is accessible.