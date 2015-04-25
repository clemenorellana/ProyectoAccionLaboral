using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AccionLaboral.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : User
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        //[DefaultValue("true")]
        //public bool Active { get; set; }
        //[DefaultValue("true")]
        //public bool Busy { get; set; } // know if the user is assigned to an employee
    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AccionLaboralContext : IdentityDbContext<User>
    {
        public AccionLaboralContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            //Defining the keys and relations
            modelBuilder.Entity<User>().ToTable("IdentityUser");
            modelBuilder.Entity<Role>().HasKey<string>(r => r.Id).ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationUserRole>().HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");  

            modelBuilder.Entity<IdentityRole>()
                .ToTable("AspNetRoles")
                .Property(c => c.Name)
                .HasMaxLength(128)
                .IsRequired();

            // We have to declare the table name here, otherwise IdentityUser 
            // will be created
            modelBuilder.Entity<User>()
                .ToTable("IdentityUser")
                .Property(c => c.UserName)
                .HasMaxLength(128)
                .IsRequired();
            modelBuilder.Entity<User>()
                .ToTable("IdentityUser")
                .Property(c => c.Email)
                .HasMaxLength(128)
                .IsOptional();
            modelBuilder.Entity<User>()
               .ToTable("IdentityUser")
               .Property(c => c.PhoneNumber)
               .IsOptional();
        }
        public bool Seed(AccionLaboralContext context)
        {
#if DEBUG
            bool success = false;

            ApplicationRoleManager _roleManager = new ApplicationRoleManager(new RoleStore<Role>(context));

            success = this.CreateRole(_roleManager, "Asesor Corporativo", "ASCOR");
            if (!success == true) return success;

            success = this.CreateRole(_roleManager, "Asesor de Reclutamiento", "ASREC");
            if (!success == true) return success;

            success = this.CreateRole(_roleManager, "Asistente de Gerencia", "ASIGE");
            if (!success) return success;

            success = this.CreateRole(_roleManager, "Gerente de Agencia", "GTEAG");
            if (!success) return success;

            success = this.CreateRole(_roleManager, "Gerente General", "GTEGE");
            if (!success) return success;

            success = this.CreateRole(_roleManager, "Admin. del Sistema", "ADMIN");
            if (!success) return success;

            // Create my debug (testing) objects here

            /*ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<User>(context));

            User user = new User();
            PasswordHasher passwordHasher = new PasswordHasher();

            user.UserName = "youremail@testemail.com";
            user.Email = "youremail@testemail.com";

            IdentityResult result = userManager.Create(user, "Pass@123");

            success = this.AddUserToRole(userManager, user.Id, "Admin");
            if (!success) return success;

            success = this.AddUserToRole(userManager, user.Id, "CanEdit");
            if (!success) return success;

            success = this.AddUserToRole(userManager, user.Id, "User");
            if (!success) return success;*/

            return success;
#endif
        }

        public bool RoleExists(ApplicationRoleManager roleManager, string name)
        {
            return roleManager.RoleExists(name);
        }

        public bool CreateRole(ApplicationRoleManager _roleManager, string name, string alias = "")
        {
            var idResult = _roleManager.Create<Role, string>(new Role(name, alias));
            return idResult.Succeeded;
        }

        public bool AddUserToRole(ApplicationUserManager _userManager, string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }
        /*
        public void ClearUserRoles(ApplicationUserManager userManager, string userId)
        {
            var user = userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.UserRoles);
            foreach (ApplicationUserRole role in currentRoles)
            {
                userManager.RemoveFromRole(userId, role.Role.Name);
            }
        }*/


        public void RemoveFromRole(ApplicationUserManager userManager, string userId, string roleName)
        {
            userManager.RemoveFromRole(userId, roleName);
        }
        /*
        public void DeleteRole(AccionLaboralContext context, ApplicationUserManager userManager, string roleId)
        {
            var roleUsers = context.Users.Where(u => u.UserRoles.Any(r => r.RoleId == roleId));
            var role = context.Roles.Find(roleId);

            foreach (var user in roleUsers)
            {
                this.RemoveFromRole(userManager, user.Id, role.Name);
            }
            context.Roles.Remove(role);
            context.SaveChanges();
        }*/

        /// <summary>
        /// Context Initializer
        /// </summary>
        public class DropCreateAlwaysInitializer : DropCreateDatabaseAlways<AccionLaboralContext>
        {
            protected override void Seed(AccionLaboralContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }
        /*public AccionLaboralContext()
            : base("name=DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public AccionLaboralContext(string connStringName) : base(connStringName) { }

        static AccionLaboralContext()
        {
            DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        }

        public static AccionLaboralContext Create()
        {
            return new AccionLaboralContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>()
                .Property(c => c.Name)
                .HasMaxLength(128)
                .IsRequired();

            // We have to declare the table name here, otherwise IdentityUser 
            // will be created
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers")
                .Property(c => c.UserName)
                .HasMaxLength(128)
                .IsRequired();
        }*/

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