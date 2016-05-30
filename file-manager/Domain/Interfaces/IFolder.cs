using System.Collections.Generic;

namespace filemanager.Domain.Interfaces
{
    public interface IFolder : IFile
    {
        IEnumerable<IFile> EnumerateFiles();
    }
}
