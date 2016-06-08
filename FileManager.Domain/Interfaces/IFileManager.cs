using FileManager.Domain.Infrastructure;

namespace FileManager.Domain.Interfaces
{
    public interface IFileManager
    {
        MyPath CurrentPath { get; }
        Folder GoForward();
        Folder GoBack();
        void Go(Folder folder);
        Folder GoUp();
        void Delete(MyFile file);
        void Create(string filename);
    }
}
