using System.IO.Enumeration;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace ENG.Plugins.Build;

sealed partial class Build
{
    Target Compile => def => def
        .DependsOn(Clean)
        .Executes(() =>
        {
            foreach (string configuration in GlobBuildConfigurations())
            {
                DotNetBuild(settings => settings
                    .SetConfiguration(configuration)
                    .SetVersion(Version)
                    .SetVerbosity(DotNetVerbosity.minimal));
            }
        });

    List<string> GlobBuildConfigurations()
    {
        var configurations = Solution.Configurations
            .Select(pair => pair.Key)
            .Select(config => config.Remove(config.LastIndexOf('|')))
            .Where(config => Configurations.Any(wildcard => FileSystemName.MatchesSimpleExpression(wildcard, config)))
            .ToList();

        Assert.NotEmpty(configurations, $"No solution configurations have been found. Pattern: {string.Join(" | ", Configurations)}");
        return configurations;
    }
}
