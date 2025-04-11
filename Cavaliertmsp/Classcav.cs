/// Auteur: Joao Mascarenhas
/// Lieu: ETML
/// Date: 11 avril 2025
/// Description: Le code pour l'énigme du cavalier en C#

using System;
using System.Collections.Generic;

public class Cavalier
{
    // Dernière position du cavalier (coordonnées)
    public int LastX;
    public int LastY;

    // Plateau de jeu et liste des cases visitées
    public char[,] Plateau { get; private set; }
    public List<string> VisitedPositions { get; private set; }
    public int Taille { get; private set; }
    public int[] DeplacementsCavalierX { get; private set; }
    public int[] DeplacementsCavalierY { get; private set; }

    // Constructeur du plateau avec taille par défaut 8x8
    public Cavalier(int taille = 8)
    {
        Taille = taille;
        Plateau = new char[Taille, Taille];
        VisitedPositions = new List<string>();
        DeplacementsCavalierX = new int[] { -2, -1, 1, 2, 2, 1, -1, -2 };
        DeplacementsCavalierY = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        InitialiserPlateau();
    }
    // Affiche le titre du jeu en ASCII
    public void Titre()
    {
        string cavaliertitre = @"
██╗     ███████╗     ██████╗ █████╗ ██╗   ██╗ █████╗ ██╗     ██╗███████╗██████╗ 
██║     ██╔════╝    ██╔════╝██╔══██╗██║   ██║██╔══██╗██║     ██║██╔════╝██╔══██╗
██║     █████╗      ██║     ███████║██║   ██║███████║██║     ██║█████╗  ██████╔╝
██║     ██╔══╝      ██║     ██╔══██║╚██╗ ██╔╝██╔══██║██║     ██║██╔══╝  ██╔══██╗
███████╗███████╗    ╚██████╗██║  ██║ ╚████╔╝ ██║  ██║███████╗██║███████╗██║  ██║
╚══════╝╚══════╝     ╚═════╝╚═╝  ╚═╝  ╚═══╝  ╚═╝  ╚═╝╚══════╝╚═╝╚══════╝╚═╝  ╚═╝
                                                                                
";
        Console.WriteLine(cavaliertitre);
    }
    public void niveau1()
    {
        string cavalierniveau1 = @"
███╗   ██╗██╗██╗   ██╗███████╗ █████╗ ██╗   ██╗     ██╗
████╗  ██║██║██║   ██║██╔════╝██╔══██╗██║   ██║    ███║
██╔██╗ ██║██║██║   ██║█████╗  ███████║██║   ██║    ╚██║
██║╚██╗██║██║╚██╗ ██╔╝██╔══╝  ██╔══██║██║   ██║     ██║
██║ ╚████║██║ ╚████╔╝ ███████╗██║  ██║╚██████╔╝     ██║
╚═╝  ╚═══╝╚═╝  ╚═══╝  ╚══════╝╚═╝  ╚═╝ ╚═════╝      ╚═╝
                                                                                              
";
        Console.WriteLine(cavalierniveau1);
    }
    public void niveau2()
    {
        string cavalierniveau2 = @"
███╗   ██╗██╗██╗   ██╗███████╗ █████╗ ██╗   ██╗    ██████╗ 
████╗  ██║██║██║   ██║██╔════╝██╔══██╗██║   ██║    ╚════██╗
██╔██╗ ██║██║██║   ██║█████╗  ███████║██║   ██║     █████╔╝
██║╚██╗██║██║╚██╗ ██╔╝██╔══╝  ██╔══██║██║   ██║    ██╔═══╝ 
██║ ╚████║██║ ╚████╔╝ ███████╗██║  ██║╚██████╔╝    ███████╗
╚═╝  ╚═══╝╚═╝  ╚═══╝  ╚══════╝╚═╝  ╚═╝ ╚═════╝     ╚══════╝
                                                                                                    
";
        Console.WriteLine(cavalierniveau2);
    }
    public void niveau3()
    {
        string cavalierniveau3 = @"
███╗   ██╗██╗██╗   ██╗███████╗ █████╗ ██╗   ██╗    ██████╗ 
████╗  ██║██║██║   ██║██╔════╝██╔══██╗██║   ██║    ╚════██╗
██╔██╗ ██║██║██║   ██║█████╗  ███████║██║   ██║     █████╔╝
██║╚██╗██║██║╚██╗ ██╔╝██╔══╝  ██╔══██║██║   ██║     ╚═══██╗
██║ ╚████║██║ ╚████╔╝ ███████╗██║  ██║╚██████╔╝    ██████╔╝
╚═╝  ╚═══╝╚═╝  ╚═══╝  ╚══════╝╚═╝  ╚═╝ ╚═════╝     ╚═════╝ 
                                                                                                                                        
";
        Console.WriteLine(cavalierniveau3);
    }

        
    //Systéme de points niveau 1
    public void Points1()
    {
        int totalcase = VisitedPositions.Count;
        Console.WriteLine("Nombre total de cases visitées : " + totalcase);
        double point = 2 * totalcase;
        Console.WriteLine("Nombre de points obtenus: " + point);
    }

