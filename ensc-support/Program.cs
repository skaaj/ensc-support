using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ensc_support
{
    class Program
    {
        static void Main(string[] args)
        {
            // (1) On instancie un tableau en mémoire.
            int[] monTableau = { 3, 1, 4 };

            // (2) Le nom des méthodes parlent d'elles mêmes :)
            Afficher(monTableau);

            Ajouter(ref monTableau, 8);
            Ajouter(ref monTableau, 9);
            Ajouter(ref monTableau, 9);

            Afficher(monTableau);

            SupprimerIndice(ref monTableau, 3);

            Afficher(monTableau);

            SupprimerElement(ref monTableau, 9, false);

            Afficher(monTableau);

            Console.ReadLine(); // (3) Permet de bloquer la fenêtre pour ceux qui lance le programme avec F5 (mode Debug).
        }

        /* Heads up !
         * 
         * Dans ce programme les méthodes sont des méthodes génériques (http://msdn.microsoft.com/fr-fr/library/twcad0zb.aspx)
         * C'est pour cela que le type utilisé est T.
         * Cela permet d'utiliser la même méthode avec une plusieurs types on pourra par exemple appeler :
         *      Afficher(ref monTableauEntiers) avec monTableauEntiers de type int[].
         *      Afficher(ref monTableauCaracts) avec monTableauCaracts de type char[].
         * 
         * Le fichier DynamicArrayInt.cs présente les méthodes pour le type entier. 
         * -> Si vous avez du mal avec les méthodes présentées ici, n'hésitez pas à vous y référer.
         * -> Cette notion (méthodes génériques ou template) n'est abordé que rapidement en fin de première année.
         * -> Donc n'ayez crainte si vous ne la comprenez pas, l'important ici est le mécanisme de méthodes (ou fonction par abus de langage) et de tableaux.
         * 
         */

        // Cette méthode permet d'afficher les éléments d'un tableau dans une ligne.
        static void Afficher<T>(T[] tab)
        {
            // Pour chaque élément de mon tableau
            for (int i = 0; i < tab.Length; i++)
                Console.Write(tab[i] + " "); // Je l'écris en lui ajoutant un espace

            Console.Write(Environment.NewLine); // Je met le curseur d'écriture à la ligne
        }

        // Cette méthode permet d'ajouter un élément `elem' la fin d'un tableau `tab'.
        static void Ajouter<T>(ref T[] tab, T elem)
        {
            // (1) On récupére la taille du tableau car on va en avoir besoin plusieurs fois
            int nbElements = tab.Length;
            T[] tmp = new T[nbElements + 1]; // (2) On instancie (new) un tableau temporaire avec une case en plus

            // (3) On copie tous les éléments du tableau d'origine dans le tableau temporaire
            for (int i = 0; i < nbElements; i++)
                tmp[i] = tab[i];

            // (4) On ajoute à la fin du tableau temporaire l'élément à ajouter
            tmp[nbElements] = elem;

            // (5) On fait pointer le tableau d'origine vers le tableau temporaire
            tab = tmp;

            /* Récapitulatif : 
             * On a créé un tableau temporaire de taille N+1 (N étant la taille du tableau d'origine).
             * On a ensuite copié tous nos éléments de départ dans le tableau temporaire.
             * Ensuite, on a ajouté l'élément à ajouter (`elem') à la fin du tableau temporaire (ça passe étant donné qu'il est de taille N+1)).
             * En mémoire on se retrouve avec quelque chose comme ça :
             *          tab[3|1|4](adresse 0x8), tmp[3|1|4|8](adresse 0x100)
             * On fait pointer notre tableau initial vers le tableau temporaire (cela est possible car on a spécifié _ref_ dans les paramétres).
             * On a alors en mémoire :
             *          tab[3|1|4|8](adresse 0x100), tmp[3|1|4|8](adresse 0x100)
             * 
             * Attention : pour l'aspect pédagogique de la chose cet algorithme est assez coûteux (on recopie tout à chaque fois).
             * En réalité, il existe d'autres stratégies évitant de tout recopier à chaque fois (en utilisant un attribut `capacity' par exemple).
             * Ce constat est le même pour les autres algorithmes proposés ici (SupprimerIndice, SupprimerElement).
             * Par conséquent, ne l'utilisez pas tel quel dans vos projets (n'hésitez pas à me contacter pour en parler).
             */
        }

        // Cette méthode permet de supprimer l'élément présent à l'indice `indice' du tableau `tab'.
        static void SupprimerIndice<T>(ref T[] tab, int indice)
        {
            // (1) On récupére la taille du tableau car on va en avoir besoin plusieurs fois
            int nbElements = tab.Length;
            // (2) On instancie (new) un tableau temporaire avec une case en moins
            T[] tmp = new T[nbElements - 1];

            // (3) On parcourt notre tableau d'origine
            // Attention ! `i' permet d'avancer sur le tableau d'origine et `j' sur le tableau temporaire
            // Cela vient du faire qu'on va sauter un indice (celui à supprimer) et donc on ne pourra plus utiliser `i' pour remplir le tableau temporaire.
            for (int i = 0, j = 0; i < nbElements; i++)
            {
                if (i == indice) // (4.1) Si on est sur l'indice à supprimer
                    continue; // (4.2) On passe au tour de boucle suivant sans faire ce qu'il y a en dessous (donc `j' n'avance pas !) [mot clé `continue']

                // (5.1) Si on arrive là c'est qu'on est pas entré dans le if donc on est pas sur l'indice à supprimer
                tmp[j] = tab[i]; // (5.2) On copie
                j++; // (5.3) On fait avancer `j' qui est l'indice permettant de remplir le tableau temporaire.
            }

            // (6) On fait pointer le tableau d'origine vers le tableau temporaire
            tab = tmp;
        }

        // Cette méthode permet de supprimer un (ou tous les) élément(s) `elem' d'un tableau `tab'.
        // Cet algorithme étant un peu compliqué et pas forcement nécessaire. Je ne l'ai pas commenté.
        // Libre à vous d'essayer comprendre son fonctionnement, ça peut faire un bon exercice.
        static void SupprimerElement<T>(ref T[] tab, T elem, bool tous)
        {
            int nbElements;

            if (!tous)
                nbElements = tab.Length - 1;
            else
            {
                int compteur = 0;
                for (int n = 0; n < tab.Length; n++)
                {
                    if (tab[n].Equals(elem))
                        compteur++;
                }
                nbElements = tab.Length - compteur;
            }

            T[] tmp = new T[nbElements];

            int i = 0, j = 0;
            bool quitter = false;

            while(i < nbElements || !quitter)
            {
                if (tab[i].Equals(elem))
                {
                    if (!tous)
                    {
                        tmp[j] = tab[i];
                        quitter = true;
                    }

                    i++;
                    continue;
                }

                tmp[j] = tab[i];
                j++;
                i++;
            }

            tab = tmp;
        }
    }
}
