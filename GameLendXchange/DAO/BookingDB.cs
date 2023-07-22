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
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM dbo.Booking";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idGame = reader.GetInt32(reader.GetOrdinal("idVideoGame"));
                            VideoGame game = VideoGame.ReadId(idGame);
                            int idPlayer = reader.GetInt32(reader.GetOrdinal("idPlayer"));
                            Player player = Player.GetPlayerById(idPlayer);
                            if (game != null && player != null)
                            {
                                Booking booking = new Booking
                                {
                                    IdBooking = reader.GetInt32(reader.GetOrdinal("idBooking")),
                                    BookingDate = reader.GetDateTime(reader.GetOrdinal("bookingDate")),
                                    Player = player,
                                    VideoGame = game, // Assign the game object to the Booking.VideoGame property.
                                };
                                bookings.Add(booking);
                            }
                        }
                    }
                }
            }

            return bookings;
        }

        public List<Booking> ReadAllBookingPlayer(int idPlayer)
        {
            List<Booking> bookings = new List<Booking>();
            Player player = Player.GetPlayerById(idPlayer);
            

            if (player == null)
            {
                // Player not found, handle the situation accordingly.
                return bookings;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM dbo.Booking WHERE idPlayer = @idPlayer";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@idPlayer", idPlayer);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idGame = reader.GetInt32(reader.GetOrdinal("idVideoGame"));
                            VideoGame game = VideoGame.ReadId(idGame);

                            if (game != null)
                            {
                                Booking booking = new Booking
                                {
                                    IdBooking = reader.GetInt32(reader.GetOrdinal("idBooking")),
                                    BookingDate = reader.GetDateTime(reader.GetOrdinal("bookingDate")),
                                    Player = player,
                                    VideoGame = game, // Assign the game object to the Booking.VideoGame property.
                                };
                                bookings.Add(booking);
                            }
                        }
                    }
                }
            }

            return bookings;
        }

        // Méthode pour récupérer toutes les réservations d'un jeu à partir de son identifiant
        public List<Booking> ReadAllBookingsByVideoGame(int idGame)
        {
            List<Booking> bookings = new List<Booking>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT IdBooking, BookingDate, IdPlayer FROM Booking WHERE IdVideoGame = @IdVideoGame";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdVideoGame", idGame);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Créer un nouvel objet Booking avec les données de la base de données
                                Booking booking = new Booking
                                {
                                    IdBooking = Convert.ToInt32(reader["IdBooking"]),
                                    BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                                    Player = Player.GetPlayerById(Convert.ToInt32(reader["IdPlayer"])),
                                    VideoGame = VideoGame.GetGameById(idGame)
                                };

                                bookings.Add(booking);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de lecture de la base de données
                Console.WriteLine("Erreur lors de la lecture des réservations : " + ex.Message);
            }

            return bookings;
        }

    }
}

