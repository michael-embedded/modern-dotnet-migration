// Modern .NET, Chapter 10, Section 10.1
public List<Customer> GetCustomers()
{
    var list = new List<Customer>();
    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Shop"].ConnectionString))
    using (var cmd = new SqlCommand("SELECT Id, Name, Email FROM Customers WHERE Active = 1", conn))
    {
        conn.Open();
        using (var r = cmd.ExecuteReader())
        {
            while (r.Read())
                list.Add(new Customer { Id = r.GetInt32(0), Name = r.GetString(1), Email = r.IsDBNull(2) ? null : r.GetString(2) });
        }
    }
    return list;
}
