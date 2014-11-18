using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Neighborhood { get; set; }
        public string CompleteAddress { get; set; }
        public string Cellphone { get; set; }
        public string HomePhone { get; set; }
        public string Hobby { get; set; }
        public byte[] Photo { get; set; } // TODO : ver como se va a salvar esto.
        public string CurrentStudies { get; set; }
        public double WageAspiration { get; set; }
        public string FacebookEmail { get; set; }
        public string BBPin { get; set; }
        public string Twitter { get; set; }
        public string DesiredEmployment { get; set; }
        public string CompaniesWithPreviouslyRequested { get; set; }        
        public bool Approved { get; set; }
        public string  RejectionDesciption { get; set; }
        public int CurrentStateId { get; set; }
        public int CityId { get; set; }

        
        public State CurrentState { get; set; }
        public City City { get; set; }
        
        
        public ICollection <AcademicEducation> AcademicEducations { get; set; }
        public ICollection<KnownLanguage> Languages { get; set; }
        public ICollection<KnownProgram> KnownPrograms { get; set; }
        public ICollection<WorkExperience> WorkExperiences { get; set; }
        public ICollection<Reference> References { get; set; }
        public ICollection<ClientTracking> Trackings { get; set; }
    }
}