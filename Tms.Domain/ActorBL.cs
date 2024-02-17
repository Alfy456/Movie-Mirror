using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tms.Data;
using Tms.Domain.ActorModels;
using Tms.Domain.Models;
using Tms.Enum;

namespace Tms.Domain
{
    public class ActorBL
    {
        private TmsEntities db = new TmsEntities();
        public List<Actor> GetAllActors()
        {
            List <Actor> lstAllActors = db.Actors.ToList();
            return lstAllActors;
        }

        public Actor GetActorViewModelById(int? p_intId)
        {
            Actor objActor = db.Actors.Find(p_intId);
            return objActor;
        }

        public void AddActor(Actor p_objActor)
        {
            db.Actors.Add(p_objActor);
            db.SaveChanges();
        }

        public void EditActor(Actor p_objActor)
        {
            db.Entry(p_objActor).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveActor(Actor p_objActor)
        {
            db.Actors.Remove(p_objActor);
            db.SaveChanges();
        }

        public void GetAllActorDataFromTvMazeApi()
        {
            bool blnExit = true;
            int intId = 0;
            do
            {
                try

                {
                    var webURL = "https://api.tvmaze.com/people?page=" + intId;

                    WebClient wC = new WebClient();
                    string jsonText = wC.DownloadString(webURL);
                    var Actors = JsonConvert.DeserializeObject<List<ActorModel>>(jsonText);
                    DateTime dtNow = DateTime.Today;
                    DateTime dtDefaultDate = new DateTime(1900, 01, 01);
                    string strImageLink = "Default";

                    var xml = new XDocument(new XElement("Actors", from Actor in Actors
                    select new XElement("Actor",
                    new XAttribute("ActorId", Actor.id),
                    new XAttribute("Name", Actor.name),
                    //new XAttribute("Country", tmp.country.name = tmp.country.name == null ? "Default" : tmp.country.name),
                    new XAttribute("Country", "Default"),
                    new XAttribute("Gender", Actor.gender = Actor.gender == null ? "Default" : Actor.gender),
                    new XAttribute("Birthday", Actor.birthday = Actor.birthday == null || Actor.birthday < dtDefaultDate ? dtDefaultDate : Actor.birthday),
                    new XAttribute("Image", strImageLink = Actor.image == null ? "Default" : Actor.image.medium = Actor.image.medium == null ? "Default" : Actor.image.medium))));

                    var p0 = new SqlXml(xml.CreateReader());
                    var sql = ResourceHelpers.GetResourceFile("Tms.Domain.InsertActors.sql");
                    db.Database.CommandTimeout = 0;
                    db.Database.ExecuteSqlCommand(sql, new SqlParameter("p0", p0));
                    intId++;
                }
                catch (Exception e)
                {
                    blnExit = false;

                }
            }
            while (blnExit);
        }
       
        public void CreateActor(Actor p_objActor)
        {
            db.Actors.Add(p_objActor);
            db.SaveChanges();
        }
        public Language GetEnumFromMovieName(String p_strMovieName)
        {
            return Language.Malayalam;
            //switch (p_strMovieName)
            //{
            //    case 1: return Language.Malayalam;
            //        break;
            //    case 2: 
            //}
        }

        public IEnumerable<TvShow> GetAnniversary(DateTime p_dtTomorrow)
        {
            var tvshows = (from t in db.TvShows
                           where
                           t.Premiered.Value.Day == p_dtTomorrow.Day &&
                           t.Premiered.Value.Month == p_dtTomorrow.Month
                           select t).ToList().OrderByDescending(t => t.Premiered);
            return tvshows;
        }

        public IEnumerable<Actor> GetBirthday(DateTime p_dtDate)
        {

            IEnumerable<Actor> objActors = (from t in db.Actors
                           where
                           t.Birthday.Value.Day == p_dtDate.Day &&
                           t.Birthday.Value.Month == p_dtDate.Month
                           select t).ToList().OrderByDescending(t => t.Birthday);
            return objActors;


        }
    }
}
