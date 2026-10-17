// Modern .NET, Chapter 8, Section 8.3
public class MvcApplication : HttpApplication
{
    protected void Application_Start()
    {
        AreaRegistration.RegisterAllAreas();
        GlobalConfiguration.Configure(WebApiConfig.Register);
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
        Database.SetInitializer<ShopContext>(null);
        Container = BuildUnityContainer();
    }

    protected void Application_Error() { /* log last error, redirect */ }
    protected void Session_Start() { Session["cart"] = new Cart(); }
}
