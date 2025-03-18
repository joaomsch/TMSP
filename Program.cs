/// Auteur: Joao Mascarenhas
/// Lieu: ETML
/// Date: 3 mars 2025
/// Description: C'est le code pour l'énigme du cavalier en C#

using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static int taille = 8;
    Cavalier cavalier = new Cavalier(8);
    static void Main()
    {
        Cavalier cavalier = new Cavalier(taille);
        cavalier.Titre();
        Thread.Sleep(1500);

        Console.WriteLine("Voulez-vous jouer au jeu du cavalier? (Oui/Non)");
        string reponseJouer = Console.ReadLine().ToLower();

        if (reponseJouer == "non")
        {
            Console.Clear();
            Console.WriteLine("Jeu du Cavalier terminé");
            return;
        }

        if (reponseJouer == "oui")
        {
            Console.Clear();
            cavalier.InitialiserPlateau();

            Console.WriteLine("Entrez la position de départ du cavalier (ex: A1, B3, etc.) :");
            string position = cavalier.ObtenirPositionValidedif();
            

            int x = taille - (position[1] - '0');
            int y = position[0] - 'A';
            cavalier.RecordLastPosition(x, y);

            cavalier.MarquerPositionVisitee(x, y);
            cavalier.AfficherPlateaudif(x, y);

            while (true)
            {
                Console.WriteLine("\nDéplacez le cavalier !");
                string deplacement = cavalier.ObtenirPositionValidedif();

                int deplacementX = taille - (deplacement[1] - '0');
                int deplacementY = deplacement[0] - 'A';

                bool mouvementValide = cavalier.VerifierMouvement(x, y, deplacementX, deplacementY);

                if (mouvementValide)
                {
                    x = deplacementX;
                    y = deplacementY;

                    

                    if (!cavalier.VisitedPositions.Contains(deplacement))
                    {
                        cavalier.MarquerPositionVisitee(x, y);
                        if (cavalier.VisitedPositions.Count == taille * taille)
                        {
                            Console.Clear();
                            Console.WriteLine("Félicitations ! Vous avez réussi l'énigme.");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("PERDU ! Vous avez déjà visité cette case.");
                        break;
                    }

                    cavalier.AfficherPlateaudif(x, y);
                    cavalier.RecordLastPosition(x, y);
                }
                else
                {
                    Console.WriteLine("Déplacement invalide pour le cavalier. Essayez encore.");
                }
            }
        }
    }
}
