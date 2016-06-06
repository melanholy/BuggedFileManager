using System.Collections.Generic;

namespace API
{
    public interface IFileOpener
    {
        List<string> Extensions { get; }
        void Open(string file);
    }
}