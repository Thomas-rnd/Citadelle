using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public abstract class Bâtiment
    {
        public int[] Localisation { get; set; }
        public int Niveau { get; set; }
        public string Type { get; protected set; }
        //Une manière de gagner la partie est de terminer sa cité
        public int Points { get; set; }
        public int PopulationUtilisées { get; protected set; }
        public Coût[] CoûtProduction { get; set; }

        public abstract void Amélioration(Curseur curseur, GestionJeu jeu, Cité cité, Bâtiment sélection);
        public abstract void DéfinitionCaractéristiques();
        public abstract string[] AffichageCaractéristiques();
    }
}
