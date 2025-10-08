using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_cours2
{
    internal class Adress
    {
        public string Rue { get; set; }

        public string Ville { get; set; }

        public string Pays { get; set; }

        public Adress(string rue, string ville, string pays)
                {
                    Rue = rue;
                    Ville = ville;
                    Pays = pays;
                }
                public void Show()
                {
                    Console.WriteLine("Adresse: " + Rue + ", " + Ville + ", " + Pays);
                }


    }
}
