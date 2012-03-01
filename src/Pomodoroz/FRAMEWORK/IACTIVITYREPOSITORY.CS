using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinnipegTomatoes.Models
{
    public interface IActivityRepository
    {
        Activity GetActivity(int activityId);
    }
}
