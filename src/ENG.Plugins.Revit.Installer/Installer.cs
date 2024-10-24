using System.Diagnostics;
using ENG.Plugins.Revit.Installer;
using WixSharp;
using WixSharp.CommonTasks;
using WixSharp.Controls;
using Assembly = System.Reflection.Assembly;
using File = WixSharp.File;

const string projectName = "ENG.Plugins.Revit";
const string bundleName = "ENG.Revit.bundle";

var installerAssembly = Assembly.GetExecutingAssembly();
string installerLocation = Directory.GetParent(installerAssembly.Location)?.FullName;
var versionInfo = FileVersionInfo.GetVersionInfo(installerAssembly.Location);
var assemblyVersion = new Version(versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart);

var project = new Project
{
    OutDir = "output",
    Name = projectName,
    Platform = Platform.x64,
    UI = WUI.WixUI_FeatureTree,
    GUID = new Guid("37B78899-E7E2-4214-9E76-A9BBA4C4D700"),
    UpgradeCode = new Guid("1BCA6E77-D1C7-46B2-9B04-EF127F2AA700"),
    BannerImage = $@"{installerLocation}\Resources\Images\BannerImage.png",
    BackgroundImage = $@"{installerLocation}\Resources\Images\BackgroundImage.png",
    Version = assemblyVersion,
    MajorUpgrade = new MajorUpgrade
    {
        Schedule = UpgradeSchedule.afterInstallInitialize,
        AllowSameVersionUpgrades = true,
        DowngradeErrorMessage = "A later version of [ProductName] is already installed. Setup will now exit.",
    },
    ControlPanelInfo =
    {
        Manufacturer = "ENG",
        HelpLink = "https://ENG.eu",
        ProductIcon = $@"{installerLocation}\Resources\Icons\ShellIcon.ico",
    },
};

// Remove Terms dialog
project.RemoveDialogsBetween(NativeDialogs.WelcomeDlg, NativeDialogs.CustomizeDlg);

var contents = Generator.GenerateWixEntities(args);

BuildSingleUserMsi();
// BuildMultiUserUserMsi();

#pragma warning disable CS8321 // Local function is declared but never used
void BuildSingleUserMsi()
{
    project.InstallScope = InstallScope.perUser;
    project.OutFileName = $"{bundleName}-{assemblyVersion}-SingleUser";
    project.Dirs =
    [
        new InstallDir($@"%AppDataFolder%\Autodesk\ApplicationPlugins\{bundleName}",
            new File($@"{installerLocation}\PackageContents.xml"),
            new Dir("Contents", contents)
        ),
    ];
    project.BuildMsi();
}

void BuildMultiUserUserMsi()
{
    project.InstallScope = InstallScope.perMachine;
    project.OutFileName = $"{bundleName}-{assemblyVersion}-MultiUser";
    project.Dirs =
    [
        new InstallDir($@"%CommonAppDataFolder%\Autodesk\ApplicationPlugins\{bundleName}",
            new File($@"{installerLocation}\PackageContents.xml"),
            new Dir("Contents", contents)
        ),
    ];
    project.BuildMsi();
}
#pragma warning restore CS8321 // Local function is declared but never used
