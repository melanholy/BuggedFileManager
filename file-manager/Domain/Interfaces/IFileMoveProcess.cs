namespace filemanager.Domain.Interfaces
{
    public interface IFileMoveProcess
    {
        bool KeepOriginal { get; set; }
        void To(IFile destFile);
    }
}