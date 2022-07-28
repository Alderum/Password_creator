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
            string text, password, lenguage;
            StringBuilder velue = new StringBuilder();

            Random random = new Random();

            string english = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            string ukraine = "йцукенгшщзхїфівапролджєячсмитьбю";
            string punctuation = "[]{};:',<.>/?-_=+|!#@$%^&*()~`";
            string number = "1234567890";

            do
            {
                velue.Clear();

                text = Console.ReadLine();

                if (text == "create password")
                {
                    //Password settings
                    Console.Write("Password setigs: " +
                        "\nThis program can create password only with three lenguage Russian Ukraine English" + 
                        "\n((анг) and next first simbol) and punctuation simbols." +
                        "\nPassword width: ");

                    int pw = Convert.ToInt32(Console.ReadLine());
                    
                    Console.Write("Lenguage: Ukraine, English" + 
                        "\nOther simbols: Punctuation, Number" +
                        "\nWhat lenguages or punctuation do you want to be in your password: ");

                    lenguage = Console.ReadLine();
                    Console.WriteLine("Your password: ");

                    AddLenguages(lenguage, velue, english, ukraine, punctuation, number);


                    password = null;

                    for (int nr = 0; nr <= pw; nr++)
                        password = password + velue[random.Next(0, velue.Length)];

                    Console.WriteLine(password);
                }
                else
                    Console.WriteLine("To create password write: create password");

            } while (text != "Stop");

            return;

            //End of function
        }
        
        static void AddLenguages(string lenguage, StringBuilder velue, string english, string ukraine, string punctuation, string number)
        {
            if (lenguage.Contains("Ukraine"))
                velue.Append(ukraine);

            if (lenguage.Contains("English"))
                velue.Append(english);

            if (lenguage.Contains("Number"))
                velue.Append(number);

            if (lenguage.Contains("Punctuation"))
                velue.Append(punctuation);
        }
    }
}
