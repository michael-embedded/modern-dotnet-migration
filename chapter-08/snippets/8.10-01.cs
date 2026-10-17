// Modern .NET, Chapter 8, Section 8.10
public class OrdersController : ApiController
{
    [HttpGet, Route("api/orders/{id}")]
    public IHttpActionResult Get(int id)
    {
        var user = HttpContext.Current.User.Identity.Name;            // static, untestable
        var connection = ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
        var order = new OrderRepository(connection).Find(id);         // new'd up
        if (order == null) return NotFound();
        if (order.Owner != user) return Unauthorized();
        return Ok(order);
    }
}
