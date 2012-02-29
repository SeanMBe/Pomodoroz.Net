using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WinnipegTomatoes.Models
{
    public class PomodoroContext : DbContext, IPomodoroContext
    {
        public PomodoroContext () : base("Tomatobase") { }

        public DbSet<WinnipegTomatoes.Models.Pomodoro> Pomodoroes { get; set; }
        public DbSet<WinnipegTomatoes.Models.Activity> Activities { get; set; }

    }
}