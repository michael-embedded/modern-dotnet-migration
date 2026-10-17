// Modern .NET, Chapter 5, Section 5.8
// Legacy: string concatenation
Logger.Info("Order " + orderId + " shipped to " + city);

// Modern: message template with named placeholders
logger.LogInformation("Order {OrderId} shipped to {City}", orderId, city);
