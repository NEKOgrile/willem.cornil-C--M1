using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_cours2
{
    internal class Lion : Animal
    {
        public Lion(Zoo zoo) : base(zoo)
        {
        }
        public void Rugir()
        {
            Console.WriteLine(name + " rugit");
        }
    }
}
