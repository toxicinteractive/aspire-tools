#!/usr/bin/env bash

# this is run on "postCreateCommand" in the container AFTER the container is CREATED for the first time
# https://containers.dev/implementors/json_reference/

DEVC_DIR=./.devcontainer

# if the image doesn't load ip_tables modules dockerd will fail to launch (docker-in-docker)
# https://github.com/devcontainers/features/issues/1235
echo "Updating iptables"
sudo update-alternatives --set iptables /usr/sbin/iptables-nft

# create a dev https cert if a custom cert is not already present
if ! [ -f $DEVC_DIR/localhost.pfx ]; then
  echo "Creating default ASP.NET dev cert"
  dotnet dev-certs https --trust
  dotnet dev-certs https -ep $DEVC_DIR/localhost.pfx -p 'changeit'
fi

# copy and trust any custom cert to the ca store
if [ -f $CUSTOM_HOST_CA ]; then
  echo "Trusting custom CA cert $CUSTOM_HOST_CA"
  sudo cp $CUSTOM_HOST_CA /usr/local/share/ca-certificates/
  sudo update-ca-certificates
fi

mkdir -p $AZURE_CONFIG_DIR
sudo chown $USER:$USER $AZURE_CONFIG_DIR

dotnet new install Aspire.ProjectTemplates@13.1.0
dotnet tool install -g Aspire.Cli --version 13.1.0
