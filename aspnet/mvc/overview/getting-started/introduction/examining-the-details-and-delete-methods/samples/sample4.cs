public async Task<ActionResult> Delete(FormCollection fcNotUsed, int id = 0)
{
    Movie movie = await db.Movies.FindAsync(id);
    if (movie == null)
    {
        return HttpNotFound();
    }
    db.Movies.Remove(movie);
    await db.SaveChangesAsync();
    return RedirectToAction("Index");
}