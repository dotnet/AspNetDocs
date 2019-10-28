public async Task<ActionResult> Index(string movieGenre, string searchString)
{
    var genres = new List<string>();

    var genreQry = from d in db.Movies
                   orderby d.Genre
                   select d.Genre;

    genres.AddRange(await genreQry.Distinct().ToListAsync());

    ViewBag.MovieGenre = new SelectList(genres);

    var movies = from m in db.Movies
                 select m;

    if (!string.IsNullOrEmpty(searchString))
    {
        movies = movies.Where(s => s.Title.Contains(searchString));
    }

    if (!string.IsNullOrEmpty(movieGenre))
    {
        movies = movies.Where(x => x.Genre == movieGenre);
    }

    return View(await movies.ToListAsync());
}