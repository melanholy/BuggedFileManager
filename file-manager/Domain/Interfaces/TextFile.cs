using System.IO;

namespace filemanager.Domain.Interfaces
{
    public abstract class TextMyFile : MyFile
    {
        public string Extension => Path.GetExt();
        public abstract void Create(Stream contents);
    }
}
