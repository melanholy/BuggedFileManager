using filemanager.Infrastructure;

namespace filemanager.Domain
{
    public class WinFileMoveProcess : IFileMoveProcess
    {
        private readonly IFile File;
        public bool KeepOriginal { get; set; }

        public WinFileMoveProcess(IFile file)
        {
            File = file;
        }

        public WinFileMoveProcess(IFile file, bool keepOriginal)
        {
            File = file;
            KeepOriginal = keepOriginal;
        }

        public void To(MyPath path)
        {
            if (File is WinFile)
            {
                System.IO.File.Copy(File.Path.Path, path.Path);
            }
            else
            {
                
            }
        }
    }
}