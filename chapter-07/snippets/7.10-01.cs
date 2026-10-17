// Modern .NET, Chapter 7, Section 7.10
public class ShipmentTracker
{
    public ShipmentStatus Track(string trackingNumber)
    {
        try
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", "Bearer " + ConfigurationManager.AppSettings["CarrierToken"]);
            string json = client.DownloadString(
                ConfigurationManager.AppSettings["CarrierUrl"] + "/track/" + trackingNumber);
            return JsonConvert.DeserializeObject<ShipmentStatus>(json);
        }
        catch (Exception ex)
        {
            Logger.Error(ex.ToString());
            return null;
        }
    }
}
