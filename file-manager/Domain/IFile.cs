using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filemanager.Domain
{
    public interface IFile
    {
        string Name { get; set; }
        void Create();
        void Delete();
    }
}
