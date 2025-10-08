using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace XmlExercice
{
    // Classe représentant un album
    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Étape 1 : créer la liste d'albums
            List<Album> albums = new List<Album>
            {
                new Album { Id = 1, Title = "For Those About To Rock We Salute You" },
                new Album { Id = 2, Title = "Balls to the Wall" },
                new Album { Id = 3, Title = "Restless and Wild" },
                new Album { Id = 4, Title = "Let There Be Rock" },
                new Album { Id = 5, Title = "Big Ones" },
                new Album { Id = 6, Title = "Jagged Little Pill" },
                new Album { Id = 7, Title = "Facelift" }
            };

            // Étape 2 : à toi de jouer !
            // Ici tu vas transformer la liste 'albums' en XML.
            // 1. Crée un XElement racine (ex: "Root")
            // 2. Parcours la liste avec foreach
            // 3. Pour chaque album, crée un XElement "Album"
            // 4. Ajoute des sous-éléments "AlbumId" et "Title"
            // 5. Ajoute le tout à ta racine
            // 6. Affiche le XML avec Console.WriteLine(...)



            //1. Crée un XElement racine (ex: "Root")
            XElement root = new XElement("Root");
            //2. Parcours la liste avec foreach
            foreach (var album in albums)
            {
                //3. Pour chaque album, crée un XElement "Album"
                XElement albumElement = new XElement("Album",
                    //4. Ajoute des sous-éléments "AlbumId" et "Title"
                    new XElement("AlbumId", album.Id),
                    new XElement("Title", album.Title)
                );
                //5. Ajoute le tout à ta racine
                root.Add(albumElement);
            }
            //6. Affiche le XML avec Console.WriteLine(...)
            Console.WriteLine(root);


            // Exemple de ligne d'affichage une fois terminé :
            // Console.WriteLine(root);
        }
    }
}
