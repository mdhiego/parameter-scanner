using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace ENG.Plugins.Build;

sealed partial class Build
{
    Target Clean => def => def
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() =>
        {
            CleanDirectory(ArtifactsDirectory);
            foreach (var project in Solution.AllProjects.Where(project => project != Solution.src.ENG_Plugins_Build))
            {
                CleanDirectory(project.Directory / "bin");
                CleanDirectory(project.Directory / "obj");
            }

            foreach (string configuration in GlobBuildConfigurations())
            {
                DotNetClean(settings => settings
                    .SetConfiguration(configuration)
                    .SetVerbosity(DotNetVerbosity.minimal)
                    .EnableNoLogo());
            }
        });

    static void CleanDirectory(AbsolutePath path)
    {
        Log.Information("Cleaning directory: {Directory}", path);
        path.CreateOrCleanDirectory();
    }
}
