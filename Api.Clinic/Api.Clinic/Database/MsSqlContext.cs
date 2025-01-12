using Library.Clinic.Models;
using Microsoft.Data.SqlClient;

namespace Api.Clinic.Database
{
    public class MsSqlContext
    {
        private string connectionString = "Server=PC1;Database=Clinic;Trusted_Connection=True;TrustServerCertificate=True";

        public MsSqlContext() { }

        public List<Physician> GetPhysicians()
        {
            var returnVal = new List<Physician>();
            using (var conn = new SqlConnection(connectionString))
            {
                var str = "SELECT * FROM Physician";
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = str;

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var newPhysician = new Physician
                            {
                                Id = (int)reader["Id"]
                                ,
                                Name = reader["Name"].ToString()
                            };

                            returnVal.Add(newPhysician);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return returnVal;
        }

        public List<Physician> SearchPhysicians(string query)
        {
            var returnVal = new List<Physician>();
            using (var conn = new SqlConnection(connectionString))
            {
                var str = "Physicians.[Search]";
                using (var cmd = new SqlCommand(str, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@query", query));

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var newPhysician = new Physician
                            {
                                Id = (int)reader["Id"]
                                ,
                                Name = reader["Name"].ToString()
                            };

                            returnVal.Add(newPhysician);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return returnVal;
        }

        public void DeletePhysician(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var str = "Physicians.[Delete]";
                using (var cmd = new SqlCommand(str, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var param = new SqlParameter("@Id", id);
                    cmd.Parameters.Add(param);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public void UpdatePhysician(Physician physician)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var str = "Physicians.[Update]";
                using (var cmd = new SqlCommand(str, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@name", physician.Name));
                    cmd.Parameters.Add(new SqlParameter("@Id", physician.Id));

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public Physician CreatePhysician(Physician physician)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var str = "Physicians.[Create]";
                using (var cmd = new SqlCommand(str, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@name", physician.Name));
                    var param = new SqlParameter("@Id", physician.Id);
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        physician.Id = (int)param.Value;
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return physician;
        }

    }
}
