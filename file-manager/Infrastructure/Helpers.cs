using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using filemanager.Domain;

namespace filemanager.Infrastructure
{
    public static class Helpers
    {
        private const string PluginsPath = "plugins/";

        public static IEnumerable<string> GetPluginFiles()
        {
            if (!Directory.Exists(PluginsPath))
                throw new DirectoryNotFoundException();
            return Directory.EnumerateFiles(PluginsPath, "*.dll", SearchOption.TopDirectoryOnly);
        }

        public static IEnumerable<WinFolder> GetRootFolders()
        {
            return DriveInfo.GetDrives()
                .Where(x => x.DriveType == DriveType.Fixed)
                .Select(x => new WinFolder(new MyPath(x.Name)));
        }

        public static bool IsCorrectMyPath(string path)
        {
            return Regex.IsMatch(path, "(?:^\\w:)|(?:/+)(?:[^/]+/+)*");
        }
    }
}
