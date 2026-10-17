// Modern .NET, Chapter 1, Section 1.1
// Legacy style: .NET Framework 4.x
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace LegacyShop
{
    public class CustomerRepository
    {
        public static List<string> GetCustomerNames()
        {
            var names = new List<string>();
            string cs = ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;

            using (var conn = new SqlConnection(cs))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Name FROM Customers", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        names.Add(reader.GetString(0));
                    }
                }
            }
            return names;
        }
    }
}
