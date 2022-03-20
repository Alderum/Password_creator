using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passwordcreator
{
    class Program
    {


        static void Main(string[] args)
        {
            string text;
            string password, lenguage;
            float rn, rn1, rn2, rn3;
            List<string> list = new List<string>();

            Random random = new Random();

            //List of velue: velueEnglish, velueUkrain, veluePunctuation, velueNumber, velueRussian
            string[] velue = { "Q", "(анг)A", "S", "W", "(анг)E", "D", "Z", "(анг)X", "(анг)C", "R", "G", "V", "F", "(анг)T", "(анг)H", "Y", "(анг)B", "U",
            "N", "(анг)M", "J", "(анг)K", "L", "(анг)O", "(анг)P", "q", "w", "(анг)e", "s", "(анг)a", "d", "z", "(анг)x", "(анг)c", "g", "h", "t", "r", "f", "v", "b", "n", "y", "u", "j",
            "(анг)i", "k", "(анг)o", "l", "(анг)p", "m", "1", "2", "3", "4", "5", "6", "7", "8", "0", "9", "-", "=",
            "!", "@", "#", "$", "^", "%", "&", "*", "(", ")", "_", "+", "`", "~", "[", "]", "{", "}",
            ":", "|", ";", "'", " ", ",", "<", ".", ">", "/", "?", "Й", "Ц", "У", "К", "Е", "Н", "Г",
            "Ф", "I", "В", "А", "Ч", "Я", "С", "М", "И", "П", "А", "Р", "О", "Т", "Ь", "Б", "Ю", "Д",
            "Л", "Ш", "Щ", "З", "Х", "Ж", "Є", "Ъ", "й", "ц", "у", "к", "е", "п", "а", "в", "i", "ч",
            "я", "ф", "с", "м", "и", "н", "р", "т", "ь", "о", "г", "ш", "л", "б", "ю", "ж", "є", "х",
            "з", "щ", "д", "ъ", "Ы", "ы", "Э", "э" };

            string[] velueEnglish = { "Q", "(анг)A", "S", "W", "(анг)E", "D", "Z", "(анг)X", "(анг)C", "R", "G", "V", "F", "(анг)T", "(анг)H", "Y", "(анг)B", "U",
            "N", "(анг)M", "J", "(анг)K", "L", "(анг)O", "(анг)P", "q", "w", "(анг)e", "s", "(анг)a", "d", "z", "(анг)x", "(анг)c", "g", "h", "t", "r", "f", "v", "b", "n", "y", "u", "j",
            "(анг)i", "k", "(анг)o", "l", "(анг)p", "m" };

            string[] velueUkraine = { "Й", "Ц", "У", "К", "Е", "Н", "Г",
            "Ф", "I", "В", "А", "Ч", "Я", "С", "М", "И", "П", "А", "Р", "О", "Т", "Ь", "Б", "Ю", "Д",
            "Л", "Ш", "Щ", "З", "Х", "Ж", "Є", "й", "ц", "у", "к", "е", "п", "а", "в", "i", "ч",
            "я", "ф", "с", "м", "и", "н", "р", "т", "ь", "о", "г", "ш", "л", "б", "ю", "ж", "є", "х",
            "з", "щ", "д"};

            string[] veluePunctuation = { "-", "=", "!", "@", "#", "$", "^", "%", "&", "*", "(", ")", "_", "+", "`", "~", "[", "]", "{", "}",
            ":", "|", ";", "'", " ", ",", "<", ".", ">", "/", "?" };

            string[] velueNumber = { "1", "2", "3", "4", "5", "6", "7", "8", "0", "9" };

            string[] velueRussian = { "Ъ", "ъ", "Ы", "ы", "Э", "э" };

            do
            {
                for (int i = 0; i < velue.Length; i++)
                {
                    list.Add(velue[i]);
                }

                text = Console.ReadLine();

                if (text == "Create password")
                {
                    //Password settings
                    Console.Write("Password setigs: " +
                        "\nThis program can create password only with three lenguage Russian Ukraine English" + "\n((анг) and next first simbol) and punctuation simbols." +
                        "\nPassword width: ");
                    int pw = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Lenguage: Ukraine, Russian, English" + "\nOther simbols: Punctuation, Number" +
                        "\nWhat lenguages or punctuation do you want to be in your password: ");
                    lenguage = Console.ReadLine();
                    Console.WriteLine("Your password: ");

                    //Remove lenguage
                    AddLenguages(lenguage, list, velueEnglish, velueUkraine, veluePunctuation, velueNumber, velueRussian);


                    password = null;

                    for (int number = 0; number <= pw; number++)
                    {
                        //Create random number
                        rn1 = random.Next(0, list.Count);
                        rn2 = random.Next(0, list.Count);
                        rn3 = random.Next(0, list.Count);
                        rn = (rn1 + rn2 + rn3) / 3;

                        password = password + list[Convert.ToInt32(rn)];
                    }
                    Console.WriteLine(password);
                }
                else
                {
                    Console.WriteLine("To create password write: Create password");
                }

            } while (text != "Stop");


            return;

            //End of function
        }

        static void AddLenguages(string lenguage, List<string> list,
            string[] velueEnglish, string[] velueUkraine, string[] veluePunctuation, string[] velueNumber, string[] velueRussian)
        {
            if (!(lenguage.Contains("Ukraine")))
            {
                for (int i = 0; i < velueUkraine.Length; i++)
                {
                    list.Remove(velueUkraine[i]);
                }
            }

            if (!(lenguage.Contains("Russian")))
            {
                for (int i = 0; i < velueRussian.Length; i++)
                {
                    list.Remove(velueRussian[i]);
                }
            }

            if (!(lenguage.Contains("English")))
            {
                for (int i = 0; i < velueEnglish.Length; i++)
                {
                    list.Remove(velueEnglish[i]);
                }
            }

            if (!(lenguage.Contains("Number")))
            {
                for (int i = 0; i < velueNumber.Length; i++)
                {
                    list.Remove(velueNumber[i]);
                }
            }

            if (!(lenguage.Contains("Punctuation")))
            {
                for (int i = 0; i < veluePunctuation.Length; i++)
                {
                    list.Remove(veluePunctuation[i]);
                }
            }
        }
    }
}
