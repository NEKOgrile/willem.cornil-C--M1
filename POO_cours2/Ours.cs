using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_cours2
{
    internal class Ours : Animal
    {

        public Ours(Zoo zoo) : base(zoo)
        {
        }
        public void Hiberner()
        {
            Console.WriteLine(name + " hiberne");
        }

    }
}
