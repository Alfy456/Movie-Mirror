using PagedList;
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
        CommonBL objCommonBL = new CommonBL();
        // GET: TvShow
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
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

            List<TvShowViewModel> lstTvShowViewModel = new List<TvShowViewModel>();
            foreach (var TvShow in objTvShowBL.GetAllTvShows())
            {
                lstTvShowViewModel.Add(new TvShowViewModel
                {
                    Id = TvShow.Id,
                    TvShowId = TvShow.Id,
                    Name = TvShow.Name,
                    Language = objCommonBL.GetEnumLanguageFromString(TvShow.Language),
                    Status= objCommonBL.GetEnumStatusFromString(TvShow.Status),
                    Premiered=TvShow.Premiered,
                    Ended=TvShow.Ended,
                    UpdatedWhen=TvShow.UpdatedWhen,
                    Image = TvShow.Image,
                });
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                lstTvShowViewModel = lstTvShowViewModel.Where(a => a.Name==searchString).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lstTvShowViewModel = lstTvShowViewModel.OrderByDescending(a => a.Name).ToList();
                    break;
                default:
                    lstTvShowViewModel = lstTvShowViewModel.OrderByDescending(a => a.Name).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(lstTvShowViewModel.ToPagedList(pageNumber, pageSize));

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
            objTvShowViewModel.Language = objCommonBL.GetEnumLanguageFromString(objTvShow.Language);
            objTvShowViewModel.Status = objCommonBL.GetEnumStatusFromString(objTvShow.Status);
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

                objTvShow.Name = tvShowViewModel.Name;
                objTvShow.Language = nameof(tvShowViewModel.Language);
                objTvShow.Status = nameof(tvShowViewModel.Status);
                objTvShow.Premiered = tvShowViewModel.Premiered;
                objTvShow.Ended = tvShowViewModel.Ended;
                objTvShow.UpdatedWhen = tvShowViewModel.UpdatedWhen;
                objTvShow.OttPlatform = nameof(tvShowViewModel.OttPlatform);
                objTvShow.NewSeasonReleaseDate = tvShowViewModel.NewSeasonReleaseDate;
                objTvShow.TrailerDate = tvShowViewModel.TrailerDate;
                objTvShow.CreatedDate = DateTime.Now;
                objTvShow.CreatedBy = "Manual Addition";
                objTvShow.IsManualAddition = true;
                
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
            objTvShowViewModel.Language = objCommonBL.GetEnumLanguageFromString(objTvShow.Language);
            objTvShowViewModel.Status = objCommonBL.GetEnumStatusFromString(objTvShow.Status);
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
        public ActionResult Edit([Bind(Include = "Id,TvShowId,Name,Language,Status,Premiered,Ended,UpdatedWhen,Image,NewSeasonReleaseDate")] TvShowViewModel tvShowViewModel)
        {
            if (ModelState.IsValid)
            {
                TvShow objTvShow = new TvShow();

                objTvShow.Id = tvShowViewModel.Id;
                objTvShow.TvShowId = tvShowViewModel.TvShowId;
                objTvShow.Name = tvShowViewModel.Name;
                objTvShow.Language = nameof(tvShowViewModel.Language);
                objTvShow.Status = nameof(tvShowViewModel.Status);
                objTvShow.Premiered = tvShowViewModel.Premiered;
                objTvShow.Ended = tvShowViewModel.Ended;
                objTvShow.UpdatedWhen = tvShowViewModel.UpdatedWhen;
                objTvShow.Image = tvShowViewModel.Image;
                objTvShow.NewSeasonReleaseDate = tvShowViewModel.NewSeasonReleaseDate;
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
            objTvShowViewModel.Language = objCommonBL.GetEnumLanguageFromString(objTvShow.Language);
            objTvShowViewModel.Status = objCommonBL.GetEnumStatusFromString(objTvShow.Status);
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
        public ActionResult Anniversary(string SelectedDate=null)
        {
            
            var dtToday = SelectedDate=="Today" ? DateTime.Now.AddDays(0) : DateTime.Now.AddDays(1);

            List<AnniversaryViewModel> lstAnniversary = new List<AnniversaryViewModel>();

            foreach (var tvshow in objTvShowBL.GetAnniversary(dtToday))
            {
                int intAge = (dtToday.Year - tvshow.Premiered.Value.Year);

                if (intAge > 0 && tvshow.Language != "Russian")
                {
                    lstAnniversary.Add(new AnniversaryViewModel { Name = tvshow.Name, Premiered = tvshow.Premiered, Age = intAge, ImageLink = tvshow.Image });
                }
            }

            return View(lstAnniversary);
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            Prefix = Prefix.ToLower().Trim();
            List<TvShow> lstTvShows = objTvShowBL.GetAllTvShows();
            var Name = lstTvShows.Where(m => m.Name?.ToLower()?.StartsWith(Prefix) == true).Select(n => new { n.Name });
            return Json(Name, JsonRequestBehavior.AllowGet);
        }
    }
}