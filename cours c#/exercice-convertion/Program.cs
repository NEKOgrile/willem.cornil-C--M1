
//1.Demander à l’utilisateur le chemin du fichier d’entrée (XML).

//2. Lire et explorer les données

//3.Détecter les champs disponibles (ex : id, name, …) à partir des éléments trouvés.

//4.Proposer à l’utilisateur d’appliquer un tri (choisir le champ + ordre croissant/décroissant).

//5.Afficher un aperçu (preview) des résultats (ex. 10 premiers objets, pagination possible).

//6.Exporter les résultats en JSON (fichier de sortie), en veillant à ce que le format d’entrée ≠ format de sortie.

//7.Option bonus : permettre de choisir quelles colonnes/champs exporter (sélection de champs).

//si il y a des phaute d ortographe c est normal j ai esseyer de genere le max de commantaire avec l auto completion et mes mots mais defoit ça voulais pas


//1.Demander à l’utilisateur le chemin du fichier d’entrée (XML).
using System.Xml.Linq;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

Console.WriteLine("Entrez le chemin COMPLET  du fichier XML d'entrée :");
string cheminXml = Console.ReadLine();

// je vérifie que le fichier existe
if (!File.Exists(cheminXml))
{
    Console.WriteLine("Fichier introuvable. Vérifiez le chemin et réessayez.");
    return;
}

XDocument doc = XDocument.Load(cheminXml);
Console.WriteLine("Fichier XML chargé avec succès !");
Console.WriteLine(doc.Root.Name);

//2. Lire et explorer les données
var elements = doc.Root.Elements();

//2.1 afficher le premier element pour voir sa structure
var premierElement = elements.First();

//Console.WriteLine("Premier élément trouvé : " + premierElement + premierElement.Name);

//3.Détecter les champs disponibles (ex : id, name, …) à partir des éléments trouvés.
var champs = premierElement.Elements().Select(e => e.Name.LocalName).ToList();
//Console.WriteLine("Champs disponibles : " + string.Join(", ", champs));

//4.Proposer à l’utilisateur d’appliquer un tri (choisir le champ + ordre croissant/décroissant).
Console.WriteLine("Champs disponibles : " + string.Join(", ", champs));
Console.WriteLine("Choisissez un champ pour le tri :");
string champTri = Console.ReadLine();
if (!champs.Contains(champTri))
{
    Console.WriteLine("Champ invalide. Opération annulée.");
    return;
}
Console.WriteLine("Choisissez l'ordre de tri (1 : asc / 2 : desc) :");
string ordreTri = Console.ReadLine().ToLower();
bool ordreAscendant = ordreTri == "1" || ordreTri == "asc" || ordreTri == "croissant";
bool ordreDescendant = ordreTri == "2" || ordreTri == "desc" || ordreTri == "décroissant";
if (!ordreAscendant && !ordreDescendant)
{
    Console.WriteLine("Ordre de tri invalide. Opération annulée.");
    return;
}


//5.Afficher un aperçu (preview) des résultats (ex. 10 premiers objets, pagination possible).

//5.1 ici je recupaire tout les elements puis je filtre tout ce qui s apelle 'champTri' et qui contien une valeur
var itemsWithField = elements.Where(b => b.Element(champTri) != null);

//5.2 je déclare une variable pour stocker le resultat du tri
// je le cree avent le if car on peut pas utiliser une variable declarée dans un if en dehors de ce meme if
IEnumerable<XElement> resultatTrie;

//5.3 j'applique le tri en fonction de l'ordre choisi
if (ordreAscendant)
{
    //5.3.1 je trie en ordre croissant en utilisant OrderBy
    //je met tout en sting pour eviter les erreur de comparaison
    //?? permet de gerer le cas null en le remplaçant par une chaine vide
    //Trim pour enlever les espaces avant et apres
    //ToLowerInvariant pour tout mettre en minuscule

    resultatTrie = from b in itemsWithField
                   orderby (((string)b.Element(champTri)) ?? "").Trim().ToLowerInvariant()
                   select b;

}
else // ordreDescendant
{
    resultatTrie = from b in itemsWithField
                   orderby (((string)b.Element(champTri)) ?? "").Trim().ToLowerInvariant() descending
                   select b;
}


