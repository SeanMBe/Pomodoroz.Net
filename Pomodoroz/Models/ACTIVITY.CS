using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WinnipegTomatoes.Models
{

    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [MaxLength(150)]
        [MinLength(2)]
        [Required]
        public string Title { get; set; }
        public bool Completed { get; set; }

        // track by user
        public string Username { get; set; }

        // property to hold list of foreign keys/references
        public virtual List<Pomodoro> Pomodoros { get; set; }

    }

}