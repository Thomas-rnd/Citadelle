using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public class GestionJeu
    {
        private static int __TOURMAX = 20;
        // Taille de la map du jeu
        private static int __X = 15;
        private static int __Y = 10;
        //Entier contenant le numéro du tour de jeu de la partie en cours
        private int Tour { get; set; }
        // Tableau contenant les infos tels que nb pop°, nb d'or etc
        public string[] Information { get; set; }
        // Tableau avec les bâtiments et personnages
        public string[,] Carte { get; set; }
        // Tableau stockant les caractéristiques de l'objet sélectionné
        public string[] Caractéristique { get; set; }
        // Tableau qui va rassembler tous les éléments des tableaux susmentionnés 
        public string[,] Jeu { get; set; }
        public int NbPersonnes { get; set; }
        public int PopulationMaximale { get; set; }
        public int NbOr { get; set; }
        public int NbOrTotal { get; set; }
        private Cité cité;
        private Curseur curseur;
        //Justification de pourquoi public parce que uniquement de l'affichage donc pas de risque pour le jeu 
        public GestionAffichage visuel;
        private ChoixUtilisateur décision;
        private CatastropheNaturelle catastrophe;

        /// <summary>
        /// 
        /// </summary>
        public GestionJeu()
        {
            curseur = new Curseur();
            décision = new ChoixUtilisateur();
            catastrophe = new CatastropheNaturelle();
            cité = new Cité(__X,__Y);
            Carte = new string[__X, __Y];
            InitialisationCarte();
            visuel = new GestionAffichage();
            Jeu = new string[__X + 1, __Y + 2];
            Information = new string[__Y];
            InitialisationInformation();
            Caractéristique = new string[__X];
            InitialisationCaractéristiques();
            MajInterface();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void InitialisationCarte()
        {
            for (int i = 0; i < Carte.GetLength(0); i++)
            {
                for (int j = 0; j < Carte.GetLength(1); j++)
                {
                    Carte[i, j] = "\t#";
                }
            }
        }

        public void InitialisationInformation()
        {
            NbPersonnes = cité.NbPersonnages();
            cité.CalculPoints();
            Information[0] = $"\t[Tour : {Tour}/{__TOURMAX}]";
            Information[1] = $"\t[Population : {NbPersonnes}/{PopulationMaximale}]";
            Information[2] = $"\t[Colons : {cité.Colons.Count}]";
            NbOr = 100;
            Information[3] = $"\t[Or : {NbOr}/{NbOrTotal}]";
            Information[4] = $"\t[Point : {cité.Points}/{Cité.__POINTSCITECOMPLETE}]";
            Information[5] = $"\tDébut de partie : {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
        }

        public void InitialisationCaractéristiques()
        {
            Caractéristique[1] = "\t[Panneau de caractéristiques]";
            for (int i = 2; i < Caractéristique.Length; i++)
            {
                Caractéristique[i] = " ";
            }
            Caractéristique[Caractéristique.Length - 1] = "\t* PASSER SON TOUR *";
        }

        public void InitialisationAffichage()
        {
            for (int i = 0; i < Jeu.GetLength(0); i++)
            {
                for (int j = 0; j < Jeu.GetLength(1); j++)
                {
                    Jeu[i, j] = "\t";
                }
            }
        }

        public void MajInformation()
        {
            NbPersonnes = cité.NbPersonnages();
            cité.CalculPoints();
            PopulationMaximale = 0;
            NbOrTotal = 0;
            for (int i = 0; i < cité.Bâtiments.Count; i++)
            {
                if (cité.Bâtiments[i] is Ferme)
                {
                    PopulationMaximale += ((Ferme)cité.Bâtiments[i]).PopulationMaximale;
                }
                else if (cité.Bâtiments[i] is Sénat)
                {
                    PopulationMaximale += ((Sénat)cité.Bâtiments[i]).PopulationMaximale;
                    NbOrTotal += ((Sénat)cité.Bâtiments[i]).Stockage;
                }
                else if (cité.Bâtiments[i] is Entrepôt)
                {
                    NbOrTotal += ((Entrepôt)cité.Bâtiments[i]).Stockage;
                }
            }
            Information[0] = $"\t[Tour : {Tour}/{__TOURMAX}]";
            Information[1] = $"\t[Population : {NbPersonnes}/{PopulationMaximale}]";
            Information[2] = $"\t[Colons : {cité.Colons.Count}]";
            Information[3] = $"\t[Or : {NbOr}/{NbOrTotal}]";
            Information[4] = $"\t[Point : {cité.Points}/{Cité.__POINTSCITECOMPLETE}]";
        }

        public void MajCaractéristiques(Bâtiment sélection)
        {
            //Boucle permet de supprimer tous les éléments qui étaient affichés
            for (int i = 0; i < Jeu.GetLength(0); i++)
            {
                Jeu[i, Jeu.GetLength(1) - 1] = " ";
            }
            Caractéristique = sélection.AffichageCaractéristiques();
            Caractéristique[Caractéristique.Length - 1] = "\t* PASSER SON TOUR *";
        }

        public void MajCarte()
        {
            InitialisationCarte();
            for (int i = 0; i < cité.Bâtiments.Count; i++)
            {
                if (cité.Bâtiments[i] is Ferme)
                {
                    Carte[cité.Bâtiments[i].Localisation[0], cité.Bâtiments[i].Localisation[1]] = "\tF";
                }
                else if (cité.Bâtiments[i] is Mine)
                {
                    Carte[cité.Bâtiments[i].Localisation[0], cité.Bâtiments[i].Localisation[1]] = "\tM";
                }
                else if (cité.Bâtiments[i] is Sénat)
                {
                    Carte[cité.Bâtiments[i].Localisation[0], cité.Bâtiments[i].Localisation[1]] = "\tS";
                }
                else if (cité.Bâtiments[i] is Entrepôt)
                {
                    Carte[cité.Bâtiments[i].Localisation[0], cité.Bâtiments[i].Localisation[1]] = "\tE";
                }
            }
        }

        public void MajInterface()
        {
            for (int i = 0; i < Jeu.GetLength(0); i++)
            {
                for (int j = 0; j < Jeu.GetLength(1); j++)
                {
                    if (i == 0 && j < Information.Length)
                    {
                        Jeu[0, j] = Information[j];
                    }
                    if (j == Jeu.GetLength(1) - 1 && i < Caractéristique.Length)
                    {
                        Jeu[i, Jeu.GetLength(1) - 1] = Caractéristique[i];
                    }
                    if (i != 0 && j < Jeu.GetLength(1) - 2)
                    {
                        Jeu[i, j] = Carte[i - 1, j];
                    }
                }
            }
        }

        public void MajJeu(Bâtiment sélection)
        {
            MajInformation();
            MajCaractéristiques(sélection);
            MajCarte();
            MajInterface();
            visuel.Interface(curseur, Jeu);
        }

        public void DéroulementJeu()
        {
            bool jouer = true;
            Bâtiment sélection = cité.Bâtiments[0];
            visuel.EcranDébutPartie(décision);
            if(décision.Choix=="3")
            {
                Tour = __TOURMAX;
            }
            while (Tour < __TOURMAX && jouer && cité.Points < Cité.__POINTSCITECOMPLETE)
            {
                MajJeu(sélection);
                curseur.DéplacementCurseur(visuel, Jeu);

                if (curseur.Y == Jeu.GetLength(1) - 1)
                {
                    int[] position = curseur.ConversionCurseurDansCaractéristiques();
                    if (Jeu[position[0], position[1]] == "\tAmélioration")
                    {
                        ActionAmélioration(sélection);
                    }
                    else if (Jeu[position[0], position[1]] == "\tRecrutement")
                    {
                        ActionRecrutement(sélection);
                    }
                    else if (Jeu[position[0], position[1]] == "\tConstruire")
                    {
                        ActionConstruire(sélection);
                    }
                    else if (Jeu[position[0], position[1]] == "\t* PASSER SON TOUR *")
                    {
                        string choix = PasserLeTour();
                        if (choix == "Tour passé")
                        {
                            MajJeu(sélection);
                            catastrophe.Dégats(cité, this, ref jouer);
                            visuel.Interface(curseur, Jeu);
                            Thread.Sleep(2000);
                            ActionDebutTour(catastrophe);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < cité.Bâtiments.Count; i++)
                    {
                        int[] position = curseur.ConversionCurseurDansCarte();
                        if (position[0] == cité.Bâtiments[i].Localisation[0] && position[1] == cité.Bâtiments[i].Localisation[1])
                        {
                            sélection = cité.Bâtiments[i];
                            Caractéristique = sélection.AffichageCaractéristiques();
                            MajJeu(sélection);
                        }
                    }
                }
                if (cité.Points == Cité.__POINTSCITECOMPLETE)
                {
                    visuel.EcranGagné();
                    Thread.Sleep(5000);
                }
            }
            RecommencerUnePartie(décision);
        }

        public void ActionAmélioration(Bâtiment sélection)
        {
            string[] information = new string[] { };
            if (sélection.Niveau < 4)
            {
                information = new string[] {"\t\n--------------------------------------------------------"+
                                           $"\t\nPour améliorer le bâtiment {sélection.Type}, il te faut : "+
                                           $"\t\n{sélection.CoûtProduction[sélection.Niveau].Amélioration()}"+
                                           $"\t\n--------------------------------------------------------",
                                           $"Améliorer",$"Rien"};
            }
            visuel.ChoixOuiNon(information);
            décision.VérificationChoix(visuel,information);
            if (décision.Choix == "1")
            {
                visuel.Interface(curseur, Jeu);
                sélection.Amélioration(curseur, this, cité, sélection);
                NbPersonnes = cité.NbPersonnages();
                MajJeu(sélection);
            }
            else
            {
                MajJeu(sélection);
            }
        }

        public void ActionRecrutement(Bâtiment sélection)
        {
            if (sélection is Sénat)
            {
                Sénat convertionSélection = (Sénat)sélection;
                string[] information = new string[] {"\t\n--------------------------------------------------------"+
                                                       $"\t\nPour recruter un colon il te faut : "+
                                                       $"\t\n{Sénat.__COUTCOLON.Recrutement()}"+
                                                       $"\t\n--------------------------------------------------------",
                                                       $"Recruter",$"Rien"};

                visuel.ChoixOuiNon(information);
                décision.VérificationChoix(visuel, information);
                if (décision.Choix == "1")
                {
                    Console.WriteLine("Combien de colons voulez-vous recruter ?");
                    string nb = Console.ReadLine();
                    while (décision.VérificationEntier(nb) == 0 || décision.VérificationEntier(nb)<0)
                    {
                        visuel.Erreur402(nb);
                        Console.WriteLine("Combien de colons voulez-vous recruter ?");
                        nb = Console.ReadLine();
                    }
                    convertionSélection.Recrutement(int.Parse(nb), cité, this, visuel);
                    MajJeu(sélection);
                }
                else
                {
                    MajJeu(sélection);
                }
            }
        }

        public void ActionConstruire(Bâtiment sélection)
        {
            string[] information = new string[] {"\t\n--------------------------------------------------------"+
                                            $"\t\nQuel bâtiment voulez-vous construire ? "+
                                            $"\t\n--------------------------------------------------------",
                                             "Mine","Ferme","Entrepôt","Rien"};
            visuel.ChoixMultiple(information);
            décision.VérificationChoix(visuel, information);
            if (décision.Choix == "1")
            {
                //Création d'un plan de bâtiment pour vérifier la possiblité de construction
                Mine mine = new Mine();
                information = new string[] {"\t\n--------------------------------------------------------"+
                                           $"\t\nPour constuire une Mine, il te faut : "+
                                           $"\t\n{mine.CoûtProduction[mine.Niveau].Amélioration()}"+
                                           $"\t\n--------------------------------------------------------",
                                           $"Construire",$"Rien"};
                visuel.ChoixOuiNon(information);
                décision.VérificationChoix(visuel, information);
                //Vérification de la décision et qu'une mine n'est pas déjà crée
                if (décision.Choix == "1" && Mine.__NOMBRE!=1)
                {
                    cité.Construction(curseur, mine, this);
                    //Maj des infos après la carte car c'est grâce à Majcarte que PopulationMax est mise à jour
                    MajJeu(sélection);
                }
                else
                {
                    visuel.Erreur403(mine);
                    visuel.Interface(curseur, Jeu);
                }
            }
            else if (décision.Choix == "2")
            {
                //Création d'un plan de bâtiment pour vérifier la possiblité de construction
                Ferme ferme = new Ferme();
                information = new string[] {"\t\n--------------------------------------------------------"+
                                            $"\t\nPour constuire une Ferme, il te faut : "+
                                            $"\t\n{ferme.CoûtProduction[ferme.Niveau].Amélioration()}"+
                                            $"\t\n--------------------------------------------------------",
                                            $"Construire",$"Rien"};
                visuel.ChoixOuiNon(information);
                décision.VérificationChoix(visuel, information);
                //Vérification de la décision et qu'une ferme n'est pas déjà crée
                if (décision.Choix == "1" && Ferme.__NOMBRE!=1)
                {
                    cité.Construction(curseur, ferme, this);
                    MajJeu(sélection);
                }
                else
                {
                    visuel.Erreur403(ferme);
                    visuel.Interface(curseur, Jeu);
                }
            }
            else if (décision.Choix == "3")
            {
                //Création d'un plan de bâtiment pour vérifier la possiblité de construction
                Entrepôt entrepôt = new Entrepôt();
                information = new string[] {"\t\n--------------------------------------------------------"+
                                            $"\t\nPour constuire un Entrepôt, il te faut : "+
                                            $"\t\n{entrepôt.CoûtProduction[entrepôt.Niveau].Amélioration()}"+
                                            $"\t\n--------------------------------------------------------",
                                            $"Construire",$"Rien"};
                visuel.ChoixOuiNon(information);
                décision.VérificationChoix(visuel, information);
                //Vérification de la décision et qu'une ferme n'est pas déjà crée
                if (décision.Choix == "1" && Entrepôt.__NOMBRE != 1)
                {
                    cité.Construction(curseur, entrepôt, this);
                    MajJeu(sélection);
                }
                else
                {
                    visuel.Erreur403(entrepôt);
                    visuel.Interface(curseur, Jeu);
                }
            }
            else
            {
                MajJeu(sélection);
            }
        }

        public void ActionDebutTour(CatastropheNaturelle castastrophe)
        {
            int OrRécoltés = 0;
            for(int i =0; i<cité.Bâtiments.Count;i++)
            {
                if (cité.Bâtiments[i] is Mine)
                {
                    OrRécoltés = ((Mine)cité.Bâtiments[i]).Rendement;
                    if(NbOr-OrRécoltés<=NbOrTotal)
                    {
                        NbOr += OrRécoltés;
                    }
                    
                }
            }
            string information = "\t\n--------------------------------------------------------"+
                                 $"\t\nTour n° {Tour} : "+
                                 $"\t\nNombre d'or récoltés : {OrRécoltés} "+
                                 $"\t\nUne catastrophe naturelle de puissance {catastrophe.Puissance} a touché "+
                                 $"\t\nvotre ville. Regardez l'état de vos bâtiments"+
                                 $"\t\n--------------------------------------------------------";
            information += "\t\n--------------------------------------------------------" +
                                 $"\t\nRécapitulatif des dégats : " +
                                 $"\t\n{catastrophe.RecapDégats}"+
                                 $"\t\n--------------------------------------------------------";
            visuel.PopUpInformation(information);
        }

        public string PasserLeTour()
        {
            string[] information = new string[] {"\t\n--------------------------------------------------------"+
                                                $"\t\n Vous pouvez passer votre tour"+
                                                $"\t\n--------------------------------------------------------",
                                                $"Oui",$"Non"};
            
            visuel.ChoixOuiNon(information);
            décision.VérificationChoix(visuel, information);
            if (décision.Choix == "1")
            {
                Tour += 1;
                return "Tour passé";
            }
            return "Tour non passé";

        }

        public void RecommencerUnePartie(ChoixUtilisateur décision)
        {
            string[] information = new string[] {"Voulez-vous recommencer une partie ?", "Oui", "Non" };
            visuel.ChoixOuiNon(information);
            décision.VérificationChoix(visuel, information);
            if (décision.Choix == "1")
            {
                //On remet à zéro le compteur afin de pouvoir recréer une cité avec un Sénat, etc...
                Sénat.__NOMBRE = 0;
                Ferme.__NOMBRE = 0;
                Mine.__NOMBRE = 0;
                Entrepôt.__NOMBRE = 0;
                GestionJeu newJeu = new GestionJeu();
                Tour = 0;
                newJeu.DéroulementJeu();
            }
            if (décision.Choix == "2")
            {
                visuel.EcranFinPartie();
            }

        }
    }  
}

