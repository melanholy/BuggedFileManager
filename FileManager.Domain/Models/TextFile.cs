using System.IO;

namespace FileManager.Domain.Models
{
    public abstract class TextFile : MyFile
    {
        public string Extension => Path.GetExt();
        public abstract void Create(Stream contents);
    }
}
