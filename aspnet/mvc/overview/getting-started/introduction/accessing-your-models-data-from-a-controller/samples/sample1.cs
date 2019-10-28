public class MoviesController : Controller
{
    private MovieDBContext db = new MovieDBContext();

    // GET: /Movies/
    public async Task<ActionResult> Index()
    {
        return View(await db.Movies.ToListAsync());
    }