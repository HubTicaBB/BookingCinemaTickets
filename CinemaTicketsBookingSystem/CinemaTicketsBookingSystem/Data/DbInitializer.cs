using CinemaTicketsBookingSystem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketsBookingSystem.Data
{
    public class DbInitializer
    {
        public static void Seed(ApplicationDbContext db)
        {
            db.Database.EnsureCreated();

            SeedGenres(db);
            SeedMovies(db);
            SeedCinemaHalls(db);
        }

        private static void SeedCinemaHalls(ApplicationDbContext db)
        {
            if (db.CinemaHalls.Any()) return;

            var cinemaHalls = new CinemaHall[]
            {
                new CinemaHall { Name = "Nickelodeon", SeatingCapacity = 50 },
                new CinemaHall { Name = "Le Gamaar Cinema", SeatingCapacity = 100 }
            };

            foreach (var cinemaHall in cinemaHalls)
            {
                db.CinemaHalls.Add(cinemaHall);
            }
            db.SaveChanges();
        }

        private static void SeedMovies(ApplicationDbContext db)
        {
            if (db.Movies.Any()) return;

            var movies = new Movie[]
            {
                new Movie
                {
                    Title = "The Shawshank Redemption",
                    ReleaseYear = 1994,
                    Duration = TimeSpan.Parse("2:22"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Drama").Id,
                    Plot = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    Director = "Frank Darabont",
                    PosterUrl = "~/img/posters/TheShawshankRedemption.jpg"
                },
                new Movie
                {
                    Title = "The Godfather",
                    ReleaseYear = 1972,
                    Duration = TimeSpan.Parse("2:55"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Crime").Id,
                    Plot = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                    Director = "Francis Ford Coppola",
                    PosterUrl = "~/img/posters/TheGodfather.jpg"
                },
                new Movie
                {
                    Title = "The Godfather: Part II",
                    ReleaseYear = 1974,
                    Duration = TimeSpan.Parse("3:22"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Crime").Id,
                    Plot = "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.",
                    Director = "Francis Ford Coppola",
                    PosterUrl = "~/img/posters/TheGodfatherPartII.jpg"
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    ReleaseYear = 2008,
                    Duration = TimeSpan.Parse("2:32"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Action").Id,
                    Plot = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    Director = "Christopher Nolan",
                    PosterUrl = "~/img/posters/TheDarkKnight.jpg"
                },
                new Movie
                {
                    Title = "The Lord of the Rings: The Return of the King",
                    ReleaseYear = 1974,
                    Duration = TimeSpan.Parse("3:21"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Adventure").Id,
                    Plot = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    Director = "Peter Jackson",
                    PosterUrl = "~/img/posters/TheLordoftheRingsTheReturnoftheKing.jpg"
                },
                new Movie
                {
                    Title = "12 Angry Men",
                    ReleaseYear = 1957,
                    Duration = TimeSpan.Parse("1:36"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Drama").Id,
                    Plot = "A jury holdout attempts to prevent a miscarriage of justice by forcing his colleagues to reconsider the evidence.",
                    Director = "Sidney Lumet",
                    PosterUrl = "~/img/posters/12AngryMen.jpg"
                },
                new Movie
                {
                    Title = "Schindler's List",
                    ReleaseYear = 1993,
                    Duration = TimeSpan.Parse("3:15"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Biography").Id,
                    Plot = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
                    Director = "Steven Spielberg",
                    PosterUrl = "~/img/posters/ShindlersList.jpg"
                },
                new Movie
                {
                    Title = "Pulp Fiction",
                    ReleaseYear = 1993,
                    Duration = TimeSpan.Parse("2:34"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Crime").Id,
                    Plot = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    Director = "Quentin Tarantino",
                    PosterUrl = "~/img/posters/PulpFiction.jpg"
                },
                new Movie
                {
                    Title = "Fight Club",
                    ReleaseYear = 1999,
                    Duration = TimeSpan.Parse("2:19"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Drama").Id,
                    Plot = "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.",
                    Director = "David Fincher",
                    PosterUrl = "~/img/posters/FightClub.jpg"
                },
                new Movie
                {
                    Title = "Inglourious Basterds",
                    ReleaseYear = 2009,
                    Duration = TimeSpan.Parse("2:33"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Adventure").Id,
                    Plot = "In Nazi-occupied France during World War II, a plan to assassinate Nazi leaders by a group of Jewish U.S. soldiers coincides with a theatre owner's vengeful plans for the same.",
                    Director = "Quentin Tarantino",
                    PosterUrl = "~/img/posters/InglouriousBasterds.jpg"
                }
            };

            foreach (var movie in movies)
            {
                db.Movies.Add(movie);
            }
            db.SaveChanges();
        }

        private static void SeedGenres(ApplicationDbContext db)
        {
            if (db.Genres.Any()) return;

            var genres = new Genre[]
            {
                new Genre { Name = "Action" },
                new Genre { Name = "Adventure" },
                new Genre { Name = "Animation" },
                new Genre { Name = "Biography" },
                new Genre { Name = "Comedy" },
                new Genre { Name = "Crime" },
                new Genre { Name = "Documentary" },
                new Genre { Name = "Drama" },
                new Genre { Name = "Family" },
                new Genre { Name = "Fantasy" },
                new Genre { Name = "Film Noir" },
                new Genre { Name = "History" },
                new Genre { Name = "Horror" },
                new Genre { Name = "Music" },
                new Genre { Name = "Musical" },
                new Genre { Name = "Mystery" },
                new Genre { Name = "Romance" },
                new Genre { Name = "Sci-Fi" },
                new Genre { Name = "Short" },
                new Genre { Name = "Sport" },
                new Genre { Name = "Superhero" },
                new Genre { Name = "Thriller" },
                new Genre { Name = "War" },
                new Genre { Name = "Western" }
            };

            foreach (var genre in genres)
            {
                db.Genres.Add(genre);
            }
            db.SaveChanges();
        }
    }
}
