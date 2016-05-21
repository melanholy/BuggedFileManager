using System;
using System.Collections.Generic;
using filemanager.Infrastructure;

namespace filemanager.Domain
{
    public class FtpFolder : IFolder
    {
        public string Name { get; set; }
        public MyPath Path { get; }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFile> EnumerateFiles()
        {
            throw new NotImplementedException();
        }
    }
}
