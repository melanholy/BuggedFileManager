using System.Collections.Generic;
using FileManager.API;
using System.IO.Compression;

namespace PluginsExample
{
    public class ArchiveUnzip : IMenuItem
    {
        public List<string> Extensions { get; }
        public string Text { get; }

        public ArchiveUnzip()
        {
            Text = "Unzip the archive";
            Extensions = new List<string> { ".zip" };
        }
        
        public void Click(string path, string filename, ClickPlace place)
        {
            if (place == ClickPlace.Folder)
                ZipFile.ExtractToDirectory(filename, path);
        }
    }
}