using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            // Les differents types (https://github.com/skaaj/ensc-support/wiki/1.1-Types)

            // L'opérateur « = » prend la partie de droite et la met dans la partie de gauche
            string titreAlbum = "Untitled";
            int nbAlbumsVendus = 5000;
            float prixAlbum = 9.99f;

            Console.WriteLine(titreAlbum + " " + " a été vendu " + nbAlbumsVendus + " fois au prix de " + prixAlbum + " euros");

            // Remarque : on aurait pu demander à l'utilisateur du programme de remplir ces informations.
            // Nous aurions alors utilisé la fonction « Console.ReadLine() » qui demande à l'utilisateur
            // de rentrer un texte qui sera la valeur renvoyée par cette fonction.
            // Ainsi, si on avait voulu demander le titre de l'album on aurait eu :
            titreAlbum = Console.ReadLine();
            // Ici, si l'utilisateur écrit « Bonjour » alors ligne précédente équivaut à :
            titreAlbum = "Bonjour";
        }
    }
}
