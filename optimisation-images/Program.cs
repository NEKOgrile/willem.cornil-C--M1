



// 1. Préparer un dossier avec plusieurs images à traiter

// 2. Faire une première version du programme en séquentiel


// 3. Faire une deuxième version en parallèle ou asynchrone


// 4. Comparer les deux temps d’exécution


// 5. Mettre le tout sur GitHub avec un petit README explicatif





//1. Préparer un dossier avec plusieurs images à traiter
using optimisation_images;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;


Console.WriteLine("quel est le docier avec tout les image donner le chemin entier");
var cheminDocierImage = Console.ReadLine();

if (string.IsNullOrEmpty(cheminDocierImage) || !Directory.Exists(cheminDocierImage))
{
    Console.WriteLine("Le chemin du dossier est invalide.");
    return;
}

//1.1 je recupere les images dans le dossier et les met dans un tableau
var images = Directory.GetFiles(cheminDocierImage, "*.jpg");

if (images.Length == 0)
{
    Console.WriteLine("Aucune image trouvée dans le dossier spécifié.");
    return;
}
Console.WriteLine($"{images.Length} images trouvées dans le dossier.");

//2. Faire une première version du programme en séquentiel

Console.WriteLine("mise en place d un system de calcule de temps");





//je vais le boucler 10 fois pour tester la performance


var boucle = 10;


for (int j = 0; j < boucle; j++)
{
  //pour la premeir boucle on fait tout normal
  if(j== 0)
    {
        var sw = Stopwatch.StartNew();

        // On prend juste la première image du tableau pour tester
        //var premiereImagePath = images[0];

        //on vas boucler pour prendre chaque image du tableau
        var nombreImage = images.Length;


        var sw1080 = Stopwatch.StartNew();

        for (int i = 0; i < nombreImage; i++)
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_1080.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 1080, outputPath);
            Console.WriteLine($"Image {i + 1}/{nombreImage} redimensionnée avec succès !");
        }

        sw1080.Stop();
        Console.WriteLine($"Temps de calcul séquentiel 1080p : {sw1080.ElapsedMilliseconds} ms");


        var sw720 = Stopwatch.StartNew();

        for (int i = 0; i < nombreImage; i++)
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_720.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 720, outputPath);
            Console.WriteLine($"Image {i + 1}/{nombreImage} redimensionnée avec succès !");
        }
        sw720.Stop();
        Console.WriteLine($"Temps de calcul séquentiel 720p : {sw720.ElapsedMilliseconds} ms");

        var sw480 = Stopwatch.StartNew();

        for (int i = 0; i < nombreImage; i++)
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_480.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 480, outputPath);
            Console.WriteLine($"Image {i + 1}/{nombreImage} redimensionnée avec succès !");
        }

        sw480.Stop();
        Console.WriteLine($"Temps de calcul séquentiel 480p : {sw480.ElapsedMilliseconds} ms");




        sw.Stop();
        Console.WriteLine($"Temps de calcul séquentiel entier : {sw.ElapsedMilliseconds} ms");


        //3. Faire une deuxième version en parallèle ou asynchrone
        Console.WriteLine("mise en place d un system de calcule de temps");

        var swpararallel = Stopwatch.StartNew();
        var sw1080parallel = Stopwatch.StartNew();

        Parallel.For(0, nombreImage, i =>
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_1080.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 1080, outputPath);
            Console.WriteLine($"Image {i + 1}/{nombreImage} redimensionnée avec succès !");
        });
        sw1080parallel.Stop();
        Console.WriteLine($"Temps de calcul parallèle 1080p : {sw1080parallel.ElapsedMilliseconds} ms");

        var sw720parallel = Stopwatch.StartNew();
        Parallel.For(0, nombreImage, i =>
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_720.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 720, outputPath);
            Console.WriteLine($"Image {i + 1}/{nombreImage} redimensionnée avec succès !");
        });
        sw720parallel.Stop();
        Console.WriteLine($"Temps de calcul parallèle 720p : {sw720parallel.ElapsedMilliseconds} ms");

        var sw480parallel = Stopwatch.StartNew();
        Parallel.For(0, nombreImage, i =>
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_480.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 480, outputPath);
            Console.WriteLine($"Image {i + 1}/{nombreImage} redimensionnée avec succès !");
        });
        sw480parallel.Stop();
        Console.WriteLine($"Temps de calcul parallèle 480p : {sw480parallel.ElapsedMilliseconds} ms");

        swpararallel.Stop();
        Console.WriteLine($"Temps de calcul parallèle entier : {swpararallel.ElapsedMilliseconds} ms");


        Console.WriteLine("la differrece de temps entrre le séquentiel et le parallèle est de ");
        Console.WriteLine($"{sw.ElapsedMilliseconds - swpararallel.ElapsedMilliseconds} ms");

    }
    else
    {

        var swboucle = Stopwatch.StartNew();

        //on vas boucler pour prendre chaque image du tableau
        var nombreImage = images.Length;


        for (int i = 0; i < nombreImage; i++)
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_1080.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 1080, outputPath);
        }



        for (int i = 0; i < nombreImage; i++)
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_720.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 720, outputPath);
        }



        for (int i = 0; i < nombreImage; i++)
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_480.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 480, outputPath);
        }


        swboucle.Stop();
        Console.WriteLine($"Temps de calcul séquentiel entier : {swboucle.ElapsedMilliseconds} ms");


        var swpararallel = Stopwatch.StartNew();

        Parallel.For(0, nombreImage, i =>
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_1080.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 1080, outputPath);
        });
      

      
        Parallel.For(0, nombreImage, i =>
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_720.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 720, outputPath);
        });
       

        Parallel.For(0, nombreImage, i =>
        {
            var premiereImagePath = images[i];
            // Charge l'image avec ImageSharp
            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(premiereImagePath);
            // Appelle la méthode SaveResized de ta classe ImageHelper
            var outputFileName = Path.GetFileNameWithoutExtension(premiereImagePath) + "_480.jpg";
            var outputPath = Path.Combine("ImagesOutput", outputFileName);
            ImageHelper.SaveResized(image, 480, outputPath);
        });
 

        swpararallel.Stop();
        Console.WriteLine($"Temps de calcul parallèle entier : {swpararallel.ElapsedMilliseconds} ms");


        Console.WriteLine("la differrece de temps entrre le séquentiel et le parallèle est de ");
        Console.WriteLine($"{swboucle.ElapsedMilliseconds - swpararallel.ElapsedMilliseconds} ms");


    }

}



    