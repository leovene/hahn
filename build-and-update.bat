@echo off

:: Set up variables
set SOLUTION_PATH=.\hahn.sln
set INFRASTRUCTURE_PROJECT=src\Hahn.Infrastructure
set STARTUP_PROJECT=src\Hahn.WebApi

:: Step 1: Clean the solution
echo Cleaning the solution...
dotnet clean %SOLUTION_PATH%
if %errorlevel% neq 0 (
    echo Failed to clean the solution.
    exit /b %errorlevel%
)

:: Step 2: Build the solution
echo Building the solution...
dotnet build %SOLUTION_PATH% --no-restore
if %errorlevel% neq 0 (
    echo Failed to build the solution.
    exit /b %errorlevel%
)

:: Step 3: Restore dependencies
echo Restoring dependencies...
dotnet restore %SOLUTION_PATH%
if %errorlevel% neq 0 (
    echo Failed to restore dependencies.
    exit /b %errorlevel%
)

:: Step 4: Apply migrations
echo Applying migrations to the database...
dotnet ef database update --project %INFRASTRUCTURE_PROJECT% --startup-project %STARTUP_PROJECT%
if %errorlevel% neq 0 (
    echo Failed to apply migrations.
    exit /b %errorlevel%
)

echo All steps completed successfully!
pause
