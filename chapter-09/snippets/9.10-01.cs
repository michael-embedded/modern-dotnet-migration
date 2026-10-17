// Modern .NET, Chapter 9, Section 9.10
public class ShipmentsController : ApiController
{
    [HttpGet]
    public HttpResponseMessage GetShipmentNew2(string id, string apiKey)       // verb in the name, key in the query
    {
        if (apiKey != ConfigurationManager.AppSettings["Key"])
            return Request.CreateResponse(HttpStatusCode.OK, new { success = false, error = "bad key" });  // 200 for failure

        var s = new ShipmentRepository().Find(id);                              // new'd up, synchronous
        if (s == null)
            return Request.CreateResponse(HttpStatusCode.OK, new { success = false, error = "not found" });

        return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = s });
    }
}
