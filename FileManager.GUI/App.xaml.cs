using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using FileManager.API;
using FileManager.GUI.Application;

namespace FileManager.GUI
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
            repo.RegisterPlugins(pluginFiles.Select(Assembly.LoadFile));
            var w = new MainWindow(repo);
            w.Show();
        }
    }

    public class Extensions
    {
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.RegisterAttached("Image", typeof(BitmapImage), typeof(Extensions), new PropertyMetadata(default(BitmapImage)));

        public static void SetImage(UIElement element, BitmapImage value)
        {
            element.SetValue(ImageProperty, value);
        }

        public static BitmapImage GetImage(UIElement element)
        {
            return (BitmapImage)element.GetValue(ImageProperty);
        }
    }
}
