using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.AzureNative.App;
using Pulumi.AzureNative.App.Inputs;
using Pulumi.AzureNative.OperationalInsights.Inputs;
using Pulumi.AzureNative.Resources;
using Insights = Pulumi.AzureNative.OperationalInsights;

return await Pulumi.Deployment.RunAsync(() =>
{
    var stackName = Pulumi.Deployment.Instance.StackName;
    var resourceGroupName = $"container-apps-{stackName}";

    var config = new Config("container-apps");
    var environment = config.Require("environment");

    var resourceGroup = new ResourceGroup("resourceGroup", new ResourceGroupArgs
    {
        ResourceGroupName = resourceGroupName,
        Tags = { { "environment", environment } }
    });

    var workspaceName = $"{environment}-logs";
    var workspace = new Insights.Workspace(workspaceName, new Insights.WorkspaceArgs
    {
        WorkspaceName = workspaceName,
        ResourceGroupName = resourceGroup.Name,
        Sku = new WorkspaceSkuArgs { Name = "PerGB2018" },
        RetentionInDays = config.RequireInt32("logs-retention-days"),
        Tags = { { "environment", environment } }
    });

    var sharedKeys = Output.Tuple(resourceGroup.Name, workspace.Name)
        .Apply(items => Insights.GetSharedKeys.InvokeAsync(
            new Insights.GetSharedKeysArgs
            {
                ResourceGroupName = items.Item1,
                WorkspaceName = items.Item2
            }));

    var managedEnvironment = new ManagedEnvironment(
        environment,
        new ManagedEnvironmentArgs
        {
            EnvironmentName = environment,
            ResourceGroupName = resourceGroup.Name,
            ZoneRedundant = false,
            AppLogsConfiguration = new AppLogsConfigurationArgs
            {
                Destination = "log-analytics",
                LogAnalyticsConfiguration = new LogAnalyticsConfigurationArgs
                {
                    CustomerId = workspace.CustomerId,
                    SharedKey = sharedKeys.Apply(r => r.PrimarySharedKey)!
                }
            },
            Tags = { { "environment", environment } }
        },
        new CustomResourceOptions
        {
            CustomTimeouts = new CustomTimeouts
            {
                Create = TimeSpan.FromMinutes(5),
                Update = TimeSpan.FromMinutes(5),
                Delete = TimeSpan.FromMinutes(5)
            }
        });

    return new Dictionary<string, object?>
    {
        ["mgd-env-id"] = managedEnvironment.Id,
        ["mgd-env-name"] = managedEnvironment.Name,
        ["mgd-env-ip"] = managedEnvironment.StaticIp,
        ["aca-rg-name"] = resourceGroup.Name
    };
});