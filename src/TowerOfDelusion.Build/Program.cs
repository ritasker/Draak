using CliWrap.Exceptions;
using TowerOfDelusion.Build;
using static TowerOfDelusion.Build.Helpers;
using static Bullseye.Targets;

var settings     = Settings.Load();
var srcDirectory = settings.RootDirectory.GetSubDirectory("src");

Target(
    "sort-refs",
    async () => { await Run("dotnet-sort-refs", "-i"); });

Target(
    "build",
    dependsOn: ["sort-refs"],
    async () => { await Run("dotnet", $"build {srcDirectory}/TowerOfDelusion -c Release"); });

Target("default", dependsOn: ["build"]);

await RunTargetsAndExitAsync(args, ex => ex is CommandExecutionException);