# -*- mode: ruby -*-
# vi: set ft=ruby :

Vagrant.configure("2") do |config|
  
  # Windows 10/11 Box
  config.vm.define "windows" do |windows|
    windows.vm.box = "gusztavvargadr/windows-server-2022-standard"
    windows.vm.hostname = "lost-found-windows"
    windows.vm.network "private_network", ip: "192.168.56.10"
    windows.vm.network "forwarded_port", guest: 5000, host: 5000 # App HTTP
    windows.vm.network "forwarded_port", guest: 5001, host: 5001 # App HTTPS
    windows.vm.network "forwarded_port", guest: 5555, host: 5555 # BaGet
    
    windows.vm.provider "virtualbox" do |vb|
      vb.memory = "2048"
      vb.cpus = 2
      vb.gui = true
    end
    
    windows.vm.provision "shell", path: "scripts/windows-provision.ps1", privileged: false
  end

  # Ubuntu Linux Box
  config.vm.define "linux" do |linux|
    linux.vm.box = "ubuntu/jammy64"
    linux.vm.hostname = "lost-found-linux"
    linux.vm.network "private_network", ip: "192.168.56.11"
    
    # Corrected Port Forwarding for Linux
    linux.vm.network "forwarded_port", guest: 5000, host: 8080
    linux.vm.network "forwarded_port", guest: 5001, host: 8081
    linux.vm.network "forwarded_port", guest: 5555, host: 5556 # BaGet
    
    linux.vm.boot_timeout = 600

    linux.vm.provider "virtualbox" do |vb|
      vb.memory = "2048"
      vb.cpus = 2       
    end
    
    linux.vm.provision "shell", path: "scripts/linux-provision.sh"
  end

  # macOS Box
  config.vm.define "macos" do |macos|
    macos.vm.box = "macinbox/macos-ventura"
    macos.vm.hostname = "lost-found-macos"
    macos.vm.network "private_network", ip: "192.168.56.12"
    macos.vm.network "forwarded_port", guest: 5000, host: 5003
    macos.vm.network "forwarded_port", guest: 5001, host: 5004
    macos.vm.network "forwarded_port", guest: 5555, host: 5557 # BaGet
    
    macos.vm.provider "virtualbox" do |vb|
      vb.memory = "4096"
      vb.cpus = 2
      vb.gui = true
    end
    
    macos.vm.provision "shell", path: "scripts/macos-provision.sh", privileged: false
  end

  # Global configuration
  config.vm.provider "virtualbox" do |vb|
    vb.check_guest_additions = false
    vb.functional_vboxsf = false
  end

end