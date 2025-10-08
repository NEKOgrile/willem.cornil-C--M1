using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_cours2
{
    internal class Zoo
    {
        private string name;

        public Adress Adresse { get; set; }

        //construdteur
        public Zoo(string zooName , Adress adresse)
        {
            name = zooName;
            Adresse = adresse;
        }


        public void Show()
        {
            Console.WriteLine("Bienvenue au zoo de" + name);
        }
    }
}

