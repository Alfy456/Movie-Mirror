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
    public class ActorController : Controller
    {
        ActorBL objActorBL = new ActorBL();

        // GET: Actor
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


            List<ActorViewModel> lstActoriewModel = new List<ActorViewModel>();
            foreach (var Actor in objActorBL.GetAllActors())
            {
                lstActoriewModel.Add(new ActorViewModel
                {
                    Id = Actor.Id,
                    ActorId = Actor.ActorId,
                    Name = Actor.Name,
                    Country = Actor.Country,
                    Birthday = Actor.Birthday,
                    Gender = Actor.Gender,
                    Image = Actor.Image,
                });
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                lstActoriewModel = lstActoriewModel.Where(a => a.Name.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lstActoriewModel = lstActoriewModel.OrderByDescending(a=>a.Name).ToList();
                    break;
                default:
                    lstActoriewModel = lstActoriewModel.OrderByDescending(a => a.Name).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(lstActoriewModel.ToPagedList(pageNumber, pageSize));
           
        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor objActor = objActorBL.GetActorViewModelById(id);
            ActorViewModel objActorViewModel = new ActorViewModel();
            objActorViewModel.Id = objActor.Id;
            objActorViewModel.ActorId = objActor.ActorId;
            objActorViewModel.Name = objActor.Name;
            objActorViewModel.Country = objActor.Country;
            objActorViewModel.Birthday = objActor.Birthday;
            objActorViewModel.Gender = objActor.Gender;
            objActorViewModel.Image = objActor.Image;

           
            if (objActorViewModel == null)
            {
                return HttpNotFound();
            }
            return View(objActorViewModel);
        }

        // GET: Actor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ActorId,Name,Country,Birthday,Gender,Image")] ActorViewModel actorViewModel)
        {
            if (ModelState.IsValid)
            {
                Actor objActor = new Actor();

                objActor.Id = actorViewModel.Id;
                objActor.ActorId = actorViewModel.ActorId;
                objActor.Name = actorViewModel.Name;
                objActor.Country = actorViewModel.Country;
                objActor.Birthday = actorViewModel.Birthday;
                objActor.Gender = actorViewModel.Gender;
                objActor.Image = actorViewModel.Image;

                objActorBL.AddActor(objActor);

                return RedirectToAction("Index");
            }

            return View(actorViewModel);
        }

        // GET: Actor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Actor objActor = objActorBL.GetActorViewModelById(id);

            ActorViewModel objActorViewModel = new ActorViewModel();

            objActorViewModel.Id = objActor.Id;
            objActorViewModel.ActorId = objActor.ActorId;
            objActorViewModel.Name = objActor.Name;
            objActorViewModel.Country = objActor.Country;
            objActorViewModel.Birthday = objActor.Birthday;
            objActorViewModel.Gender = objActor.Gender;
            objActorViewModel.Image = objActor.Image;


            if (objActorViewModel == null)
            {
                return HttpNotFound();
            }

            return View(objActorViewModel);
        }

        // POST: Actor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ActorId,Name,Country,Birthday,Gender,Image")] ActorViewModel actorViewModel)
        {
            if (ModelState.IsValid)
            {
                Actor objActor = new Actor();

                objActor.Id = actorViewModel.Id;
                objActor.ActorId = actorViewModel.ActorId;
                objActor.Name = actorViewModel.Name;
                objActor.Country = actorViewModel.Country;
                objActor.Birthday = actorViewModel.Birthday;
                objActor.Gender = actorViewModel.Gender;
                objActor.Image = actorViewModel.Image;

                objActorBL.EditActor(objActor);

                return RedirectToAction("Index");
            }
            return View(actorViewModel);
        }

        // GET: Actor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Actor objActor = objActorBL.GetActorViewModelById(id);

            ActorViewModel objActorViewModel = new ActorViewModel();

            objActorViewModel.Id = objActor.Id;
            objActorViewModel.ActorId = objActor.ActorId;
            objActorViewModel.Name = objActor.Name;
            objActorViewModel.Country = objActor.Country;
            objActorViewModel.Birthday = objActor.Birthday;
            objActorViewModel.Gender = objActor.Gender;
            objActorViewModel.Image = objActor.Image;

            if (objActorViewModel == null)
            {
                return HttpNotFound();
            }
            return View(objActorViewModel);
        }

        // POST: Actor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Actor objActor = objActorBL.GetActorViewModelById(id);
            objActorBL.RemoveActor(objActor);
            return RedirectToAction("Index");
        }

        public ActionResult GetAllActorData()
        {
            objActorBL.GetAllActorDataFromTvMazeApi();
            return RedirectToAction("Index");

        }

        public ActionResult BirthdayTommorow()
        {
            var dtTomorrow = DateTime.Now.AddDays(1);
           

            List<BirthdayViewModel> lstBirthday = new List<BirthdayViewModel>();

            foreach (var actor in objActorBL.GetBirthday(dtTomorrow))
            {

                int intAge = (dtTomorrow.Year - actor.Birthday.Value.Year);



                lstBirthday.Add(new BirthdayViewModel { Name = actor.Name, PersonBirthday = actor.Birthday, Age = intAge, ImageLink = actor.Image });
            }

            return View(lstBirthday);

        }

        public ActionResult BirthdayToday()
        {
            var dtToday = DateTime.Now;


            List<BirthdayViewModel> lstBirthday = new List<BirthdayViewModel>();

            foreach (var actor in objActorBL.GetBirthday(dtToday))
            {

                int intAge = (dtToday.Year - actor.Birthday.Value.Year);



                lstBirthday.Add(new BirthdayViewModel { Name = actor.Name, PersonBirthday = actor.Birthday, Age = intAge, ImageLink = actor.Image });
            }

            return View(lstBirthday);
        }

    }
}
