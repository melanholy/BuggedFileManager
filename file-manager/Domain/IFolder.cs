using System.Collections.Generic;

namespace filemanager.Domain
{
    public interface IFolder : IFile
    {
        IEnumerable<IFile> EnumerateFiles();
    }
}
