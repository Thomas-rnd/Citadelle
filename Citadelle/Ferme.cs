using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public class Ferme : BâtimentProduction
    {
        public static int __NOMBRE = 0;
        private static int[] __COORDONNEES = new int[] { 0, 1 };

        private static Coût __NIVEAU1 = new Coût(0, 0);
        private static Coût __NIVEAU2 = new Coût(0, 1);
        private static Coût __NIVEAU3 = new Coût(0, 5);
        private static Coût __NIVEAU4 = new Coût(0, 16);
        private static string[,] __CARACTERISTIQUE = new string[,] {{"Niveau","1","2","3","4"}
                                                        ,{"Points","14","17","19","22"}, {"Population maximale","14","38","69","105"}};
        public int PopulationMaximale { get; set; }


        public Ferme()
        {
            //Pré-construction
            Type = "Ferme";
            CoûtProduction = new Coût[] { __NIVEAU1, __NIVEAU2, __NIVEAU3, __NIVEAU4 };
        }

        public Ferme(Curseur curseur)
        {
            if (__NOMBRE != 1)
            {
                __NOMBRE += 1;
                Type = "Ferme";
                CoûtProduction = new Coût[] { __NIVEAU1, __NIVEAU2, __NIVEAU3, __NIVEAU4 };
                Localisation = __COORDONNEES;
                Niveau = 1;
                PopulationUtilisées = CoûtProduction[Niveau-1].NbColonsNecessaire;
                PopulationMaximale = int.Parse(__CARACTERISTIQUE[1, Niveau]);
                Points = int.Parse(__CARACTERISTIQUE[1, Niveau]);
            }
        }

        public override void Amélioration(Curseur curseur, GestionJeu jeu, Cité cité, Bâtiment sélection)
        {
            if (Niveau < 4 && jeu.NbOr >= CoûtProduction[Niveau].Or && cité.Colons.Count >= CoûtProduction[Niveau].NbColonsNecessaire)
            {
                Niveau += 1;
                Console.WriteLine($"Amélioration de la Ferme au niveau {Niveau}");
                cité.CirculationColons(curseur, sélection, jeu);
                DéfinitionCaractéristiques();
                jeu.NbOr -= CoûtProduction[Niveau - 1].Or;
                cité.SupprimerColons(CoûtProduction[Niveau - 1].NbColonsNecessaire);
            }
            else
            {
                jeu.visuel.Erreur404(jeu.NbOr, CoûtProduction[Niveau].Or, cité.Colons.Count, CoûtProduction[Niveau].NbColonsNecessaire);
            }
        }

        public override void DéfinitionCaractéristiques()
        {
            Points = int.Parse(__CARACTERISTIQUE[1, Niveau]);
            PopulationMaximale = int.Parse(__CARACTERISTIQUE[1, Niveau]);
            PopulationUtilisées += CoûtProduction[Niveau - 1].NbColonsNecessaire;
        }

        public override string[] AffichageCaractéristiques()
        {
            string[] affichage = new string[] {"",$"\t[Panneau de caractéristiques]", $"\t{Type} : ", $"\tNiveau : {Niveau}", $"\tPopulation utilisée : {PopulationUtilisées}",
                $"\tPopulation maximale : {PopulationMaximale}", $"\tPoints : {Points}" , $"\tAmélioration", "\t*****************"};
            return affichage;
        }
    }
}
