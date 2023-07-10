using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudAPI.Models
{
    /// <summary>
    /// The various courses followed by students in an institution.
    /// </summary>
    public partial class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(200)]
        public string Description { get; set; } = null!;
        
        [Required(ErrorMessage = "Credit is required")]
        public int Credit { get; set; }
        
        // [ForeignKey("Student")]
        // public int StudentId { get; set; }
        
        public virtual Student? Student { get; set; }
    }
}
