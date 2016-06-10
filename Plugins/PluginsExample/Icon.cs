using System;
using System.Collections.Generic;
using FileManager.API;

namespace PluginsExample
{
    public class Icon : IFileIcon
    {
        public List<string> Extensions { get; }
        public Uri IconUri { get; }

        public Icon()
        {
            Extensions = new List<string> { ".zip" };
            IconUri = new Uri("zip.png");
        }
    }
}