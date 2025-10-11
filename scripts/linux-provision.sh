#!/bin/bash
# Linux Provisioning Script for Lost & Found Web Platform
# This script sets up .NET 8, BaGet, and deploys the application

echo "Starting Linux provisioning for Lost & Found Web Platform..."

# Update package list
sudo apt-get update

# Install prerequisites
echo "Installing prerequisites..."
sudo apt-get install -y wget curl apt-transport-https software-properties-common

# Install .NET 8 SDK
echo "Installing .NET 8 SDK..."
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Install Docker
echo "Installing Docker..."
sudo apt-get install -y docker.io
sudo systemctl start docker
sudo systemctl enable docker
sudo usermod -aG docker $USER

# Install Git
echo "Installing Git..."
sudo apt-get install -y git

# Start BaGet using Docker
echo "Starting BaGet NuGet server..."
sudo docker run -d --name baget -p 5555:80 -v baget-data:/var/baget/packages -v baget-config:/var/baget/config loicsharma/baget:latest

# Wait for BaGet to start
sleep 10

# Configure NuGet to use BaGet
echo "Configuring NuGet to use BaGet..."
dotnet nuget add source http://localhost:5555/v3/index.json --name "BaGet" --username "admin" --password "NUGET-SERVER-API-KEY" --store-password-in-clear-text

# Create application directory
APP_DIR="/home/vagrant/LostFoundApp"
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

app.MapGet("/", () => "Lost & Found Web Platform - Linux Deployment Successful!");

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

echo "Linux provisioning completed successfully!"
echo "Application is running at:"
echo "  HTTP: http://localhost:5000"
echo "  HTTPS: https://localhost:5001"
echo "  BaGet: http://localhost:5555"
