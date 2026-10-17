// Modern .NET, Chapter 5, Section 5.10
public static class NotificationService
{
    public static void NotifyShipment(int orderId)
    {
        var order = Database.GetOrder(orderId);
        var smtp = new SmtpClient(ConfigurationManager.AppSettings["SmtpHost"]);
        smtp.Send("noreply@example.com", order.CustomerEmail,
                  "Shipped", "Your order " + orderId + " shipped at " + DateTime.Now);
        Logger.Info("Notified " + orderId);
    }
}
