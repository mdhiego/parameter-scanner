using Nuke.Common.Git;
using Nuke.Common.ProjectModel;

namespace ENG.Plugins.Build;

sealed partial class Build : NukeBuild
{
    private string[] Configurations;
    private Dictionary<Project, Project> InstallersMap;

    [Secret] [Parameter] string VersionControlToken;
    [GitRepository] readonly GitRepository GitRepository;

    [Solution(GenerateProjects = true)] Solution Solution;

    public static int Main() => Execute<Build>(static build => build.Compile);
}
