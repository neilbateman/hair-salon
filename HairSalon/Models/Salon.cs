using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Salon
  {
    public string Name {get; set;}
    public string Description {get; set;}
    public int Id {get; set;}

    public Salon (string name, string description, int id = 0)
    {
      Name = name;
      Description = description;
      Id = id;
    }
    public static Salon Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM salons WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int salonId = 0;
      string name = "";
      string salonDescription = "";
      while(rdr.Read())
      {
        salonId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        salonDescription = rdr.GetString(2);
      }
      Salon newSalon = new Salon(name,salonDescription,salonId);
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
                bool nameEquality = (this.Name == newSalon.Name);
                bool descriptionEquality = (this.Description == newSalon.Description);
                return (idEquality && nameEquality && descriptionEquality);
            }
        }
        public void DeleteSalon()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM salons WHERE id = @search_id;";
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

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO salons (name, description) VALUES (@name, @description);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this.Name;
            cmd.Parameters.Add(name);
            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@description";
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
            cmd.CommandText = @"SELECT * FROM salons ORDER BY name;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int salonId = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string salonDescription = rdr.GetString(2);
                Salon newSalon = new Salon (name,salonDescription,salonId);
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
//  salonId.ParameterName = "@stylist_id";
//  salonId.Value = this.Id;
//   cmd.Parameters.Add(stylistId);
//   var rdr = cmd.ExecuteReader() as MySqlDataReader;
//   while(rdr.Read())
//   {
//     int clientId = rdr.GetInt32(0);
//     string clientName = rdr.GetString(1);
//     string clientPhone = rdr.GetString(2);
//     string clientEmail = rdr.GetString(3);
//     int clientSalonId = rdr.GetInt32(4);
//     Client newClient = new Client(clientName, clientPhone, clientEmail, clientSalonId, clientId);
//     allSalonClients.Add(newClient);
//   }
//   conn.Close();
//   if (conn != null)
//   {
//       conn.Dispose();
//   }
//   return allSalonClients;
// }
