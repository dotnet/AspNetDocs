public async Task<ActionResult> Index(string searchString) 
{
    var movies = from m in db.Movies
                 select m;

    if (!String.IsNullOrEmpty(searchString)) 
    { 
        movies = movies.Where(s => s.Title.Contains(searchString)); 
    }

    return View(await movies.ToListAsync());
}