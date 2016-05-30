namespace filemanager.Domain
{
    public interface ITextFile : IFile
    {
        string Extension { get; }
    }
}
