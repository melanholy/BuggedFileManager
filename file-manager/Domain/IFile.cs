namespace filemanager.Domain
{
    public interface IFile
    {
        string Name { get; set; }
        void Create();
        void Delete();
    }
}
