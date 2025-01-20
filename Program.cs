using System.ComponentModel.Design;
using System.Timers;

class Program
{
    static char[,] plateau; // Tableau représentant le plateau de jeu
    static int taille = 8;  // Taille du quadrillage 8x8

    static int[] deplacementsCavalierX = new int[] { 2, 2, -2, -2, 1, 1, -1, -1 };
    static int[] deplacementsCavalierY = new int[] { 1, -1, 1, -1, 2, -2, 2, -2 };
    static List<string> visitedPositions = new List<string>();  // Liste pour suivre les positions visitées

    static void Main()
    {
        // Affichage du titre
        string cavaliertitre = @"
    █       ██████          █████    █████   █   █   █████   █       ███  ██████  ████  
    █       █               █        █   █   █   █   █   █   █        █   █       █   █ 
    █       ████            █        █████   █   █   █████   █        █   ████    ████
    █       █               █        █   █   █   █   █   █   █        █   █       █   █  
    █████   ██████          █████    █   █    ███    █   █   █████   ███  ██████  █    █
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
            Console.WriteLine("QUEL MODE DE JEU?(1,2,3,4,5)");
            //échiquier 4x4
            Console.WriteLine("1.TRES FACILE");
            //échiquier 5x5
            Console.WriteLine("2.FACILE");
            //échiquier 6x6
            Console.WriteLine("3.Moyen");
            //échiquier 8x8
            Console.WriteLine("4.DIFFICILE");
            //échhiquier 8x8 plus temps(5min)
            Console.WriteLine("5.EXTREME");
            string choixdiff = Console.ReadLine();

            if (choixdiff == "1")
            {
                // Initialisation du plateau
                Console.Clear();
                plateau = new char[taille, taille];

                // Remplissage du plateau avec des cases vides
                for (int i = 0; i < taille; i++)
                {
                    for (int j = 0; j < taille; j++)
                    {
                        plateau[i, j] = ' ';
                    }
                }

                // Demander à l'utilisateur où placer le pion cavalier
                Console.WriteLine("Entrez la position de départ du cavalier (ex: A1, B3, etc.) :");
                string position = ObtenirPositionValide();
                visitedPositions.Add(position);

                // Convertir la notation de type A1 en indices de tableau
                int x = taille - (position[1] - '0');
                int y = position[0] - 'A';

                // Placer le pion cavalier dans la position choisie
                plateau[x, y] = 'C';

                // Afficher le plateau initial avec le cavalier
                AfficherPlateau(x, y);

                // Boucle de jeu
                while (true)
                {
                    Console.WriteLine("\nDéplacez le cavalier !");
                    string deplacement = ObtenirPositionValide();

                    // Convertir la notation du déplacement en indices de tableau
                    int deplacementX = taille - (deplacement[1] - '0');
                    int deplacementY = deplacement[0] - 'A';

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

                        bool notUsedBefore = true;
                        foreach (string dataP in visitedPositions)
                        {
                            if (dataP == deplacement)
                            {
                                notUsedBefore = false;
                                break;
                            }
                        }

                        if (notUsedBefore)
                        {
                            visitedPositions.Add(deplacement);
                        }
                        else
                        {
                            // Si le cavalier revient sur une case déjà visitée
                            Console.WriteLine("PERDU ! Vous avez déjà visité cette case.");
                            break;
                        }

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

            // Fonction pour obtenir une position valide sous forme de notation (ex: A1, B3)
            static string ObtenirPositionValide()
            {
                while (true)
                {
                    string position = Console.ReadLine().ToUpper();

                    if (position.Length == 2 &&
                        position[0] >= 'A' && position[0] <= 'D' &&
                        position[1] >= '1' && position[1] <= '4')
                    {
                        return position;
                    }
                    else
                    {
                        Console.WriteLine("Position invalide. Entrez une position valide (ex: A1, B3, etc.).");
                    }
                }
            }

            // Fonction pour afficher le plateau avec les numéros des lignes et des colonnes
            static void AfficherPlateau(int cavalierX, int cavalierY)
            {
                Console.Clear();

                // Afficher les lettres de A à H en haut du plateau
                Console.Write("    ");
                for (int i = 0; i < taille; i++)
                {
                    Console.Write("  " + (char)(65 + i) + " ");
                }
                Console.WriteLine();

                for (int i = 0; i < taille; i++)
                {
                    // Affichage des lignes horizontales
                    Console.Write("    ");
                    for (int j = 0; j < taille; j++)
                    {
                        Console.Write("╬═══");
                    }
                    Console.WriteLine("╣");

                    // Affichage des numéros de lignes dans l'ordre décroissant (de 1 à 8)
                    Console.Write("  " + (8 - i) + " ");

                    for (int j = 0; j < taille; j++)
                    {
                        // Colorier les cases visitées en rouge
                        if (visitedPositions.Contains($"{(char)(j + 'A')}{8 - i}"))
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

                                // Vérifier si la case est un déplacement valide
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

                // Dernière ligne horizontale du quadrillage
                Console.Write("    ");
                for (int j = 0; j < taille; j++)
                {
                    Console.Write("╬═══");
                }
                Console.WriteLine("╣");
            }

            if (choixdiff == "4")
            {
                // Initialisation du plateau
                Console.Clear();
                plateau = new char[taille, taille];

                // Remplissage du plateau avec des cases vides
                for (int i = 0; i < taille; i++)
                {
                    for (int j = 0; j < taille; j++)
                    {
                        plateau[i, j] = ' ';
                    }
                }

                // Demander à l'utilisateur où placer le pion cavalier
                Console.WriteLine("Entrez la position de départ du cavalier (ex: A1, B3, etc.) :");
                string position = ObtenirPositionValidedif();
                visitedPositions.Add(position);

                // Convertir la notation de type A1 en indices de tableau
                int x = taille - (position[1] - '0');
                int y = position[0] - 'A';

                // Placer le pion cavalier dans la position choisie
                plateau[x, y] = 'C';

                // Afficher le plateau initial avec le cavalier
                AfficherPlateau(x, y);

                // Boucle de jeu
                while (true)
                {
                    Console.WriteLine("\nDéplacez le cavalier !");
                    string deplacement = ObtenirPositionValidedif();

                    // Convertir la notation du déplacement en indices de tableau
                    int deplacementX = taille - (deplacement[1] - '0');
                    int deplacementY = deplacement[0] - 'A';

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

                        bool notUsedBefore = true;
                        foreach (string dataP in visitedPositions)
                        {
                            if (dataP == deplacement)
                            {
                                notUsedBefore = false;
                                break;
                            }
                        }

                        if (notUsedBefore)
                        {
                            visitedPositions.Add(deplacement);
                        }
                        else
                        {
                            // Si le cavalier revient sur une case déjà visitée
                            Console.WriteLine("PERDU ! Vous avez déjà visité cette case.");
                            break;
                        }

                        plateau[x, y] = 'C';

                        // Afficher le plateau mis à jour
                        AfficherPlateaudif(x, y);
                    }
                    else
                    {
                        Console.WriteLine("Déplacement invalide pour le cavalier. Essayez encore.");
                    }
                }
            }

            // Fonction pour obtenir une position valide sous forme de notation (ex: A1, B3)
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

            // Fonction pour afficher le plateau avec les numéros des lignes et des colonnes
            static void AfficherPlateaudif(int cavalierX, int cavalierY)
            {
                Console.Clear();

                // Afficher les lettres de A à H en haut du plateau
                Console.Write("    ");
                for (int i = 0; i < taille; i++)
                {
                    Console.Write("  " + (char)(65 + i) + " ");
                }
                Console.WriteLine();

                for (int i = 0; i < taille; i++)
                {
                    // Affichage des lignes horizontales
                    Console.Write("    ");
                    for (int j = 0; j < taille; j++)
                    {
                        Console.Write("╬═══");
                    }
                    Console.WriteLine("╣");

                    // Affichage des numéros de lignes dans l'ordre décroissant (de 1 à 8)
                    Console.Write("  " + (8 - i) + " ");

                    for (int j = 0; j < taille; j++)
                    {
                        // Colorier les cases visitées en rouge
                        if (visitedPositions.Contains($"{(char)(j + 'A')}{8 - i}"))
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

                                // Vérifier si la case est un déplacement valide
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

                // Dernière ligne horizontale du quadrillage
                Console.Write("    ");
                for (int j = 0; j < taille; j++)
                {
                    Console.Write("╬═══");
                }
                Console.WriteLine("╣");
            }


            if (choixdiff == "5")
            {
             
                // Initialisation du plateau
                Console.Clear();
                plateau = new char[taille, taille];

                // Remplissage du plateau avec des cases vides
                for (int i = 0; i < taille; i++)
                {
                    for (int j = 0; j < taille; j++)
                    {
                        plateau[i, j] = ' ';
                    }
                }

                // Demander à l'utilisateur où placer le pion cavalier
                Console.WriteLine("Entrez la position de départ du cavalier (ex: A1, B3, etc.) :");
                string position = ObtenirPositionValideex();
                visitedPositions.Add(position);

                // Convertir la notation de type A1 en indices de tableau
                int x = taille - (position[1] - '0');
                int y = position[0] - 'A';

                // Placer le pion cavalier dans la position choisie
                plateau[x, y] = 'C';

                // Afficher le plateau initial avec le cavalier
                AfficherPlateaudif(x, y);

                // Boucle de jeu
                while (true)
                {
                    Console.WriteLine("\nDéplacez le cavalier !");
                    string deplacement = ObtenirPositionValideex();

                    // Convertir la notation du déplacement en indices de tableau
                    int deplacementX = taille - (deplacement[1] - '0');
                    int deplacementY = deplacement[0] - 'A';

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

                        bool notUsedBefore = true;
                        foreach (string dataP in visitedPositions)
                        {
                            if (dataP == deplacement)
                            {
                                notUsedBefore = false;
                                break;
                            }
                        }

                        if (notUsedBefore)
                        {
                            visitedPositions.Add(deplacement);
                        }
                        else
                        {
                            // Si le cavalier revient sur une case déjà visitée
                            Console.WriteLine("PERDU ! Vous avez déjà visité cette case.");
                            break;
                        }

                        plateau[x, y] = 'C';

                        // Afficher le plateau mis à jour
                        AfficherPlateauex(x, y);
                    }
                    else
                    {
                        Console.WriteLine("Déplacement invalide pour le cavalier. Essayez encore.");
                    }
                }
            }

            // Fonction pour obtenir une position valide sous forme de notation (ex: A1, B3)
            static string ObtenirPositionValideex()
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

            // Fonction pour afficher le plateau avec les numéros des lignes et des colonnes
            static void AfficherPlateauex(int cavalierX, int cavalierY)
            {
                Console.Clear();

                // Afficher les lettres de A à H en haut du plateau
                Console.Write("    ");
                for (int i = 0; i < taille; i++)
                {
                    Console.Write("  " + (char)(65 + i) + " ");
                }
                Console.WriteLine();

                for (int i = 0; i < taille; i++)
                {
                    // Affichage des lignes horizontales
                    Console.Write("    ");
                    for (int j = 0; j < taille; j++)
                    {
                        Console.Write("╬═══");
                    }
                    Console.WriteLine("╣");

                    // Affichage des numéros de lignes dans l'ordre décroissant (de 1 à 8)
                    Console.Write("  " + (8 - i) + " ");

                    for (int j = 0; j < taille; j++)
                    {
                        // Colorier les cases visitées en rouge
                        if (visitedPositions.Contains($"{(char)(j + 'A')}{8 - i}"))
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

                                // Vérifier si la case est un déplacement valide
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

                // Dernière ligne horizontale du quadrillage
                Console.Write("    ");
                for (int j = 0; j < taille; j++)
                {
                    Console.Write("╬═══");
                }
                Console.WriteLine("╣");
            }
        }
    }
}
