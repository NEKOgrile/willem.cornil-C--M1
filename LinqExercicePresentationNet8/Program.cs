using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DataSources;


var allAlbums = ListAlbumsData.ListAlbums;

// xml exercice

//1. Crée un XElement racine (ex: "Root")
//    XElement root = new XElement("Root");
//2. Parcours la liste avec foreach
//          foreach (var album in allAlbums)
//     {
//3. Pour chaque album, crée un XElement "Album"
//          XElement albumElement = new XElement("Album",
//4. Ajoute des sous-éléments "AlbumId" et "Title"
//               new XElement("AlbumId", album.AlbumId),
//                new XElement("Title", album.Title),
//                new XElement("ArtistId", album.ArtistId)
//          );
//5. Ajoute le tout à ta racine
//          root.Add(albumElement);
//        }
//6. Affiche le XML avec Console.WriteLine(...)
//    Console.WriteLine(root);


// Exemple de ligne d'affichage une fois terminé :
// Console.WriteLine(root);

//linq exercice

