﻿using GameLendXchange.Classes;
using GameLendXchange.Classes.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameLendXchange.DAO
{
    internal class LoanDB
    {
        private String connectionString;

        public LoanDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["XchangeDB"].ConnectionString;
        }

        public bool Create(Loan l)
        {
            var success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    int OnGo = 0;
                    if (l.OnGoing == true)
                    {
                        OnGo = 1;
                    }
                    else
                    {
                        OnGo = 0;
                    }
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.Loan(startDate, endDate, onGoing, idCopy, idLender, idBorrower) VALUES('{l.StartDate:yyyy-MM-dd}', '{l.EndDate:yyyy-MM-dd}', '{OnGo}', {l.Copy.IdCopy},{l.Lender.IdUser},{l.Borrower.IdUser})", connection);
                    connection.Open();
                    int res = cmd.ExecuteNonQuery();
                    success = res > 0;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Une erreur SQL s'est produite : " + ex.Message);
                }
            }
            return success;
        }

        public Loan Read(int idLoan)
        {
            Loan l = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Loan WHERE idLoan='@idLoan'", connection);

                cmd.Parameters.AddWithValue("idLoan", idLoan);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        l = new Loan();
                        l.IdLoan = reader.GetInt32("idLoan");
                        l.StartDate = reader.GetDateTime("startDate");
                        l.EndDate = reader.GetDateTime("endDate");
                        l.OnGoing = reader.GetBoolean("onGoing");
                        l.Copy.IdCopy = reader.GetInt32("idCopy");
                        l.Lender.IdUser = reader.GetInt32("idLender");
                        l.Borrower.IdUser = reader.GetInt32("idBorrower");
                    }
                }
            }
            return l;
        }
        public bool Update(Loan l)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.Loan SET StartDate = {l.StartDate}, EndDate = {l.EndDate}, OnGoing = {l.OnGoing}, idCopy = {l.Copy.IdCopy}, idLender = {l.Lender.IdUser}, idBorrower = {l.Borrower.IdUser}", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public bool Delete(Loan l)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.Loan WHERE idLoan = {l.IdLoan}", connection);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }


        public List<Loan> ReadAll()
        {
            List<Loan> loan = new List<Loan>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Loan", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan l = new Loan();
                        l.IdLoan = reader.GetInt32("idLoan");
                        l.StartDate = reader.GetDateTime("startDate");
                        l.EndDate = reader.GetDateTime("endDate");
                        l.OnGoing = reader.GetBoolean("onGoing");                 
                        l.Lender = Player.GetPlayerById(reader.GetInt32("idLender"));
                        l.Borrower = Player.GetPlayerById(reader.GetInt32("idBorrower"));
                        l.Copy = new Copy();
                        l.Copy.IdCopy = reader.GetInt32("idCopy");
                        loan.Add(l);
                    }
                }
            }
            return loan;
        }

        public List<Loan> ReadAllId(int idBorrower)
        {
            List<Loan> loan = new List<Loan>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Loan WHERE idBorrower=@idBorrower", connection);
                cmd.Parameters.AddWithValue("idBorrower", idBorrower);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan l = new Loan();
                        l.IdLoan = reader.GetInt32("idLoan");
                        l.StartDate = reader.GetDateTime("startDate");
                        l.EndDate = reader.GetDateTime("endDate");
                        l.OnGoing = reader.GetBoolean("onGoing");
                        l.Lender = Player.GetPlayerById(reader.GetInt32("idLender"));
                        l.Borrower = Player.GetPlayerById(reader.GetInt32("idBorrower"));
                        l.Copy = new Copy();
                        l.Copy.IdCopy = reader.GetInt32("idCopy");
                        loan.Add(l);
                    }
                }
            }
            return loan;
        }

        public bool EndLoan(int idLoan)
        {
            bool success = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE dbo.Loan SET onGoing = 0 WHERE idLoan = @IdLoan", connection);
                cmd.Parameters.AddWithValue("IdLoan", idLoan);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }

        public bool CalculateBalance(int loanId)
        {
            Loan loan = new Loan();
            Player p = new Player();
            Player p2 = new Player();
            bool success = false;
            bool flag = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.Loan WHERE idLoan = @IdLoan", connection);
                cmd.Parameters.AddWithValue("@IdLoan", loanId);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        loan.IdLoan = reader.GetInt32("idLoan");
                        loan.EndDate = reader.GetDateTime("endDate");
                        loan.OnGoing = reader.GetBoolean("onGoing");
                        loan.Borrower = Player.GetPlayerById(reader.GetInt32("idBorrower"));
                        loan.Lender = Player.GetPlayerById(reader.GetInt32("idLender"));

                        int daysLate = (int)(DateTime.Now - loan.EndDate).TotalDays;
                        if (daysLate > 0)
                        {
                            int creditsToDeduct = daysLate * 5;
                            if (creditsToDeduct > 0)
                            {
                                p = loan.Borrower;
                                p2 = loan.Lender;
                                p.Credit -= creditsToDeduct;
                                p2.Credit += creditsToDeduct;
                                flag = true;     
                            }
                        }
                        else
                        {
                            success = true;
                        }
                    }
                }
                if (flag)
                {
                    using (SqlConnection connection2 = new SqlConnection(connectionString))
                    {
                        string updateSql = "UPDATE dbo.Player SET credit = @credits WHERE idPlayer = @playerId";
                        SqlCommand updateCmd = new SqlCommand(updateSql, connection);
                        updateCmd.Parameters.AddWithValue("@credits", p.Credit);
                        updateCmd.Parameters.AddWithValue("@playerId", p.IdUser);
                        int res = updateCmd.ExecuteNonQuery();
                        success = res > 0;
                    }

                    using (SqlConnection connection3 = new SqlConnection(connectionString))
                    {
                        string updateSql = "UPDATE dbo.Player SET credit = @credits WHERE idPlayer = @playerId";
                        SqlCommand updateCmd = new SqlCommand(updateSql, connection);
                        updateCmd.Parameters.AddWithValue("@credits", p2.Credit);
                        updateCmd.Parameters.AddWithValue("@playerId", p2.IdUser);
                        int res = updateCmd.ExecuteNonQuery();
                    }

                }
            }
            return success;
        }
    }
}
