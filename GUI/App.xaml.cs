using System.Collections.Generic;
using System.IO;
using System.Windows;
using API;
using GUI.Application;

namespace GUI
{
    public partial class App
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

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var repo = new PluginRepo(new [] {
                typeof(IFileOpener),
                typeof(IFileIcon),
                typeof(IMenuItem)
            });
            var pluginFiles = GetPluginFiles();
            repo.RegisterPlugins(pluginFiles);
            var w = new MainWindow(repo);
            w.Show();
        }
    }
}
