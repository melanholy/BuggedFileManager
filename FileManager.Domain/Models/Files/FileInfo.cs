using System;

namespace FileManager.Domain.Models.Files
{
    public class FileInfo
    {
        public FileSize Size { get; }
        public DateTime Created { get; }
        public DateTime Modified { get; }

        public FileInfo(FileSize size, DateTime created, DateTime modified)
        {
            Size = size;
            Created = created;
            Modified = modified;
        }
    }
}
