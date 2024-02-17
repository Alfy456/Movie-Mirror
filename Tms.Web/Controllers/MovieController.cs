using PagedList;
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
        CommonBL objCommonBL = new CommonBL();
        // GET: Movie
        public ActionResult Index(string sortOrder, string currentFilter, string searchString,int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<MovieViewModel> lstMovieViewModel = new List<MovieViewModel>();
            foreach (var Movie in objMovieBL.GetAllMovies())
            {
                lstMovieViewModel.Add(new MovieViewModel
                {
                    Id = Movie.Id,
                    MovieId = Movie.MovieId,
                    Name = Movie.Name,
                    Language = objMovieBL.GetEnumLanguageFromString( Movie.Language),
                    OttPlatform =objMovieBL.GetEnumOttFromString(Movie.OttPlatform),
                    IsUploadedToTelegram = Movie.IsUploadedToTelegram.Value,
                    ReleaseDate = Movie.ReleaseDate,
                    TrailerDate = Movie.TrailerDate,
                    TeaserDate = Movie.TeaserDate,
                    OttReleaseDate = Movie.OttReleaseDate,
                    Image = Movie.Image,
                });
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                objCommonBL.test();
                lstMovieViewModel = lstMovieViewModel.Where(movies => movies.Name==searchString).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lstMovieViewModel = lstMovieViewModel.OrderByDescending(movies => movies.ReleaseDate).ToList();
                    break;
                default:
                    lstMovieViewModel = lstMovieViewModel.OrderByDescending(movies => movies.ReleaseDate).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(lstMovieViewModel.ToPagedList(pageNumber, pageSize));
            
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
                OttPlatform = p_objMovieViewModel.OttPlatform.ToString(),
                ReleaseDate = p_objMovieViewModel.ReleaseDate,
                IsUploadedToTelegram= p_objMovieViewModel.IsUploadedToTelegram,
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
            
            MovieViewModel objMovieViewModel = new MovieViewModel
            {
                Id= objMovieBL.GetMovieById(id).Id,
                MovieId= objMovieBL.GetMovieById(id).MovieId,
                Name= objMovieBL.GetMovieById(id).Name,
                Language= objMovieBL.GetEnumLanguageFromString(objMovieBL.GetMovieById(id).Language),
                OttPlatform= objMovieBL.GetEnumOttFromString(objMovieBL.GetMovieById(id).OttPlatform),
                IsUploadedToTelegram = objMovieBL.GetMovieById(id).IsUploadedToTelegram.Value,
                ReleaseDate = objMovieBL.GetMovieById(id).ReleaseDate,
                TrailerDate= objMovieBL.GetMovieById(id).TrailerDate,
                TeaserDate= objMovieBL.GetMovieById(id).TeaserDate,
                OttReleaseDate= objMovieBL.GetMovieById(id).OttReleaseDate,
                Image= objMovieBL.GetMovieById(id).Image
            };

            return View(objMovieViewModel);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,MovieId,Name,Language,OttPlatform,ReleaseDate,TrailerDate,TeaserDate,OttReleaseDate,Image")] MovieViewModel p_objMovieViewModel)
        {
            try
            {
                Movie objNewMovie = new Movie
                {
                    Id = p_objMovieViewModel.Id,
                    MovieId = p_objMovieViewModel.MovieId,
                    Name = p_objMovieViewModel.Name,
                    Language = p_objMovieViewModel.Language.ToString(),
                    OttPlatform = p_objMovieViewModel.OttPlatform.ToString(),
                    IsUploadedToTelegram = p_objMovieViewModel.IsUploadedToTelegram,
                    ReleaseDate = p_objMovieViewModel.ReleaseDate,
                    TrailerDate = p_objMovieViewModel.TrailerDate,
                    TeaserDate = p_objMovieViewModel.TeaserDate,
                    OttReleaseDate = p_objMovieViewModel.OttReleaseDate,
                    Image = p_objMovieViewModel.Image,
                };

                objMovieBL.EditeMovie(objNewMovie);

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
        public ActionResult Release(bool IsAReleaseToday = false)
        {
            DateTime dtDate = IsAReleaseToday ? DateTime.Now.AddDays(0) : DateTime.Now.AddDays(1);

            List<ReleaseViewModel> lstRelease = new List<ReleaseViewModel>();

            foreach (var movie in objMovieBL.GetReleaseData(dtDate))
            {
                var Date= objMovieBL.GetDate(movie);
                string strPlatform = movie.OttPlatform == null || movie.OttPlatform == nameof(Ott.Unknown) ? nameof(Ott.Theatre) : movie.OttPlatform;
                lstRelease.Add(new ReleaseViewModel {Id=movie.Id, Name = movie.Name,Language=nameof(movie.Language),ReleaseDate= Date ,Platform= strPlatform });
            }

            return View(lstRelease);
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            Prefix = Prefix.ToLower().Trim();
            List<Movie> lstMovies = objMovieBL.GetAllMovies();
            var Name = lstMovies.Where(m => m.Name?.ToLower()?.StartsWith(Prefix)== true).Select(n=>new { n.Name});
            return Json(Name, JsonRequestBehavior.AllowGet);
        }

        public void MovieBulkUpload()
        {
            objMovieBL.MovieBulkUpload();
        }
    }
}
