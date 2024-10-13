﻿using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        [Key]
        [Display(Name = "EDP Code")]
        public string EdpCode { get; set; }

        [Required]
        [Display(Name = "Subject Code")]
        public string SubjCode { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        [Required]
        public string Room { get; set; }

        [Required]
        public string Section { get; set; }

        [Required]
        [Display(Name = "School Year")]
        public string SchoolYear { get; set; }

    }
}