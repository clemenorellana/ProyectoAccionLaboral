using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccionLaboral.Models
{
    public class Career
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CareerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int AcademicLevelId { get; set; }

        public AcademicLevel AcademicLevel { get; set; }
    }
}