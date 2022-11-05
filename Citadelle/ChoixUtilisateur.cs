using System;
namespace Citadelle
{
    public class ChoixUtilisateur
    {
        public string Choix { get; set; }

        public ChoixUtilisateur()
        {
        }

        public void VérificationChoix(GestionAffichage visuel, string[] information)
        {
            Choix = "0";
            string décision = Console.ReadLine();
            //Vérification de la validité de la décision
            if (information.Length == 3)
            {
                while (VérificationEntier(décision) == 0 || VérificationEntier(décision) > 2 || VérificationEntier(décision) < 0)
                {
                    visuel.Erreur402(décision);
                    visuel.ChoixOuiNon(information);
                    décision = Console.ReadLine();
                }
            }
            else
            {
                while (VérificationEntier(décision) == 0 || VérificationEntier(décision) > 3 || VérificationEntier(décision) < 0)
                {
                    visuel.Erreur402(décision);
                    visuel.ChoixMultiple(information);
                    décision = Console.ReadLine();
                }
            }
            Choix = décision;
        }

        public int VérificationEntier(string type)
        {
            int résultat;
            int.TryParse(type, out résultat);
            return résultat;
        }

    }
}
