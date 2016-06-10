using System.Collections.Generic;
using System.Diagnostics;
using FileManager.API;

namespace PluginsExample
{
    public class OpenFiles : IFileOpener
    {
        public List<string> Extensions { get; }

        public OpenFiles()
        {
            Extensions = new List<string> { ".txt", ".jpg", ".png" };
        }

        void IFileOpener.Open(string file)
        {
            var proc = Process.Start(file);
            proc?.WaitForExit();
            proc?.Close();
        }
    }
}