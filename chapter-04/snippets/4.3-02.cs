// Modern .NET, Chapter 4, Section 4.3
var a = new Order(1, 49.90m, OrderStatus.Pending);
var b = new Order(1, 49.90m, OrderStatus.Pending);

Console.WriteLine(a == b);          // True: compared by value
Console.WriteLine(a);               // Order { Id = 1, Total = 49.90, Status = Pending }

var paid = a with { Status = OrderStatus.Paid };  // new object, original untouched
Console.WriteLine(paid);
