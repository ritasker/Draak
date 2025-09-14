using Pulumi.AzureNative.Resources;
using System.Collections.Generic;
using Pulumi;

return await Pulumi.Deployment.RunAsync(() =>
{
    var stackName = Pulumi.Deployment.Instance.StackName;
    var config = new Config();
    var location = config.Require("location");
    var resourceGroup = new ResourceGroup("resourceGroup");

    return new Dictionary<string, object?>
    {
    };
});