using GameLendXchange.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLendXchange.DAO
{
    internal class BookingDB
    {
        private String connectionString;

        public BookingDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["XchangeDB"].ConnectionString;
        }

        public bool Create(Booking b)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Booking(bookingDate, idPlayer, idVideoGame) VALUES('{b.BookingDate}',{b.Player.IdUser} ,{b.VideoGame.IdGame})", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public Booking Read(String idBooking)
        {
            Booking b = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Booking WHERE idBooking='@idBooking'", connection);

                cmd.Parameters.AddWithValue("idBooking", idBooking);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        b = new Booking();
                        b.IdBooking = reader.GetInt32("idBooking");
                        b.BookingDate = reader.GetDateTime("bookingDate");
                        b.Player.IdUser = reader.GetInt32("idPlayer");
                        b.VideoGame.IdGame = reader.GetInt32("idVideoGame");
                    }
                }
            }
            return b;
        }
        public bool Update(Booking b)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.Loan SET bookingDate = {b.BookingDate}, idPlayer = {b.Player.IdUser}, idVideoGame = {b.VideoGame.IdGame}", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public bool Delete(Booking b)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.Loan WHERE idBooking = {b.IdBooking}", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }


        public List<Booking> ReadAll()
        {
            List<Booking> booking = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Booking", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking b = new Booking();
                        b = new Booking();
                        b.IdBooking = reader.GetInt32("idBooking");
                        b.BookingDate = reader.GetDateTime("bookingDate");
                        b.Player.IdUser = reader.GetInt32("idPlayer");
                        b.VideoGame.IdGame = reader.GetInt32("idVideoGame");
                        booking.Add(b);
                    }
                }
            }
            return booking;
        }


    }
}
