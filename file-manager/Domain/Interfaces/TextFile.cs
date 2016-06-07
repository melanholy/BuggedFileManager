using System.IO;

namespace filemanager.Domain.Interfaces
{
    public abstract class TextFile : MyFile
    {
        public string Extension => Path.GetExt();
        public abstract void Create(Stream contents);
    }
}
