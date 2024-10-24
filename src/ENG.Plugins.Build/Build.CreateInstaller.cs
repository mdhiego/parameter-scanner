using System.Diagnostics.CodeAnalysis;
using Nuke.Common.Git;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;

namespace ENG.Plugins.Build;

sealed partial class Build
{
    Target CreateInstaller => def => def
        .DependsOn(Compile)
        .OnlyWhenStatic(() => IsLocalBuild || GitRepository.IsOnMainOrMasterBranch())
        .Executes(() =>
        {
            foreach (var (installer, project) in InstallersMap)
            {
                Log.Information("Project: {Name}", project.Name);

                string exePattern = $"*{installer.Name}.exe";
                string exeFile = Directory.EnumerateFiles(installer.Directory, exePattern, SearchOption.AllDirectories)
                    .FirstOrDefault()
                    .NotNull($"No installer file was found for the project: {installer.Name}");

                string[] directories = Directory.GetDirectories(project.Directory, "bin/Release*", SearchOption.AllDirectories);
                Assert.NotEmpty(directories, "No files were found to create an installer");

                string arguments = directories.Select(path => path.DoubleQuoteIfNeeded()).JoinSpace();
                var process = ProcessTasks.StartProcess(exeFile, arguments, logInvocation: false, logger: InstallLogger);
                process.AssertZeroExitCode();
            }
        });

    [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
    void InstallLogger(OutputType outputType, string output)
    {
        if (outputType == OutputType.Err)
        {
            Log.Error(output);
            return;
        }

        var arguments = ArgumentsRegex.Matches(output);
        if (arguments.Count == 0)
        {
            Log.Debug(output);
            return;
        }

        object[] properties = arguments
            .Select(match => match.Value.Substring(1, match.Value.Length - 2))
            .Cast<object>()
            .ToArray();

        string messageTemplate = ArgumentsRegex.Replace(output, match => $"{{Property{match.Index}}}");
        Log.Information(messageTemplate, properties);
    }
}
