using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    public class Colon : Peuple
    {
        public Colon()
        {          
            Spécialité = "Colon";
            Défense = 1;
            Coordonnées = new int[] { 5, 4 };
            //Déplacement = 3;
            //HP uniquement pour le monstre 
            PointsVie = 0;
            Moral = 100;
            Faim = 100;
        }

        public override void Construire(int[] coordonnées, Bâtiment batConstruit)
        {
            
        }

        public override void Manger()
        {

        }

        public override void SeDéplacer(Curseur curseur, int [] coordonnéesBâtiment, GestionJeu jeu)
        {
            string élémentSupprimé = "";
            while (coordonnéesBâtiment[0] != Coordonnées[0])
            {
                if (coordonnéesBâtiment[0] < Coordonnées[0])
                {
                    Coordonnées[0] -= 1;
                    // Affichage du colon en sauvegardant l'élément remplacé pour ne pas perdre l'information
                    // présente sur la carte
                    élémentSupprimé = jeu.Carte[Coordonnées[0], Coordonnées[1]];
                    jeu.Carte[Coordonnées[0], Coordonnées[1]] = "\tc";
                    // Affichage du colon
                    jeu.MajInterface();
                    jeu.visuel.Interface(curseur, jeu.Jeu);
                    
                }
                else if (coordonnéesBâtiment[0] > Coordonnées[0])
                {
                    Coordonnées[0] += 1;
                    élémentSupprimé = jeu.Carte[Coordonnées[0], Coordonnées[1]];
                    jeu.Carte[Coordonnées[0], Coordonnées[1]] = "\tc";
                    jeu.MajInterface();
                    jeu.visuel.Interface(curseur, jeu.Jeu);
                }
                //Supression du colon qd il part de la case
                jeu.Carte[Coordonnées[0], Coordonnées[1]] = élémentSupprimé;
                jeu.MajInterface();
                // Mise en place d'une pause de 1s dans le programme pour bien voir le déplacement du colon
                Thread.Sleep(1000);
            }

            while(coordonnéesBâtiment[1] != Coordonnées[1])
            {
                if (coordonnéesBâtiment[1] < Coordonnées[1])
                {
                    Coordonnées[1] -= 1;
                    élémentSupprimé = jeu.Carte[Coordonnées[0], Coordonnées[1]];
                    jeu.Carte[Coordonnées[0], Coordonnées[1]] = "\tc";
                    jeu.MajInterface();
                    jeu.visuel.Interface(curseur, jeu.Jeu);
                }
                else if (coordonnéesBâtiment[1] > Coordonnées[1])
                {
                    Coordonnées[1] += 1;
                    élémentSupprimé = jeu.Carte[Coordonnées[0], Coordonnées[1]];
                    jeu.Carte[Coordonnées[0], Coordonnées[1]] = "\tc";
                    jeu.MajInterface();
                    jeu.visuel.Interface(curseur, jeu.Jeu);
                }
                jeu.Carte[Coordonnées[0], Coordonnées[1]] = élémentSupprimé;
                Thread.Sleep(1000);
            }
        }
    }
}
