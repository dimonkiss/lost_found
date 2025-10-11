#!/bin/bash
# macOS Provisioning Script for Lost & Found Web Platform
# This script sets up .NET 8, BaGet, and deploys the application

echo "Starting macOS provisioning for Lost & Found Web Platform..."

# Install Homebrew if not present
if ! command -v brew &> /dev/null; then
    echo "Installing Homebrew..."
    /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
fi

# Install .NET 8 SDK
echo "Installing .NET 8 SDK..."
brew install --cask dotnet-sdk

# Install Docker Desktop
echo "Installing Docker Desktop..."
brew install --cask docker

# Install Git
echo "Installing Git..."
brew install git

# Start Docker Desktop (this might require manual intervention)
echo "Please start Docker Desktop manually if it's not already running..."

# Wait for Docker to be available
echo "Waiting for Docker to start..."
sleep 30

# Start BaGet using Docker
echo "Starting BaGet NuGet server..."
docker run -d --name baget -p 5555:80 -v baget-data:/var/baget/packages -v baget-config:/var/baget/config loicsharma/baget:latest

# Wait for BaGet to start
sleep 10

# Configure NuGet to use BaGet
echo "Configuring NuGet to use BaGet..."
dotnet nuget add source http://localhost:5555/v3/index.json --name "BaGet" --username "admin" --password "NUGET-SERVER-API-KEY" --store-password-in-clear-text

# Create application directory
APP_DIR="/Users/vagrant/LostFoundApp"
mkdir -p $APP_DIR

# Create a simple test application to demonstrate the deployment
echo "Setting up application directory..."
cat > $APP_DIR/Program.cs << 'EOF'
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.MapGet("/", () => "Lost & Found Web Platform - macOS Deployment Successful!");

app.Run();
EOF

# Create project file
cat > $APP_DIR/LostFoundApp.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk.Web">
    <PropertyGroup>
        <TargetFramework>net8.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>
</Project>
EOF

# Build and run the application
cd $APP_DIR
echo "Building and running Lost & Found application..."
dotnet build
nohup dotnet run --urls "http://localhost:5000;https://localhost:5001" > app.log 2>&1 &

echo "macOS provisioning completed successfully!"
echo "Application is running at:"
echo "  HTTP: http://localhost:5000"
echo "  HTTPS: https://localhost:5001"
echo "  BaGet: http://localhost:5555"
