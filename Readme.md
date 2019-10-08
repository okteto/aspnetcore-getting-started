# ASP.NET Core Sample App 

This example shows how to leverage [Okteto](https://github.com/okteto/okteto) to develop a ASP.NET Core Sample App directly in Kubernetes. The ASP.NET Core Sample App is deployed using Kubernetes manifests.

Okteto works in any Kubernetes cluster by reading your local kubeconfig file. If you need easy access to a Kubernetes cluster, [Okteto Cloud](https://cloud.okteto.com) gives you free access to 4CPUs and 8Gb virtual Kubernetes clusters.

## Step 1: Deploy the ASP.NET Core Sample App

Get a local version of the ASP.NET Sample App by executing the following commands in your local terminal:

```console
$ git clone https://github.com/okteto/aspnetcore-getting-started
$ cd aspnetcore-getting-started
```

The `k8s.yml` file contains the raw Kubernetes manifests to deploy the ASP.NET Core Sample App. Run the application by executing:

```console
$ kubectl apply -f k8s.yml
```

```console
deployment.apps "hello-world" created
service "hello-world" created
```

This is cool! You typed one command and your application just runs 😎. 

## Step 2: Start your development environment in Kubernetes

With the ASP.NET Core sample application deployed, run the following command in your local terminal:

```console
$ okteto up
```

```console
 ✓  Persistent volume provisioned
 ✓  Files synchronized
 ✓  Okteto Environment activated
    Namespace: rberrelleza
    Name:      hello-world
    Forward:   5000 -> 5000

okteto>
```

The `okteto up` command starts a [Kubernetes development environment](https://okteto.com/docs/reference/development-environment/index.html), which means:

- The ASP.NET Core Sample App container is updated with the Docker image `mcr.microsoft.com/dotnet/core/sdk`. This image contains the required dev tools to build, test and run a ASP.NET Core application.
- A [file synchronization service](https://okteto.com/docs/reference/file-synchronization/index.html) is created to keep your changes up-to-date between your local filesystem and your application pods.
- Container port 5000  is forwarded to localhost.
- Start a terminal into the remote container. Build, test and run your application as if you were in your local machine and get your application logs immediately in your terminal.

All of this (and more) can be customized via the `okteto.yml` [manifest file](https://okteto.com/docs/reference/manifest/index.html).


To start the application, execute in the Okteto terminal:
```console
 dotnet watch run
```

```console
watch : Polling file watcher is enabled
watch : Started
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /usr/src/app
```

Test your application by running the command below in a local terminal:

```console
$ curl localhost:5000
```

```console
Hello world REST API!
```

## Step 3: Develop directly in Kubernetes

Open `Controllers/HelloWorldController.cs` in your favorite local IDE and modify the response message on line 25 to be *Hello world REST API from the cluster!*. Save your changes. 

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace helloworld.Controllers
{
    [ApiController]
    [Route("")]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;

        public HelloWorldController(ILogger<HelloWorldController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
             return "Hello world REST API from the cluster!";
        }
    }
}
```

Take a look at the Okteto terminal and notice how the changes are detected by `dotnet watch run` and automatically built and hot reloaded.

Call your application to validate the changes:

```console
$ curl localhost:5000
```

```console
Hello world REST API from the cluster!
```

Cool! Your code changes were instantly applied to Kubernetes. No commit, build or push required 😎!

## Step 4: Cleanup

Cancel the `okteto up` command by pressing `ctrl + c` + `ctrl + d` and run the following commands to remove the resources created by this guide: 

```console
$ okteto down
 ✓  Okteto Environment deactivated
 ✓  Persistent volume deleted
```

```console
$ kubectl delete -f k8s.yml
deployment.apps "hello-world" deleted
service "hello-world" deleted
```