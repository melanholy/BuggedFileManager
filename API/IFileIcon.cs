using System;
using System.Collections.Generic;

namespace API
{
    public interface IFileIcon
    {
        List<string> Extensions { get; }
        Uri IconUri { get; }
    }
}