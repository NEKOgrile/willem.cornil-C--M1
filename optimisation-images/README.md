# Optimisation Image

Ce programme redimensionne des images en C# et compare les performances entre **traitement séquentiel** et **parallèle** sur 10 boucles.

---

## 🔹 Mini tableau des 10 boucles

| Boucle | Séquentiel (ms) | Parallèle (ms) | Différence (ms) |
|--------|----------------|----------------|----------------|
| 1      | 491            | 216            | 275            |
| 2      | 334            | 98             | 236            |
| 3      | 182            | 52             | 130            |
| 4      | 157            | 49             | 108            |
| 5      | 185            | 55             | 130            |
| 6      | 174            | 63             | 111            |
| 7      | 160            | 49             | 111            |
| 8      | 205            | 48             | 157            |
| 9      | 155            | 45             | 110            |
| 10     | 142            | 48             | 94             |

> Chaque boucle traite toutes les images du dossier en **1080p, 720p et 480p**, et mesure le temps total.

---

## 💻 Exemple rapide du code

```bash
for(int j = 0; j < 10; j++)
{
    // Séquentiel
    Stopwatch swSeq = Stopwatch.StartNew();
    foreach(var img in images)
        ImageHelper.SaveResized(Image.Load<Rgba32>(img), 1080, "ImagesOutput");
    swSeq.Stop();

    // Parallèle
    Stopwatch swPar = Stopwatch.StartNew();
    Parallel.For(0, images.Length, i =>
        ImageHelper.SaveResized(Image.Load<Rgba32>(images[i]), 1080, "ImagesOutput"));
    swPar.Stop();

    Console.WriteLine($"Boucle {j+1}: Séq={swSeq.ElapsedMilliseconds} ms, Par={swPar.ElapsedMilliseconds} ms, Diff={swSeq.ElapsedMilliseconds - swPar.ElapsedMilliseconds} ms");
}
```