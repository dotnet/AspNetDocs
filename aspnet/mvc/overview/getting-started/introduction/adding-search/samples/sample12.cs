var genreQry = from d in db.Movies
               orderby d.Genre
               select d.Genre;