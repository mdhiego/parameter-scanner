using System.Text.RegularExpressions;
using WixSharp;

namespace ENG.Plugins.Revit.Installer;

public static class Generator
{
    public static WixEntity[] GenerateWixEntities(IEnumerable<string> args)
    {
        var versionRegex = new Regex(@"\d+");
        var versionStorages = new Dictionary<string, List<WixEntity>>();

        foreach (string? directory in args)
        {
            var directoryInfo = new DirectoryInfo(directory);
            string? fileVersion = versionRegex.Match(directoryInfo.Name).Value;
            var feature = new Feature
            {
                Name = $"Revit {fileVersion}",
                Description = $"Install add-in for Revit {fileVersion}",
                ConfigurableDir = $"INSTALL{fileVersion}",
            };

            var files = new Files(feature, $@"{directory}\*.*");
            if (versionStorages.TryGetValue(fileVersion, out var storage))
                storage.Add(files);
            else
                versionStorages.Add(fileVersion, [files]);

            string[]? assemblies = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            Console.WriteLine($"Installer files for version '{fileVersion}':");
            foreach (string? assembly in assemblies)
            {
                Console.WriteLine($"'{assembly}'");
            }
        }

        return versionStorages
            .Select(storage => new Dir(new Id($"INSTALL{storage.Key}"), storage.Key, storage.Value.ToArray()))
            .Cast<WixEntity>()
            .ToArray();
    }
}
