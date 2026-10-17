// Modern .NET, Chapter 5, Section 5.1
using System;
using System.Configuration;
using System.Net.Mail;

public class OrderProcessor
{
    public void Process(int orderId)
    {
        var order = Database.GetOrder(orderId);          // static data access
        var tax = Convert.ToDecimal(ConfigurationManager.AppSettings["TaxRate"]);
        order.Total += order.Total * tax;
        order.ProcessedAt = DateTime.Now;                // hidden clock
        Database.Save(order);

        var client = new SmtpClient(ConfigurationManager.AppSettings["SmtpHost"]);
        client.Send("shop@example.com", order.CustomerEmail, "Order processed", "Thanks!");

        Logger.Info("Processed order " + orderId);       // static logger
    }
}
