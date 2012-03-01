using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using WinnipegTomatoes.Models;

namespace WinnipegTomatoes.Framework
{
    public class PomodoroStartupModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPomodoroContext>().To<PomodoroContext>();
            Bind<IPomodoroRepository>().To<PomodoroRepository>();
            Bind<IActivityRepository>().To<ActivityRepository>();
        }
    }
}