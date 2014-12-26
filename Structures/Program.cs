using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
public class Program
{
    struct Personne
    {
        public string prenom; // N'oubliez pas d'ajouter "public"
        public string nom;
        public int age;
    }

    static void Main(string[] args)
    {
        // 1. Instanciation
        // 1.1 En utilisant "new"
        Personne p1 = new Personne();
        // p1 est remplit avec les valeurs par défaut des types:
        //    prenom = "" | car c'est un string
        //    nom = ""    | idem
        //    age = 0     |car c'est un entier
        AfficherPersonne(p1); // Affiche : "  0 ans"
        // 1.2 En assignant tous les champs
        Personne p2;
        p2.prenom = "Benjamin"; // On accède aux variables encapsulée en utilisant un point (".")
        p2.nom    = "Denom";
        p2.age    = 22;
        AfficherPersonne(p2); // Affiche : "Benjamin Denom 22 ans"

        // 2. Exemple d'utilisation
        // 2.1 Exemple simple
        Anniversaire(ref p2); // On oublie pas le "ref" car une structure est un type valeur !
        AfficherPersonne(p2); // Affiche : "Benjamin Denom 23 ans"
            
        // 2.2 Chargement depuis un fichier
        // Admettons qu'on ait un fichier "personnes.csv" ayant ce contenu :
        //   PRENOM;NOM;AGE
        //   Benjamin;Denom;22
        //   Random;Dude;85
        // On souhaite le stocker dans notre programme

        string[] lignes = System.IO.File.ReadAllLines("../../personnes.csv");
        // On a nos lignes brutes mais on aimerait mieux structurer nos données grâce aux ...
        // ... structures.
        Personne[] personnes = new Personne[lignes.Length - 1]; // - 1 car on ne veut pas garder l'entête "PRENOM;NOM;AGE"
        for (int i = 1; i < lignes.Length; i++) // de même, on ne commence pas à 0 mais 1 pour éviter l'entête
        {
            //  D'abord on découpe notre ligne comme ceci :
            string[] ligneDecoupee = lignes[i].Split(';'); // On découpe notre ligne grâce au caractère séparateur ";"
            // Ici ligneDecoupee vaut quelque chose comme 
            //   [0] = "Random"
            //   [1] = "Dude"
            //   [2] = "85"

            // On a plus qu'à mettre à jour notre i-ème personne
            // Attention, on itère sur i et comme on commence à un pour enlever l'entête
            // on doit enlever 1 ici pour ne pas dépasser notre tableau de personnes (qui est d'une case plus petit que le tableau lignes)
            personnes[i - 1].prenom = ligneDecoupee[0];
            personnes[i - 1].nom    = ligneDecoupee[1];
            personnes[i - 1].age    = int.Parse(ligneDecoupee[2]); // on oublie pas de transformer notre string "22" en un entier 22
        }

        // À cet endroit du code on a bien chargé notre fichier de personnes dans un tableau de "Personne"
        // L'avantage est qu'on a pas un tableau par colonne (prénoms, noms, ages)
        // Par exemple si je veux afficher le prénom de la deuxième personne il me suffit de faire :
        Console.WriteLine(personnes[1].prenom); // Affiche : "Random"
        // Et si c'est son anniversaire :
        Anniversaire(ref personnes[1]);
        AfficherPersonne(personnes[1]); // Affiche : "Random Dude 86 ans"

        Console.ReadLine();
    }

    static void Anniversaire(ref Personne p)
    {
        p.age++;
    }

    static void AfficherPersonne(Personne p)
    {
        Console.WriteLine(p.prenom + ' ' + p.nom + ' ' + p.age + " ans");
    }
}
}