    // Initialise le plateau avec des espaces vides
    public void InitialiserPlateau()
    {
        for (int i = 0; i < Taille; i++)
            for (int j = 0; j < Taille; j++)
                Plateau[i, j] = ' ';
    }

    // Affiche le plateau pour le niveau 1 avec couleurs (rouge: visité, bleu: mouvements possibles)
    public void AfficherPlateau1(int cavalierX, int cavalierY)
    {
        Console.Clear();

        Plateau[cavalierX, cavalierY] = 'C';

        // En-tête des colonnes (A-H)
        Console.Write("    ");
        for (int i = 0; i < Taille; i++)
            Console.Write("  " + (char)(65 + i) + " ");
        Console.WriteLine();

        // Affichage ligne par ligne du plateau
        for (int i = 0; i < Taille; i++)
        {
            Console.Write("    ");
            for (int j = 0; j < Taille; j++)
                Console.Write("╬═══");
            Console.WriteLine("╣");

            Console.Write("  " + (Taille - i) + " ");

            for (int j = 0; j < Taille; j++)
            {
                string position = $"{(char)(j + 'A')}{Taille - i}";

                // Case déjà visitée
                if (VisitedPositions.Contains(position))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("║ " + Plateau[i, j] + " ");
                    Console.ResetColor();
                }
                else
                {
                    // Vérifie si mouvement possible depuis la position actuelle
                    bool mouvementValide = false;
                    for (int k = 0; k < 8; k++)
                    {
                        int nouvellePosX = cavalierX + DeplacementsCavalierX[k];
                        int nouvellePosY = cavalierY + DeplacementsCavalierY[k];

                        if (nouvellePosX == i && nouvellePosY == j && !VisitedPositions.Contains(position))
                        {
                            mouvementValide = true;
                            break;

                        }
                    }

                    // Fond bleu pour les mouvements valides
                    if (mouvementValide)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("║ " + Plateau[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("║ " + Plateau[i, j] + " ");
                    }
                }
            }
            Console.WriteLine("║");
        }
        // Bas du plateau
        Console.Write("    ");
        for (int j = 0; j < Taille; j++)
            Console.Write("╬═══");
        Console.WriteLine("╣");

        // Nettoie la position actuelle
        Plateau[cavalierX, cavalierY] = ' ';

    }
    // Sytéme de points niveau 2
    public void Points2()
    {
        int totalcase = VisitedPositions.Count;
        Console.WriteLine("Nombre total de cases visitées : " + totalcase);
        double point = 4 * totalcase;
        Console.WriteLine("Nombre de points obtenus: " + point);
    }
    // Affiche le plateau pour le niveau 2 sans les déplacements en bleu 
    public void AfficherPlateau2(int cavalierX, int cavalierY, bool hideKnight = false)
    {
        Console.Clear();
        if (!hideKnight)
        {
            Plateau[cavalierX, cavalierY] = 'C';
        }

        Console.Write("    ");
        for (int i = 0; i < Taille; i++)
            Console.Write("  " + (char)(65 + i) + " ");
        Console.WriteLine();

        for (int i = 0; i < Taille; i++)
        {
            Console.Write("    ");
            for (int j = 0; j < Taille; j++)
                Console.Write("╬═══");
            Console.WriteLine("╣");

            Console.Write("  " + (Taille - i) + " ");

            for (int j = 0; j < Taille; j++)
            {
                string position = $"{(char)(j + 'A')}{Taille - i}";

                if (VisitedPositions.Contains(position))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("║ " + Plateau[i, j] + " ");
                    Console.ResetColor();
                }
                else
                {
                    bool mouvementValide = false;
                    for (int k = 0; k < 8; k++)
                    {
                        int nouvellePosX = cavalierX + DeplacementsCavalierX[k];
                        int nouvellePosY = cavalierY + DeplacementsCavalierY[k];

                        if (nouvellePosX == i && nouvellePosY == j && !VisitedPositions.Contains(position))
                        {
                            mouvementValide = true;
                            break;

                        }
                    }

                    if (mouvementValide)
                    {
                        Console.Write("║ " + Plateau[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("║ " + Plateau[i, j] + " ");
                    }
                }
            }
            Console.WriteLine("║");
        }

        Console.Write("    ");
        for (int j = 0; j < Taille; j++)
            Console.Write("╬═══");
        Console.WriteLine("╣");

        Plateau[cavalierX, cavalierY] = ' ';

    }
    //Systéme de points niveau 3
    public void Points3()
    {
        int totalcase = VisitedPositions.Count;
        Console.WriteLine("Nombre total de cases visitées : " + totalcase);
        double point = 6 * totalcase;
        Console.WriteLine("Nombre de points obtenus: " + point);
    }
    //Affiche plateau niveau 3 avec du temps limité et les cases visitées en rouge
    public void AfficherPlateau3(int cavalierX, int cavalierY, bool hideKnight = false)
    {
        Console.Clear();
        if (!hideKnight)
        {
            Plateau[cavalierX, cavalierY] = 'C';
        }

        Console.Write("    ");
        for (int i = 0; i < Taille; i++)
            Console.Write("  " + (char)(65 + i) + " ");
        Console.WriteLine();

        for (int i = 0; i < Taille; i++)
        {
            Console.Write("    ");
            for (int j = 0; j < Taille; j++)
                Console.Write("╬═══");
            Console.WriteLine("╣");

            Console.Write("  " + (Taille - i) + " ");

            for (int j = 0; j < Taille; j++)
            {
                string position = $"{(char)(j + 'A')}{Taille - i}";

                if (VisitedPositions.Contains(position))
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("║ " + Plateau[i, j] + " ");
                    Console.ResetColor();
                }
                else
                {
                    bool mouvementValide = false;
                    for (int k = 0; k < 8; k++)
                    {
                        int nouvellePosX = cavalierX + DeplacementsCavalierX[k];
                        int nouvellePosY = cavalierY + DeplacementsCavalierY[k];

                        if (nouvellePosX == i && nouvellePosY == j && !VisitedPositions.Contains(position))
                        {
                            mouvementValide = true;
                            break;

                        }
                    }

                    if (mouvementValide)
                    {
                        Console.Write("║ " + Plateau[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("║ " + Plateau[i, j] + " ");
                    }
                }
            }
            Console.WriteLine("║");
        }

        Console.Write("    ");
        for (int j = 0; j < Taille; j++)
            Console.Write("╬═══");
        Console.WriteLine("╣");

        Plateau[cavalierX, cavalierY] = ' ';

    }

    // Enregistre la dernière position du cavalier
    internal void RecordLastPosition(int x, int y)
    {
        this.LastX = x;
        this.LastY = y;
    }
    //Vérifier l'entrée de l'utilisateur pour les coordonnées
    public string ObtenirPositionValidedif()
    {
        while (true)
        {
            string position = Console.ReadLine().ToUpper();

            if (position.Length == 2 &&
                position[0] >= 'A' && position[0] < 'A' + Taille &&
                position[1] >= '1' && position[1] <= (Taille + '0'))
            {
                return position;

            }
            else
            {
                Console.WriteLine("Position invalide. Entrez une position valide (ex: A1, B3, etc.).");
            }
        }
    }

    // Marque une case comme visitée
    public void MarquerPositionVisitee(int x, int y)
    {

        string position = $"{(char)(y + 'A')}{Taille - x}";
        if (!VisitedPositions.Contains(position))
        {
            VisitedPositions.Add(position);
        }

    }

    // Vérifie si un mouvement du cavalier est valide à partir de la position actuelle
    public bool VerifierMouvement(int x, int y, int deplacementX, int deplacementY)
    {
        for (int i = 0; i < 8; i++)
        {
            int nouvelleX = x + DeplacementsCavalierX[i];
            int nouvelleY = y + DeplacementsCavalierY[i];

            if (nouvelleX == deplacementX && nouvelleY == deplacementY)
            {
                return true;
            }
        }
        return false;
    }
}
