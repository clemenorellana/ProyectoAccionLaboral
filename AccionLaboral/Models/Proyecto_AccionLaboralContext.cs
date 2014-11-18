using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Proyecto_AccionLaboral.Models
{
    public class Proyecto_AccionLaboralContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Proyecto_AccionLaboralContext() : base("name=Proyecto_AccionLaboralContext")
        {
        }

        public DbSet<Proyecto_AccionLaboral.Models.User> Users { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.Career> Careers { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.AcademicLevel> AcademicLevels { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.Privilege> Privileges { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.Role> Roles { get; set; }
        
        public DbSet<Proyecto_AccionLaboral.Models.Country> Countries { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.City> Cities { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.Company> Companies { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.ContractTemplate> ContractTemplates { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.ContactByCompany> ContactByCompanies { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.EducationType> EducationTypes { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.ShipmentType> ShipmentTypes { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.LanguageLevel> LanguageLevels { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.Language> Languages { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.VacantLevel> VacantLevels { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.VacantByCompany> VacantByCompanies { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.State> States { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.Employee> Employees { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.ReferenceType> ReferenceTypes { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.TrackingType> TrackingTypes { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.Client> Clients { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.ClientTracking> ClientTrackings { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.KnownLanguage> KnownLanguages { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.KnownProgram> KnownPrograms { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.Reference> References { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.TrackingDetail> TrackingDetails { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.WorkExperience> WorkExperiences { get; set; }

        public DbSet<Proyecto_AccionLaboral.Models.AcademicEducation> AcademicEducations { get; set; }
    
    }
}
