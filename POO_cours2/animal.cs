using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_cours2
{
    internal class Animal : apstract_animal
    {
        public override string name { get; set; }

        public override int age { get; set; }

        public override Zoo Zooici { get; set; }

        public Animal(Zoo zoo)
        {
            Zooici = zoo;
        }

        public override void Dormir()
        {
            Console.WriteLine(name + " dort");
        }

        public override void Age()
        {
            Console.WriteLine(name + " a " + age + " ans");
        }
    }
}
