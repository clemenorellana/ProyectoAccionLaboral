using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AccionLaboral.Models
{
    public class AccionLaboralContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AccionLaboralContext() : base("name=AccionLaboralContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Career> Careers { get; set; }

        public DbSet<AcademicLevel> AcademicLevels { get; set; }

        public DbSet<Privilege> Privileges { get; set; }

        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<ContractTemplate> ContractTemplates { get; set; }

        public DbSet<ContactByCompany> ContactByCompanies { get; set; }

        public DbSet<EducationType> EducationTypes { get; set; }

        public DbSet<ShipmentType> ShipmentTypes { get; set; }

        public DbSet<LanguageLevel> LanguageLevels { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<VacantLevel> VacantLevels { get; set; }

        public DbSet<VacantByCompany> VacantByCompanies { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ReferenceType> ReferenceTypes { get; set; }

        public DbSet<TrackingType> TrackingTypes { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Tracking> Trackings { get; set; }

        public DbSet<KnownLanguage> KnownLanguages { get; set; }

        public DbSet<KnownProgram> KnownPrograms { get; set; }

        public DbSet<Reference> References { get; set; }

        public DbSet<TrackingDetail> TrackingDetails { get; set; }

        public DbSet<WorkExperience> WorkExperiences { get; set; }

        public DbSet<AcademicEducation> AcademicEducations { get; set; }
    
    }
}
