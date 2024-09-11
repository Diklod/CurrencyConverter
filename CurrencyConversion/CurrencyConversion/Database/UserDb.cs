using api_crud_stationerys.Database;
using CurrencyConversion.Model;
using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Security.Cryptography;

namespace CurrencyConversion.Database
{
    public class UserDb
    {
        public bool Add(User user)
        {
            bool result = false;

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"INSERT INTO users " +
                                         @"(name, adress, phone, birth_date, email, password) " +
                                         @"VALUES " +
                                         @"(@name, @adress, @phone, @birth_date, @email, @password);";

                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@adress", user.Adress);
                    command.Parameters.AddWithValue("@phone", user.Phone);
                    command.Parameters.AddWithValue("@birth_date", user.BirthDate);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@password", user.Password);


                    AccessDb db = new AccessDb();

                    using (command.Connection = db.OpenConnection())
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public bool EmailExists(string email)
        {
            bool result = false;
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT COUNT(id) FROM users " +
                                          @"WHERE email = @email;";

                    command.Parameters.AddWithValue("@email", email);


                    using (command.Connection = db.OpenConnection())
                    {
                        int response = Convert.ToInt32(command.ExecuteScalar());

                        if (response > 0)
                            result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public bool GenerateToken(string email, string password)
        {
            bool result = false;
            AccessDb db = new AccessDb();
            Guid guid = Guid.NewGuid();
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"UPDATE users " +
                                      @"SET token = @token " +
                                      @"WHERE email = @email and password = @password;";

                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@token", guid);

                    using (command.Connection = db.OpenConnection())
                    {
                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex) { }

            return result;
        }

        public Currency ConvertCurrency(double real)
        {
            try
            {
                Currency conversions = new Currency();

                conversions.Real = real;
                conversions.DolarAmericano = real / 5.66;
                conversions.Euro = real / 6.23;
                conversions.LibrasEsterlina = real / 7.41;
                conversions.PesosArgentinos = real / 0.0059;

                return conversions;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool ValidateToken(string token)
        {
            bool result = false;
            AccessDb db = new AccessDb();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.CommandText = @"SELECT COUNT(id) FROM users " +
                                          @"WHERE token = @token;";

                    command.Parameters.AddWithValue("@token", token);


                    using (command.Connection = db.OpenConnection())
                    {
                        int response = Convert.ToInt32(command.ExecuteScalar());

                        if (response > 0)
                            result = true;
                    }
                }
            }
            catch (Exception ex) { }

            return result;
        }
    }
}
