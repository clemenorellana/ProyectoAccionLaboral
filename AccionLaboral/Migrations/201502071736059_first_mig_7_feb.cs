namespace AccionLaboral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first_mig_7_feb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AcademicEducations",
                c => new
                    {
                        AcademicEducationId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        InstitutionName = c.String(unicode: false),
                        TrainingName = c.String(unicode: false),
                        CityId = c.Int(nullable: false),
                        AcademicLevelId = c.Int(),
                        CareerId = c.Int(nullable: false),
                        EducationTypeId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AcademicEducationId)                
                .ForeignKey("AcademicLevels", t => t.AcademicLevelId)
                .ForeignKey("Careers", t => t.CareerId, cascadeDelete: true)
                .ForeignKey("Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("EducationTypes", t => t.EducationTypeId, cascadeDelete: true)
                .Index(t => t.CityId)
                .Index(t => t.AcademicLevelId)
                .Index(t => t.CareerId)
                .Index(t => t.EducationTypeId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "AcademicLevels",
                c => new
                    {
                        AcademicLevelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.AcademicLevelId)                ;
            
            CreateTable(
                "Careers",
                c => new
                    {
                        CareerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        AcademicLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CareerId)                
                .ForeignKey("AcademicLevels", t => t.AcademicLevelId, cascadeDelete: true)
                .Index(t => t.AcademicLevelId);
            
            CreateTable(
                "Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)                
                .ForeignKey("Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.CountryId)                ;
            
            CreateTable(
                "Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        CorrelativeCode = c.String(maxLength: 10, storeType: "nvarchar"),
                        FirstName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        LastName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        IdentityNumber = c.String(unicode: false),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        Age = c.Int(nullable: false),
                        Gender = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Neighborhood = c.String(unicode: false),
                        CompleteAddress = c.String(unicode: false),
                        Cellphone = c.String(unicode: false),
                        HomePhone = c.String(unicode: false),
                        Hobby = c.String(unicode: false),
                        Photo = c.Binary(),
                        CurrentStudies = c.String(unicode: false),
                        WageAspiration = c.Double(nullable: false),
                        FacebookEmail = c.String(unicode: false),
                        BBPin = c.String(unicode: false),
                        Twitter = c.String(unicode: false),
                        DesiredEmployment = c.String(unicode: false),
                        CompaniesWithPreviouslyRequested = c.String(unicode: false),
                        Approved = c.Boolean(nullable: false),
                        RejectionDescription = c.String(unicode: false),
                        StateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        EmployeeId = c.Int(),
                        CompanyId = c.Int(),
                        EnrollDate = c.DateTime(precision: 0),
                        Occupation = c.String(unicode: false),
                        Shipped = c.String(unicode: false),
                        InformationMedia = c.String(unicode: false),
                        EnglishPercentage = c.Int(),
                        IsStudying = c.Boolean(),
                        QtyClasses = c.Int(),
                        HaveCar = c.Boolean(),
                        HaveMotorcycle = c.Boolean(),
                        HaveLicense = c.Boolean(),
                        LicenseType = c.String(unicode: false),
                        Comment = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ClientId)                
                .ForeignKey("Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("Companies", t => t.CompanyId)
                .ForeignKey("Employees", t => t.EmployeeId)
                .ForeignKey("States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.CorrelativeCode, unique: true)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.EmployeeId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Area = c.String(unicode: false),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.CompanyId)                
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "ContactByCompanies",
                c => new
                    {
                        ContactByCompanyId = c.Int(nullable: false, identity: true),
                        ContactName = c.String(nullable: false, unicode: false),
                        ContactEmail = c.String(unicode: false),
                        Telephone = c.String(unicode: false),
                        Extension = c.String(unicode: false),
                        Observations = c.String(unicode: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactByCompanyId)                
                .ForeignKey("Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "VacantByCompanies",
                c => new
                    {
                        VacantByCompanyId = c.Int(nullable: false, identity: true),
                        VacantName = c.String(unicode: false),
                        Active = c.Boolean(nullable: false),
                        Quantity = c.Int(nullable: false),
                        StartAge = c.Int(nullable: false),
                        EndAge = c.Int(nullable: false),
                        Gender = c.String(unicode: false),
                        Requirements = c.String(unicode: false),
                        ChargeDescription = c.String(unicode: false),
                        RequestDate = c.DateTime(nullable: false, precision: 0),
                        CoverdDate = c.DateTime(nullable: false, precision: 0),
                        CompanyId = c.Int(nullable: false),
                        AcademicLevelId = c.Int(nullable: false),
                        CareerId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        VacantLevelId = c.Int(nullable: false),
                        InterviewTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VacantByCompanyId)                
                .ForeignKey("AcademicLevels", t => t.AcademicLevelId, cascadeDelete: true)
                .ForeignKey("Careers", t => t.CareerId, cascadeDelete: true)
                .ForeignKey("Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("InterviewTypes", t => t.InterviewTypeId, cascadeDelete: true)
                .ForeignKey("VacantLevels", t => t.VacantLevelId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.AcademicLevelId)
                .Index(t => t.CareerId)
                .Index(t => t.CityId)
                .Index(t => t.VacantLevelId)
                .Index(t => t.InterviewTypeId);
            
            CreateTable(
                "InterviewTypes",
                c => new
                    {
                        InterviewTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.InterviewTypeId)                ;
            
            CreateTable(
                "VacantLevels",
                c => new
                    {
                        VacantLevelId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.VacantLevelId)                ;
            
            CreateTable(
                "Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        LastName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Email = c.String(unicode: false),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        Age = c.Int(nullable: false),
                        Cellphone = c.String(unicode: false),
                        HomePhone = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Gender = c.String(unicode: false),
                        EmployeeAlias = c.String(unicode: false),
                        CareerId = c.Int(nullable: false),
                        AdmissionDate = c.DateTime(nullable: false, precision: 0),
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.EmployeeId)                
                .ForeignKey("Careers", t => t.CareerId, cascadeDelete: true)
                .ForeignKey("Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CareerId)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.RoleId)                ;
            
            CreateTable(
                "Privileges",
                c => new
                    {
                        PrivilegeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.PrivilegeId)                ;
            
            CreateTable(
                "Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                        Password = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.UserId)                
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "KnownPrograms",
                c => new
                    {
                        KnownProgramId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KnownProgramId)                
                .ForeignKey("Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "KnownLanguages",
                c => new
                    {
                        KnownLanguageId = c.Int(nullable: false, identity: true),
                        Percentage = c.Double(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        LanguageLevelId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KnownLanguageId)                
                .ForeignKey("Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("LanguageLevels", t => t.LanguageLevelId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.LanguageLevelId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "Languages",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.LanguageId)                ;
            
            CreateTable(
                "LanguageLevels",
                c => new
                    {
                        LanguageLevelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.LanguageLevelId)                ;
            
            CreateTable(
                "References",
                c => new
                    {
                        ReferenceId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
                        Charge = c.String(unicode: false),
                        Cellphone = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        CompanyName = c.String(unicode: false),
                        Relationship = c.String(unicode: false),
                        ReferenceTypeId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReferenceId)                
                .ForeignKey("Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("ReferenceTypes", t => t.ReferenceTypeId, cascadeDelete: true)
                .Index(t => t.ReferenceTypeId)
                .Index(t => t.CityId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "ReferenceTypes",
                c => new
                    {
                        ReferenceTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.ReferenceTypeId)                ;
            
            CreateTable(
                "States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.StateId)                ;
            
            CreateTable(
                "Trackings",
                c => new
                    {
                        TrackingId = c.Int(nullable: false, identity: true),
                        TrackingTypeId = c.Int(),
                        ClientId = c.Int(),
                        CompanyId = c.Int(),
                        StateId = c.Int(nullable: false),
                        VacantByCompanyId = c.Int(),
                    })
                .PrimaryKey(t => t.TrackingId)                
                .ForeignKey("Clients", t => t.ClientId)
                .ForeignKey("Companies", t => t.CompanyId)
                .ForeignKey("States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("TrackingTypes", t => t.TrackingTypeId)
                .ForeignKey("VacantByCompanies", t => t.VacantByCompanyId)
                .Index(t => t.TrackingTypeId)
                .Index(t => t.ClientId)
                .Index(t => t.CompanyId)
                .Index(t => t.StateId)
                .Index(t => t.VacantByCompanyId);
            
            CreateTable(
                "TrackingDetails",
                c => new
                    {
                        TrackingDetailId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(precision: 0),
                        Description = c.String(nullable: false, unicode: false),
                        Comment = c.String(unicode: false),
                        ShipmentTypeId = c.Int(),
                        TrackingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrackingDetailId)                
                .ForeignKey("ShipmentTypes", t => t.ShipmentTypeId)
                .ForeignKey("Trackings", t => t.TrackingId, cascadeDelete: true)
                .Index(t => t.ShipmentTypeId)
                .Index(t => t.TrackingId);
            
            CreateTable(
                "ShipmentTypes",
                c => new
                    {
                        ShipmentTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.ShipmentTypeId)                ;
            
            CreateTable(
                "TrackingTypes",
                c => new
                    {
                        TrackingTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.TrackingTypeId)                ;
            
            CreateTable(
                "WorkExperiences",
                c => new
                    {
                        WorkExperienceId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(unicode: false),
                        CompanyArea = c.String(unicode: false),
                        Charge = c.String(unicode: false),
                        StartDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(nullable: false, precision: 0),
                        Achievements = c.String(unicode: false),
                        CityId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkExperienceId)                
                .ForeignKey("Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.CityId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "EducationTypes",
                c => new
                    {
                        EducationTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.EducationTypeId)                ;
            
            CreateTable(
                "ContractTemplates",
                c => new
                    {
                        ContractTemplateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(nullable: false, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContractTemplateId)                ;
            
            CreateTable(
                "PrivilegeRoles",
                c => new
                    {
                        Privilege_PrivilegeId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Privilege_PrivilegeId, t.Role_RoleId })                
                .ForeignKey("Privileges", t => t.Privilege_PrivilegeId, cascadeDelete: true)
                .ForeignKey("Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.Privilege_PrivilegeId)
                .Index(t => t.Role_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("AcademicEducations", "EducationTypeId", "EducationTypes");
            DropForeignKey("WorkExperiences", "ClientId", "Clients");
            DropForeignKey("WorkExperiences", "CityId", "Cities");
            DropForeignKey("Trackings", "VacantByCompanyId", "VacantByCompanies");
            DropForeignKey("Trackings", "TrackingTypeId", "TrackingTypes");
            DropForeignKey("TrackingDetails", "TrackingId", "Trackings");
            DropForeignKey("TrackingDetails", "ShipmentTypeId", "ShipmentTypes");
            DropForeignKey("Trackings", "StateId", "States");
            DropForeignKey("Trackings", "CompanyId", "Companies");
            DropForeignKey("Trackings", "ClientId", "Clients");
            DropForeignKey("Clients", "StateId", "States");
            DropForeignKey("References", "ReferenceTypeId", "ReferenceTypes");
            DropForeignKey("References", "ClientId", "Clients");
            DropForeignKey("References", "CityId", "Cities");
            DropForeignKey("KnownLanguages", "LanguageLevelId", "LanguageLevels");
            DropForeignKey("KnownLanguages", "LanguageId", "Languages");
            DropForeignKey("KnownLanguages", "ClientId", "Clients");
            DropForeignKey("KnownPrograms", "ClientId", "Clients");
            DropForeignKey("Clients", "EmployeeId", "Employees");
            DropForeignKey("Employees", "UserId", "Users");
            DropForeignKey("Employees", "RoleId", "Roles");
            DropForeignKey("PrivilegeRoles", "Role_RoleId", "Roles");
            DropForeignKey("PrivilegeRoles", "Privilege_PrivilegeId", "Privileges");
            DropForeignKey("Employees", "CareerId", "Careers");
            DropForeignKey("Clients", "CompanyId", "Companies");
            DropForeignKey("VacantByCompanies", "VacantLevelId", "VacantLevels");
            DropForeignKey("VacantByCompanies", "InterviewTypeId", "InterviewTypes");
            DropForeignKey("VacantByCompanies", "CompanyId", "Companies");
            DropForeignKey("VacantByCompanies", "CityId", "Cities");
            DropForeignKey("VacantByCompanies", "CareerId", "Careers");
            DropForeignKey("VacantByCompanies", "AcademicLevelId", "AcademicLevels");
            DropForeignKey("ContactByCompanies", "CompanyId", "Companies");
            DropForeignKey("Clients", "CityId", "Cities");
            DropForeignKey("AcademicEducations", "ClientId", "Clients");
            DropForeignKey("AcademicEducations", "CityId", "Cities");
            DropForeignKey("Cities", "CountryId", "Countries");
            DropForeignKey("AcademicEducations", "CareerId", "Careers");
            DropForeignKey("AcademicEducations", "AcademicLevelId", "AcademicLevels");
            DropForeignKey("Careers", "AcademicLevelId", "AcademicLevels");
            DropIndex("PrivilegeRoles", new[] { "Role_RoleId" });
            DropIndex("PrivilegeRoles", new[] { "Privilege_PrivilegeId" });
            DropIndex("WorkExperiences", new[] { "ClientId" });
            DropIndex("WorkExperiences", new[] { "CityId" });
            DropIndex("TrackingDetails", new[] { "TrackingId" });
            DropIndex("TrackingDetails", new[] { "ShipmentTypeId" });
            DropIndex("Trackings", new[] { "VacantByCompanyId" });
            DropIndex("Trackings", new[] { "StateId" });
            DropIndex("Trackings", new[] { "CompanyId" });
            DropIndex("Trackings", new[] { "ClientId" });
            DropIndex("Trackings", new[] { "TrackingTypeId" });
            DropIndex("References", new[] { "ClientId" });
            DropIndex("References", new[] { "CityId" });
            DropIndex("References", new[] { "ReferenceTypeId" });
            DropIndex("KnownLanguages", new[] { "ClientId" });
            DropIndex("KnownLanguages", new[] { "LanguageLevelId" });
            DropIndex("KnownLanguages", new[] { "LanguageId" });
            DropIndex("KnownPrograms", new[] { "ClientId" });
            DropIndex("Users", new[] { "UserName" });
            DropIndex("Employees", new[] { "UserId" });
            DropIndex("Employees", new[] { "RoleId" });
            DropIndex("Employees", new[] { "CareerId" });
            DropIndex("VacantByCompanies", new[] { "InterviewTypeId" });
            DropIndex("VacantByCompanies", new[] { "VacantLevelId" });
            DropIndex("VacantByCompanies", new[] { "CityId" });
            DropIndex("VacantByCompanies", new[] { "CareerId" });
            DropIndex("VacantByCompanies", new[] { "AcademicLevelId" });
            DropIndex("VacantByCompanies", new[] { "CompanyId" });
            DropIndex("ContactByCompanies", new[] { "CompanyId" });
            DropIndex("Companies", new[] { "Name" });
            DropIndex("Clients", new[] { "CompanyId" });
            DropIndex("Clients", new[] { "EmployeeId" });
            DropIndex("Clients", new[] { "CityId" });
            DropIndex("Clients", new[] { "StateId" });
            DropIndex("Clients", new[] { "CorrelativeCode" });
            DropIndex("Cities", new[] { "CountryId" });
            DropIndex("Careers", new[] { "AcademicLevelId" });
            DropIndex("AcademicEducations", new[] { "ClientId" });
            DropIndex("AcademicEducations", new[] { "EducationTypeId" });
            DropIndex("AcademicEducations", new[] { "CareerId" });
            DropIndex("AcademicEducations", new[] { "AcademicLevelId" });
            DropIndex("AcademicEducations", new[] { "CityId" });
            DropTable("PrivilegeRoles");
            DropTable("ContractTemplates");
            DropTable("EducationTypes");
            DropTable("WorkExperiences");
            DropTable("TrackingTypes");
            DropTable("ShipmentTypes");
            DropTable("TrackingDetails");
            DropTable("Trackings");
            DropTable("States");
            DropTable("ReferenceTypes");
            DropTable("References");
            DropTable("LanguageLevels");
            DropTable("Languages");
            DropTable("KnownLanguages");
            DropTable("KnownPrograms");
            DropTable("Users");
            DropTable("Privileges");
            DropTable("Roles");
            DropTable("Employees");
            DropTable("VacantLevels");
            DropTable("InterviewTypes");
            DropTable("VacantByCompanies");
            DropTable("ContactByCompanies");
            DropTable("Companies");
            DropTable("Clients");
            DropTable("Countries");
            DropTable("Cities");
            DropTable("Careers");
            DropTable("AcademicLevels");
            DropTable("AcademicEducations");
        }
    }
}
