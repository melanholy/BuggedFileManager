using System;

namespace filemanager.Domain
{
    class FtpFile : ITextFile
    {
        public string Name { get; set; }
        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public string Extension { get; }
    }
}
