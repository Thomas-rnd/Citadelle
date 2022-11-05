using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public class Mine : BâtimentProduction
    {
        public static int __NOMBRE = 0;
        private static int[] __COORDONNEES = new int[] { 9, 9 };

        private static Coût  __NIVEAU1 = new Coût(1, 4);
        private static Coût __NIVEAU2 = new Coût(3, 14);
        private static Coût __NIVEAU3 = new Coût(4, 29);
        private static Coût __NIVEAU4 = new Coût(6, 49);
        private static string[,] __CARACTERISTIQUE = new string[,] {{"Niveau","1","2","3","4"},{"Rendement","8","12","18","24"}
                                                        ,{"Points","22","24","27","30"}};

        public Mine()
        {
            //Pré-construction
            Type = "Mine";
            CoûtProduction = new Coût[] { __NIVEAU1, __NIVEAU2, __NIVEAU3, __NIVEAU4 };
        }

        public Mine(Curseur curseur)
        {
            //int[] coordonnées = new int[] { curseur.X, curseur.Y };
            if (__NOMBRE != 1)
            {
                Type = "Mine";
                CoûtProduction = new Coût[] { __NIVEAU1, __NIVEAU2, __NIVEAU3, __NIVEAU4 };
                Localisation = __COORDONNEES;
                Niveau = 1;
                PopulationUtilisées = CoûtProduction[Niveau - 1].NbColonsNecessaire;
                Points = int.Parse(__CARACTERISTIQUE[2, Niveau]);
                Rendement = int.Parse(__CARACTERISTIQUE[1, Niveau]);
                __NOMBRE += 1;
            }
        }

        public override void Amélioration(Curseur curseur, GestionJeu jeu, Cité cité, Bâtiment sélection)
        {
            if (Niveau < 4 && jeu.NbOr >= CoûtProduction[Niveau].Or && cité.Colons.Count >= CoûtProduction[Niveau].NbColonsNecessaire)
            {
                Niveau += 1;
                Console.WriteLine($"Amélioration de la Mine au niveau {Niveau}");
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
            Rendement = int.Parse(__CARACTERISTIQUE[1, Niveau]);
        }

        public override string[] AffichageCaractéristiques()
        {
            string[] affichage = new string[] {"",$"\t[Panneau de caractéristiques]", $"\t{Type} : ",$"\tNiveau : {Niveau}", $"\tPopulation utilisée : {PopulationUtilisées}", $"\tProduction : {Rendement} Or/Tour", $"\tPoints : {Points}",
                $"\tAmélioration", "\t*****************"};
            return affichage;
        }
    }
}
