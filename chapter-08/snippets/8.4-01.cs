// Modern .NET, Chapter 8, Section 8.4
public class ProductsController : Controller
{
    private readonly ShopContext _db = new ShopContext();                  // new'd up

    public ActionResult Index(string q)
    {
        var products = _db.Products.Where(p => q == null || p.Name.Contains(q)).ToList();
        ViewBag.Query = q;
        return View(products);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Product model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Products.Add(model);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
        var product = _db.Products.Find(id);
        if (product == null) return HttpNotFound();
        return View(product);
    }
}
