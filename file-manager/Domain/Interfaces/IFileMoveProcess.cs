using filemanager.Infrastructure;

namespace filemanager.Domain
{
    public interface IFileMoveProcess
    {
        bool KeepOriginal { get; set; }
        void To(IFile destFile);
    }
}