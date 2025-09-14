using Pulumi.AzureNative.Resources;
using System.Collections.Generic;
using Config = Pulumi.Config;
using AzureNative = Pulumi.AzureNative;

return await Pulumi.Deployment.RunAsync(() =>
{
    var config = new Config();
    var location = config.Require("location");
    
    var resourceGroup = new ResourceGroup("resourceGroup", new ResourceGroupArgs
    {
        ResourceGroupName = "rg-pier8-production",
        Location = location,
    });
    
    var containerRegistry = new AzureNative.ContainerRegistry.Registry("registry", new()
    {
        AdminUserEnabled = true,
        AnonymousPullEnabled = false,
        RegistryName = "pier-8",
        ResourceGroupName = resourceGroup.Name,
        Location = resourceGroup.Location,
        Sku = new AzureNative.ContainerRegistry.Inputs.SkuArgs
        {
            Name = AzureNative.ContainerRegistry.SkuName.Basic,
        },
    });
    
    return new Dictionary<string, object?>
    {
        ["rg-name"]  = resourceGroup.Name,
        ["registry"] = containerRegistry.Name
    };
});