using CliWrap;

namespace TowerOfDelusion.Build;

public static class Helpers
{
    public static Task<CommandResult> Run(string targetFilePath, string arguments, string workingDirectory = ".")
    {
        var standardPipelineTarget = PipeTarget.ToStream(Console.OpenStandardOutput());
        var errorPipeTarget        = PipeTarget.ToStream(Console.OpenStandardError());

        // Wrap CliWrap with defaults
        return Cli
            .Wrap(targetFilePath)
            .WithStandardOutputPipe(standardPipelineTarget!)
            .WithStandardErrorPipe(errorPipeTarget!)
            .WithArguments(arguments)
            .WithWorkingDirectory(workingDirectory)
            .ExecuteAsync();
    }
}