using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace MiniSchool.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        // Foreign key for Class
        public int ClassId { get; set; }
        public Classes Class { get; set; }
    }
}