using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{

    public class Cité
    {
        public static int __POINTSCITECOMPLETE = 220;
        //Taille de la cité
        public int X { get; private set; }
        public int Y { get; private set; }
        public int PopulationTotale { get; private set; }

        public List<Colon> Colons { get; set; }
        public List<Bâtiment> Bâtiments { get; set; }
        public string[] BâtimentsDispo { get; set; }
        //Pour vérifier si la cité est complète
        public int Points { get; set; }
        
        public Cité(int x, int y)
        {
            X = x;
            Y = y;
            Colons = new List<Colon> { };
            Bâtiments = new List<Bâtiment> { };
            Sénat sénat = new Sénat();
            Bâtiments.Add(sénat);
            BâtimentsDispo = new string[] { "Ferme", "Mine" };
            PopulationTotale = NbPersonnages();
        }

        public void CalculPoints()
        {
            Points = 0;
            for (int i = 0; i < Bâtiments.Count; i++)
            {
                Points += Bâtiments[i].Points;
            }
        }

        public int NbPersonnages()
        {
            int popBâtiments = 0;
            for(int i = 0;i<Bâtiments.Count;i++)
            {
                popBâtiments += Bâtiments[i].PopulationUtilisées;
            }
            return (Colons.Count + popBâtiments);
        }

        public void SupprimerColons(int nb)
        {
            for (int i = 0; i<nb;i++)
            {
                //C'est une liste le nombre d'élement se met à jour automatiquement
                //Donc pour éviter les exception on enlève nb fois le premier terme 
                Colons.RemoveAt(0);
            }
        }

        public void Construction(Curseur curseur, Bâtiment choix, GestionJeu jeu)
        {
            //choix.CoûtProduction[0] donne le coût de production pour la construction du bâtiment
            if (jeu.NbOr >= choix.CoûtProduction[0].Or && Colons.Count >= choix.CoûtProduction[0].NbColonsNecessaire)
            {
                Console.WriteLine($"Construction de le/la {choix.Type}");
                Bâtiment bâtiment;
                if (choix is Ferme)
                {
                    bâtiment = new Ferme(curseur);
                    Bâtiments.Add(bâtiment);
                }
                else if (choix is Entrepôt)
                {
                    bâtiment = new Entrepôt(curseur);
                    Bâtiments.Add(bâtiment);
                }
                else
                {
                    bâtiment = new Mine(curseur);
                    Bâtiments.Add(bâtiment);
                }
                CirculationColons(curseur, bâtiment, jeu);
                jeu.MajCarte();
                SupprimerColons(choix.CoûtProduction[0].NbColonsNecessaire);
                jeu.NbOr -= choix.CoûtProduction[choix.Niveau].Or;
                PopulationTotale = NbPersonnages();
            }
            else
            {
                jeu.visuel.Erreur404(jeu.NbOr, choix.CoûtProduction[0].Or, Colons.Count, choix.CoûtProduction[0].NbColonsNecessaire);
            }

        }

        public void CirculationColons(Curseur curseur, Bâtiment bâtimentEnTravaux, GestionJeu jeu)
        {
            for (int i = 0; i < bâtimentEnTravaux.CoûtProduction[0].NbColonsNecessaire; i++)
            {
                Colons[i].SeDéplacer(curseur, bâtimentEnTravaux.Localisation, jeu);
            }
        }
    }
}
