// GET: Movies/Delete/5
public async Task<ActionResult> Delete(int? id)
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

// POST: Movies/Delete/5
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<ActionResult> DeleteConfirmed(int id)
{
    Movie movie = await db.Movies.FindAsync(id);
    db.Movies.Remove(movie);
    await db.SaveChangesAsync();
    return RedirectToAction("Index");
}