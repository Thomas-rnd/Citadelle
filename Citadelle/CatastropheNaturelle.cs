using System;
namespace Citadelle
{
    public class CatastropheNaturelle
    {
        //Nombre de niveaux enlevé aux bâtiments
        public int Puissance { get; set;}
        //Pourcentage de bâtiments touchés
        public int[,] Zone { get; set; }
        public string RecapDégats { get; set; }
        private Random r;

        public CatastropheNaturelle()
        {
            r = new Random();
            RecapDégats="";
        }

        public void ChoixPuissance()
        {
            //50% de chance d'avoir une Puissance = 1
            int hasard = r.Next(0, 100);
            if(hasard%2==0)
            {
                Puissance = 1;
            }
            else
            {
                Puissance = 0;
            }
            //25% de chance d'avoir une Puissance = 2
            hasard = r.Next(0,100);
            if (hasard % 2 == 0 && Puissance==1)
            {
                Puissance = 2;
            }
            //12,5% de chance d'avoir une Puissance = 3
            hasard = r.Next(0,100);
            if (hasard % 2 == 0 && Puissance == 2)
            {
                Puissance = 3;
            }

        }

        public void ChoixZone(int x, int y)
        {
            //L'affichage cadré sur la carte commence en (1,0) en coordonnées dans le jeu. 
            int hasardX = r.Next(1, x);
            int hasardY = r.Next(0, y);
            int hasardX1 = r.Next(1, hasardX);
            int hasardY1 = r.Next(0, hasardY);
            Zone = new int[,] {{ hasardX1, hasardY1 },{ hasardX, hasardY }};
        }

        public void Dégats(Cité cité, GestionJeu jeu, ref bool jouer)
        {
            RecapDégats = "";
            ChoixPuissance();
            ChoixZone(cité.X, cité.Y);
            for (int i = Zone[0, 0]; i < Zone[1, 0]; i++)
            {
                for (int j = Zone[0, 1]; j < Zone[1, 1]; j++)
                {
                    if(jeu.Jeu[i, j] =="\t#")
                    {
                        jeu.Jeu[i, j] = "\tW";
                    }
                }
            }
            for (int i = cité.Bâtiments.Count - 1; i >= 0; i--)
            {
                //Vérification que le bâtiment soit dans la zone touchée par la catastrophe
                if (cité.Bâtiments[i].Localisation[0] <= Zone[1, 0] && cité.Bâtiments[i].Localisation[0] >= Zone[0, 0] && cité.Bâtiments[i].Localisation[1] <= Zone[1, 1] && cité.Bâtiments[i].Localisation[1] >= Zone[0, 1])
                {
                    if (cité.Bâtiments[i].Niveau > Puissance)
                    {
                        RecapDégats += $"\t\n Ancien niveau {cité.Bâtiments[i].Type} : {cité.Bâtiments[i].Niveau}";
                        cité.Bâtiments[i].Niveau -= Puissance;
                        cité.Bâtiments[i].DéfinitionCaractéristiques();
                        RecapDégats += $" => Nouveau niveau {cité.Bâtiments[i].Type} : {cité.Bâtiments[i].Niveau}";
                    }
                    else
                    {
                        if (cité.Bâtiments[i] is Ferme)
                        {
                            Ferme.__NOMBRE = 0;
                            RecapDégats += $"\t\n {cité.Bâtiments[i].Type} détruit(e)";
                        }
                        else if (cité.Bâtiments[i] is Entrepôt)
                        {
                            Entrepôt.__NOMBRE = 0;
                            RecapDégats += $"\t\n {cité.Bâtiments[i].Type} détruit(e)";
                        }
                        else if (cité.Bâtiments[i] is Mine)
                        {
                            Mine.__NOMBRE = 0;
                            RecapDégats += $"\t\n {cité.Bâtiments[i].Type} détruit(e)";
                        }
                        else if (cité.Bâtiments[i] is Sénat)
                        {
                            jouer = false;
                            Sénat.__NOMBRE = 0;
                            RecapDégats += $"\t\n {cité.Bâtiments[i].Type} détruit(e)";
                        }
                        cité.Bâtiments.Remove(cité.Bâtiments[i]);
                    }
                }
            }
        }
    }
}
