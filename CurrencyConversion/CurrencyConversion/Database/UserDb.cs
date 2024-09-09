using api_crud_stationerys.Database;
using CurrencyConversion.Model;
using Npgsql;
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

        //public bool GenerateToken(User user)
        //{
        //    bool result = false;
        //    AccessDb db = new AccessDb();
        //    try
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand())
        //        {
        //            command.CommandText = @"UPDATE jobs " +
        //                              @"SET name = @name, " +
        //                              @"salary = @salary, " +
        //                              @"description = @description " +
        //                              @"WHERE id = @id;";

        //            command.Parameters.AddWithValue("@id", user.Id);
        //            command.Parameters.AddWithValue("@name", user.Name);
        //            command.Parameters.AddWithValue("@salary", user.Salary);
        //            command.Parameters.AddWithValue("@description", user.Description);

        //            using (command.Connection = db.OpenConnection())
        //            {
        //                command.ExecuteNonQuery();
        //                result = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex) { }

        //    return result;
        //}
    }
}
