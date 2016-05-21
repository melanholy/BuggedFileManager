using System;
using System.Collections.Generic;

namespace API
{
    public interface IFileOpener
    {
        List<string> Extensions { get; }
        void Open(string file);
    }

    public interface IMenuItem
    {
        List<string> Extensions { get; }
        string Text { get; }
        void Click(string file);
    }

    public interface IFileIcon
    {
        List<string> Extensions { get; }
        Uri IconUri { get; }
    }
}
