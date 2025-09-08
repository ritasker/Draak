using CliWrap.Exceptions;
using static TowerOfDelusion.Build.Helpers;
using static Bullseye.Targets;

Target(
    "sort-refs",
    async () => { await Run("dotnet-sort-refs", "-i"); });

Target(
    "build",
    dependsOn: ["sort-refs"],
    async () => { await Run("dotnet", "build ..\\TowerOfDelusion\\TowerOfDelusion.csproj -c Release"); });

Target("default", dependsOn: ["build"]);

await RunTargetsAndExitAsync(args, ex => ex is CommandExecutionException);