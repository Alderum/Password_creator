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
            string text, password, language;
            StringBuilder value = new StringBuilder();

            Random random = new Random();

            string english = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            string ukraine = "йцукенгшщзхїфівапролджєячсмитьбю";
            string punctuation = "[]{};:',<.>/?-_=+|!#@$%^&*()~`";
            string number = "1234567890";

            do
            {
                value.Clear();

                text = Console.ReadLine();

                if (text == "create password")
                {
                    //Password settings
                    Console.Write("Password setigs: " +
                        "\nThis program can create password only with three language Ukraine English" + 
                        "\nand punctuation simbols." +
                        "\nPassword width: ");

                    int pw = Convert.ToInt32(Console.ReadLine());
                    
                    Console.Write("Language: Ukraine, English" + 
                        "\nOther simbols: Punctuation, Number" +
                        "\nWhat languages or punctuation do you want to be in your password: ");

                    language = Console.ReadLine();
                    Console.WriteLine("Your password: ");

                    AddLenguages(language, value, english, ukraine, punctuation, number);


                    password = null;

                    for (int nr = 0; nr <= pw; nr++)
                        password = password + value[random.Next(0, value.Length)];

                    Console.WriteLine(password);
                }
                else
                    Console.WriteLine("To create password write: create password");

            } while (text != "Stop");

            return;

            //End of function
        }
        
        static void AddLenguages(string language, StringBuilder value, string english, string ukraine, string punctuation, string number)
        {
            if (language.Contains("Ukraine"))
                value.Append(ukraine);

            if (language.Contains("English"))
                value.Append(english);

            if (language.Contains("Number"))
                value.Append(number);

            if (language.Contains("Punctuation"))
                value.Append(punctuation);
        }
    }
}
