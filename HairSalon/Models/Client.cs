using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        public string Name {get; set;}
        public string PhoneNumber {get; set;}
        public string EmailAddress {get; set;}
        public int Id {get; set;}
        public int StylistId {get; set;}

        public Client (string name, string phoneNumber, string emailAddress, int stylistId,  int id = 0)
        {
          Name = name;
          PhoneNumber = phoneNumber;
          EmailAddress = emailAddress;
          StylistId = stylistId;
          Id = id;
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                string clientPhone = rdr.GetString(2);
                string clientEmail = rdr.GetString(3);
                int clientStylistId = rdr.GetInt32(4);
                Client newClient = new Client(clientName, clientPhone, clientEmail, clientStylistId);
                allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            string clientPhone = "";
            string clientEmail = "";
            int clientStylistId = 0;
            while(rdr.Read())
            {
                clientId = rdr.GetInt32(0);
                clientName = rdr.GetString(1);
                clientPhone = rdr.GetString(2);
                clientEmail = rdr.GetString(3);
                clientStylistId = rdr.GetInt32(4);
            }
            Client newClient = new Client(clientName, clientPhone, clientEmail, clientStylistId, clientId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newClient;
        }

        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = this.Id == newClient.Id;
                bool NameEquality = this.Name == newClient.Name;
                bool phoneNumberEquality = this.PhoneNumber == newClient.PhoneNumber;
                bool emailAddressEquality = this.EmailAddress == newClient.EmailAddress;
                bool stylistEquality = this.StylistId == newClient.StylistId;
                return (idEquality && NameEquality && phoneNumberEquality && emailAddressEquality && stylistEquality);
             }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (name, phone_number, email_address, stylist_id) VALUES (@name, @phone_number, @email_address, @stylist_id);";
            MySqlParameter Name = new MySqlParameter();
            Name.ParameterName = "@name";
            Name.Value = this.Name;
            cmd.Parameters.Add(Name);
            MySqlParameter phoneNumber = new MySqlParameter();
            phoneNumber.ParameterName = "@phone_number";
            phoneNumber.Value = this.PhoneNumber;
            cmd.Parameters.Add(phoneNumber);
            MySqlParameter emailAddress = new MySqlParameter();
            emailAddress.ParameterName = "@email_address";
            emailAddress.Value = this.EmailAddress;
            cmd.Parameters.Add(emailAddress);
            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this.StylistId;
            cmd.Parameters.Add(stylistId);
            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newPhone, string newEmail)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET phone_number = @new_phone_number, email_address = @new_email_address WHERE id = @search_id;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@search_id";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);
            MySqlParameter phoneNumber = new MySqlParameter();
            phoneNumber.ParameterName = "@new_phone_number";
            phoneNumber.Value = newPhone;
            cmd.Parameters.Add(phoneNumber);
            MySqlParameter emailAddress = new MySqlParameter();
            emailAddress.ParameterName = "@new_email_address";
            emailAddress.Value = newEmail;
            cmd.Parameters.Add(emailAddress);
            cmd.ExecuteNonQuery();
            PhoneNumber = newPhone;
            EmailAddress = newEmail;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE id = @search_id;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@search_id";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
