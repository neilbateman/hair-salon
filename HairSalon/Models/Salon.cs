using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Salon
  {
    public string SalonName {get; set;}
    public string Description {get; set;}
    public int Id {get; set;}

    public Salon (string salonName, string description, int id = 0)
    {
      SalonName = salonName;
      Description = description;
      Id = id;
    }

        public override bool Equals(System.Object otherSalon)
        {
            if (!(otherSalon is Salon))
            {
                return false;
            }
            else
            {
                Salon newSalon = (Salon) otherSalon;
                bool idEquality = (this.Id == newSalon.Id);
                bool salonNameEquality = (this.SalonName == newSalon.SalonName);
                bool descriptionEquality = (this.Description == newSalon.Description);
                return (idEquality && salonNameEquality && descriptionEquality);
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO salons (salonName, description) VALUES (@salonName, @description);";
            MySqlParameter salonName = new MySqlParameter();
            salonName.ParameterSalonName = "@salonName";
            salonName.Value = this.SalonName;
            cmd.Parameters.Add(salonName);
            MySqlParameter description = new MySqlParameter();
            description.ParameterSalonName = "@description";
            description.Value = this.Description;
            cmd.Parameters.Add(description);
            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }


        public static List<Salon> GetAll()
        {
            List<Salon> allSalons = new List<Salon> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM salons ORDER BY salonName;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int salonId = rdr.GetInt32(0);
                string salonName = rdr.GetString(1);
                string salonDescription = rdr.GetString(2);
                Salon newSalon = new Salon (salonName,salonDescription,salonId);
                allSalons.Add(newSalon);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSalons;
        }

        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_salons (stylist_id, salon_id) VALUES (@stylist_id, @salon_id);";
            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@stylist_id";
            stylist_id.Value = newStylist.Id;
            cmd.Parameters.Add(stylist_id);
            MySqlParameter salon_id = new MySqlParameter();
            salon_id.ParameterName = "@salon_id";
            salon_id.Value = Id;
            cmd.Parameters.Add(salon_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Stylist> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM salons
                                  JOIN stylists_salons ON (salons.id = stylists_salons.salon_id)
                                  JOIN stylists ON (stylists_salons.stylist_id stylists.id)
                              WHERE salons.id = @salon_id;";
            MySqlParameter salonIdParameter = new MySqlParameter();
            salonIdParameter.ParameterName = "@salon_id";
            salonIdParameter.Value = Id;
            cmd.Parameters.Add(salonIdParameter);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Stylist> stylists = new List<Stylist> {};
            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                string stylistDescription = rdr.GetString(2);
                Stylist foundStylist = new Stylist(stylistName, stylistDescription, stylistId);
                stylists.Add(foundStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return stylists;


        public static Salon Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM salons WHERE id = (@search_id);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterSalonName = "@search_id";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int salonId = 0;
            string salonName = "";
            string salonDescription = "";
            while(rdr.Read())
            {
             salonId = rdr.GetInt32(0);
             salonName = rdr.GetString(1);
             salonDescription = rdr.GetString(2);
            }
            Salon newSalon = new Salon(salonName,salonDescription,salonId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newSalon;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM salons;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
// public List<Client> GetClients()
// {
//   List<Client> allSalonClients = new List<Client> {};
//   MySqlConnection conn = DB.Connection();
//   conn.Open();
//   var cmd = conn.CreateCommand() as MySqlCommand;
//   cmd.CommandText = @"SELECT * FROM clients WHEREsalon_id = @stylist_id;";
//   MySqlParametersalonId = new MySqlParameter();
//  salonId.ParameterSalonName = "@stylist_id";
//  salonId.Value = this.Id;
//   cmd.Parameters.Add(stylistId);
//   var rdr = cmd.ExecuteReader() as MySqlDataReader;
//   while(rdr.Read())
//   {
//     int clientId = rdr.GetInt32(0);
//     string clientSalonName = rdr.GetString(1);
//     string clientPhone = rdr.GetString(2);
//     string clientEmail = rdr.GetString(3);
//     int clientSalonId = rdr.GetInt32(4);
//     Client newClient = new Client(clientSalonName, clientPhone, clientEmail, clientSalonId, clientId);
//     allSalonClients.Add(newClient);
//   }
//   conn.Close();
//   if (conn != null)
//   {
//       conn.Dispose();
//   }
//   return allSalonClients;
// }
