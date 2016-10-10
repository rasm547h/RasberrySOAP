using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private const string SelectAllReading = "select * from Reading";
        private const string SelectReadingByValue = "select * from Reading where reading_value <1000";
        private const string InsertIntoReading = "insert into Reading (reading_value) values (@Value)";
        
        public string InsertReadingService(int saveReading)
        {
            string connString = GetConnectionStringFromWebConfig();
            using (SqlConnection databaseConnection = new SqlConnection(connString))
            {
                databaseConnection.Open();
                {
                    while (true)
                    {
                        var value = saveReading;
                        if (value == null) break;
                        using (SqlCommand insertCommand = new SqlCommand(InsertIntoReading, databaseConnection))
                        {
                            insertCommand.Parameters.AddWithValue("@Value", value);
                            int rowsAffected = insertCommand.ExecuteNonQuery();
                            return rowsAffected + " row(s) affected";
                        }
                    }
                }
            }
            return "Nothing much has been done around here!";
        }

        public Reading GetReadingService(int newReading)
        {
            Reading reading = new Reading();
            string connString = GetConnectionStringFromWebConfig();
            using (SqlConnection databaseConnection = new SqlConnection(connString))
            {
                databaseConnection.Open();
                {
                    using (SqlCommand selectCommand = new SqlCommand(SelectReadingByValue, databaseConnection))
                    {
                        selectCommand.Parameters.AddWithValue("@Value", newReading);
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int value = reader.GetInt32(0);
                                    reading = new Reading() {Value = value};
                                }
                                return reading;
                            }
                            else
                            {
                                return reading;
                            }
                        }
                    }
                }
            }
        }
        /*
        public IEnumerable<Reading> GetStudentsService()
        {
            List<Reading> students = new List<Reading>();
            string connString = GetConnectionStringFromWebConfig();
            using (SqlConnection databaseConnection = new SqlConnection(connString))
            {
                databaseConnection.Open();
                {
                    using (SqlCommand selectCommand = new SqlCommand(SelectAllReading, databaseConnection))
                    {
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int value = reader.GetInt32(0);
                                    Reading reading = new Reading() {Value = value};
                                    students.Add(reading);
                                }
                                return students;
                            }
                            else
                            {
                                return students;
                            }
                        }
                    }
                }
            }
        }
        */
        private string GetConnectionStringFromWebConfig()
        {
            ConnectionStringSettingsCollection connectionStringSettingsCollection =
                ConfigurationManager.ConnectionStrings;
            ConnectionStringSettings connStringSettings = connectionStringSettingsCollection["MySQLDatabase"];
            string connString = connStringSettings.ConnectionString;
            return connString;
        }
    }
}

