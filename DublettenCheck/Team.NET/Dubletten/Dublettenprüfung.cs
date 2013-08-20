using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dubletten
{
    public class Dublettenprüfung : IDublettenprüfung
    {
        private MockFileService fileService;
        private Dictionary<string, List<string>> foundFilesWithPaths = new Dictionary<string, List<string>>();

        public Dublettenprüfung(MockFileService fileService)
        {
            // TODO: Complete member initialization
            this.fileService = fileService;
        }
        public IEnumerable<IDublette> Sammle_Kandidaten(string pfad)
        {
            var files = fileService.GetFiles(pfad);
            if (files.Count() > 1)
            {
                var filePaths = new List<string>();
                foreach (var file in files)
                {
                    var fileName = GetFileName(file);

                    if (!foundFilesWithPaths.ContainsKey(fileName))
                    {
                        foundFilesWithPaths.Add(fileName, new List<string>());
                    }
                    foundFilesWithPaths[fileName].Add(file.Path);
                }

                return foundFilesWithPaths.Where(f=> f.Value.Count > 1).Select(f => new Dublette(f.Value));
            }
            return new List<IDublette>();
        }

        private string GetFileName(IFile file)
        {
            return Path.GetFileName(file.Path);
        }
    }
}
