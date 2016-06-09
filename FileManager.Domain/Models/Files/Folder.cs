using System.Collections.Generic;

namespace FileManager.Domain.Models.Files
{
    public abstract class Folder : MyFile
    {
        public abstract IEnumerable<MyFile> EnumerateFiles();
    }
}
