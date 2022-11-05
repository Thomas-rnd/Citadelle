using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public class Entrepôt : Bâtiment
    {
        public static int __NOMBRE = 0;
        private static int[] __COORDONNEES = new int[] { 7, 6 };
        private static Coût __NIVEAU1 = new Coût(0, 0);
        private static Coût __NIVEAU2 = new Coût(0, 47);
        private static Coût __NIVEAU3 = new Coût(0, 93);
        private static Coût __NIVEAU4 = new Coût(0, 150);
        private static string[,] __CARACTERISTIQUE = new string[,] {{"Niveau","1","2","3","4"}
                                                        ,{"Points","15","17","19","22"}, {"Stockage","300","700","1200","1700"}};
        public int Stockage { get; set; }

        public Entrepôt()
        {
            //Pré-construction
            Type = "Entrepôt";
            CoûtProduction = new Coût[] { __NIVEAU1, __NIVEAU2, __NIVEAU3, __NIVEAU4 };
        }

        public Entrepôt(Curseur curseur)
        {
            //int[] coordonnées = new int[] { curseur.X, curseur.Y };
            //Le paramètre curseur permettra par la suite de choisir la position de construction
            //dans une autre version du jeu.  
            if (__NOMBRE != 1)
            {
                Type = "Entrepôt";
                Localisation = __COORDONNEES;
                Niveau = 1;
                CoûtProduction = new Coût[] { __NIVEAU1, __NIVEAU2, __NIVEAU3, __NIVEAU4 };
                PopulationUtilisées = CoûtProduction[Niveau - 1].NbColonsNecessaire;
                Points = int.Parse(__CARACTERISTIQUE[1, Niveau]);
                Stockage = int.Parse(__CARACTERISTIQUE[2, Niveau]);
                __NOMBRE += 1;
            }
        }

        public override void Amélioration(Curseur curseur, GestionJeu jeu, Cité cité, Bâtiment sélection)
        {
            if (Niveau < 4 && jeu.NbOr >= CoûtProduction[Niveau].Or && cité.Colons.Count >= CoûtProduction[Niveau].NbColonsNecessaire)
            {
                Niveau += 1;
                Console.WriteLine($"Amélioration de l'Entrepôt au niveau {Niveau}");
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
            Points = int.Parse(__CARACTERISTIQUE[2, Niveau]);
            PopulationUtilisées += CoûtProduction[Niveau - 1].NbColonsNecessaire;
            Stockage = int.Parse(__CARACTERISTIQUE[2, Niveau]);
        }

        public override string[] AffichageCaractéristiques()
        {
            string[] affichage = new string[] { "",$"\t[Panneau de caractéristiques]", $"\t{Type} : ", $"\tNiveau : {Niveau}", $"\tPopulation utilisée : {PopulationUtilisées}",
                $"\tPoints : {Points}", $"\tStockage : {Stockage}", $"\tAmélioration","\t*****************"};
            return affichage;
        }
    }
}
