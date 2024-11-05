namespace ENG.Plugins.Build;

sealed partial class Build
{
    const string Version = "0.1.3";
    readonly AbsolutePath ArtifactsDirectory = RootDirectory / "output";
    readonly AbsolutePath ChangeLogPath = RootDirectory / "Changelog.md";

    protected override void OnBuildInitialized()
    {
        Configurations = ["Release*", "Installer*"];
        InstallersMap = new()
        {
            { Solution.src.ENG_Plugins_Revit_Installer, Solution.src.ENG_Plugins_Revit },
        };
    }
}
