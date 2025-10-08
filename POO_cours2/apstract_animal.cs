using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_cours2
{
    internal abstract class apstract_animal
    {

        public abstract string name { get; set; }

        public abstract int age { get; set; }

        public abstract Zoo Zooici { get; set; }


        public abstract void Dormir();


        public abstract void Age();

        public void Manger()
        {
            Console.WriteLine(name + " mange");
        }
    }
}
