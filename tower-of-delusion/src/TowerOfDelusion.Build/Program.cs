using CliWrap.Exceptions;
using TowerOfDelusion.Build;
using static TowerOfDelusion.Build.Helpers;
using static Bullseye.Targets;

var settings     = Settings.Load();
var srcDirectory = settings.RootDirectory.GetSubDirectory(Path.Combine("tower-of-delusion","src"));
var artifactsDirectory = settings.RootDirectory.GetSubDirectory("artifacts");

Target(
    "sort-refs",
    async () => { await Run("dotnet-sort-refs", "-i"); });

Target(
    "build",
    dependsOn: ["sort-refs"],
    async () => { await Run("dotnet", $"build {srcDirectory}/TowerOfDelusion -c Release"); });

Target(
    "publish",
    async () =>
    {
        await Run("dotnet", $"publish {srcDirectory}/TowerOfDelusion -c Release -o {artifactsDirectory}/TowerOfDelusion");
        await Run(
            "docker",
            $"build -f {artifactsDirectory}/TowerOfDelusion/Dockerfile "
            + $"--tag {settings.DockerImageName}:{settings.CommitSha} "
            + $"--tag {settings.DockerImageName}:latest "
            + $"{artifactsDirectory}/TowerOfDelusion");
    });

Target(
    "push-image",
    dependsOn:["publish"],
    async () => { await Run("docker", $"push {settings.DockerImageName}:{settings.CommitSha}"); });

Target("default", dependsOn: ["build", "publish", "push-image"]);

await RunTargetsAndExitAsync(args, ex => ex is CommandExecutionException);