using Castle.Core.Resource;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using CinemaTicketsBookingSystem.Models;
using CinemaTicketsBookingSystem.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketsBookingSystem.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var db = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            Seed(db);
        }

        public static void Seed(ApplicationDbContext db)
        {
            //db.Database.EnsureDeleted();
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
                    Plot = "Chronicles the experiences of a formerly successful banker as a prisoner in the gloomy jailhouse of Shawshank after being found guilty of a crime he did not commit. The film portrays the man's unique way of dealing with his new, torturous life; along the way he befriends a number of fellow prisoners, most notably a wise long-term inmate named Red.",
                    Director = "Frank Darabont",
                    PosterUrl = "~/img/posters/TheShawshankRedemption.jpg"
                },
                new Movie
                {
                    Title = "The Godfather",
                    ReleaseYear = 1972,
                    Duration = TimeSpan.Parse("2:55"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Crime").Id,
                    Plot = "The Godfather \"Don\" Vito Corleone is the head of the Corleone mafia family in New York. He is at the event of his daughter's wedding. Michael, Vito's youngest son and a decorated WW II Marine is also present at the wedding. Michael seems to be uninterested in being a part of the family business. Vito is a powerful man, and is kind to all those who give him respect but is ruthless against those who do not. But when a powerful and treacherous rival wants to sell drugs and needs the Don's influence for the same, Vito refuses to do it. What follows is a clash between Vito's fading old values and the new ways which may cause Michael to do the thing he was most reluctant in doing and wage a mob war against all the other mafia families which could tear the Corleone family apart.",
                    Director = "Francis Ford Coppola",
                    PosterUrl = "~/img/posters/TheGodfather.jpg"
                },
                new Movie
                {
                    Title = "The Godfather: Part II",
                    ReleaseYear = 1974,
                    Duration = TimeSpan.Parse("3:22"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Crime").Id,
                    Plot = "The continuing saga of the Corleone crime family tells the story of a young Vito Corleone growing up in Sicily and in 1910s New York; and follows Michael Corleone in the 1950s as he attempts to expand the family business into Las Vegas, Hollywood and Cuba.",
                    Director = "Francis Ford Coppola",
                    PosterUrl = "~/img/posters/TheGodfatherPartII.jpg"
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    ReleaseYear = 2008,
                    Duration = TimeSpan.Parse("2:32"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Action").Id,
                    Plot = "Set within a year after the events of Batman Begins (2005), Batman, Lieutenant James Gordon, and new District Attorney Harvey Dent successfully begin to round up the criminals that plague Gotham City, until a mysterious and sadistic criminal mastermind known only as \"The Joker\" appears in Gotham, creating a new wave of chaos. Batman's struggle against The Joker becomes deeply personal, forcing him to \"confront everything he believes\" and improve his technology to stop him. A love triangle develops between Bruce Wayne, Dent, and Rachel Dawes.",
                    Director = "Christopher Nolan",
                    PosterUrl = "~/img/posters/TheDarkKnight.jpg"
                },
                new Movie
                {
                    Title = "The Lord of the Rings: The Return of the King",
                    ReleaseYear = 1974,
                    Duration = TimeSpan.Parse("3:21"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Adventure").Id,
                    Plot = "Set within a year after the events of Batman Begins (2005), Batman, Lieutenant James Gordon, and new District Attorney Harvey Dent successfully begin to round up the criminals that plague Gotham City, until a mysterious and sadistic criminal mastermind known only as \"The Joker\" appears in Gotham, creating a new wave of chaos. Batman's struggle against The Joker becomes deeply personal, forcing him to \"confront everything he believes\" and improve his technology to stop him. A love triangle develops between Bruce Wayne, Dent, and Rachel Dawes.",
                    Director = "Peter Jackson",
                    PosterUrl = "~/img/posters/TheLordoftheRingsTheReturnoftheKing.jpg"
                },
                new Movie
                {
                    Title = "12 Angry Men",
                    ReleaseYear = 1957,
                    Duration = TimeSpan.Parse("1:36"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Drama").Id,
                    Plot = "The defense and the prosecution have rested and the jury is filing into the jury room to decide if a young man is guilty or innocent of murdering his father. What begins as an open-and-shut case of murder soon becomes a detective story that presents a succession of clues creating doubt, and a mini-drama of each of the jurors' prejudices and preconceptions about the trial, the accused, and each other. Based on the play, all of the action takes place on the stage of the jury room.",
                    Director = "Sidney Lumet",
                    PosterUrl = "~/img/posters/12AngryMen.jpg"
                },
                new Movie
                {
                    Title = "Schindler's List",
                    ReleaseYear = 1993,
                    Duration = TimeSpan.Parse("3:15"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Biography").Id,
                    Plot = "Oskar Schindler is a vain and greedy German businessman who becomes an unlikely humanitarian amid the barbaric German Nazi reign when he feels compelled to turn his factory into a refuge for Jews. Based on the true story of Oskar Schindler who managed to save about 1100 Jews from being gassed at the Auschwitz concentration camp, it is a testament to the good in all of us.",
                    Director = "Steven Spielberg",
                    PosterUrl = "~/img/posters/ShindlersList.jpg"
                },
                new Movie
                {
                    Title = "Pulp Fiction",
                    ReleaseYear = 1993,
                    Duration = TimeSpan.Parse("2:34"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Crime").Id,
                    Plot = "Jules Winnfield (Samuel L. Jackson) and Vincent Vega (John Travolta) are two hit men who are out to retrieve a suitcase stolen from their employer, mob boss Marsellus Wallace (Ving Rhames). Wallace has also asked Vincent to take his wife Mia (Uma Thurman) out a few days later when Wallace himself will be out of town. Butch Coolidge (Bruce Willis) is an aging boxer who is paid by Wallace to lose his fight. The lives of these seemingly unrelated people are woven together comprising of a series of funny, bizarre and uncalled-for incidents.",
                    Director = "Quentin Tarantino",
                    PosterUrl = "~/img/posters/PulpFiction.jpg"
                },
                new Movie
                {
                    Title = "Fight Club",
                    ReleaseYear = 1999,
                    Duration = TimeSpan.Parse("2:19"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Drama").Id,
                    Plot = "A nameless first person narrator (Edward Norton) attends support groups in attempt to subdue his emotional state and relieve his insomniac state. When he meets Marla (Helena Bonham Carter), another fake attendee of support groups, his life seems to become a little more bearable. However when he associates himself with Tyler (Brad Pitt) he is dragged into an underground fight club and soap making scheme. Together the two men spiral out of control and engage in competitive rivalry for love and power. When the narrator is exposed to the hidden agenda of Tyler's fight club, he must accept the awful truth that Tyler may not be who he says he is.",
                    Director = "David Fincher",
                    PosterUrl = "~/img/posters/FightClub.jpg"
                },
                new Movie
                {
                    Title = "Inglourious Basterds",
                    ReleaseYear = 2009,
                    Duration = TimeSpan.Parse("2:33"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Adventure").Id,
                    Plot = "In German-occupied France, young Jewish refugee Shosanna Dreyfus witnesses the slaughter of her family by Colonel Hans Landa. Narrowly escaping with her life, she plots her revenge several years later when German war hero Fredrick Zoller takes a rapid interest in her and arranges an illustrious movie premiere at the theater she now runs. With the promise of every major Nazi officer in attendance, the event catches the attention of the \"Basterds\", a group of Jewish-American guerrilla soldiers led by the ruthless Lt. Aldo Raine. As the relentless executioners advance and the conspiring young girl's plans are set in motion, their paths will cross for a fateful evening that will shake the very annals of history.",
                    Director = "Quentin Tarantino",
                    PosterUrl = "~/img/posters/InglouriousBasterds.jpg"
                },
                new Movie
                {
                    Title = "Inception",
                    ReleaseYear = 2010,
                    Duration = TimeSpan.Parse("2:28"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Action").Id,
                    Plot = "Dom Cobb is a skilled thief, the absolute best in the dangerous art of extraction, stealing valuable secrets from deep within the subconscious during the dream state, when the mind is at its most vulnerable. Cobb's rare ability has made him a coveted player in this treacherous new world of corporate espionage, but it has also made him an international fugitive and cost him everything he has ever loved. Now Cobb is being offered a chance at redemption. One last job could give him his life back but only if he can accomplish the impossible, inception. Instead of the perfect heist, Cobb and his team of specialists have to pull off the reverse: their task is not to steal an idea, but to plant one. If they succeed, it could be the perfect crime. But no amount of careful planning or expertise can prepare the team for the dangerous enemy that seems to predict their every move. An enemy that only Cobb could have seen coming.",
                    Director = "Christopher Nolan",
                    PosterUrl = "~/img/posters/Inception.jpg"
                },
                new Movie
                {
                    Title = "The Lord of the Rings: The Fellowship of the Ring",
                    ReleaseYear = 2001,
                    Duration = TimeSpan.Parse("2:58"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Action").Id,
                    Plot = "An ancient Ring thought lost for centuries has been found, and through a strange twist of fate has been given to a small Hobbit named Frodo. When Gandalf discovers the Ring is in fact the One Ring of the Dark Lord Sauron, Frodo must make an epic quest to the Cracks of Doom in order to destroy it. However, he does not go alone. He is joined by Gandalf, Legolas the elf, Gimli the Dwarf, Aragorn, Boromir, and his three Hobbit friends Merry, Pippin, and Samwise. Through mountains, snow, darkness, forests, rivers and plains, facing evil and danger at every corner the Fellowship of the Ring must go. Their quest to destroy the One Ring is the only hope for the end of the Dark Lords reign.",
                    Director = "Peter Jackson",
                    PosterUrl = "~/img/posters/TheLordoftheRingsTheFellowshipoftheRing.jpg"
                },
                new Movie
                {
                    Title = "Forrest Gump",
                    ReleaseYear = 1994,
                    Duration = TimeSpan.Parse("2:22"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Drama").Id,
                    Plot = "Forrest Gump is a simple man with a low I.Q. but good intentions. He is running through childhood with his best and only friend Jenny. His 'mama' teaches him the ways of life and leaves him to choose his destiny. Forrest joins the army for service in Vietnam, finding new friends called Dan and Bubba, he wins medals, creates a famous shrimp fishing fleet, inspires people to jog, starts a ping-pong craze, creates the smiley, writes bumper stickers and songs, donates to people and meets the president several times. However, this is all irrelevant to Forrest who can only think of his childhood sweetheart Jenny Curran, who has messed up her life. Although in the end all he wants to prove is that anyone can love anyone.",
                    Director = "Robert Zemeckis",
                    PosterUrl = "~/img/posters/ForrestGump.jpg"
                },
                new Movie
                {
                    Title = "Il buono, il brutto, il cattivo",
                    ReleaseYear = 1966,
                    Duration = TimeSpan.Parse("2:41"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Western").Id,
                    Plot = "Blondie (The Good) (Clint Eastwood) is a professional gunslinger who is out trying to earn a few dollars. Angel Eyes (The Bad) (Lee Van Cleef) is a hitman who always commits to a task and sees it through, as long as he is paid to do so. And Tuco (The Ugly) (Eli Wallach) is a wanted outlaw trying to take care of his own hide. Tuco and Blondie share a partnership together making money off of Tuco's bounty, but when Blondie unties the partnership, Tuco tries to hunt down Blondie. When Blondie and Tuco come across a horse carriage loaded with dead bodies, they soon learn from the only survivor, Bill Carson (Antonio Casale), that he and a few other men have buried a stash of gold in a cemetery. Unfortunately, Carson dies and Tuco only finds out the name of the cemetery, while Blondie finds out the name on the grave. Now the two must keep each other alive in order to find the gold. Angel Eyes (who had been looking for Bill Carson) discovers that Tuco and Blondie met with Carson and knows ...",
                    Director = "Sergio Leone",
                    PosterUrl = "~/img/posters/Ilbuonoilbruttoilcattivo.jpg"
                },
                new Movie
                {
                    Title = "Matrix",
                    ReleaseYear = 1999,
                    Duration = TimeSpan.Parse("2:16"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Sci-Fi").Id,
                    Plot = "Thomas A. Anderson is a man living two lives. By day he is an average computer programmer and by night a hacker known as Neo. Neo has always questioned his reality, but the truth is far beyond his imagination. Neo finds himself targeted by the police when he is contacted by Morpheus, a legendary computer hacker branded a terrorist by the government. Morpheus awakens Neo to the real world, a ravaged wasteland where most of humanity have been captured by a race of machines that live off of the humans' body heat and electrochemical energy and who imprison their minds within an artificial reality known as the Matrix. As a rebel against the machines, Neo must return to the Matrix and confront the agents: super-powerful computer programs devoted to snuffing out Neo and the entire human rebellion.",
                    Director = "The Wachowski Brothers",
                    PosterUrl = "~/img/posters/Matrix.jpg"
                },
                new Movie
                {
                    Title = "Goodfellas",
                    ReleaseYear = 1990,
                    Duration = TimeSpan.Parse("2:26"),
                    GenreId = db.Genres.FirstOrDefault(g => g.Name == "Biography").Id,
                    Plot = "Henry Hill might be a small time gangster, who may have taken part in a robbery with Jimmy Conway and Tommy De Vito, two other gangsters who might have set their sights a bit higher. His two partners could kill off everyone else involved in the robbery, and slowly start to think about climbing up through the hierarchy of the Mob. Henry, however, might be badly affected by his partners' success, but will he consider stooping low enough to bring about the downfall of Jimmy and Tommy?",
                    Director = "Martin Scorsese",
                    PosterUrl = "~/img/posters/Goodfellas.jpg"
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
