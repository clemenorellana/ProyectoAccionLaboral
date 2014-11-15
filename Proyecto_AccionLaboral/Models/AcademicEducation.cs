using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class AcademicEducation
    {
        public int AcademicTrainingId { get; set; }
        public int Year { get; set; }        
        public string InstitutionName { get; set; }        
        public string TrainingName { get; set; }
        public int CityId { get; set; }
        public int AcademicLevelId { get; set; }
        public int CareerId { get; set; }
        public int EducationTypeId { get; set; }
        public int ClientId { get; set; }

        public Client Client { get; set; }
        public City City { get; set; }
        public AcademicLevel AcademicLevel { get; set; }
        public Career Career { get; set; }
        public EducationType EducationType { get; set; }

    }
}