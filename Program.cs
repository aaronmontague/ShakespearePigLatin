using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ShakespearePigLatin
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxSonnetNumber = 154; // TODO automate this
            int selectedSonnetNumber = 1;

            Console.WriteLine("Hello Position Imaging!");
            // Assumptions:
            // Pig Latin Rules: https://en.wikipedia.org/wiki/Pig_Latin# I used the top three, which boils down to "chop at the first vowel"
            // Sonnets Source: https://ocw.mit.edu/ans7870/6/6.006/s08/lecturenotes/files/t8.shakespeare.txt

            // Get the selection
            bool endlessLoop = true;
            while (endlessLoop == true)
            {
                Console.WriteLine("Please select a Sonnet by Number 1 - {0}", maxSonnetNumber);
                if (!Int32.TryParse(Console.ReadLine(), out selectedSonnetNumber))
                {
                    Console.WriteLine("Please enter a valid number");
                }
                else
                {
                    endlessLoop = false;
                }
            }

            // Get Sonnet's translation and display
            pigLatinTranslation(selectedSonnetNumber);

            Console.WriteLine("all done");
        }

        public static void pigLatinTranslation(int selectedSonnetNumber)
        {
            List<string> originalSonnet = shakespeareSourceText(selectedSonnetNumber);

            // Convert to Pig Latin
            List<string> translatedSonnet = new List<string>();
            foreach (string word in originalSonnet)
            {
                translatedSonnet.Add(wordToPigLatin(word));
            }

            // Display final output
            foreach (string word in translatedSonnet)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
        }

        public static List<string> shakespeareSourceText(int selectedSonnet)
        {
            // Get sonnnet from source
            // TODO Create API
            string source = "";
            string tempSource;
            string sonnetNumber = selectedSonnet.ToString();
            string trimmedLine;

            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("..\\..\\..\\shakes.txt");

                //Read the first line of text
                tempSource = sr.ReadLine();

                //Find the sonnet and append each line
                bool keepAppending = false;
               
                while (tempSource != null)
                {
                    trimmedLine = tempSource.Trim();

                    if (trimmedLine == sonnetNumber)
                    {
                        keepAppending = true;
                    }
                    else if (trimmedLine == "" && keepAppending)
                    {
                        break;
                    }
                    else if (keepAppending)
                    {
                        source += tempSource.Trim();
                    }

                    tempSource = sr.ReadLine();
                }
                
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                //Console.WriteLine("Executing finally block.");
            }

            // Parse into a list
            // TODO add line parsing
            // TODO add special char handling
            List<string> orginalSonnet = new List<string>();
            orginalSonnet = source.Split().ToList();

            return orginalSonnet;
        }

        public static string wordToPigLatin(string word)
        {
            char[] vowels = { 'a','e','i','o','u' };
            int firstVowelIndex = -1;

            List<char> lettersInWord = new List<char>();
            foreach(char character in word)
            {
                // Get the index of the first vowel
                if (firstVowelIndex == -1 && vowels.Contains(character))
                {
                    firstVowelIndex = lettersInWord.Count;
                }
                lettersInWord.Add(character);
            }
            if (firstVowelIndex == 0)
            {
                return word + "yay";
            }
            else
            {
                for (int i = 0; i < firstVowelIndex; i++)
                {
                    char tempChar = lettersInWord[0];
                    lettersInWord.RemoveAt(0);
                    lettersInWord.Insert(lettersInWord.Count, tempChar);
                }
                StringBuilder builder = new StringBuilder();
                foreach  (char letter in lettersInWord)
                {
                    builder.Append(letter);
                }

                return builder.ToString() + "ay";
            }

            static void printer(List<char> tempList)
            {
                foreach (char letter in tempList)
                {
                    Console.Write(letter);
                }
                Console.Write("\n");
            }
        }
    }
}
