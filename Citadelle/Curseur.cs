using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    /// <summary>
    /// Il me reste à gérer le fais que le curseur peut se balader dans la carte, dans les caractéristiques ou
    /// dans la zone des actions possibles mais pas ailleurs 
    /// </summary>
    public class Curseur
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Curseur()
        {
            //Curseur indice respecte les indices de la carte dans l'affichage donc commence à (1,0) 
            X = 1;
            Y = 0;
        }

        public void DéplacementCurseur(GestionAffichage visuel, string [,] affichage)
        {
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.DownArrow && X < affichage.GetLength(0) - 1)
                {
                    X += 1;
                    visuel.Interface(this, affichage);
                }
                if (Console.ReadKey(true).Key == ConsoleKey.RightArrow && Y < affichage.GetLength(1) - 1)
                {
                    Y += 1;
                    visuel.Interface(this, affichage);
                }
                if (Console.ReadKey(true).Key == ConsoleKey.UpArrow && X > 1)
                {
                    X -= 1;
                    visuel.Interface(this, affichage);
                }
                if (Console.ReadKey(true).Key == ConsoleKey.LeftArrow && Y >= 1)
                {
                    Y -= 1;
                    visuel.Interface(this, affichage);
                }
            }
            Console.Clear();
        }

        
        public int[] ConversionCurseurDansCarte()
        {
            int[] res = new int[] { X - 1, Y };
            return res;
        }

        public int[] ConversionCurseurDansCaractéristiques()
        {
            int[] res = new int[] { X , Y };
            return res;
        }

    }
}
