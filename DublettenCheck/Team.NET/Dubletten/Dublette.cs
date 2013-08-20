using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dubletten
{
    public class Dublette : IDublette
    {
        private List<string> filePaths;

        public Dublette(List<string> filePaths)
        {
            this.filePaths = filePaths;
        }

        public IEnumerable<string> Dateipfade
        {
            get { return filePaths; }
        }
    }
}
