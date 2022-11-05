
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public class Coût
    {
        public int NbColonsNecessaire { get; set; }
        public int Or { get; set; }

        public Coût(int nbColonsNécessaire, int or)
        {
            NbColonsNecessaire = nbColonsNécessaire;
            Or = or;
        }
        /// <summary>
        /// Uniquement à afficher qd choix amélioration
        /// </summary>
        public string Amélioration()
        {
            string affichage = $"" +
                $"Coût d'amélioration : [Population] = {NbColonsNecessaire} // [Or] = {Or}";
            return affichage;
        }

        public string Recrutement()
        {
            string affichage = $"" +
                $"Coût de recrutement : [Population] = {NbColonsNecessaire} // [Or] = {Or}";
            return affichage;
        }
    }
}
