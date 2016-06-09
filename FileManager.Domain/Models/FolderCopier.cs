using System;
using FileManager.Domain.Models.Files;

namespace FileManager.Domain.Models
{
    internal class FolderCopier
    {
        public static void Copy(Folder source, Folder destination, Action<TextFile> moveFile)
        {
            destination.Create();
            foreach (var file in source.EnumerateFiles())
            {
                if (file is TextFile)
                    moveFile((TextFile)file);
                else if (file is Folder)
                    Copy((Folder)file, destination, moveFile);
                else
                    throw new ArgumentException();
            }
        }
    }
}
