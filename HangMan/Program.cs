using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;


/*
  * Skapa en json fil med ord i olika svårighetsgrader
  * Skapa en json fil med highscore/poängsystem
  * Skapa klasser och vad som kan behövas i separata filer
  * Lägga in showmenu i separat fil så vi kan använda den i spelet, finns nu i en mapp
  * Skapa funktionerna som behövs
  * Felhantering vid fel input från användare
  * Bryta ner koden till ett objektorienterat program
  * Visa vem som har Highscore efter varje avslutad runda.
  * Kanske ha med spelade matcher i statistiken tillsammans med Highscore?
*/
namespace OwnProjects
{
    public class Player
    {
        public string Name;
        public int Score;
        public Player(string Nickname)
        {
            Nickname = Name;
        }
        public int PlayerScore(int score)
        {
            score = Score;
            return score;
        }
    }
    public class Program
    {
        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Instructions: \nTry to guess the word by entering one character letter at a time.\nYou got 9 tries.\nYou will get to choose the difficulty: Easy, Medium or Hard.\nBased on difficulty you shall receive 5, 10 or 20 points.");
            Console.WriteLine();
            Console.WriteLine("Good luck!");
            Console.WriteLine();
            Console.ResetColor();

            //Ha en ShowMenu här där du frågar användaren vad han vill göra?

            //Generating random word from a csv file.
            string textFile = @"C:\Users\orhan\source\repos\OwnProjects\OwnProjects\Utilities\randomWords.csv";
            string[] randomWords = File.ReadAllLines(textFile);
            Random rnd = new Random();
            int random = rnd.Next(randomWords.Length);

            //Console.WriteLine(randomWords[random]); //Denna ska bort eftersom den skriver ut vad ordet ska vara, den finns med bara för att testa så att programmet fungerar.
            //Console.WriteLine();

            var alreadyGuessed = new List<char>(); //Saving already guessed chars into a char array.

            string hiddenWord = "";
            char guess;
            int guessCount = 9; //To make sure the user only gets 9 guesses. We are counting down from 9 - 0.

            foreach (char letter in randomWords[random])
            {
                hiddenWord += "*";
            }

            List<Player> playerName = new List<Player>();
            Console.Write("Please enter your Nickname: ");
            string name = Console.ReadLine();
            var p = new Player(name);
            playerName.Add(p);

            //List<int> myValues;
            //string csv = String.Join(",", myValues.Select(x => x.ToString()).ToArray());

            Console.Clear();

            Console.WriteLine("Welcome, " + name);
            Console.WriteLine();
            Thread.Sleep(1000);

            Console.Write("Press [Any] key to start the game: ");
            Console.ReadKey();
            Console.Clear();

            bool running = true;
            while (running)
            {
                Console.WriteLine("The hidden word: " + hiddenWord); //Visar längden på ordet, täckt med asterisker.
                Console.Write("Wrong guesses: ");
                alreadyGuessed.Select(x => x.ToString());
                Console.Write(string.Join(", ", alreadyGuessed)); //Här skriver vi ut det användaren har gissat fel.
                Console.WriteLine();

                if (guessCount > 1) //Vi skriver ut till användaren hur många gissnar som är kvar.
                {
                    Console.Write("You have " + guessCount + " guesses left.");
                }
                else if (guessCount == 1) //Om det endast är en gissning kvar så får användaren ett varningsmeddelande.
                {
                    Console.Write("Careful, you only have " + guessCount + " guess left!");
                }

                Console.WriteLine();
                Console.Write("Guess: ");
                guess = char.Parse(Console.ReadLine().ToLower());

                if (randomWords[random].Contains(guess)) //I loopen nedan så byter vi ut asterisken mot bokstaven som är rättgissad.
                {
                    for (int i = 0; i < randomWords[random].Length; i++)
                    {
                        if (guess == randomWords[random][i])
                        {
                            hiddenWord = hiddenWord.Remove(i, 1).Insert(i, guess.ToString());
                        }
                    }
                }

                else if (alreadyGuessed.Contains(guess)) //Den här visas inte eftersom vi använder oss utav Console.Clear();
                {
                    Console.WriteLine("You have already guessed this letter.\n");
                }

                else
                {
                    Console.WriteLine("Nope.\n"); //Den här visas inte eftersom vi använder oss utav Console.Clear();                 
                    alreadyGuessed.Add(guess); //Detta görs "bakom kulliserna"
                    guessCount--; //Detta görs "bakom kulisserna"
                }
                Console.Clear();

                if (hiddenWord == randomWords[random])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("VICTORY!!!");
                    Console.ResetColor();
                    break;
                }

                if (guessCount == 0)
                {
                    Console.WriteLine("The hidden word was: " + hiddenWord);
                    Console.WriteLine("The actual word was: " + randomWords[random]);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("GAME OVER!!!");
                    Console.ResetColor();
                    break;
                }
            }
        }
    }
}

