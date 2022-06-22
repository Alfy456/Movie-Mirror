using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Data;
using Tms.Domain;
using Tms.Web.Models;

namespace Tms.Web.Controllers
{
    public class MovieController1: Controller
    {
        MovieBL objMovieBL = new MovieBL();
        public ActionResult Index()
        {
           
            List<MovieViewModel> lstMovieViewModel = new List<MovieViewModel>();
            foreach (var Movie in objMovieBL.GetAllMovies())
            {
                lstMovieViewModel.Add(new MovieViewModel 
                {
                    Id=Movie.Id, 
                    MovieId =Movie.MovieId, 
                    Name =Movie.Name,
                    Language=Enum.Language.Malayalam,
                    OttPlatform=Movie.OttPlatform, 
                    ReleaseDate=Movie.ReleaseDate , 
                    TrailerDate=Movie.TrailerDate , 
                    TeaserDate =Movie.TeaserDate,
                    OttReleaseDate =Movie.OttReleaseDate,
                    Image=Movie.Image, 
                });
            }
            return View(lstMovieViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MovieId,Name,Language,OttPlatform,ReleaseDate,TrailerDate,TeaserDate,OttReleaseDate,Image")] MovieViewModel p_objMovieViewModel)
        {
            Movie objNewMovie = new Movie
            { 
                Id=p_objMovieViewModel.Id,
                MovieId=p_objMovieViewModel.MovieId,
                Name=p_objMovieViewModel.Name,
                Language=p_objMovieViewModel.Language.ToString(),
                OttPlatform=p_objMovieViewModel.OttPlatform,
                ReleaseDate=p_objMovieViewModel.ReleaseDate,
                TrailerDate=p_objMovieViewModel.TrailerDate,
                TeaserDate=p_objMovieViewModel.TeaserDate,
                OttReleaseDate=p_objMovieViewModel.OttReleaseDate,
                Image=p_objMovieViewModel.Image,
            };

            objMovieBL.CreateMovie(objNewMovie);
            return RedirectToAction("Index");
            
        }

    }
}