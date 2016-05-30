using System.IO;

namespace filemanager.Domain.Interfaces
{
    public interface ITextFile : IFile
    {
        string Extension { get; }
        void Create(Stream contents);
    }
}
