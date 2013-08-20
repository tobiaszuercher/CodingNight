using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dubletten
{
    public class MockFileService : IFileService
    {
        private Dictionary<string, IEnumerable<IFile>> directories = new Dictionary<string, IEnumerable<IFile>>();

        public MockFileService()
        {
        }
        public IEnumerable<IFile> GetFiles(string path)
        {
            return directories[path];
        }

        public MockFileService Setup(string path, IEnumerable<string> files)
        {
            directories.Add(path, files.Select(file => new File() { Path = file }));
            return this;
        }
    }
}
