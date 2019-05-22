using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    public string Name {get; set;}
    public string Details {get; set;}
    public int Id {get; set;}

    public Stylist (string name, string details, int id = 0)
    {
      Name = name;
      Details = details;
      Id = id;
    }

        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                bool idEquality = (this.Id == newStylist.Id);
                bool nameEquality = (this.Name == newStylist.Name);
                bool detailsEquality = (this.Details == newStylist.Details);
                return (idEquality && nameEquality && detailsEquality);
            }
        }
        public void AddSalon(int salonId)
      {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO stylists_salon (salon_id, stylist_id) VALUES (@salonId, @stylistId);";
          MySqlParameter salonIdParam = new MySqlParameter();
          salonIdParam.ParameterName = "@salonId";
          salonIdParam.Value = salonId;
          cmd.Parameters.Add(salonIdParam);
          MySqlParameter stylistid = new MySqlParameter();
          stylistid.ParameterName = "@stylistId";
          stylistid.Value = this.Id;
          cmd.Parameters.Add(stylistid);
          cmd.ExecuteNonQuery();
          Id = (int) cmd.LastInsertedId;
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
            cmd.CommandText = @"INSERT INTO stylists (name, details) VALUES (@name, @details);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this.Name;
            cmd.Parameters.Add(name);
            MySqlParameter details = new MySqlParameter();
            details.ParameterName = "@details";
            details.Value = this.Details;
            cmd.Parameters.Add(details);
            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }

        public List<Client> GetClients()
        {
          List<Client> allStylistClients = new List<Client> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";
          MySqlParameter stylistId = new MySqlParameter();
          stylistId.ParameterName = "@stylist_id";
          stylistId.Value = this.Id;
          cmd.Parameters.Add(stylistId);
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int clientId = rdr.GetInt32(0);
            string clientName = rdr.GetString(1);
            string clientPhone = rdr.GetString(2);
            string clientEmail = rdr.GetString(3);
            int clientStylistId = rdr.GetInt32(4);
            Client newClient = new Client(clientName, clientPhone, clientEmail, clientStylistId, clientId);
            allStylistClients.Add(newClient);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allStylistClients;
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists ORDER BY name;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                string stylistDetails = rdr.GetString(2);
                Stylist newStylist = new Stylist(stylistName, stylistDetails, stylistId);
                allStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@search_id);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@search_id";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int stylistId = 0;
            string stylistName = "";
            string stylistDetails = "";
            while(rdr.Read())
            {
              stylistId = rdr.GetInt32(0);
              stylistName = rdr.GetString(1);
              stylistDetails = rdr.GetString(2);
            }
            Stylist newStylist = new Stylist(stylistName, stylistDetails, stylistId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newStylist;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
