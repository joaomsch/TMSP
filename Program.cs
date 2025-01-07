using System;
using System.Threading;

class Program
{
    static char[,] plateau; // Tableau représentant le plateau de jeu
    static int taille = 8;  // Taille du quadrillage 8x8

    // Définir les mouvements du cavalier (les déplacements possibles)
    static int[] deplacementsCavalierX = new int[] { 2, 2, -2, -2, 1, 1, -1, -1 };
    static int[] deplacementsCavalierY = new int[] { 1, -1, 1, -1, 2, -2, 2, -2 };

    static void Main()
    {
        string cavalierArt = @"
 CCCC    AAAAA   V   V   AAAAA   L       III  EEEEE  RRRR  
C        A   A   V   V   A   A   L        I   E      R   R 
C        AAAAA   V   V   AAAAA   L        I   EEEE   RRRR  
C        A   A   V   V   A   A   L        I   E      R  R  
 CCCC    A   A    VVV    A   A   LLLLL   III  EEEEE  R   R
";

        Console.WriteLine(cavalierArt);
        Thread.Sleep(1500);

        Console.WriteLine("Voulez-vous jouer au jeu du cavalier? (Oui/Non)");
        string reponseJouer = Console.ReadLine();

        if (reponseJouer.ToLower() == "non")
        {
            Console.Clear();
            Console.WriteLine("Jeu du Cavalier terminé");
            return;
        }

        // Initialisation du plateau
        Console.Clear();
        plateau = new char[taille, taille];

        // Remplissage du plateau avec des cases vides
        for (int i = 0; i < taille; i++)
        {
            // Chaque case est initialisée à un espace vide
            for (int j = 0; j < taille; j++)
            {
                plateau[i, j] = ' ';
            }
        }

        // Demander à l'utilisateur où placer le pion cavalier
        Console.WriteLine("Entrez la position de départ du cavalier (ex: 3 4 pour ligne 3, colonne 4) :");
        // Réduire de 1 pour correspondre à l'index du tableau
        int x = ObtenirCoordonneeValide("Ligne") - 1;
        // Réduire de 1 pour correspondre à l'index du tableau
        int y = ObtenirCoordonneeValide("Colonne") - 1;

        // Placer le pion cavalier dans la position choisie
        plateau[x, y] = 'C';

        // Afficher le plateau initial avec le cavalier
        AfficherPlateau(x, y);

        // Boucle de jeu
        while (true)
        {
            Console.WriteLine("\nDéplacez le cavalier !");
            int deplacementX = ObtenirDeplacementValide("Ligne") - 1;
            int deplacementY = ObtenirDeplacementValide("Colonne") - 1;

            // Vérifier si le mouvement est valide
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
                // Effacer l'ancienne position du cavalier
                plateau[x, y] = ' ';

                // Mettre à jour la nouvelle position du cavalier
                x = deplacementX;
                y = deplacementY;
                plateau[x, y] = 'C';

                // Afficher le plateau mis à jour
                AfficherPlateau(x, y);
            }
            else
            {
                Console.WriteLine("Déplacement invalide pour le cavalier. Essayez encore.");
            }
        }
    }

    // Fonction pour obtenir une coordonnée valide
    static int ObtenirCoordonneeValide(string type)
    {
        int coordonnee;
        while (true)
        {
            Console.Write($"Entrez la {type} (entre 1 et {taille}): ");
            if (int.TryParse(Console.ReadLine(), out coordonnee) && coordonnee >= 1 && coordonnee <= taille)
            {
                return coordonnee;
            }
            else
            {
                Console.WriteLine($"Coordonnée invalide. Veuillez entrer un nombre entre 1 et {taille}.");
            }
        }
    }

    // Fonction pour obtenir un déplacement valide du cavalier
    static int ObtenirDeplacementValide(string type)
    {
        int deplacement;
        while (true)
        {
            Console.Write($"Entrez la {type} pour le déplacement du cavalier (entre 1 et {taille}): ");
            if (int.TryParse(Console.ReadLine(), out deplacement) && deplacement >= 1 && deplacement <= taille)
            {
                return deplacement;
            }
            else
            {
                Console.WriteLine($"Mouvement invalide. Veuillez entrer un nombre entre 1 et {taille}.");
            }
        }
    }

    // Fonction pour afficher le plateau avec les numéros des lignes et des colonnes
    static void AfficherPlateau(int cavalierX, int cavalierY)
    {
        Console.Clear();

        // Afficher les numéros de colonnes
        Console.Write("    ");
        for (int i = 1; i <= taille; i++)
        {
            Console.Write(i + "   ");
        }
        Console.WriteLine();

        for (int i = 0; i < taille; i++)
        {
            // Affichage des numéros de lignes
            Console.Write(i + 1 + "   ");

            // Affichage de la ligne horizontale
            for (int j = 0; j < taille; j++)
            {
                Console.Write("╬═══");
            }
            Console.WriteLine("╣");

            // Affichage des cases avec les lignes verticales
            Console.Write("    ");
            for (int j = 0; j < taille; j++)
            {
                // Colorier la case contenant le cavalier en rouge
                if (i == cavalierX && j == cavalierY)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("║ " + plateau[i, j] + " ");
                    Console.ResetColor();
                }
                else
                {
                    // Colorier les cases possibles en bleu
                    bool mouvementValide = false;
                    for (int k = 0; k < 8; k++)
                    {
                        int nouvelleposX = cavalierX + deplacementsCavalierX[k];
                        int nouvelleposY = cavalierY + deplacementsCavalierY[k];

                        if (i == nouvelleposX && j == nouvelleposY)
                        {
                            mouvementValide = true;
                            break;
                        }
                    }

                    if (mouvementValide)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;  // Colorier les cases possibles en bleu
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

        // Dernière ligne horizontale du quadrillage
        Console.Write("    ");
        for (int j = 0; j < taille; j++)
        {
            Console.Write("╬═══");
        }
        Console.WriteLine("╣");
    }
}
