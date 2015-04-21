using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AccionLaboral.Models
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        [DefaultValue("true")]
        public bool Active { get; set; }
        [DefaultValue("true")]
        public bool Busy { get; set; } // know if the user is assigned to an employee
    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AccionLaboralContext : IdentityDbContext<ApplicationUser>
    {
        public AccionLaboralContext()
            : base("DefaultConnection")
        {
        }

        public static AccionLaboralContext Create()
        {
            return new AccionLaboralContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>()
                .Property(c => c.Name).HasMaxLength(256).IsRequired();

            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>().ToTable("AspNetUsers")//I have to declare the table name, otherwise IdentityUser will be created
                .Property(c => c.UserName).HasMaxLength(256).IsRequired();
        }

        //public DbSet<User> Users { get; set; }

        public DbSet<Career> Careers { get; set; }

        public DbSet<AcademicLevel> AcademicLevels { get; set; }

        public DbSet<Privilege> Privileges { get; set; }

        //public DbSet<Role> Roles { get; set; }

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

        public DbSet<InterviewType> InterviewTypes { get; set; }

        public DbSet<VacantCovered> VacantCovers { get; set; }
    }
}