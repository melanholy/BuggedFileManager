using System;
using System.Collections.Generic;

namespace API
{
    public interface IFileOpener
    {
        List<string> Extensions { get; }
        void Open(string file);
    }

    public enum ClickPlace
    {
        Folder,
        Empty,
        File
    }
    
    public interface IMenuItem
    {
        List<string> Extensions { get; }
        string Text { get; }
        void Click(string path, string filename, ClickPlace place);
    }

    public interface IFileIcon
    {
        List<string> Extensions { get; }
        Uri IconUri { get; }
    }
}
