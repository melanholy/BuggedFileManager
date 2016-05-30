using filemanager.Infrastructure;

namespace filemanager.Domain.Interfaces
{
    public interface IFile
    {
        MyPath Path { get; }
        void Create();
        void Delete();
        IFileMoveProcess Move(bool keepOriginal);
    }
}
