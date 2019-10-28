public ActionResult Index()
{
    return View(await db.Movies.ToListAsync());
}