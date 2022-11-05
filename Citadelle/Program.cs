using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Citadelle
{
    class Program
    {
        static void Main(string[] args)
        { 
            GestionJeu lancement = new GestionJeu();
            lancement.DéroulementJeu();
        }

    }
}
