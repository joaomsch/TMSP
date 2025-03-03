///Auteur: Joao Mascarenhas
///Lieu: ETML
///Date: 3 mars 2025
///Description: C'est le code pour lénigme du cavalier en C#
using System.ComponentModel.Design;
using System.Timers;
//Commentaires : idée : un deuxième niveau avec sans le déplacement en bleu et un troisième sans avec du temps avec un système de points.
class Program
{
    static char[,] plateau;
    static int taille = 8;

    static int[] deplacementsCavalierX = new int[] { 2, 2, -2, -2, 1, 1, -1, -1 };
    static int[] deplacementsCavalierY = new int[] { 1, -1, 1, -1, 2, -2, 2, -2 };
    static List<string> visitedPositions = new List<string>();

    static void Main()
    {
        // Affichage du titre
        string cavaliertitre = @"
    ██╗     ███████╗     ██████╗ █████╗ ██╗   ██╗ █████╗ ██╗     ██╗███████╗██████╗ 
    ██║     ██╔════╝    ██╔════╝██╔══██╗██║   ██║██╔══██╗██║     ██║██╔════╝██╔══██╗
    ██║     █████╗      ██║     ███████║██║   ██║███████║██║     ██║█████╗  ██████╔╝
    ██║     ██╔══╝      ██║     ██╔══██║╚██╗ ██╔╝██╔══██║██║     ██║██╔══╝  ██╔══██╗
    ███████╗███████╗    ╚██████╗██║  ██║ ╚████╔╝ ██║  ██║███████╗██║███████╗██║  ██║
    ╚══════╝╚══════╝     ╚═════╝╚═╝  ╚═╝  ╚═══╝  ╚═╝  ╚═╝╚══════╝╚═╝╚══════╝╚═╝  ╚═╝
                                                                                
";
        Console.WriteLine(cavaliertitre);
        Thread.Sleep(1500);

        Console.WriteLine("Voulez-vous jouer au jeu du cavalier? (Oui/Non)");
        string reponseJouer = Console.ReadLine();

        if (reponseJouer.ToLower() == "non")
        {
            Console.Clear();
            Console.WriteLine("Jeu du Cavalier terminé");
            return;
        }

        if (reponseJouer.ToLower() == "oui")
        {
            Console.WriteLine("Niveau 1");
            Console.Clear();
            plateau = new char[taille, taille];

            for (int i = 0; i < taille; i++)
                for (int j = 0; j < taille; j++)
                    plateau[i, j] = ' ';

            Console.WriteLine("Entrez la position de départ du cavalier (ex: A1, B3, etc.) :");
            string position = ObtenirPositionValidedif();
            visitedPositions.Add(position);

            int x = taille - (position[1] - '0');
            int y = position[0] - 'A';

            plateau[x, y] = 'C';
            AfficherPlateaudif(x, y);

            while (true)
            {
                Console.WriteLine("\nDéplacez le cavalier !");
                string deplacement = ObtenirPositionValidedif();

                int deplacementX = taille - (deplacement[1] - '0');
                int deplacementY = deplacement[0] - 'A';

                bool mouvementValide = false;
                for (int i = 0; i < 8; i++)
                {
                    int nouvelleX = x + deplacementsCavalierX[i];
                    int nouvelleY = y + deplacementsCavalierY[i];

                    if (nouvelleX == deplacementX && nouvelleY == deplacementY)
                    {
                        mouvementValide = true;
                        break;
                    }
                }

                if (mouvementValide)
                {
                    plateau[x, y] = ' ';
                    x = deplacementX;
                    y = deplacementY;

                    if (!visitedPositions.Contains(deplacement))
                    {
                        visitedPositions.Add(deplacement);
                        if (visitedPositions.Count == 64)
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

                    plateau[x, y] = 'C';
                    AfficherPlateaudif(x, y);
                }
                else
                {
                    Console.WriteLine("Déplacement invalide pour le cavalier. Essayez encore.");
                }
            }
        }
    }

    static string ObtenirPositionValidedif()
    {
        while (true)
        {
            string position = Console.ReadLine().ToUpper();

            if (position.Length == 2 &&
                position[0] >= 'A' && position[0] <= 'H' &&
                position[1] >= '1' && position[1] <= '8')
            {
                return position;
            }
            else
            {
                Console.WriteLine("Position invalide. Entrez une position valide (ex: A1, B3, etc.).");
            }
        }
    }

    static void AfficherPlateaudif(int cavalierX, int cavalierY)
    {
        Console.Clear();

        Console.Write("    ");
        for (int i = 0; i < taille; i++)
            Console.Write("  " + (char)(65 + i) + " ");
        Console.WriteLine();

        for (int i = 0; i < taille; i++)
        {
            Console.Write("    ");
            for (int j = 0; j < taille; j++)
                Console.Write("╬═══");
            Console.WriteLine("╣");

            Console.Write("  " + (8 - i) + " ");
            for (int j = 0; j < taille; j++)
            {
                if (visitedPositions.Contains($"{(char)(j + 'A')}{8 - i}"))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("║ " + plateau[i, j] + " ");
                    Console.ResetColor();
                }
                else
                {
                    bool mouvementValide = false;
                    for (int k = 0; k < 8; k++)
                    {
                        int nouvelleposX = cavalierX + deplacementsCavalierX[k];
                        int nouvelleposY = cavalierY + deplacementsCavalierY[k];

                        if (i == nouvelleposX && j == nouvelleposY && !visitedPositions.Contains($"{(char)(j + 'A')}{8 - i}"))
                        {
                            mouvementValide = true;
                            break;
                        }
                    }

                    if (mouvementValide)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("║ " + plateau[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("║ " + plateau[i, j] + " ");
                    }
                }
            }
            Console.WriteLine("║");
        }

        Console.Write("    ");
        for (int j = 0; j < taille; j++)
            Console.Write("╬═══");
        Console.WriteLine("╣");
    }
}
