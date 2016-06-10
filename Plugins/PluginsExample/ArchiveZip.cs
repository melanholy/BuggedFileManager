using System.Collections.Generic;
using System.IO.Compression;
using FileManager.API;

namespace PluginsExample
{
    public class ArchiveZip
    {
        public List<string> Extensions { get; }
        public string Text => "Add to zip archive";

        public ArchiveZip()
        {
            Extensions = new List<string> { "zip" };
        }

        public void Click(string path, string filename, ClickPlace place)
        {
            if (place == ClickPlace.File)
                ZipFile.CreateFromDirectory(filename, path);
        }
    }
}