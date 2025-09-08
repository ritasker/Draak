using Pulumi;
using Pulumi.AzureNative.Resources;

namespace TowerOfDelusion.Pulumi;

public class AppStack : Stack
{
    public AppStack()
    {
        var resourceGroup = new ResourceGroup("rg-delusion-prod");
    }
}