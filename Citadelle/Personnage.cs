using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public abstract class Personnage
    {
        public string Spécialité {get; protected set; }
        public int Attaque { get; protected set; }
        public int Défense { get; protected set; }
        public int Déplacement { get; protected set; }
        public int PointsVie { get; protected set; } //Uniquement pour le boss
        public int Moral { get; protected set; }
        public int Faim { get; protected set; }
        public int[] Coordonnées { get; set; }

        public abstract void Manger();
        public abstract void SeDéplacer(Curseur curseur, int[] coordonnées, GestionJeu jeu);
    }
}
