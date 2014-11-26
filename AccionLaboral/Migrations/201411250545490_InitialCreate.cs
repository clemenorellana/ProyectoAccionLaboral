namespace AccionLaboral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicEducations",
                c => new
                    {
                        AcademicEducationId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        InstitutionName = c.String(unicode: false),
                        TrainingName = c.String(unicode: false),
                        CityId = c.Int(nullable: false),
                        AcademicLevelId = c.Int(nullable: false),
                        CareerId = c.Int(nullable: false),
                        EducationTypeId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AcademicEducationId)
                .ForeignKey("dbo.AcademicLevels", t => t.AcademicLevelId, cascadeDelete: true)
                .ForeignKey("dbo.Careers", t => t.CareerId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.EducationTypes", t => t.EducationTypeId, cascadeDelete: true)
                .Index(t => t.CityId)
                .Index(t => t.AcademicLevelId)
                .Index(t => t.CareerId)
                .Index(t => t.EducationTypeId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.AcademicLevels",
                c => new
                    {
                        AcademicLevelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.AcademicLevelId);
            
            CreateTable(
                "dbo.Careers",
                c => new
                    {
                        CareerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        AcademicLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CareerId)
                .ForeignKey("dbo.AcademicLevels", t => t.AcademicLevelId, cascadeDelete: true)
                .Index(t => t.AcademicLevelId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        CorrelativeCode = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        FirstName = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        LastName = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
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
                        RejectionDesciption = c.String(unicode: false),
                        CurrentStateId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        AdvisorId = c.Int(nullable: false),
                        Advisor_EmployeeId = c.Int(),
                        CurrentState_StateId = c.Int(),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Employees", t => t.Advisor_EmployeeId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.CurrentState_StateId)
                .Index(t => t.CorrelativeCode, unique: true)
                .Index(t => new { t.FirstName, t.LastName }, unique: true, name: "IX_ClientFirstNameLastName")
                .Index(t => t.CityId)
                .Index(t => t.Advisor_EmployeeId)
                .Index(t => t.CurrentState_StateId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
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
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Careers", t => t.CareerId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => new { t.FirstName, t.LastName }, unique: true, name: "IX_EmployeeFirstNameLastName")
                .Index(t => t.CareerId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, unicode: false),
                        Password = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Privileges",
                c => new
                    {
                        PrivilegeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.PrivilegeId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.KnownPrograms",
                c => new
                    {
                        KnownProgramId = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KnownProgramId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.KnownLanguages",
                c => new
                    {
                        KnownLanguageId = c.Int(nullable: false, identity: true),
                        Percentage = c.Double(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        LanguageLevelId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KnownLanguageId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.LanguageLevels", t => t.LanguageLevelId, cascadeDelete: true)
                .Index(t => t.LanguageId)
                .Index(t => t.LanguageLevelId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "dbo.LanguageLevels",
                c => new
                    {
                        LanguageLevelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.LanguageLevelId);
            
            CreateTable(
                "dbo.References",
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
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.ReferenceTypes", t => t.ReferenceTypeId, cascadeDelete: true)
                .Index(t => t.ReferenceTypeId)
                .Index(t => t.CityId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.ReferenceTypes",
                c => new
                    {
                        ReferenceTypeId = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReferenceTypeId);
            
            CreateTable(
                "dbo.Trackings",
                c => new
                    {
                        TrackingId = c.Int(nullable: false, identity: true),
                        TrackingTypeId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        VacantByCompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrackingId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.TrackingTypes", t => t.TrackingTypeId, cascadeDelete: true)
                .ForeignKey("dbo.VacantByCompanies", t => t.VacantByCompanyId, cascadeDelete: true)
                .Index(t => t.TrackingTypeId)
                .Index(t => t.ClientId)
                .Index(t => t.CompanyId)
                .Index(t => t.StateId)
                .Index(t => t.VacantByCompanyId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Area = c.String(unicode: false),
                        DateCreated = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.CompanyId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ContactByCompanies",
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
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.VacantByCompanies",
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
                    })
                .PrimaryKey(t => t.VacantByCompanyId)
                .ForeignKey("dbo.AcademicLevels", t => t.AcademicLevelId, cascadeDelete: true)
                .ForeignKey("dbo.Careers", t => t.CareerId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.VacantLevels", t => t.VacantLevelId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.AcademicLevelId)
                .Index(t => t.CareerId)
                .Index(t => t.CityId)
                .Index(t => t.VacantLevelId);
            
            CreateTable(
                "dbo.VacantLevels",
                c => new
                    {
                        VacantLevelId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.VacantLevelId);
            
            CreateTable(
                "dbo.TrackingDetails",
                c => new
                    {
                        TrackingDetailId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 0),
                        Description = c.String(nullable: false, unicode: false),
                        ShipmentTypeId = c.Int(nullable: false),
                        TrackingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrackingDetailId)
                .ForeignKey("dbo.ShipmentTypes", t => t.ShipmentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Trackings", t => t.TrackingId, cascadeDelete: true)
                .Index(t => t.ShipmentTypeId)
                .Index(t => t.TrackingId);
            
            CreateTable(
                "dbo.ShipmentTypes",
                c => new
                    {
                        ShipmentTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.ShipmentTypeId);
            
            CreateTable(
                "dbo.TrackingTypes",
                c => new
                    {
                        TrackingTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.TrackingTypeId);
            
            CreateTable(
                "dbo.WorkExperiences",
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
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.CityId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.EducationTypes",
                c => new
                    {
                        EducationTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.EducationTypeId);
            
            CreateTable(
                "dbo.ContractTemplates",
                c => new
                    {
                        ContractTemplateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(nullable: false, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContractTemplateId);
            
            CreateTable(
                "dbo.PrivilegeRoles",
                c => new
                    {
                        Privilege_PrivilegeId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Privilege_PrivilegeId, t.Role_RoleId })
                .ForeignKey("dbo.Privileges", t => t.Privilege_PrivilegeId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.Privilege_PrivilegeId)
                .Index(t => t.Role_RoleId);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_RoleId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleId, t.User_UserId })
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Role_RoleId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AcademicEducations", "EducationTypeId", "dbo.EducationTypes");
            DropForeignKey("dbo.WorkExperiences", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.WorkExperiences", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Trackings", "VacantByCompanyId", "dbo.VacantByCompanies");
            DropForeignKey("dbo.Trackings", "TrackingTypeId", "dbo.TrackingTypes");
            DropForeignKey("dbo.TrackingDetails", "TrackingId", "dbo.Trackings");
            DropForeignKey("dbo.TrackingDetails", "ShipmentTypeId", "dbo.ShipmentTypes");
            DropForeignKey("dbo.Trackings", "StateId", "dbo.States");
            DropForeignKey("dbo.Trackings", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.VacantByCompanies", "VacantLevelId", "dbo.VacantLevels");
            DropForeignKey("dbo.VacantByCompanies", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.VacantByCompanies", "CityId", "dbo.Cities");
            DropForeignKey("dbo.VacantByCompanies", "CareerId", "dbo.Careers");
            DropForeignKey("dbo.VacantByCompanies", "AcademicLevelId", "dbo.AcademicLevels");
            DropForeignKey("dbo.ContactByCompanies", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Trackings", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.References", "ReferenceTypeId", "dbo.ReferenceTypes");
            DropForeignKey("dbo.References", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.References", "CityId", "dbo.Cities");
            DropForeignKey("dbo.KnownLanguages", "LanguageLevelId", "dbo.LanguageLevels");
            DropForeignKey("dbo.KnownLanguages", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.KnownLanguages", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.KnownPrograms", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "CurrentState_StateId", "dbo.States");
            DropForeignKey("dbo.Clients", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Clients", "Advisor_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.PrivilegeRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.PrivilegeRoles", "Privilege_PrivilegeId", "dbo.Privileges");
            DropForeignKey("dbo.Employees", "CareerId", "dbo.Careers");
            DropForeignKey("dbo.AcademicEducations", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.AcademicEducations", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AcademicEducations", "CareerId", "dbo.Careers");
            DropForeignKey("dbo.Careers", "AcademicLevelId", "dbo.AcademicLevels");
            DropForeignKey("dbo.AcademicEducations", "AcademicLevelId", "dbo.AcademicLevels");
            DropIndex("dbo.RoleUsers", new[] { "User_UserId" });
            DropIndex("dbo.RoleUsers", new[] { "Role_RoleId" });
            DropIndex("dbo.PrivilegeRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.PrivilegeRoles", new[] { "Privilege_PrivilegeId" });
            DropIndex("dbo.WorkExperiences", new[] { "ClientId" });
            DropIndex("dbo.WorkExperiences", new[] { "CityId" });
            DropIndex("dbo.TrackingDetails", new[] { "TrackingId" });
            DropIndex("dbo.TrackingDetails", new[] { "ShipmentTypeId" });
            DropIndex("dbo.VacantByCompanies", new[] { "VacantLevelId" });
            DropIndex("dbo.VacantByCompanies", new[] { "CityId" });
            DropIndex("dbo.VacantByCompanies", new[] { "CareerId" });
            DropIndex("dbo.VacantByCompanies", new[] { "AcademicLevelId" });
            DropIndex("dbo.VacantByCompanies", new[] { "CompanyId" });
            DropIndex("dbo.ContactByCompanies", new[] { "CompanyId" });
            DropIndex("dbo.Companies", new[] { "Name" });
            DropIndex("dbo.Trackings", new[] { "VacantByCompanyId" });
            DropIndex("dbo.Trackings", new[] { "StateId" });
            DropIndex("dbo.Trackings", new[] { "CompanyId" });
            DropIndex("dbo.Trackings", new[] { "ClientId" });
            DropIndex("dbo.Trackings", new[] { "TrackingTypeId" });
            DropIndex("dbo.References", new[] { "ClientId" });
            DropIndex("dbo.References", new[] { "CityId" });
            DropIndex("dbo.References", new[] { "ReferenceTypeId" });
            DropIndex("dbo.KnownLanguages", new[] { "ClientId" });
            DropIndex("dbo.KnownLanguages", new[] { "LanguageLevelId" });
            DropIndex("dbo.KnownLanguages", new[] { "LanguageId" });
            DropIndex("dbo.KnownPrograms", new[] { "ClientId" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.Employees", new[] { "UserId" });
            DropIndex("dbo.Employees", new[] { "CareerId" });
            DropIndex("dbo.Employees", "IX_EmployeeFirstNameLastName");
            DropIndex("dbo.Clients", new[] { "CurrentState_StateId" });
            DropIndex("dbo.Clients", new[] { "Advisor_EmployeeId" });
            DropIndex("dbo.Clients", new[] { "CityId" });
            DropIndex("dbo.Clients", "IX_ClientFirstNameLastName");
            DropIndex("dbo.Clients", new[] { "CorrelativeCode" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Careers", new[] { "AcademicLevelId" });
            DropIndex("dbo.AcademicEducations", new[] { "ClientId" });
            DropIndex("dbo.AcademicEducations", new[] { "EducationTypeId" });
            DropIndex("dbo.AcademicEducations", new[] { "CareerId" });
            DropIndex("dbo.AcademicEducations", new[] { "AcademicLevelId" });
            DropIndex("dbo.AcademicEducations", new[] { "CityId" });
            DropTable("dbo.RoleUsers");
            DropTable("dbo.PrivilegeRoles");
            DropTable("dbo.ContractTemplates");
            DropTable("dbo.EducationTypes");
            DropTable("dbo.WorkExperiences");
            DropTable("dbo.TrackingTypes");
            DropTable("dbo.ShipmentTypes");
            DropTable("dbo.TrackingDetails");
            DropTable("dbo.VacantLevels");
            DropTable("dbo.VacantByCompanies");
            DropTable("dbo.ContactByCompanies");
            DropTable("dbo.Companies");
            DropTable("dbo.Trackings");
            DropTable("dbo.ReferenceTypes");
            DropTable("dbo.References");
            DropTable("dbo.LanguageLevels");
            DropTable("dbo.Languages");
            DropTable("dbo.KnownLanguages");
            DropTable("dbo.KnownPrograms");
            DropTable("dbo.States");
            DropTable("dbo.Privileges");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Employees");
            DropTable("dbo.Clients");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Careers");
            DropTable("dbo.AcademicLevels");
            DropTable("dbo.AcademicEducations");
        }
    }
}
