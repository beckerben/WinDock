# Windows Docker Container Tests

This repo contains a simple .NET 4.8 console application which will run indefinitely outputting text to the console.  It accepts an environment variable and is also setup to pull an environment from an application configuration file to demonstrate cases where the config can be bind mounted into the volume or could be referenced via an environment variable.  This example shows it as an environment variable.

In order to run this, I created a Windows server 2022 VM on Azure as the sandbox and installed the docker engine using the Docker CE / Moby engine as outlined in this [article](https://learn.microsoft.com/en-us/virtualization/windowscontainers/quick-start/set-up-environment?tabs=dockerce&utm_source=pocket_saves#windows-server-10). I also installed portainer following these [instructions](https://docs.portainer.io/v/2.15/start/install/server/docker/wcs).

I originally tried using Docker desktop in the VM and was going to have it use windows containers but the docker engine would always hang and eventually error out so I went the moby route which is what is used at the client for Linux containers.

To build and run the image, the following command can be run:

``` sh
docker build --pull --rm -f "Dockerfile" -t windockerpoc:latest "." 
```

This will build the docker image with the simple console application.

I normally would use portainer to create the container, but I was unable to get the creation to work for a windows container since it looked like the capabilities it adds to the command behind the scenes are not compatible with the windows container host.  I even tried disabling them.  I followed up in the portainer slack channel and the support team indicate that this may be a bug with windows server 2022 and they will investigate further.

I did find once the container is created, the logs can be viewed and the container started, stopped, and console attached from portainer, I just can't use the re-create feature which comes in handy for making changes.  The command line has to be used to create the container, at least in my environment.

To create the container, the following command can be used:

``` sh
docker container stop windocker
docker container rm windocker
docker run -d --name windocker --env-file env_file windockerpoc:latest
```

There are three commands here:
1. stops the container (ignore errors if not exists)
1. removes the container (ignore errors if not exists)
1. re-creates and starts a container named "windocker" that will run headless and will set two environment variables stored in the "env_file"
