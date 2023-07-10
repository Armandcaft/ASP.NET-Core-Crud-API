using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudAPI.Models
{
    /// <summary>
    /// Student basic information with associated course.
    /// </summary>
    public partial class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public int Age { get; set; }

        // public virtual Course Course { get; set; }
        // public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
