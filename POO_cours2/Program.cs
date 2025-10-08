using System;
using POO_cours2;

class Program
{
    static void Main(string[] args)
    {
        Adress monAdresse = new Adress("rue des lilas", "paris", "france");

        Zoo monZoo = new Zoo("zooland" , monAdresse);
        monZoo.Show();

        monZoo.Adresse.Show();

        Lion simba = new Lion(monZoo);
        simba.name = "simba";
        simba.age = 3;
        simba.Dormir();
        simba.Age();
        simba.Rugir();
        simba.Zooici.Adresse.Show();
       
        simba.Manger();

        Ours balou = new Ours(monZoo);
        balou.name = "balou";
        balou.age = 5;
        balou.Dormir();
        balou.Age();
        balou.Zooici.Adresse.Show();
        balou.Manger();
        balou.Hiberner();



        Console.ReadLine();
    }
}
