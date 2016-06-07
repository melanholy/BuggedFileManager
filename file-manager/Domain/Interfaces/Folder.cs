using System.Collections.Generic;

namespace filemanager.Domain.Interfaces
{
    public abstract class Folder : MyFile
    {
        public abstract IEnumerable<MyFile> EnumerateFiles();
    }
}
