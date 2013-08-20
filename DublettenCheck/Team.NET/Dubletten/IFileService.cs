using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dubletten
{
    public interface IFileService
    {
        IEnumerable<IFile> GetFiles(String path);
    }


    public interface IFile
    {
        string Path { get; set; }
    }
}
