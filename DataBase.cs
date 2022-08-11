using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PasswordCreator;

namespace PasswordCreator
{
    class DataBase
    {
        public string utils = "datasource=localhost;port=3306;username=root;password=";

        public string AddPassword()
        {
            Console.Write("Password: ");
            return Console.ReadLine();
        }

        private void CommandExecutor(string text)
        {

            MySqlConnection connection = new MySqlConnection(utils);

            MySqlCommand command = new MySqlCommand(text, connection);

            MySqlDataReader reader;
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
            }
            connection.Close();
        }


        public void Save(string password, string name)
        {
            try
            {
                //Command for execute
                string insert = "insert into pascr_sql.list(pass, name)" +
                               "values('" + password + "','" + name + "');";

                //Execute command insert
                CommandExecutor(insert);
                Console.WriteLine("Password saved");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void Delete(string name)
        {
            try
            {
                //Command for execute
                string delete = "delete from pascr_sql.list where name = '" + name + "';";

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
            string text = "SELECT pass, name FROM pascr_sql.list";
            MySqlConnection connection = new MySqlConnection(utils);

            MySqlCommand command = new MySqlCommand(text, connection);

            MySqlDataReader reader;
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string pass = (string)reader["pass"];
                string name = (string)reader["name"];

                // Print the data.
                Console.WriteLine(name + " " + pass);
            }
            connection.Close();
        }

        public void ShowByName(string name)
        {
            //Command for execute
            string text = "SELECT pass, name FROM pascr_sql.list where name = '" + name + "';";
            MySqlConnection connection = new MySqlConnection(utils);

            MySqlCommand command = new MySqlCommand(text, connection);

            MySqlDataReader reader;
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string pass = (string)reader["pass"];

                // Print the data.
                Console.WriteLine(name + " " + pass);
            }
            connection.Close();
        }


        public void Update(string name, string pass)
        {
            string update = "update pascr_sql.list set pass = '" + pass + "' where name='" + name + "';";

            //Execute command update
            CommandExecutor(update);
            Console.WriteLine("Password whith name: " + name + " was updated to: " + pass);
        }
    }
}
