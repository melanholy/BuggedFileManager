using System.IO;

namespace filemanager.Domain
{
    public interface ITextFile : IFile
    {
        string Extension { get; }
        void Create(Stream contents);
    }
}
