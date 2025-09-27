using System;
using System.Collections.Generic;
using System.Net;
using Pulumi;
using Pulumi.AzureNative.App;
using Pulumi.AzureNative.App.Inputs;
using Pulumi.AzureNative.ContainerRegistry;
using ContainerArgs = Pulumi.AzureNative.App.Inputs.ContainerArgs;
using Deployment = Pulumi.Deployment;
using SecretArgs = Pulumi.AzureNative.App.Inputs.SecretArgs;


return await Deployment.RunAsync(async () =>
{
    var stackName = Deployment.Instance.StackName;

    var containerRegistryStackRef = new StackReference($"ritasker/Azure.ContainerRegistry/{stackName}");
    var containerRegistryLoginServerUrl = containerRegistryStackRef.RequireOutput("registry-login-server").Apply(s => (string)s);
    var containerRegistryResourceGroup = containerRegistryStackRef.RequireOutput("rg-name").Apply(s => (string)s);
    var containerRegistryName = containerRegistryStackRef.RequireOutput("registry").Apply(s => (string)s);
    
    var credentials = Output.Tuple(containerRegistryResourceGroup, containerRegistryName).Apply(async t =>
        await ListRegistryCredentials.InvokeAsync(new ListRegistryCredentialsArgs
        {
            ResourceGroupName = t.Item1,
            RegistryName = t.Item2
        }));

    var containerAppStackRef = new StackReference($"ritasker/Azure.ContainerApps/{stackName}");
    var mgdEnvId = containerAppStackRef.RequireOutput("Id").Apply(s => (string)s);
    var resourceGroupName = $"container-apps-{stackName}";

    var imageTag = Environment.GetEnvironmentVariable("CI_COMMIT_SHORT_SHA");
    var containerImage = Output.Format($"{containerRegistryLoginServerUrl}/tower-of-delusion:{imageTag}");
    
    var containerApp = new ContainerApp("tower-of-delusion", new ContainerAppArgs
        {
            ContainerAppName = "tower-of-delusion",
            ResourceGroupName = resourceGroupName,
            ManagedEnvironmentId = mgdEnvId,
            Configuration = new ConfigurationArgs
            {
                Ingress = new IngressArgs
                {
                    External = true,
                    TargetPort = 4000,
                    Traffic = new TrafficWeightArgs
                    {
                        Weight = 100,
                        LatestRevision = true
                    }
                },
                Registries =
                {
                    new RegistryCredentialsArgs
                    {
                        Server = containerRegistryLoginServerUrl,
                        Username = credentials.Apply(c => c.Username),
                        PasswordSecretRef = "pwd"
                    }
                },
                Secrets =
                {
                    new SecretArgs
                    {
                        Name = "pwd",
                        Value = credentials.Apply(c => c.Passwords[0].Value)
                    }
                }
            },
            Template = new TemplateArgs
            {
                RevisionSuffix = imageTag,
                Containers =
                {
                    new ContainerArgs
                    {
                        Name = "tower-of-delusion",
                        Image = containerImage,
                        Env = {  new EnvironmentVarArgs
                        {
                            Name = "ASPNETCORE_URLS",
                            Value = $"http://{IPAddress.Any}:4000"
                        } },
                        Resources = new ContainerResourcesArgs
                        {
                            Cpu = 2,
                            Memory = "4.0Gi"
                        }
                    }
                }
            }
        },
        new CustomResourceOptions
        {
            CustomTimeouts = new CustomTimeouts
            {
                Create = TimeSpan.FromMinutes(20),
                Update = TimeSpan.FromMinutes(20)
            }
        });
    
    return new Dictionary<string, object?>
    {
        ["url"] = Output.Format($"https://{containerApp.Configuration.Apply(c => c.Ingress).Apply(i => i.Fqdn)}")
    };
});