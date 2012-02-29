using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WinnipegTomatoes.Models
{
    public class ActivityRepository : IActivityRepository
    {
        PomodoroContext _context = new PomodoroContext();

        Activity IActivityRepository.GetActivity(int activityId)
        {
            Activity activity = _context.Activities.Where(a => a.ActivityId == activityId).FirstOrDefault();
            return activity;
        }
    }
}