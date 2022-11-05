using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public abstract class BâtimentsSpéciaux : Bâtiment
    {
        public abstract void Recrutement(int nb, Cité cité, GestionJeu jeu, GestionAffichage visuel);
    }
}
