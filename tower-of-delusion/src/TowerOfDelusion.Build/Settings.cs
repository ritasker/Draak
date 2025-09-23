using Microsoft.Extensions.Configuration;

namespace TowerOfDelusion.Build;

public class Settings
{
    private const string DefaultCommitSha = "00000000";
    public string CommitSha { get; set; } = DefaultCommitSha;
    public DirectoryInfo RootDirectory { get; private set; } = null!;

    private string ContainerRegistryServer => "pier8.azurecr.io";
    public string DockerImageName => $"{ContainerRegistryServer}/tower-of-delusion";

    public static Settings Load()
    {
        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddCommandLine(Environment.GetCommandLineArgs())
            .Build();

        var settings = new Settings();
        config.Bind(settings);

        var githubVariables = new GithubVariables();
        config.Bind(githubVariables);

        settings.CommitSha = githubVariables.GITHUB_SHA.Substring(0, 8);
        settings.RootDirectory = GetRootDirectory();

        return settings;
    }

    private static DirectoryInfo GetRootDirectory()
    {
        var dirInfo = new DirectoryInfo(AppContext.BaseDirectory);
        while (dirInfo != null)
        {
            if (dirInfo.Name == "Draak") return dirInfo;

            dirInfo = dirInfo.Parent;
        }

        throw new InvalidOperationException("Draak dir not found");
    }
}