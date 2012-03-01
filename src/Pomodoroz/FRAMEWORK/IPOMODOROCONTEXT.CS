using System;
namespace WinnipegTomatoes.Models
{
    public interface IPomodoroContext
    {
        System.Data.Entity.DbSet<Activity> Activities { get; set; }
        System.Data.Entity.DbSet<Pomodoro> Pomodoroes { get; set; }
    }
}
