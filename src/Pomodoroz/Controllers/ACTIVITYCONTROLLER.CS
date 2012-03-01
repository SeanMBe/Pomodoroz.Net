using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnipegTomatoes.Models;
using WinnipegTomatoes.ActionFilters;

namespace WinnipegTomatoes.Controllers
{   
    [Authorize]
    public class ActivityController : Controller
    {
        private PomodoroContext context = new PomodoroContext();

        [InjectUsername]
        public ViewResult Index(string username)
        {
            return View(context.Activities
                .Where(a => a.Username == username)
                .Include(activity => activity.Pomodoros)
                .ToList());
        }

        [InjectUsername]
        public PartialViewResult RunningPomodoro(string username)
        {
            Pomodoro pomodoro = null;

            // try to grab an existing pomodoro
            if (username != null)
            {
                DateTime datefilter = DateTime.Now.AddMinutes(-25);
                pomodoro = context.Pomodoroes.Where(p => 
                    p.Username == username 
                    && p.CreatedAt > datefilter 
                    && (p.CompletedAt == null 
                    && p.ResetAt == null)).FirstOrDefault();
            }

            return PartialView(pomodoro);
            
        }

        [InjectUsername]
        public PartialViewResult LoadActivities(string username)
        {
            return PartialView(context.Activities
                .Where(a => a.Username == username)
                .Include(activity => activity.Pomodoros)
                .ToList());
        }


        // CRUD...
        public ActionResult Create()
        {
            return View();
        } 

        [InjectUsername]
        [HttpPost]
        public ActionResult Create(Activity activity, string username)
        {
            if (ModelState.IsValid)
            {
                // make sure it's not hacked by script kiddies
                activity.Username = username;
                context.Activities.Add(activity);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(activity);
        }
        
        public ActionResult Edit(int id)
        {
            Activity activity = context.Activities.Single(x => x.ActivityId == id);
            return View(activity);
        }
        
        [HttpPut]
        [InjectUsername]
        public string SetActivityComplete(int activityId, bool complete, string username)
        {
            string result = string.Format("Activity marked as {0}.", complete ? "complete" : "incomplete");

            Activity activity = context.Activities.Where(a => a.ActivityId == activityId && a.Username == username).First();
            if (activity != null)
            {
                activity.Completed = complete;
                foreach (var pomodoro in activity.Pomodoros.Where(p => p.CompletedAt == null && p.ResetAt == null))
                {
                    pomodoro.CompletedAt = DateTime.Now;
                }
                context.SaveChanges();
            }
            else
            {
                result = "Activity not found.";
            }

            return result;
        }


        [HttpPost]
        public ActionResult Edit(Activity activity)
        {
            if (ModelState.IsValid)
            {
                context.Entry(activity).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        public ActionResult Delete(int id)
        {
            Activity activity = context.Activities.Single(x => x.ActivityId == id);
            return View(activity);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = context.Activities.Single(x => x.ActivityId == id);
            context.Activities.Remove(activity);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}