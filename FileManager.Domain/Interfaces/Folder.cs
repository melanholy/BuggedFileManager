using System.Collections.Generic;

namespace FileManager.Domain.Interfaces
{
    public abstract class Folder : MyFile
    {
        public abstract IEnumerable<MyFile> EnumerateFiles();
    }
}
