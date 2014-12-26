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
            // Type référence (int, double, struct...)
            int entier = 111;
            Console.WriteLine(entier); // 111
            ChangerEntier(entier);
            Console.WriteLine(entier); // 111

            // Type référence (tableaux, objets)
            int[] tableau = { 1, 1, 1 };
            AfficherTableau(tableau); // 111
            ChangerTableau(tableau);
            AfficherTableau(tableau); // 007

            Console.ReadLine();
        }

        static void ChangerEntier(int a)
        {
            a = 7;
        }

        static void ChangerTableau(int[] tab)
        {
            for (int i = 0; i < tab.Length - 1; i++)
                tab[i] = 0;

            tab[tab.Length - 1] = 7;
        }

        static void AfficherTableau(int[] tab)
        {
            foreach (var entier in tab)
                Console.Write(entier);

            Console.Write(Environment.NewLine); // On revient à la ligne
        }
    }
}
