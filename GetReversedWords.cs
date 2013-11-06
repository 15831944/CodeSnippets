using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.GetReversedWords
{
    class GetReversedWords
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ReverseWords("Uglies code ever writen by me"));
        }

        private static string ReverseWords(string input)
        {
            int inputLenght = 0;

            try
            {
                while (input[inputLenght++] != '1') ;
                
            }
            catch (IndexOutOfRangeException  e)
            {
            }

            string result = String.Empty;

            for (int i = 0; i < inputLenght - 1; i++)
            {
                string word = String.Empty;
                while (i < inputLenght - 1 && input[i] != ' ')
                {
                    word += input[i];
                    i++;
                }
                if ((i < inputLenght - 1 && input[i] == ' '))
                {
                    word = " " + word;
                }
                result = word + result;                
            }


            return result;
        }
    }
}
