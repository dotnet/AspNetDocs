public async Task<ActionResult> Details(int? id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }
    Movie movie = await db.Movies.FindAsync(id);
    if (movie == null)
    {
        return HttpNotFound();
    }
    return View(movie);
}