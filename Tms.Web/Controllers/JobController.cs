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
    public class JobController : Controller
    {
        JobBL ObjJobBL = new JobBL();

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


            List<JobViewModel> lstJobViewModel = new List<JobViewModel>();
            foreach (var Job in ObjJobBL.GetAllJobs())
            {
                lstJobViewModel.Add(new JobViewModel
                {
                    JobId = Job.JobId,
                    Name = Job.Name,
                    Status=Job.Status,
                    CreatedWhen=Job.CreatedWhen,
                    UpdatedWhen=Job.UpdatedWhen

                });
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                lstJobViewModel = lstJobViewModel.Where(a => a.Name.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lstJobViewModel = lstJobViewModel.OrderByDescending(a => a.Name).ToList();
                    break;
                default:
                    lstJobViewModel = lstJobViewModel.OrderByDescending(a => a.UpdatedWhen).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(lstJobViewModel.ToPagedList(pageNumber, pageSize));

        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job ObjJob = ObjJobBL.GetJobViewModelById(id);
            JobViewModel ObjJobViewModel = new JobViewModel();
            ObjJobViewModel.JobId = ObjJob.JobId;
            ObjJobViewModel.Name = ObjJob.Name;
            ObjJobViewModel.Status = ObjJob.Status;
            ObjJobViewModel.CreatedWhen = ObjJob.CreatedWhen;
            ObjJobViewModel.UpdatedWhen = ObjJob.UpdatedWhen;

            if (ObjJobViewModel == null)
            {
                return HttpNotFound();
            }
            return View(ObjJobViewModel);
        }

        // GET: Actor/Create
        public ActionResult Create()
        {
            return View();
        }

        public void Run()
        {
            ObjJobBL.Run();
        }

        // POST: Actor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,Name,Status,CreatedWhen,UpdatedWhen")] JobViewModel p_objJobViewModel)
        {
            if (ModelState.IsValid)
            {
                Job ObjJob = new Job();

                ObjJob.JobId = p_objJobViewModel.JobId;
                ObjJob.Name = p_objJobViewModel.Name;
                ObjJob.Status = p_objJobViewModel.Status;
                ObjJob.CreatedWhen = p_objJobViewModel.CreatedWhen;
                ObjJob.UpdatedWhen = p_objJobViewModel.UpdatedWhen;
                ObjJobBL.CreateJob(ObjJob);
                return RedirectToAction("Index");
            }

            return View(p_objJobViewModel);
        }

        // GET: Actor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Job ObjJob = ObjJobBL.GetJobViewModelById(id);

            JobViewModel ObjJobViewModel = new JobViewModel();

            ObjJobViewModel.JobId = ObjJob.JobId;
            ObjJobViewModel.Name = ObjJob.Name;
            ObjJobViewModel.Status = ObjJob.Status;
            ObjJobViewModel.CreatedWhen = ObjJob.CreatedWhen;
            ObjJobViewModel.UpdatedWhen = ObjJob.UpdatedWhen;

            if (ObjJobViewModel == null)
            {
                return HttpNotFound();
            }

            return View(ObjJobViewModel);
        }

        // POST: Actor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,Name,Status,CreatedWhen,UpdatedWhen")] JobViewModel p_ObjViewModel)
        {
            if (ModelState.IsValid)
            {
                Job ObjJob = new Job();

                ObjJob.JobId = p_ObjViewModel.JobId;
                ObjJob.Name = p_ObjViewModel.Name;
                ObjJob.Status = p_ObjViewModel.Status;
                ObjJob.CreatedWhen = p_ObjViewModel.CreatedWhen;
                ObjJob.UpdatedWhen = p_ObjViewModel.UpdatedWhen;

                ObjJobBL.EditJob(ObjJob);

                return RedirectToAction("Index");
            }
            return View(p_ObjViewModel);
        }

        // GET: Actor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Job ObjJob = ObjJobBL.GetJobViewModelById(id);

            JobViewModel ObjJobViewModel = new JobViewModel();

            ObjJobViewModel.JobId = ObjJob.JobId;
            ObjJobViewModel.Name = ObjJob.Name;
            ObjJobViewModel.Status = ObjJob.Status;
            ObjJobViewModel.CreatedWhen = ObjJob.CreatedWhen;
            ObjJobViewModel.UpdatedWhen = ObjJob.UpdatedWhen;

            if (ObjJobViewModel == null)
            {
                return HttpNotFound();
            }
            return View(ObjJobViewModel);
        }

        // POST: Actor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job ObjJob = ObjJobBL.GetJobViewModelById(id);
            ObjJobBL.RemoveJob(ObjJob);
            return RedirectToAction("Index");
        }

    }
}
