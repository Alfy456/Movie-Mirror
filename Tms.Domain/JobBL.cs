using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Data;

namespace Tms.Domain
{
    public class JobBL
    {
        private TmsEntities db = new TmsEntities();
        MovieBL objMovieBL = new MovieBL();
        public List<Job> GetAllJobs()
        {
            List<Job> lstAllJobs = db.Jobs.ToList();
            return lstAllJobs;
        }

        public Job GetJobViewModelById(int? p_intId)
        {
            Job objJob = db.Jobs.Find(p_intId);
            return objJob;
        }

        public void CreateJob(Job p_objJob)
        {
            db.Jobs.Add(p_objJob);
            db.SaveChanges();
        }

        public void EditJob(Job p_objJob)
        {
            db.Entry(p_objJob).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveJob(Job p_objJob)
        {
            db.Jobs.Remove(p_objJob);
            db.SaveChanges();
        }

        public void Run()
        {
            objMovieBL.MovieBulkUpload();
        }
    }
}
