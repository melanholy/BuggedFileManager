using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace filemanager.Infrastructure
{
    public static class Helpers
    {
        private const string PluginsPath = "plugins/";

        public static IEnumerable<string> GetPluginFiles()
        {
            if (!Directory.Exists(PluginsPath))
                throw new DirectoryNotFoundException();
            return Directory.EnumerateFiles(
                PluginsPath, 
                "*.dll",
                SearchOption.TopDirectoryOnly
            );
        }

        public static bool IsCorrectMyPath(string path)
        {
            return Regex.IsMatch(path, "(?:^\\w:)|(?:/+)(?:[^/]+/+)*");
        }
    }
}
