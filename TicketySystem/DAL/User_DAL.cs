using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TicketySystem.Models;

namespace TicketySystem.DAL
{

    public class User_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();
        public List<User> login(string username, string password)
        {
            List<User> UserList = new List<User>();

            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UserLogin";
                cmd.Parameters.AddWithValue("@ip_username", username);
                cmd.Parameters.AddWithValue("@ip_password", password);
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sqlConnection.Open();
                sqlData.Fill(dt);
                sqlConnection.Close();
                Guid myGuid = Guid.NewGuid();
                foreach (DataRow dataRow in dt.Rows)
                {
                    User user = new User
                    {
                        Id = (Guid)dataRow["ID"],
                        Name = dataRow["Name"].ToString(),
                        Username = dataRow["Username"].ToString(),
                        UserRole = (User.Role)Enum.Parse(typeof(User.Role), dataRow["UserRole"].ToString())
                    };
                    UserList.Add(user);
                }
            }
            return UserList;
        }

        public static User GetUserByUsername(string username)
        {
            User user = null;

            // Your database connection string
            string conString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                // Define a SQL query to retrieve the user based on the username
                string query = "SELECT Id, Name, UserRole FROM Users WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the username as a parameter to the SQL query
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Id = (Guid)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Username = username, // Retrieve it from the parameter
                                UserRole = (User.Role)(int)reader["UserRole"]
                            };
                        }
                    }
                }
            }

            return user;
        }

        public List<User> GetAllUser()
        {
            List<User> UserList = new List<User>();

            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllUser";
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sqlConnection.Open();
                sqlData.Fill(dt);
                sqlConnection.Close();
                Guid myGuid = Guid.NewGuid();
                foreach (DataRow dataRow in dt.Rows)
                {
                    Guid guidValue = (Guid)dataRow["ID"];
                    UserList.Add(new User
                    {
                        Id = guidValue,
                        Name = dataRow["Name"].ToString(),
                        UserRole = (User.Role)Convert.ToInt32(dataRow["UserRole"])
                    });
                }
            }

            return UserList;
        }

        public bool insertUser(User user)
        {
            Guid newGuid = Guid.NewGuid();
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand("insertUser", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", newGuid);
                sqlCommand.Parameters.AddWithValue("@name", user.Name);
                sqlCommand.Parameters.AddWithValue("@username", user.Username);
                sqlCommand.Parameters.AddWithValue("@password", user.Password);
                sqlCommand.Parameters.AddWithValue("@userRole", user.UserRole);

                connection.Open();
                id = sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            return false;
        }
    }
}