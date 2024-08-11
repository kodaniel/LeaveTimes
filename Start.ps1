# Define variables
param(
	[string]$buildConfiguration = "Release",
	[int]$port = 5000,
	[string]$environment = "Production"
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

$env:ASPNETCORE_URLS="http://localhost:$port"
$env:ASPNETCORE_ENVIRONMENT=$environment

dotnet LeaveTimes.API.dll

Write-Host "Application is running on http://localhost:$port"