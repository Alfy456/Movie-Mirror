using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Data;
using Tms.Domain;
using Tms.Enum;
using Tms.Web.Models;

namespace Tms.Web.Controllers
{
    public class MovieController : Controller
    {
        MovieBL objMovieBL = new MovieBL();
        // GET: Movie
        public ActionResult Index()
        {
            List<MovieViewModel> lstMovieViewModel = new List<MovieViewModel>();
            foreach (var Movie in objMovieBL.GetAllMovies())
            {
                lstMovieViewModel.Add(new MovieViewModel
                {
                    Id = Movie.Id,
                    MovieId = Movie.MovieId,
                    Name = Movie.Name,
                    //Language = (Language)Enum.Parse(typeof(Language), Movie.Language),
                    Language=Language.Malayalam,
                OttPlatform = Movie.OttPlatform,
                    ReleaseDate = Movie.ReleaseDate,
                    TrailerDate = Movie.TrailerDate,
                    TeaserDate = Movie.TeaserDate,
                    OttReleaseDate = Movie.OttReleaseDate,
                    Image = Movie.Image,
                });
            }
            return View(lstMovieViewModel);
            
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MovieId,Name,Language,OttPlatform,ReleaseDate,TrailerDate,TeaserDate,OttReleaseDate,Image")] MovieViewModel p_objMovieViewModel)
        {
            Movie objNewMovie = new Movie
            {
                Id = p_objMovieViewModel.Id,
                MovieId = p_objMovieViewModel.MovieId,
                Name = p_objMovieViewModel.Name,
                Language = p_objMovieViewModel.Language.ToString(),
                OttPlatform = p_objMovieViewModel.OttPlatform,
                ReleaseDate = p_objMovieViewModel.ReleaseDate,
                TrailerDate = p_objMovieViewModel.TrailerDate,
                TeaserDate = p_objMovieViewModel.TeaserDate,
                OttReleaseDate = p_objMovieViewModel.OttReleaseDate,
                Image = p_objMovieViewModel.Image,
            };

            objMovieBL.CreateMovie(objNewMovie);
            return RedirectToAction("Index");

        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
