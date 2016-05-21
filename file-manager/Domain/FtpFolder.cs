using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filemanager.Domain
{
    class FtpFolder : IFolder
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

        public IEnumerable<IFile> EnumerateFiles()
        {
            throw new NotImplementedException();
        }
    }
}
