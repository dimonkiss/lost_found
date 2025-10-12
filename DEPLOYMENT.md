# Lost & Found Web Platform - Deployment Guide

This guide explains how to package the Lost & Found Web Platform as a NuGet package, set up a private NuGet repository using BaGet, and deploy the application across different operating systems using Vagrant.

## Prerequisites

- **Vagrant** installed on your system
- **VirtualBox** or compatible virtualization software
- **Git** for version control
- **PowerShell** (Windows) or **Bash** (Linux/macOS)

## 1. NuGet Package Configuration

The application is configured to be packaged as a NuGet package with the following properties:

- **Package ID**: `LostFoundWeb`
- **Version**: `1.0.0`
- **Target Framework**: `.NET 8.0`
- **Authors**: University Platform Team
- **Description**: A web application for university "Lost & Found" platform

### Building the NuGet Package

```powershell
# Navigate to the project root
cd lost_found

# Run the build and package script
.\scripts\build-and-package.ps1
```

This will create:
- NuGet package in `packages/` directory
- Deployment files in `deploy/` directory

## 2. BaGet Private NuGet Repository

BaGet is configured as a private NuGet repository with the following settings:

- **Port**: 5555 (configurable in Vagrantfile)
- **API Key**: `NUGET-SERVER-API-KEY`
- **Storage**: File system based
- **Database**: SQLite

### BaGet Configuration

The BaGet configuration is stored in `baget-config.json`:

```json
{
  "ApiKey": "NUGET-SERVER-API-KEY",
  "PackageDeletionBehavior": "Unlist",
  "AllowPackageOverwrites": false,
  "Database": {
    "Type": "Sqlite",
    "ConnectionString": "Data Source=baget.db"
  },
  "Storage": {
    "Type": "FileSystem",
    "Path": "/var/baget/packages"
  }
}
```

## 3. Vagrant Multi-OS Deployment

The Vagrantfile supports deployment on three operating systems:

### Supported Operating Systems

1. **Windows Server 2022** (`vagrant up windows`)
   - IP: 192.168.56.10
   - Ports: 5000 (HTTP), 5001 (HTTPS), 5555 (BaGet)

2. **Ubuntu 22.04 LTS** (`vagrant up linux`)
   - IP: 192.168.56.11
   - Ports: 8080 (HTTP), 8081 (HTTPS), 5556 (BaGet)

3. **macOS Ventura** (`vagrant up macos`)
   - IP: 192.168.56.12
   - Ports: 5003 (HTTP), 5004 (HTTPS), 5557 (BaGet)

### Deployment Commands

```bash
# Deploy to Windows
vagrant up windows

# Deploy to Linux
vagrant up linux

# Deploy to macOS
vagrant up macos

# Deploy to all systems
vagrant up

# Access a specific system
vagrant ssh windows
vagrant ssh linux
vagrant ssh macos

# Destroy all systems
vagrant destroy
```

## 4. Provisioning Process

Each operating system has its own provisioning script that:

1. **Installs .NET 8 SDK**
2. **Installs Docker** (for BaGet)
3. **Starts BaGet NuGet server**
4. **Configures NuGet to use BaGet**
5. **Creates and runs the application**

### Provisioning Scripts

- `scripts/windows-provision.ps1` - Windows PowerShell script
- `scripts/linux-provision.sh` - Linux Bash script
- `scripts/macos-provision.sh` - macOS Bash script

## 5. Application Access

After successful deployment, the application will be accessible at:

### Windows Deployment
- **HTTP**: http://192.168.56.10:5000
- **HTTPS**: https://192.168.56.10:5001
- **BaGet**: http://192.168.56.10:5555

### Linux Deployment
- **HTTP**: http://192.168.56.11:8080
- **HTTPS**: https://192.168.56.11:8081
- **BaGet**: http://192.168.56.11:5556

### macOS Deployment
- **HTTP**: http://192.168.56.12:5003
- **HTTPS**: https://192.168.56.12:5004
- **BaGet**: http://192.168.56.12:5557

## 6. Package Deployment to BaGet

To deploy your NuGet package to BaGet:

```powershell
# Build and package the application
.\scripts\build-and-package.ps1

# Deploy to BaGet
.\scripts\deploy-to-baget.ps1
```

## 7. Testing the Deployment

### Verify Application is Running

1. Open a web browser
2. Navigate to the appropriate URL for your deployment
3. You should see: "Lost & Found Web Platform - [OS] Deployment Successful!"

### Verify BaGet is Working

1. Navigate to the BaGet URL
2. You should see the BaGet web interface
3. Your package should be listed if deployed

### Verify NuGet Configuration

```bash
# SSH into the deployed system
vagrant ssh [windows|linux|macos]

# Check NuGet sources
dotnet nuget list source

# Install your package
dotnet add package LostFoundWeb --source http://localhost:5555/v3/index.json
```

## 8. Troubleshooting

### Common Issues

1. **Docker not starting**: Ensure Docker Desktop is installed and running
2. **Port conflicts**: Check if ports are already in use
3. **Network issues**: Verify VirtualBox network configuration
4. **Permission issues**: Run scripts with appropriate privileges

### Logs

- Application logs: Check the console output during provisioning
- BaGet logs: `docker logs baget`
- Vagrant logs: `vagrant up --debug`

## 9. Security Considerations

- Change the default BaGet API key in production
- Use HTTPS for all communications
- Implement proper authentication and authorization
- Regular security updates for all components

## 10. Performance Optimization

- Increase VM memory allocation for better performance
- Use SSD storage for faster I/O operations
- Configure proper resource limits for Docker containers
- Monitor system resources during deployment

## Support

For issues or questions regarding the deployment:

1. Check the troubleshooting section
2. Review Vagrant and VirtualBox documentation
3. Consult BaGet documentation for repository issues
4. Contact the development team for application-specific problems
