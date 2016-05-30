using filemanager.Infrastructure;

namespace filemanager.Domain
{
    public interface IFile
    {
        MyPath Path { get; }
        void Create();
        void Delete();
        IFileMoveProcess Move(bool keepOriginal);
    }
}
