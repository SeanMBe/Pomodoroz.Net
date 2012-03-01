using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WinnipegTomatoes.Models
{
    public class Pomodoro
    {
        [Key]
        public int PomodoroId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? ResetAt { get; set; }

        // track by user
        [Required]
        public string Username { get; set; }

        // property to hold foreign key/reference
        public virtual Activity Activity { get; set; }
        public virtual int ActivityId { get; set; }
        
    }
}