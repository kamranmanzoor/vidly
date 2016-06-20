using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        #region Private Members
        private ApplicationDbContext _context;
        #endregion

        #region Constructor
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        #region Index Action
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }
        #endregion

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>()
            {
                new Movie(){ Duration = "80",Id = 1, Name = "Shrek"},
                new Movie(){ Duration = "60",Id = 2, Name = "Harry Potter"}
            };
        }


        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            return View(movie);
        }


        public ActionResult New()
        {
            var genre = _context.Genres.ToList();
            var movieForm = new MovieFormViewModel
            {
                Genre = genre
            };

            return View("MovieForm", movieForm);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var movieForm = new MovieFormViewModel(movie)
            {
                Genre = _context.Genres.ToList(),
            };


            return View("MovieForm", movieForm);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var movieForm = new MovieFormViewModel(movie)
                {
                    Genre = _context.Genres.ToList()
                };

                return View("MovieForm", movieForm);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");

        }


        #region GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Smurfs" };

            // Create ViewData dictionary object
            //ViewData["Movie"] = movie;

            var customers = new List<Customer>
            {
                new Customer {Name = "Customer1"},
                new Customer {Name = "Customer2"}
            };

            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);



        }
        #endregion


        #region Index Old
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;
        //    if (string.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";
        //    return Content(String.Format("pageIndex ={0} sorty by = {1}", pageIndex, sortBy));

        //} 
        #endregion

        #region Route Example
        //[Route("movies/released/{year:regex(\\d{4})}/{month?}")]
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int? month)
        {
            if (!month.HasValue)
                month = 10;
            return Content(year + "/" + month);
        }
        #endregion
    }

}