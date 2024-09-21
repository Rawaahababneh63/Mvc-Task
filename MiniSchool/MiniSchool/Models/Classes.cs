using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniSchool.Models
{
    public class Classes
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClassName { get; set; } 

        public ICollection<Student> Students { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

        public Classes()
        {
            Students = new List<Student>();
            Assignments = new List<Assignment>();
        }
    }
}