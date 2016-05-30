using System;
using filemanager.Infrastructure;

namespace filemanager.Domain
{
    public class FtpFile : ITextFile
    {
        public MyPath Path { get; }
        public string Extension { get; }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public IFileMoveProcess Move(bool keepOriginal)
        {
            throw new NotImplementedException();
        }

        public IFileMoveProcess Copy()
        {
            throw new NotImplementedException();
        }

        public IFileMoveProcess Move()
        {
            throw new NotImplementedException();
        }
    }
}
