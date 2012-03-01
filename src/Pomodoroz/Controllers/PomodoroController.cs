using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnipegTomatoes.Models;
using WinnipegTomatoes.ActionFilters;

namespace WinnipegTomatoes.Controllers
{
    [Authorize]
    public class PomodoroController : Controller
    {
        IPomodoroRepository _pomodoros;
        IActivityRepository _activities;

        // this is handled through dependency injection
        public PomodoroController(IPomodoroRepository pomodoros, IActivityRepository activities)
        {
            _pomodoros = pomodoros;
            _activities = activities;
        }

        [HttpPost]
        [InjectUsername]
        public ActionResult GetPomodoros(string username)
        {
            return Json(_pomodoros.GetPomodoros(username));
        }
        
        [HttpPost]
        [InjectUsername]
        public string StartPomodoro(int activityId, string username)
        {
            Pomodoro pomodoro = new Pomodoro();
            pomodoro.ActivityId = activityId;
            pomodoro.Username = username;
            pomodoro.CreatedAt = DateTime.Now;

            _pomodoros.AddPomodoro(pomodoro);
            _pomodoros.Save();

            return "Pomodoro started.";
        }

        [HttpPut]
        [InjectUsername]
        public string ResetPomodoro(int activityId, string username)
        {
            _pomodoros.GetPomodoro(activityId, username).ResetAt = DateTime.Now;
            _pomodoros.Save();
            return "Pomodoro interrupted.  :(";
        }

    }
}
