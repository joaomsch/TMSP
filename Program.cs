/// Auteur: Joao Mascarenhas
/// Lieu: ETML
/// Date: 11 avril 2025
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
            Console.WriteLine("1. Déplacements possibles en bleu, case visitées en rouge, temps illimité");
            Console.WriteLine("2. Cases visitées en rouge, temps illimité");
            Console.WriteLine("3. Cases visitées en rouge, temps limité (6 minutes)");
            Console.WriteLine("\nA quel niveau voulez-vous jouer?(1, 2, 3):");
            string choixniveau = Console.ReadLine();


            if (choixniveau == "1")
            {
                cavalier.niveau1();
                Thread.Sleep(2000);
                Console.Clear();
                cavalier.InitialiserPlateau();
                cavalier.AfficherPlateau3(0, 0, true);
                Console.WriteLine("Entrez la position de départ du cavalier (ex: A1, B3, etc.) :");
                string position = cavalier.ObtenirPositionValidedif();


                int x = taille - (position[1] - '0');
                int y = position[0] - 'A';
                cavalier.RecordLastPosition(x, y);

                cavalier.MarquerPositionVisitee(x, y);
                cavalier.AfficherPlateau1(x, y);

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
                                Console.WriteLine("Félicitations ! Vous avez réussi l'énigme niveau 1.");
                                break;

                            }
                        }
                        else
                        {
                            Console.WriteLine("PERDU ! Vous avez déjà visité cette case. Vous avez malheureusement pas résolu l'éngime du cavalier niveau 1.");
                            break;
                        }

                        cavalier.AfficherPlateau1(x, y);
                        cavalier.RecordLastPosition(x, y);
                    }
                    else
                    {
                        Console.WriteLine("Déplacement invalide pour le cavalier. Essayez encore.");
                    }
                }
            }

            if (choixniveau == "2")
            {
                cavalier.niveau2();
                Thread.Sleep(1000);
                Console.Clear();
                cavalier.InitialiserPlateau();
                cavalier.AfficherPlateau2(0, 0, true);

                Console.WriteLine("Entrez la position de départ du cavalier (ex: A1, B3, etc.) :");
                string position = cavalier.ObtenirPositionValidedif();


                int x = taille - (position[1] - '0');
                int y = position[0] - 'A';
                cavalier.RecordLastPosition(x, y);

                cavalier.MarquerPositionVisitee(x, y);
                cavalier.AfficherPlateau2(x, y);

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
                                Console.WriteLine("Félicitations ! Vous avez réussi l'énigme niveau 2.");
                                break;

                            }
                        }
                        else
                        {
                            Console.WriteLine("PERDU ! Vous avez déjà visité cette case. Vous avez malheureusement pas résolu l'éngime du cavalier niveau 2.");
                            break;
                        }

                        cavalier.AfficherPlateau2(x, y);
                        cavalier.RecordLastPosition(x, y);
                    }
                    else
                    {
                        Console.WriteLine("Déplacement invalide pour le cavalier. Essayez encore.");
                    }
                }
            }
            if (choixniveau == "3")
            {
                cavalier.niveau3();
                Thread.Sleep(1000);
                Console.Clear();
                cavalier.InitialiserPlateau();

                cavalier.AfficherPlateau3(0, 0, true);

                Console.WriteLine("Entrez la position de départ du cavalier (ex: A1, B3, etc.) :");
                string position = cavalier.ObtenirPositionValidedif();

                int x = taille - (position[1] - '0');
                int y = position[0] - 'A';
                cavalier.RecordLastPosition(x, y);

                cavalier.MarquerPositionVisitee(x, y);
                cavalier.AfficherPlateau3(x, y);

                new Thread(() =>
                {
                    for (int i = 0; i < 360; i++)
                    {
                        int x = Console.CursorLeft;
                        int y = Console.CursorTop;
                        Console.CursorVisible = false;
                        Console.SetCursorPosition(70, 0);
                        Console.Write(360 - i + " secondes");
                        Console.CursorVisible = true;
                        Console.SetCursorPosition(x, y);

                        Thread.Sleep(1000);
                    }
                    Console.Clear();
                    Console.WriteLine("Vous avez plus assez de temps. Vous avez malheureusement pas résolu l'éngime du cavalier niveau 3.");
                    Environment.Exit(0);

                }).Start();

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
                                Console.WriteLine("Félicitations ! Vous avez réussi l'énigme niveau 3.");
                                break;

                            }
                        }
                        else
                        {
                            Console.WriteLine("PERDU ! Vous avez déjà visité cette case. Vous avez malheureusement pas résolu l'éngime du cavalier niveau 3.");
                            break;
                        }

                        cavalier.AfficherPlateau3(x, y);
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
}
