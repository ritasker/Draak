using CliWrap.Exceptions;
using TowerOfDelusion.Build;
using static TowerOfDelusion.Build.Helpers;
using static Bullseye.Targets;

var settings     = Settings.Load();
var srcDirectory = settings.RootDirectory.GetSubDirectory("src");
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
            + $"--tag {settings.DockerImageName}:local-latest "
            + $"{artifactsDirectory}/TowerOfDelusion");
    });

Target(
    "docker-login",
    dependsOn:["publish"],
    async () => { await Run("docker", $"login -u {settings.DockerUsername} -p {settings.DockerPassword}"); });

Target(
    "push-image",
    dependsOn:["docker-login"],
    async () => { await Run("docker", $"push {settings.DockerImageName}:{settings.CommitSha}"); });

Target("default", dependsOn: ["build", "publish", "push-image"]);

await RunTargetsAndExitAsync(args, ex => ex is CommandExecutionException);