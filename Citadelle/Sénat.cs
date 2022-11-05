using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public class Sénat : BâtimentsSpéciaux
    {
        public static int  __NOMBRE =0;
        //C'est le sénat qui définit le coût d'un colon
        public static Coût __COUTCOLON = new Coût(1, 5);
        private static int[] __COORDONNEES = new int[] { 5, 4 };
        private static Coût __NIVEAU1 = new Coût(1, 0);
        private static Coût __NIVEAU2 = new Coût(3, 10);
        private static Coût __NIVEAU3 = new Coût(5, 25);
        private static Coût __NIVEAU4 = new Coût(8, 49);
        private static string[,] __CARACTERISTIQUE = new string[,] {{"Niveau","1","2","3","4"}
                                                        ,{"Points","110","121","133","146"}};
        //Le sénat en plus de la ferme peut acceuillir un nombre de personnes fixe 
        public int PopulationMaximale { get; set; }
        //Le sénat en plus de l'entrepôt peut stocker de l'or
        public int Stockage { get; set; }

        public Sénat()
        {
            //Vérification qu'il y ait bien 1 seul bâtiment du même type 
            if (__NOMBRE != 1)
            {
                Type = "Sénat";
                CoûtProduction = new Coût[] { __NIVEAU1, __NIVEAU2, __NIVEAU3, __NIVEAU4 };
                Localisation = __COORDONNEES;
                Niveau = 1;
                PopulationUtilisées = 1;
                PopulationMaximale = 3;
                Stockage = 100;
                Points = int.Parse(__CARACTERISTIQUE[1, Niveau]);
                __NOMBRE += 1;
            }
        }

        public override void Amélioration(Curseur curseur, GestionJeu jeu, Cité cité, Bâtiment sélection)
        {
            if (Niveau < 4 && jeu.NbOr >= CoûtProduction[Niveau].Or && cité.Colons.Count >= CoûtProduction[Niveau].NbColonsNecessaire)
            {
                Niveau += 1;
                Console.WriteLine($"Amélioration du Sénat au niveau {Niveau}");
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
            PopulationUtilisées += CoûtProduction[Niveau - 1].NbColonsNecessaire;
        }

        public override void Recrutement(int nb, Cité cité, GestionJeu jeu, GestionAffichage visuel)
        {
            if (nb*__COUTCOLON.Or<=jeu.NbOr && nb<=jeu.PopulationMaximale-jeu.NbPersonnes)
            {
                for(int i = 0; i<nb;i++)
                {
                    Colon c = new Colon();
                    cité.Colons.Add(c);
                }
                jeu.NbOr -= nb * __COUTCOLON.Or;
            }
            else
            {
                visuel.Erreur404(jeu.NbOr, nb*__COUTCOLON.Or, jeu.PopulationMaximale - jeu.NbPersonnes, nb);
            }
        }

        public override string[] AffichageCaractéristiques()
        {
            string[] affichage = new string[] {"",$"\t[Panneau de caractéristiques]", $"\t{Type} : ", $"\tNiveau : {Niveau}", $"\tPopulation utilisée : {PopulationUtilisées}",
                $"\tPoints : {Points}", $"\tAmélioration", $"\tRecrutement", $"\tConstruire", "\t*****************"};
            return affichage;
        }
    }
}
