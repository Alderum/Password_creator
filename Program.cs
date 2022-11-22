using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordCreator;
using System.Windows.Media;
using System.Threading;

namespace Passwordcreator
{
    class Program
    {
        public static string assetsPath = Environment.CurrentDirectory.Replace("\\bin\\Debug", "\\Assets");
        public static uint hash;

        static void Main(string[] args)
        {
            string text, password, language;

            StringBuilder value = new StringBuilder();
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(assetsPath + "\\Login.mp3", UriKind.Absolute));
            mediaPlayer.Play();

            Random random = new Random();

            DataBase dataBase = new DataBase(assetsPath);
            dataBase.LogIn();

            string english = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            string ukraine = "йцукенгшщзхїфівапролджєячсмитьбю";
            string punctuation = "[]{};:',<.>/?-_=+|!#@$%^&*()~`";
            string number = "1234567890";
            string name, pass;
            do
            {
                value.Clear();

                text = Console.ReadLine();
                switch(text)
                {
                    case "cp":
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
                        Console.WriteLine("Do you want to save that password? (Yes - Y, No - N)");

                        if(Console.ReadLine().Contains("Y"))
                        {
                            Console.WriteLine("Enter name for your password: ");
                            name = Console.ReadLine();

                            mediaPlayer.Open(new Uri(assetsPath + "\\Save.mp3", UriKind.Absolute));
                            mediaPlayer.Play();
                            //Save password with name
                            dataBase.SavePassword(password, name);
                        }
                        break;

                    case "dp":
                        Console.WriteLine("Enter name of the password for delete: ");

                        name = Console.ReadLine();
                        //Delete password by name
                        dataBase.Delete(name);
                        break;

                    case "sp":
                        mediaPlayer.Open(new Uri(assetsPath + "\\Show.mp3", UriKind.Absolute));
                        mediaPlayer.Play();
                        dataBase.Show();
                        break;

                    case "spbn":
                        Console.WriteLine("Enter the name of your password: ");
                        //Extract passwords name
                        name = Console.ReadLine();


                        mediaPlayer.Open(new Uri(assetsPath + "\\Show.mp3", UriKind.Absolute));
                        mediaPlayer.Play();
                        //Show password by name (text)
                        dataBase.ShowByName(name);
                        break;

                    case "up":
                        Console.WriteLine("Name of password wich you want to update: ");
                        name = Console.ReadLine();

                        Console.WriteLine("New password: ");
                        pass = Console.ReadLine();

                        //Update password by name to pass
                        dataBase.Update(name, pass);
                        break;

                    case "cyp":
                        Console.WriteLine("Enter a name of your password:");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter a password text:");
                        pass = Console.ReadLine();

                        mediaPlayer.Open(new Uri(assetsPath + "\\Save.mp3", UriKind.Absolute));
                        mediaPlayer.Play();
                        //Save password
                        dataBase.SavePassword(pass, name);
                        break;

                    case "help":
                        Console.WriteLine("To create and save random password write: cp" +
                                          "\nTo delete password write:               dp" +
                                          "\nTo show all passwords:                  sp" +
                                          "\nTo show password by name:               spbn" +
                                          "\nTo create your password:                cyp" +
                                          "\nTo update password write:               up");
                        break;

                    case "stop":
                        break;

                    default:
                        Console.WriteLine("To create password write: create password" +
                            "\nFor more information write: help");
                        break;
                }
            } while (text != "stop");

            mediaPlayer.Open(new Uri(assetsPath + "\\Exit.mp3", UriKind.Absolute));
            mediaPlayer.Play();
            Thread.Sleep(4000);

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
