# Define variables
param(
	[string]$buildConfiguration = "Release",
	[int]$port = 5000
)

$projectFile = "LeaveTimes.API.csproj"

# Restore dependencies
Write-Host "Restoring dependencies..."
dotnet restore

# Build the project
Write-Host "Building the project..."
dotnet build -c $buildConfiguration

# Publish the project
Write-Host "Publishing the project..."
$publishPath = "$buildConfiguration\net8.0\publish"
dotnet publish -c $buildConfiguration -o $publishPath

# Run the application
Write-Host "Running the application..."
Set-Location -Path $publishPath
dotnet LeaveTimes.API.dll --urls https://localhost:$port

Write-Host "Application is running on https://localhost:$port"