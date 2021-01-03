using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Utils
    {
        private static string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string alphabetUrl = "0123456789";
        private static Random randomNum = new Random();

        public static string GenerateWebKitFormId()
        {
            string generatedString = "";
            for (int i = 0; i < 34; i++)
            {
                int randomNumber = randomNum.Next(0, alphabet.Length);
                generatedString += alphabet[randomNumber];
            }
            return generatedString;
        }

        public static string GenerateUrlId()
        {
            string generatedString = "";
            for (int i = 0; i < 17; i++)
            {
                int randomNumber = randomNum.Next(0, alphabetUrl.Length);
                generatedString += alphabetUrl[randomNumber];
            }
            return generatedString;
        }
    }
}
