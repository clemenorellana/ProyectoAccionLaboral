using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class Career
    {
        public int CareerId { get; set; }
        public string Name { get; set; }
        public int AcademicLevelId { get; set; }

        public AcademicLevel AcademicLevel { get; set; }
    }
}