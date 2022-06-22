using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tms.Data;
using Tms.Domain;
using Tms.Web.Models;

namespace Tms.Web.Controllers
{
    public class TvShowController : Controller
    {
        TvShowBL objTvShowBL = new TvShowBL();
        // GET: TvShow
        public ActionResult Index()
        {
            List<TvShowViewModel> lstMovieViewModel = new List<TvShowViewModel>();
            foreach (var TvShow in objTvShowBL.GetAllTvShows())
            {
                lstMovieViewModel.Add(new TvShowViewModel
                {
                    Id = TvShow.Id,
                    TvShowId = TvShow.Id,
                    Name = TvShow.Name,
                    Language = Enum.Language.English.ToString(),
                    Status=TvShow.Status,
                    Premiered=TvShow.Premiered,
                    Ended=TvShow.Ended,
                    UpdatedWhen=TvShow.UpdatedWhen,
                    Image = TvShow.Image,
                });
            }
            return View(lstMovieViewModel);
        }

        // GET: TvShow/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TvShow objTvShow = objTvShowBL.GetTvShowModelById(id);

            TvShowViewModel objTvShowViewModel = new TvShowViewModel();

            objTvShowViewModel.Id = objTvShow.Id;
            objTvShowViewModel.TvShowId= objTvShow.Id;
            objTvShowViewModel.Name = objTvShow.Name;
            objTvShowViewModel.Language = objTvShow.Language;
            objTvShowViewModel.Status = objTvShow.Status;
            objTvShowViewModel.Premiered = objTvShow.Premiered;
            objTvShowViewModel.Ended = objTvShow.Ended;
            objTvShowViewModel.UpdatedWhen = objTvShow.UpdatedWhen;
            objTvShowViewModel.Image = objTvShow.Image;

            if (objTvShowViewModel == null)
            {
                return HttpNotFound();
            }
            return View(objTvShowViewModel);
        }

        // GET: TvShow/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TvShow/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TvShowId,Name,Language,Status,Premiered,Ended,UpdatedWhen,Image")] TvShowViewModel tvShowViewModel)
        {
            if (ModelState.IsValid)
            {
                TvShow objTvShow = new TvShow();

                objTvShow.Id = tvShowViewModel.Id;
                objTvShow.Id = tvShowViewModel.Id;
                objTvShow.Name = tvShowViewModel.Name;
                objTvShow.Language = tvShowViewModel.Language;
                objTvShow.Status = tvShowViewModel.Status;
                objTvShow.Premiered = tvShowViewModel.Premiered;
                objTvShow.Ended = tvShowViewModel.Ended;
                objTvShow.UpdatedWhen = tvShowViewModel.UpdatedWhen;
                objTvShow.Image = tvShowViewModel.Image;

                objTvShowBL.AddTvShow(objTvShow);

                return RedirectToAction("Index");
            }

            return View(tvShowViewModel);
        }

        // GET: TvShow/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TvShow objTvShow = objTvShowBL.GetTvShowModelById(id);

            TvShowViewModel objTvShowViewModel = new TvShowViewModel();

            objTvShowViewModel.Id = objTvShow.Id;
            objTvShowViewModel.TvShowId = objTvShow.Id;
            objTvShowViewModel.Name = objTvShow.Name;
            objTvShowViewModel.Language = objTvShow.Language;
            objTvShowViewModel.Status = objTvShow.Status;
            objTvShowViewModel.Premiered = objTvShow.Premiered;
            objTvShowViewModel.Ended = objTvShow.Ended;
            objTvShowViewModel.UpdatedWhen = objTvShow.UpdatedWhen;
            objTvShowViewModel.Image = objTvShow.Image;


            if (objTvShowViewModel == null)
            {
                return HttpNotFound();
            }
            return View(objTvShowViewModel);
        }

        // POST: TvShow/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TvShowId,Name,Language,Status,Premiered,Ended,UpdatedWhen,Image")] TvShowViewModel tvShowViewModel)
        {
            if (ModelState.IsValid)
            {
                TvShow objTvShow = new TvShow();

                objTvShow.Id = tvShowViewModel.Id;
                objTvShow.TvShowId = tvShowViewModel.TvShowId;
                objTvShow.Name = tvShowViewModel.Name;
                objTvShow.Language = tvShowViewModel.Language;
                objTvShow.Status = tvShowViewModel.Status;
                objTvShow.Premiered = tvShowViewModel.Premiered;
                objTvShow.Ended = tvShowViewModel.Ended;
                objTvShow.UpdatedWhen = tvShowViewModel.UpdatedWhen;
                objTvShow.Image = tvShowViewModel.Image;
                objTvShowBL.EditTvShow(objTvShow);
               
                return RedirectToAction("Index");
            }
            return View(tvShowViewModel);
        }

        // GET: TvShow/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TvShow objTvShow = objTvShowBL.GetTvShowModelById(id);

            TvShowViewModel objTvShowViewModel = new TvShowViewModel();

            objTvShowViewModel.Id = objTvShow.Id;
            objTvShowViewModel.TvShowId = objTvShow.Id;
            objTvShowViewModel.Name = objTvShow.Name;
            objTvShowViewModel.Language = objTvShow.Language;
            objTvShowViewModel.Status = objTvShow.Status;
            objTvShowViewModel.Premiered = objTvShow.Premiered;
            objTvShowViewModel.Ended = objTvShow.Ended;
            objTvShowViewModel.UpdatedWhen = objTvShow.UpdatedWhen;
            objTvShowViewModel.Image = objTvShow.Image;

            if (objTvShowViewModel == null)
            {
                return HttpNotFound();
            }

            return View(objTvShowViewModel);
        }

        // POST: TvShow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TvShow objTvShow = objTvShowBL.GetTvShowModelById(id);
            objTvShowBL.RemoveTvShow(objTvShow);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateTvShows()
        {

            objTvShowBL.UpdateTvShowsBulk();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateTvShowsInformation()
        {
            objTvShowBL.UpdateTvShowsInformationBulk();
            return RedirectToAction("Index");

        }
        public ActionResult Anniversary()
        {
            var dtTomorrow = DateTime.Now.AddDays(0);
            var tvshows = objTvShowBL.GetAnniversary(dtTomorrow);
            List<AnniversaryViewModel> lstAnniversary = new List<AnniversaryViewModel>();

            foreach (var tvshow in tvshows)
            {
                int intAge = (dtTomorrow.Year - tvshow.Premiered.Value.Year);

                if (intAge > 0 && tvshow.Language != "Russian")
                {
                    lstAnniversary.Add(new AnniversaryViewModel { Name = tvshow.Name, Premiered = tvshow.Premiered, Age = intAge, ImageLink = tvshow.Image });
                }
            }

            return View(lstAnniversary);
        }

    }


}
