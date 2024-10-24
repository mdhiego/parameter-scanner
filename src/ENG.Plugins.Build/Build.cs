using Nuke.Common.Git;
using Nuke.Common.ProjectModel;

namespace ENG.Plugins.Build;

sealed partial class Build : NukeBuild
{
    private string[] Configurations;
    private Dictionary<Project, Project> InstallersMap;

    [GitRepository] readonly GitRepository GitRepository;
    [Secret] [Parameter] string GitHubToken;

    [Solution(GenerateProjects = true)] Solution Solution;

    public static int Main() => Execute<Build>(static build => build.CreateInstaller);
}
