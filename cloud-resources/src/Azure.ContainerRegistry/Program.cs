using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.ContainerRegistry;
using Pulumi.AzureNative.Resources;
using Config = Pulumi.Config;
using AzureNative = Pulumi.AzureNative;

return await Pulumi.Deployment.RunAsync(() =>
{
    var config = new Config("azure-native");
    var location = config.Require("location");

    var resourceGroup = new ResourceGroup("resourceGroup", new ResourceGroupArgs
    {
        ResourceGroupName = "rg-pier8-production",
        Location = location
    });

    var containerRegistry = new Registry("registry", new RegistryArgs
    {
        AdminUserEnabled = true,
        AnonymousPullEnabled = false,
        RegistryName = "pier8",
        ResourceGroupName = resourceGroup.Name,
        Location = resourceGroup.Location,
        Sku = new AzureNative.ContainerRegistry.Inputs.SkuArgs
        {
            Name = SkuName.Basic
        }
    }, new CustomResourceOptions
    {
        CustomTimeouts = new CustomTimeouts
        {
            Create = TimeSpan.FromMinutes(5)
        }
    });
    
    var credentials = Output.Tuple(resourceGroup.Name, containerRegistry.Name).Apply(items =>
        ListRegistryCredentials.InvokeAsync(new ListRegistryCredentialsArgs
        {
            ResourceGroupName = items.Item1,
            RegistryName = items.Item2
        }));
    
    var adminUsername = credentials.Apply(c => c.Username);
    var adminPassword = credentials.Apply(c => c.Passwords[0].Value);

    return new Dictionary<string, object?>
    {
        ["rg-name"] = resourceGroup.Name,
        ["registry"] = containerRegistry.Name,
        ["registry-login-server"] = containerRegistry.LoginServer,
        ["registry-admin-uname"] = adminUsername,
        ["registry-admin-pwd"] = adminPassword,
    };
});