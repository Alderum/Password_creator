using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordCreator;

namespace Passwordcreator
{
    class Program
    {


        static void Main(string[] args)
        {
            string text, password, language;
            StringBuilder value = new StringBuilder();

            Random random = new Random();
            DataBase dataBase = new DataBase();
            //Utils to MySQL
            dataBase.utils = "datasource=localhost;port=3306;username=root;password=" + AddPassword();

            string english = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            string ukraine = "йцукенгшщзхїфівапролджєячсмитьбю";
            string punctuation = "[]{};:',<.>/?-_=+|!#@$%^&*()~`";
            string number = "1234567890";
            do
            {
                value.Clear();

                text = Console.ReadLine();

                if (text.Contains("create password"))
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

                    //Add lenguage by name to value
                    AddLenguages(language, value, english, ukraine, punctuation, number);


                    password = null;

                    for (int nr = 0; nr <= pw; nr++)
                        password = password + value[random.Next(0, value.Length)];

                    Console.WriteLine(password);

                    if (text.Contains("save"))
                    {
                        Console.WriteLine("Enter name for your password: ");
                        string name = Console.ReadLine();

                        //Save password with name
                        dataBase.Save(password, name);
                    }

                }
                else if(text == "delete password")
                {
                    Console.WriteLine("Enter name of the password for delete: ");

                    string name = Console.ReadLine();
                    //Delete password by name
                    dataBase.Delete(name);
                }
                else if(text == "show passwords")
                {
                    dataBase.Show();
                }
                else if(text.Contains("show password by name: "))
                {
                    //Extract passwords name
                    text = text.Remove(0, 23);
                    //Show password by name (text)
                    dataBase.ShowByName(text);
                }
                else if (text.Contains("update password"))
                {
                    Console.WriteLine("Name of password wich you want to update: ");
                    string name = Console.ReadLine();

                    Console.WriteLine("New password: ");
                    string pass = Console.ReadLine();

                    //Update password by name to pass
                    dataBase.Update(name, pass);
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

        private static string AddPassword()
        {
            Console.Write("Password: ");
            return Console.ReadLine();
        }
    }
}
