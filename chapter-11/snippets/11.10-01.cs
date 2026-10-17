// Modern .NET, Chapter 11, Section 11.10
public class InvoiceController : ApiController
{
    public IHttpActionResult Post(InvoiceRequest request)
    {
        try
        {
            File.AppendAllText(@"C:\logs\app.log", DateTime.Now + " creating invoice for " + request.CustomerId + "\r\n");
            var invoice = _service.Create(request);
            File.AppendAllText(@"C:\logs\app.log", DateTime.Now + " done\r\n");
            return Ok(invoice);
        }
        catch (Exception ex)
        {
            File.AppendAllText(@"C:\logs\app.log", DateTime.Now + " ERROR " + ex + "\r\n");
            return InternalServerError();
        }
    }
}
