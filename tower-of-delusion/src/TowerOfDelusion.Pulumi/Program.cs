using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.App;
using Pulumi.AzureNative.App.Inputs;
using ContainerArgs = Pulumi.AzureNative.App.Inputs.ContainerArgs;
using Deployment = Pulumi.Deployment;
using SecretArgs = Pulumi.AzureNative.App.Inputs.SecretArgs;


return await Deployment.RunAsync(() =>
{
    var stackName = Deployment.Instance.StackName;

    var containerRegistryStackRef = new StackReference($"ritasker/Azure.ContainerRegistry/{stackName}");
    var registryLoginServer = containerRegistryStackRef.GetOutput("registry-login-server");
    var registryAdminUname = containerRegistryStackRef.GetOutput("registry-admin-uname");
    var registryAdminPwd = containerRegistryStackRef.GetOutput("registry-admin-pwd");

    var containerAppStackRef = new StackReference($"ritasker/Azure.ContainerApps/{stackName}");
    var resourceGroupName = containerAppStackRef.GetOutput("aca-rg-name");
    var mgdEnvId = containerAppStackRef.GetOutput("mgd-env-id");
    
    var config = new Config("tower-of-delusion");
    var imageTag = config.Require("ImageTag");

    var containerApp = new ContainerApp("tower-of-delusion", new ContainerAppArgs
        {
            ContainerAppName = "tower-of-delusion",
            ResourceGroupName = resourceGroupName.ToString(),
            ManagedEnvironmentId = mgdEnvId.ToString(),
            Configuration = new ConfigurationArgs
            {
                Ingress = new IngressArgs
                {
                    External = true,
                    TargetPort = 80,
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
                        Server = registryLoginServer.ToString(),
                        Username = registryAdminUname.ToString(),
                        PasswordSecretRef = "pwd"
                    }
                },
                Secrets =
                {
                    new SecretArgs
                    {
                        Name = "pwd",
                        Value = registryAdminPwd.ToString()
                    }
                }
            },
            Template = new TemplateArgs
            {
                Containers =
                {
                    new ContainerArgs
                    {
                        Name = "tower-of-delusion",
                        Image = $"{registryLoginServer}/tower-of-delusion:{imageTag}",
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
                Update = TimeSpan.FromMinutes(5)
            }
        });
    
    return new Dictionary<string, object?>
    {
        ["url"] = Output.Format($"https://{containerApp.Configuration.Apply(c => c.Ingress).Apply(i => i.Fqdn)}")
    };
});