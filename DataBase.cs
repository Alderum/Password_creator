using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using PasswordCreator;
using Passwordcreator;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Windows;

namespace PasswordCreator
{
    class DataBase
    {
        static string stringConnection;
        public DataBase(string assetsPath)
        {
            stringConnection = string.Format("provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}\\MADatabase.accdb", assetsPath);
        }

        private void CommandExecutor(string text)
        {
            using(OleDbConnection connection = new OleDbConnection(stringConnection))
            {
                using (OleDbCommand command = new OleDbCommand(text, connection))
                {
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                }
            }
        }

        public void CreateUser()
        {
            //Offer creaing new user
            Console.WriteLine("This user has not been registrated, do you want to registrate new one? \n(Yes - Y, No - N)");
            //If user doesn't want to create new one, return to the LogIn()
            if (Console.ReadLine().Contains("Y"))
            {
                try
                {
                    //Get user name
                    Console.Write("Name: ");
                    string userName = Console.ReadLine();
                    //Get user password
                    Console.Write("Password: ");
                    string userPassword = Console.ReadLine();

                    //Chek
                    if (CheckUserByName(userName))
                    {
                        Console.WriteLine("User has been signed."); ;
                    }
                    //Command for execute
                    string insert = string.Format("INSERT INTO users([id], [userName], [userPassword])" +
                                   "VALUES('{0}','{1}','{2}');", GetHash(userName), userName, userPassword);

                    //Execute command insert
                    CommandExecutor(insert);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            LogIn();
        }
        public void SavePassword(string passwordText, string passwordName)
        {
            try
            {
                //Command for execute
                string insert = string.Format("INSERT INTO passwords([passwordId], [userId], [passwordText], [passwordName]) " +
                               "VALUES({0},{1},'{2}','{3}');", GetHash(passwordText), Program.hash, passwordText, passwordName);

                //Execute command insert
                CommandExecutor(insert);
                Console.WriteLine("Password saved \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void Delete(string passwordName)
        {
            try
            {
                //Command for execute
                string delete = string.Format("DELETE FROM passwords WHERE passwordName = '{0}' and userId = {1};", passwordName, Program.hash);

                //Execute command delete
                CommandExecutor(delete);
                Console.WriteLine("Password deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Show()
        {
            //Command for execute
            string text = string.Format($"SELECT * FROM passwords WHERE [userId] = {Program.hash}");

            //Create new connection
            using(OleDbConnection connection = new OleDbConnection(stringConnection))
            {
                //Create new cmd command
                using(OleDbCommand command = new OleDbCommand(text, connection))
                {
                    connection.Open();
                    //Create reader and execute command
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string passwordText = (string)reader["passwordText"];
                                string passwordName = (string)reader["passwordName"];

                                //Display the data.
                                Console.WriteLine("Name of your password: " + passwordName);
                                Console.WriteLine("Your password:         " + passwordText + "\n");
                            }
                        }
                    }
                }
            }
        }

        public void ShowByName(string passwordName)
        {
            //Command for execute
            string text = $"SELECT * FROM passwords WHERE [passwordName] = '{passwordName}';";
            
            using (OleDbConnection connection = new OleDbConnection(stringConnection))
            {
                using (OleDbCommand command = new OleDbCommand(text, connection))
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string passwordText = (string)reader["passwordText"];

                            // Print the data.
                            Console.WriteLine("\nName of your password: " + passwordName);
                            Console.WriteLine("Your password:         " + passwordText + "\n");
                        }
                    }

                }
            }
        }

        public void Update(string passwordName, string passwordText)
        {
            string update = $"UPDATE passwords SET passwordText = '{passwordText}' where userId='{Program.hash}' AND passwordName='{passwordName}';";

            //Execute command update
            CommandExecutor(update);
            Console.WriteLine("Password whith name: " + GetName() + " was updated to: " + passwordText);
        }

        public static uint GetHash(string text)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToUInt32(hashed, 0) / 100000;
        }
        private string GetName()
        {
            //Command for execute
            string text = string.Format("SELECT name FROM passwords WHERE hash = {0}", Program.hash);

            using (OleDbConnection connection = new OleDbConnection(stringConnection))
            {
                using (OleDbCommand command = new OleDbCommand(text, connection))
                {
                    OleDbDataReader reader;
                    connection.Open();
                    reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return (string)reader["name"];
                        }
                    }
                    return "Name has not avilable.";
                }
            }
        }

        public void LogIn()
        {
            //Get users name and password
            Console.Write("Please, login. \nName: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string userPassword = Console.ReadLine();

            //Get user hash
            Program.hash = GetHash(userName);

            //Check users name and password
            if (CheckUser(userName, userPassword))
                Console.WriteLine("LogIn was succes");
            else
            {
                //Create user if there is no users with that name and passwords
                CreateUser();
            }
        }

        private bool CheckUser(string userName, string userPassword)
        {
            //command text
            string query = $"SELECT * FROM users WHERE userName = '{userName}' AND userPassword = '{userPassword}'";
            using (OleDbConnection connection = new OleDbConnection(stringConnection))
            {
                //Open OleDb connection
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    //execute command
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            return true;
                        else
                            return false;
                    }
                }
            }
        }

        private bool CheckUserByName(string userName)
        {
            //command text
            string query = $"SELECT * FROM users WHERE userName = '{userName}'";
            using (OleDbConnection connection = new OleDbConnection(stringConnection))
            {
                //Open OleDb connection
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    //execute command
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                            return true;
                        else
                            return false;
                    }
                }
            }
        }
    }
}
