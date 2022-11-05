using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public abstract class Peuple : Personnage
    {
        public abstract void Construire(int[] coordonnées, Bâtiment batConstruit);
    }
}
