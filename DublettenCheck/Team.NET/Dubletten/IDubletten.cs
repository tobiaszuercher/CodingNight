using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubletten
{
    public interface IDublettenprüfung
    {
        IEnumerable<IDublette> Sammle_Kandidaten(string pfad);
    }

    public interface IDublette
    {
        IEnumerable<string> Dateipfade { get; }
    }
}
