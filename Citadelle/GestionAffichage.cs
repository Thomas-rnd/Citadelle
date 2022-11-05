using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public class GestionAffichage
    {

        public GestionAffichage()
        {
        }

        public void Interface(Curseur curseur, string[,] affichage)
        {
            Console.Clear();
            CouleurParDéfaut();
            int[] position = curseur.ConversionCurseurDansCarte();
            Console.WriteLine($"Curseur : [{position[0]},{position[1]}]");
            for (int lignes = 0; lignes < affichage.GetLength(0); lignes++)
            {
                for (int colonnes = 0; colonnes < affichage.GetLength(1); colonnes++)
                {
                    if (lignes == 0)
                    {
                        GestionCouleurs(lignes, colonnes, curseur, affichage, "Informations");
                    }
                    else if (colonnes == affichage.GetLength(1) - 1)
                    {
                        GestionCouleurs(lignes, colonnes, curseur, affichage, "Caractéristiques");
                    }
                    else
                    {
                        //Zone vierge de la carte
                        if (affichage[lignes, colonnes] == "\t#")
                        {
                            GestionCouleurs(lignes, colonnes, curseur, affichage, "Carte");
                        }
                        //Zone de la carte touchée par une catastrophe naturelle
                        else if (affichage[lignes, colonnes] == "\tW")
                        {
                            GestionCouleurs(lignes, colonnes, curseur, affichage, "Catastrophe");
                        }
                        else
                        {
                            GestionCouleurs(lignes, colonnes, curseur, affichage, "Bâtiment");
                        }

                    }
                }
                Console.WriteLine("");
                CouleurParDéfaut();
            }
        }

        public void GestionCouleurs(int ligne, int colonne, Curseur curseur, string[,] affichage, string zone)
        {
            CouleurParDéfaut();
            if (zone == "Informations")
            {
                //Couleur curseur
                if (ligne == curseur.X && colonne == curseur.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(affichage[ligne, colonne]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(affichage[ligne, colonne]);
                }
            }
            else if (zone == "Caractéristiques")
            {
                //Couleur curseur
                if (ligne == curseur.X && colonne == curseur.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(affichage[ligne, colonne]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(affichage[ligne, colonne]);
                }
            }
            else if (zone == "Bâtiment")
            {
                //Couleur curseur
                if (ligne == curseur.X && colonne == curseur.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(affichage[ligne, colonne]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(affichage[ligne, colonne]);
                }
            }
            else if (zone == "Catastrophe")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(affichage[ligne, colonne]);
            }
            else
            {
                //Couleur curseur
                if (ligne == curseur.X && colonne == curseur.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(affichage[ligne, colonne]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(affichage[ligne, colonne]);
                }
            }
        }

        public void RessourceAmélioration(string[] amélioration, string[,] affichage)
        {
            affichage[affichage.GetLength(0) - 3, affichage.GetLength(1) - 3] = amélioration[0];
            affichage[affichage.GetLength(0) - 2, affichage.GetLength(1) - 2] = amélioration[1];
            affichage[affichage.GetLength(0) - 1, affichage.GetLength(1) - 1] = amélioration[2];
        }

        public void ChoixOuiNon(string[] information)
        {
            Console.Clear();
            CouleurParDéfaut();
            Console.Write(@$"
{information[0]}

                                            Que voulez-vous faire?

                                         ____________________________
                                        |     1. {information[1]}    |
                                         ----------------------------
                                         ____________________________
                                        |     2. {information[2]}    |
                                         ----------------------------

                                        Entrez votre choix <1/2> :");
        }

        public void ChoixMultiple(string[] information)
        {
            Console.Clear();
            CouleurParDéfaut();
            Console.Write(@$"
{information[0]}

                                            Que voulez-vous faire?

                                         ____________________________
                                        |     1. {information[1]}    |
                                         ----------------------------
                                         ____________________________
                                        |     2. {information[2]}    |
                                         ----------------------------
                                         ____________________________
                                        |     3. {information[3]}    |
                                         ----------------------------

                                        Entrez votre choix <1/2/3> :");
        }

        public void PopUpInformation(string information)
        {
            Console.Clear();
            CouleurParDéfaut();
            Console.Write($"{information}");
            Console.WriteLine("\nAppuyez sur entrée pour revenir à la page principale : "); Console.ReadLine();
        }

        public void RègleDuJeu(ChoixUtilisateur décision)
        {
            Console.Clear();
            CouleurParDéfaut();
            Console.ForegroundColor = System.ConsoleColor.DarkGreen;
            Console.Write(@"
                                                           ▄████████    ▄████████    ▄██████▄   ▄█          ▄████████    ▄████████      ████████▄  ███    █▄            ▄█    ▄████████ ███    █▄  
                                                          ███    ███   ███    ███   ███    ███ ███         ███    ███   ███    ███      ███   ▀███ ███    ███          ███   ███    ███ ███    ███ 
                                                          ███    ███   ███    █▀    ███    █▀  ███         ███    █▀    ███    █▀       ███    ███ ███    ███          ███   ███    █▀  ███    ███ 
                                                         ▄███▄▄▄▄██▀  ▄███▄▄▄      ▄███        ███        ▄███▄▄▄       ███             ███    ███ ███    ███          ███  ▄███▄▄▄     ███    ███ 
                                                        ▀▀███▀▀▀▀▀   ▀▀███▀▀▀     ▀▀███ ████▄  ███       ▀▀███▀▀▀     ▀███████████      ███    ███ ███    ███          ███ ▀▀███▀▀▀     ███    ███ 
                                                        ▀███████████   ███    █▄    ███    ███ ███         ███    █▄           ███      ███    ███ ███    ███          ███   ███    █▄  ███    ███ 
                                                          ███    ███   ███    ███   ███    ███ ███▌    ▄   ███    ███    ▄█    ███      ███   ▄███ ███    ███          ███   ███    ███ ███    ███ 
                                                          ███    ███   ██████████   ████████▀  █████▄▄██   ██████████  ▄████████▀       ████████▀  ████████▀       █▄ ▄███   ██████████ ████████▀  
                                                          ███    ███                           ▀                                                                   ▀▀▀▀▀▀                          
                                    ");
            CouleurParDéfaut();

            Console.Write(@"   

                                                                Citadelle est un jeu de gestion de colonies qui a été codé sur le langage de programmation orientée objet C#.

                                            	_______________________________________________________________________________________________________________________


                                	Au lancement du jeu, vous vous retrouverez face à une carte en 2D de format 15 (lignes) x 10 (colonnes). Le but du jeu est ensuite simple : GAGNER !

    	Pour ce faire, il vous faut développer votre Cité en un temps limité. À savoir, ici, construire puis augmenter tous vos bâtiments (Sénat, Entrepôt, Ferme et Mine) au niveau maximal (4) avant le 20ème et dernier tour.


    	-    Mais, comment améliorer mes bâtiments ?


    	Vous pouvez construire (sauf le Sénat qui est initialement présent sur la carte au Tour 0) puis améliorer vos bâtiments grâce à deux ressources : l’or et vos colons.
    	En effet, chaque construction ou amélioration a un coût : une quantité d’or et un nombre de colons donnés.
    	Vous pouvez donc réaliser une des deux actions susmentionnées si et seulement si vos ressources en or et de colons sont supérieures ou égales aux quantités exigées par ladite action à mener.

                                            	_______________________________________________________________________________________________________________________

    	-    Comment gagner plus et stocker plus ?


    	Chaque bâtiment a un rôle bien précis.

    	Si le Sénat permet de recruter des colons (avec de l’or) et de construire un bâtiment (s’il n’existe pas encore), les trois autres bâtiments ont purement un rôle dans le stockage et la production.
    	La ferme fixe la taille de la population de votre Cité (habitants utilisés dans les bâtiments + colons).
    	L’entrepôt permet de stocker l’or de la Cité.
    	La mine permet de produire de l’or.

    	Améliorer un bâtiment permettra donc à celui-ci d’améliorer ses capacités dans son domaine de compétence.

                                            	_______________________________________________________________________________________________________________________


    	Vous êtes sûrement en train de vous dire que ce jeu est d’une trivialité encore jamais vue auparavant et qu’il vous suffira de 3 tours pour le terminer.
    	Cela serait faire preuve de naïveté quant à notre capacité à vous mener la vie dure...

    	En effet, votre Cité, bien que n’attirant pas de monstres mythologiques ou les plus grandes armées que notre Terre ait connu, sera, à chaque fin de tour, à la merci de catastrophes naturelles.
    	Celles-ci pourront, chaque nuit, endommager votre Cité et, avec, toutes les améliorations que vous y avez apportées. Une catastrophe naturelle a 50% de chance d’être de puissance 1, 25% de chance d’être de puissance 2 et
    	12,5% de chance d’être de puissance 3.
    	La zone balayée par cette catastrophe est aléatoire : elle peut aller d’un simple point à.. l’entièreté de la carte. Chaque point de la carte touchée par cette catastrophe est symbolisé par un “W”.

    	Deux cas de figures sont alors envisageables :

            	-    Si la puissance de la catastrophe est supérieure ou égale au niveau d’un bâtiment donné, celui-ci sera immédiatement détruit.
            	-    Dans le cas où la catastrophe est de puissance strictement inférieure au niveau du bâtiment, celui-ci perd un nombre de niveau égale à la puissance de la catastrophe.

                                            	_______________________________________________________________________________________________________________________

    	-    Comment... perdre ?


    	Il y a 2 façons de perdre.

    	La première est évidente : échouer à développer suffisamment votre Cité (cf plus haut) aux termes des 20 tours.
    	La deuxième est que votre Sénat soit détruit par une catastrophe naturelle. En effet, étant le seul bâtiment qui ne peut pas être (re)construit, sa destruction entraîne la fin de la partie.

                                            	_______________________________________________________________________________________________________________________

");
            Console.WriteLine(" ");

            Console.ForegroundColor = System.ConsoleColor.Red;
            Console.WriteLine("                                                                                              BON JEU A TOI !!!");

            CouleurParDéfaut();
            Console.WriteLine("                                                                             Appuyez sur entrée pour revenir à la page principale : "); Console.ReadLine();
            Console.Clear();
            EcranDébutPartie(décision);
        }

        public void EcranDébutPartie(ChoixUtilisateur décision)
        {
            Console.Clear();
            CouleurParDéfaut();
            string[] information = new string[] {@"
                
                 ▄████████  ▄█      ███        ▄████████ ████████▄     ▄████████  ▄█        ▄█          ▄████████ 
                ███    ███ ███  ▀█████████▄   ███    ███ ███   ▀███   ███    ███ ███       ███         ███    ███ 
                ███    █▀  ███▌    ▀███▀▀██   ███    ███ ███    ███   ███    █▀  ███       ███         ███    █▀  
                ███        ███▌     ███   ▀   ███    ███ ███    ███  ▄███▄▄▄     ███       ███        ▄███▄▄▄     
                ███        ███▌     ███     ▀███████████ ███    ███ ▀▀███▀▀▀     ███       ███       ▀▀███▀▀▀     
                ███    █▄  ███      ███       ███    ███ ███    ███   ███    █▄  ███       ███         ███    █▄  
                ███    ███ ███      ███       ███    ███ ███   ▄███   ███    ███ ███▌    ▄ ███▌    ▄   ███    ███ 
                ████████▀  █▀      ▄████▀     ███    █▀  ████████▀    ██████████ █████▄▄██ █████▄▄██   ██████████ 
                                                                                 ▀         ▀                      


                                        Par LEGER Sébastien & RENAUD Thomas

",
                                       $"Jouer",$"Règles du jeu","Quitter"};

            ChoixMultiple(information);
            décision.VérificationChoix(this, information);
            if (décision.Choix == "2")
            {
                RègleDuJeu(décision);
            }
        }

        public void EcranFinPartie()
        {
            CouleurParDéfaut();
            Console.WriteLine(@"


                       ▄████████  ▄█  ███▄▄▄▄        ████████▄  ███    █▄            ▄█    ▄████████ ███    █▄  
                      ███    ███ ███  ███▀▀▀██▄      ███   ▀███ ███    ███          ███   ███    ███ ███    ███ 
                      ███    █▀  ███▌ ███   ███      ███    ███ ███    ███          ███   ███    █▀  ███    ███ 
                     ▄███▄▄▄     ███▌ ███   ███      ███    ███ ███    ███          ███  ▄███▄▄▄     ███    ███ 
                    ▀▀███▀▀▀     ███▌ ███   ███      ███    ███ ███    ███          ███ ▀▀███▀▀▀     ███    ███ 
                      ███        ███  ███   ███      ███    ███ ███    ███          ███   ███    █▄  ███    ███ 
                      ███        ███  ███   ███      ███   ▄███ ███    ███          ███   ███    ███ ███    ███ 
                      ███        █▀    ▀█   █▀       ████████▀  ████████▀       █▄ ▄███   ██████████ ████████▀  
                                                                                ▀▀▀▀▀▀                          

");
        }

        public void EcranGagné()
        {
            Console.Clear();
            CouleurParDéfaut();
            Console.ForegroundColor = System.ConsoleColor.DarkRed;
            Console.WriteLine(@"

          ▄████████    ▄████████  ▄█        ▄█   ▄████████  ▄█      ███        ▄████████     ███      ▄█   ▄██████▄  ███▄▄▄▄   
          ███    ███   ███    ███ ███       ███  ███    ███ ███  ▀█████████▄   ███    ███ ▀█████████▄ ███  ███    ███ ███▀▀▀██▄ 
          ███    █▀    ███    █▀  ███       ███▌ ███    █▀  ███▌    ▀███▀▀██   ███    ███    ▀███▀▀██ ███▌ ███    ███ ███   ███ 
         ▄███▄▄▄      ▄███▄▄▄     ███       ███▌ ███        ███▌     ███   ▀   ███    ███     ███   ▀ ███▌ ███    ███ ███   ███ 
        ▀▀███▀▀▀     ▀▀███▀▀▀     ███       ███▌ ███        ███▌     ███     ▀███████████     ███     ███▌ ███    ███ ███   ███ 
          ███          ███    █▄  ███       ███  ███    █▄  ███      ███       ███    ███     ███     ███  ███    ███ ███   ███ 
          ███          ███    ███ ███▌    ▄ ███  ███    ███ ███      ███       ███    ███     ███     ███  ███    ███ ███   ███ 
          ███          ██████████ █████▄▄██ █▀   ████████▀  █▀      ▄████▀     ███    █▀     ▄████▀   █▀    ▀██████▀   ▀█   █▀  
                          ▀                                                                                             

                                                                                ");
            
            Console.WriteLine(@"
                                        --------------------------------------------
                                       |            Vous avez gagné !!!!            |
                                       |          Votre cité est complète           |
                                        --------------------------------------------
");

            Console.WriteLine("");
        }

        public void Erreur402(string paramètre)
        {
            Console.Clear();
            CouleurParDéfaut();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"
Error 402 : Entrée d'un champ non valide

Description : La réponse rentrée ne convient pas avec le format demandé.

Source Error : Décision non reconnue : {0}

Veuillez appuyer sur entrée : ", paramètre); Console.ReadLine();
            
        }

        public void Erreur404(int orDispo, int orCoût, int populationDispo, int populationDemandée)
        {
            Console.Clear();
            CouleurParDéfaut();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"
Error 404 : Condition non remplie pour réaliser l'action souhaitée

Description : Vous avez choisi de dépenser de l'argent ou faire appel à des colons
              que vous ne disposez pas actuellement. Faites vos achats et revenez
              plus tard.

Source Error : Décision non reconnue : [Or disponible] {0} < [Coût] {1}
            ou/et [Population disponible] {2} < [Coût] {3}
            

Veuillez appuyer sur entrée : ", orDispo, orCoût, populationDispo, populationDemandée); Console.ReadLine();
            
        }

        public void Erreur403(Bâtiment bâtiment)
        {
            Console.Clear();
            CouleurParDéfaut();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@$"
Error 403 : Condition non remplie pour réaliser l'action souhaitée

Description : Vous essayez de construire un bâtiment déjà construit.

Source Error : Décision non reconnue : [Bâtiment construit] {bâtiment.Type} 
            

Veuillez appuyer sur entrée : "); Console.ReadLine();
            
        }

        public void CouleurParDéfaut()
        {
            Console.BackgroundColor = System.ConsoleColor.White;
            Console.ForegroundColor = System.ConsoleColor.Black;
        }
    }

}
