# Stock API

Stock API requires .Net 5 installation on your machine.

# Build and Run on Windows

## Build and Run in Visual Studio 2019 Community Edition
- git clone https://github.com/molnattila991/stock-api.git
- Open the Solution
- Run project with IIS Express

## Build and Run in CMD
- git clone https://github.com/molnattila991/stock-api.git
- Go into src folder (.\stock-api\src)
- Give the following commands
    - dotnet restore
    - dotnet build -c Release -o ./build
    - dotnet .\build\stock-api.dll
- Open in browser http://localhost:5000/swagger