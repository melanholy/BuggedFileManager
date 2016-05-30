using System.Windows;
using API;
using filemanager.Application;
using filemanager.Infrastructure;

namespace GUI
{
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var repo = new PluginRepo(new [] {
                typeof(IFileOpener),
                typeof(IFileIcon),
                typeof(IMenuItem)
            });
            var pluginFiles = Helpers.GetPluginFiles();
            repo.RegisterPlugins(pluginFiles);
            var w = new MainWindow(repo);
            w.Show();
        }
    }
}