Console.WriteLine("Tri appliqué avec succès !");

// IMPORTANT : utiliser resultatTrie ici, pas elements
var apercu = resultatTrie.Take(10);
Console.WriteLine("Aperçu des 10 premiers éléments :");
foreach (var elem in apercu)
{
    Console.WriteLine(elem);
}

//6.Exporter les résultats en JSON (fichier de sortie), en veillant à ce que le format d’entrée ≠ format de sortie.
Console.WriteLine("Entrez le chemin COMPLET du fichier JSON de sortie :");
string cheminJson = Console.ReadLine();
//6.1 je vérifie que le chemin n'est pas vide
if (string.IsNullOrWhiteSpace(cheminJson))
{
    Console.WriteLine("Chemin de sortie invalide. Opération annulée.");
    return;
}
//6.2 je verifie que le fichier de sortie n'est pas le meme que le fichier d'entrée
if (Path.GetFullPath(cheminJson) == Path.GetFullPath(cheminXml))
{
    Console.WriteLine("Le fichier de sortie ne peut pas être le même que le fichier d'entrée. Opération annulée.");
    return;
}
//6.3 je verifie que le dossier de sortie existe 
string dossierSortie = Path.GetDirectoryName(cheminJson);
if (!Directory.Exists(dossierSortie))
{
    Console.WriteLine("Le dossier de sortie n'existe pas. Opération annulée.");
    return;
}

//6.4 transformer chaque element xml en dictionaire
//ici x = tout les element xml trier
//ToDictionary permet de creer un dictionnaire ou el = chaque element xml 
//exemple el.Name.LocalName = "name" et (string)el = "block de pierre"
// ca donnera un dictionnaire { "name" : "block de pierre"}
//et on fait ça pour chaque element xml pour avoir tout le xml en dictionnaire
var listeDict = resultatTrie.Select(x => x.Elements()
                                        .ToDictionary(el => el.Name.LocalName, el => (string)el))
                                        .ToList();



//6.5 je serialise en json avec des retour a la ligne pour que ce soit lisible pour un humain ici moi 
var jsonString = JsonSerializer.Serialize(listeDict, new JsonSerializerOptions { WriteIndented = true });

//6.6 j'ecris le json dans le fichier de sortie
File.WriteAllText(cheminJson, jsonString);
Console.WriteLine("Données exportées avec succès vers " + cheminJson);



