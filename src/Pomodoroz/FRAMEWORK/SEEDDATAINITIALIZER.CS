using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WinnipegTomatoes.Models
{
    public class SeedDataInitializer : DropCreateDatabaseIfModelChanges<PomodoroContext>
    {
        protected override void Seed(PomodoroContext context)
        {
            List<Activity> activities = new List<Activity>
            {
                new Activity { ActivityId = 1, Title = "Find Recipe", Username = "Admin"},
                new Activity { ActivityId = 2, Title = "Buy Ingredients Online", Username = "Admin"},
                new Activity { ActivityId = 3, Title = "Bake Cookies", Username = "Admin"},
                new Activity { ActivityId = 4, Title = "Eat Cookies", Username = "Admin"},
                new Activity { ActivityId = 5, Title = "Send Cookies to Mom", Username = "Admin"}
            };

            foreach (var activity in activities)
                context.Activities.Add(activity);

            context.SaveChanges();

            List<Pomodoro> pomodoros = new List<Pomodoro> {
                new Pomodoro { CreatedAt= DateTime.Now.AddDays(-5), CompletedAt = DateTime.Now.AddDays(-4), Username = "Admin", Activity = activities.Where(a=>a.ActivityId == 1).First()},
                new Pomodoro { CreatedAt= DateTime.Now.AddDays(-4), CompletedAt = DateTime.Now.AddDays(-4).AddMinutes(25), Username = "Admin", Activity = activities.Where(a=>a.ActivityId == 1).First()},
                new Pomodoro { CreatedAt= DateTime.Now.AddDays(-4), CompletedAt = DateTime.Now.AddDays(-4), Username = "Admin", Activity = activities.Where(a=>a.ActivityId == 1).First()},
                new Pomodoro { CreatedAt= DateTime.Now.AddDays(-3), CompletedAt = DateTime.Now.AddDays(-4), Username = "Admin", Activity = activities.Where(a=>a.ActivityId == 2).First()},
                new Pomodoro { CreatedAt= DateTime.Now.AddDays(-3), CompletedAt = DateTime.Now.AddDays(-4), Username = "Admin", Activity = activities.Where(a=>a.ActivityId == 3).First()}
            };

            foreach (var pomodoro in pomodoros)
                context.Pomodoroes.Add(pomodoro);

            context.SaveChanges();

            base.Seed(context); 
        }
    }
}