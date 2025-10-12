#!/bin/bash
# Linux Provisioning Script for Lost & Found Web Platform
# FINAL VERSION: This script correctly PUBLISHES and runs the Linux version of the app.

echo "Starting Linux provisioning for Lost & Found Web Platform..."

# Update package list
sudo apt-get update -y

# Install prerequisites
echo "Installing prerequisites..."
sudo apt-get install -y wget curl apt-transport-https software-properties-common

# Install .NET 8 SDK
echo "Installing .NET 8 SDK..."
if ! command -v dotnet &> /dev/null
then
    wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb
    sudo apt-get update -y
    sudo apt-get install -y dotnet-sdk-8.0
else
    echo ".NET SDK is already installed."
fi

# Install Docker
echo "Installing Docker..."
if ! command -v docker &> /dev/null
then
    sudo apt-get install -y docker.io
    sudo systemctl start docker
    sudo systemctl enable docker
    sudo usermod -aG docker vagrant
else
    echo "Docker is already installed."
fi

# Start BaGet using Docker
echo "Starting BaGet NuGet server..."
if [ ! "$(sudo docker ps -q -f name=baget)" ]; then
    if [ "$(sudo docker ps -aq -f status=exited -f name=baget)" ]; then
        sudo docker rm baget
    fi
    sudo docker run -d --name baget -p 5555:80 --restart always loicsharma/baget:latest
else
    echo "BaGet container is already running."
fi

# --- FINAL CORRECTED LOGIC ---
# Publishing the application specifically for Linux

PROJECT_DIR="/vagrant/lost_found_web"
PUBLISH_DIR="/home/vagrant/app_publish"

if [ -d "$PROJECT_DIR" ]; then
  echo "Project directory found. Publishing for Linux..."
  
  # Publish the project for the linux-x64 runtime.
  # This creates a self-contained, optimized version of the app.
  dotnet publish "$PROJECT_DIR/lost_found_web.csproj" --configuration Release --runtime linux-x64 --output "$PUBLISH_DIR"
  
  echo "Application published to $PUBLISH_DIR."
  
  # Run the published application from its own directory
  cd "$PUBLISH_DIR"
  export ASPNETCORE_ENVIRONMENT=Development
  nohup ./lost_found_web --urls "http://0.0.0.0:5000;https://0.0.0.0:5001" > /home/vagrant/app.log 2>&1 &
  
  echo "Linux provisioning completed successfully!"
  echo "Application is running. Check logs at /home/vagrant/app.log"
  echo "Access it from your HOST machine at: http://localhost:8080"
  echo "BaGet is available at: http://localhost:5556"

else
  echo "ERROR: Project directory not found at $PROJECT_DIR!"
fi