//7.Option bonus : permettre de choisir quelles colonnes/champs exporter (sélection de champs).
Console.WriteLine("Voulez-vous sélectionner des champs spécifiques à exporter ? (oui/non) :");
string reponse = Console.ReadLine().ToLower();
if (reponse == "yes" || reponse == "oui" || reponse == "y" || reponse == "o")
{
    //7.1 afficher les champs disponibles
    Console.WriteLine("Entrez les champs à exporter, ceparer par des virgule parmie les champs suivant " + string.Join(", ", champs) + " (ex : id,name) :");
    string champsSelectionnesInput = Console.ReadLine();
    //7.2 je verifie que l'utilisateur a bien entré quelque chose
    var champsSelectionnes = champsSelectionnesInput.Split(',')
                                                   .Select(c => c.Trim())
                                                   .Where(c => champs.Contains(c))
                                                   .ToList();

    //7.3 je verifie qu'il y a au moins un champ valide
    if (champsSelectionnes.Count == 0)
    {
        Console.WriteLine("Aucun champ valide sélectionné. Opération annulée.");
        return;
    }
    //7.4 je filtre les dictionnaires pour ne garder que les champs sélectionnés
    // je fais ça en recréant une nouvelle liste de dictionnaire
    // pour chaque dictionnaire dans listeDict(que j ai cree plutot), je garde seulement les paires clé/valeur
    // dont la clé est dans champsSelectionnes
    // puis je recrée un dictionnaire avec ces paires clé/valeur filtrées
    //je les filtre grace a Where et je recrée un dictionnaire avec ToDictionary
    //exemple : si champsSelectionnes = ["id", "name"] et que j'ai un dictionnaire { "id": "1", "name": "block de pierre", "type": "solid" } 
    // alors le dictionnaire filtré sera { "id": "1", "name": "block de pierre" }
    //on peut le voir avec where kv => champsSelectionnes.Contains(kv.Key) ce qui veux dire "garde seulement les cles et valeurs ou la cles est dans champsSelectionnes"
    //kv veux dire kelf et valeur
    var listeDictFiltree = listeDict.Select(d => d.Where(kv => champsSelectionnes.Contains(kv.Key))
                                                  .ToDictionary(kv => kv.Key, kv => kv.Value))
                                    .ToList();

    //je met les .todictionary a la fin de chaque select pour recrée un dictionnaire a chaque fois
    //car sinon ça maffiche juste un json malformer avec seulement KEY : name valeur : 'valeur' et non name : 'valeur' ....

    //7.5 je serialise en json avec des retour a la ligne pour que ce soit lisible pour un humain ici moi
    var jsonStringFiltree = JsonSerializer.Serialize(listeDictFiltree, new JsonSerializerOptions { WriteIndented = true });

    //7.6 je reecrase le fichier de sortie avec les données filtrées
    File.WriteAllText(cheminJson, jsonStringFiltree);
    Console.WriteLine("Données filtrées exportées avec succès vers " + cheminJson);

}



















/* ========================= DOCS DE RÉFÉRENCE =========================
1. Lecture et manipulation de XML avec XDocument / XElement
   - XDocument.Load, XElement.Elements, XElement.Element
   - Docs Microsoft : https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument
   - Exemple général : https://learn.microsoft.com/en-us/dotnet/standard/linq/linq-to-xml-overview

2. Vérification de fichiers et dossiers
   - File.Exists, Directory.Exists, Path.GetFullPath, Path.GetDirectoryName
   - Docs Microsoft : https://learn.microsoft.com/en-us/dotnet/api/system.io.file.exists
                     https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.exists
                     https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfullpath
                     https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getdirectoryname

3. LINQ pour filtrer et trier les éléments
   - IEnumerable<T>.Where, OrderBy, OrderByDescending, Take, Select
   - Docs Microsoft : https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable
   - Exemple tri : https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/orderby-clause

4. Conversion d’éléments XML en dictionnaires
   - XElement.Elements, ToDictionary
   - Docs Microsoft : https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.todictionary

5. Sérialisation en JSON avec System.Text.Json
   - JsonSerializer.Serialize, JsonSerializerOptions, WriteIndented
   - Docs Microsoft : https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializer
   - Exemple : https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to

6. Lecture de saisie console
   - Console.ReadLine
   - Docs Microsoft : https://learn.microsoft.com/en-us/dotnet/api/system.console.readline

7. Affichage console
   - Console.WriteLine
   - Docs Microsoft : https://learn.microsoft.com/en-us/dotnet/api/system.console.writeline

8. Gestion de conditions et booléens simples
   - if / else, bool, string.IsNullOrWhiteSpace
   - Docs Microsoft : https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/if-else
                     https://learn.microsoft.com/en-us/dotnet/api/system.string.isnullorwhitespace

9. Manipulation de chaînes (Trim, ToLowerInvariant, null-coalescing)
   - string.Trim, string.ToLowerInvariant, ?? (opérateur null-coalescing)
   - Docs Microsoft : https://learn.microsoft.com/en-us/dotnet/api/system.string.trim
                     https://learn.microsoft.com/en-us/dotnet/api/system.string.tolowerinvariant
                     https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator

======================================================================== */
