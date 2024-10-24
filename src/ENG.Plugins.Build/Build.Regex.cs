using System.Text.RegularExpressions;

namespace ENG.Plugins.Build;

sealed partial class Build
{
    readonly Regex ArgumentsRegex = ArgumentsRegexGenerator();

    [GeneratedRegex("'(.+?)'", RegexOptions.Compiled)]
    private static partial Regex ArgumentsRegexGenerator();
}
