namespace AccionLaboral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
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
                    AcademicLevelId = c.Int(),
                    CareerId = c.Int(nullable: false),
                    EducationTypeId = c.Int(nullable: false),
                    ClientId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.AcademicEducationId)
                .ForeignKey("dbo.AcademicLevels", t => t.AcademicLevelId)
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
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.CorrelativeCode, unique: true)
                .Index(t => t.StateId)
                .Index(t => t.CityId)
                .Index(t => t.EmployeeId)
                .Index(t => t.CompanyId);

            CreateTable(
                "dbo.Companies",
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
                    CoveredDate = c.DateTime(nullable: false, precision: 0),
                    CoveredTime = c.String(unicode: false),
                    CompanyId = c.Int(nullable: false),
                    AcademicLevelId = c.Int(nullable: false),
                    CareerId = c.Int(nullable: false),
                    CityId = c.Int(nullable: false),
                    VacantLevelId = c.Int(nullable: false),
                    InterviewTypeId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.VacantByCompanyId)
                .ForeignKey("dbo.AcademicLevels", t => t.AcademicLevelId, cascadeDelete: true)
                .ForeignKey("dbo.Careers", t => t.CareerId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.InterviewTypes", t => t.InterviewTypeId, cascadeDelete: true)
                .ForeignKey("dbo.VacantLevels", t => t.VacantLevelId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.AcademicLevelId)
                .Index(t => t.CareerId)
                .Index(t => t.CityId)
                .Index(t => t.VacantLevelId)
                .Index(t => t.InterviewTypeId);

            CreateTable(
                "dbo.InterviewTypes",
                c => new
                {
                    InterviewTypeId = c.Int(nullable: false, identity: true),
                    Name = c.String(unicode: false),
                })
                .PrimaryKey(t => t.InterviewTypeId);

            CreateTable(
                "dbo.VacantLevels",
                c => new
                {
                    VacantLevelId = c.Int(nullable: false, identity: true),
                    Name = c.String(unicode: false),
                })
                .PrimaryKey(t => t.VacantLevelId);

            CreateTable(
                "dbo.Employees",
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
                    RoleId = c.String(maxLength: 128, storeType: "nvarchar"),
                    UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                    Photo = c.Binary(),
                })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Careers", t => t.CareerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CareerId)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    Alias = c.String(unicode: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    UserName = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    PasswordHash = c.String(unicode: false),
                    SecurityStamp = c.String(unicode: false),
                    Active = c.Boolean(defaultValue:true),
                    Busy = c.Boolean(defaultValue: true),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ClaimType = c.String(unicode: false),
                    ClaimValue = c.String(unicode: false),
                    User_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.KnownPrograms",
                c => new
                {
                    KnownProgramId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, unicode: false),
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
                    Name = c.String(nullable: false, unicode: false),
                })
                .PrimaryKey(t => t.ReferenceTypeId);

            CreateTable(
                "dbo.States",
                c => new
                {
                    StateId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, unicode: false),
                    Alias = c.String(maxLength: 2, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.StateId)
                .Index(t => t.Alias, unique: true);

            CreateTable(
                "dbo.Trackings",
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
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.TrackingTypes", t => t.TrackingTypeId)
                .ForeignKey("dbo.VacantByCompanies", t => t.VacantByCompanyId)
                .Index(t => t.TrackingTypeId)
                .Index(t => t.ClientId)
                .Index(t => t.CompanyId)
                .Index(t => t.StateId)
                .Index(t => t.VacantByCompanyId);

            CreateTable(
                "dbo.TrackingDetails",
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
                .ForeignKey("dbo.ShipmentTypes", t => t.ShipmentTypeId)
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
                "dbo.Privileges",
                c => new
                {
                    PrivilegeId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, unicode: false),
                })
                .PrimaryKey(t => t.PrivilegeId);

            CreateTable(
                "dbo.VacantCovereds",
                c => new
                {
                    VacantCoveredId = c.Int(nullable: false, identity: true),
                    VacantByCompanyId = c.Int(nullable: false),
                    EmployeeId = c.Int(nullable: false),
                    NumberOfProfiles = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.VacantCoveredId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.VacantByCompanies", t => t.VacantByCompanyId, cascadeDelete: true)
                .Index(t => t.VacantByCompanyId)
                .Index(t => t.EmployeeId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.VacantCovereds", "VacantByCompanyId", "dbo.VacantByCompanies");
            DropForeignKey("dbo.VacantCovereds", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AcademicEducations", "EducationTypeId", "dbo.EducationTypes");
            DropForeignKey("dbo.WorkExperiences", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.WorkExperiences", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Trackings", "VacantByCompanyId", "dbo.VacantByCompanies");
            DropForeignKey("dbo.Trackings", "TrackingTypeId", "dbo.TrackingTypes");
            DropForeignKey("dbo.TrackingDetails", "TrackingId", "dbo.Trackings");
            DropForeignKey("dbo.TrackingDetails", "ShipmentTypeId", "dbo.ShipmentTypes");
            DropForeignKey("dbo.Trackings", "StateId", "dbo.States");
            DropForeignKey("dbo.Trackings", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Trackings", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "StateId", "dbo.States");
            DropForeignKey("dbo.References", "ReferenceTypeId", "dbo.ReferenceTypes");
            DropForeignKey("dbo.References", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.References", "CityId", "dbo.Cities");
            DropForeignKey("dbo.KnownLanguages", "LanguageLevelId", "dbo.LanguageLevels");
            DropForeignKey("dbo.KnownLanguages", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.KnownLanguages", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.KnownPrograms", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Employees", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Employees", "CareerId", "dbo.Careers");
            DropForeignKey("dbo.Clients", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.VacantByCompanies", "VacantLevelId", "dbo.VacantLevels");
            DropForeignKey("dbo.VacantByCompanies", "InterviewTypeId", "dbo.InterviewTypes");
            DropForeignKey("dbo.VacantByCompanies", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.VacantByCompanies", "CityId", "dbo.Cities");
            DropForeignKey("dbo.VacantByCompanies", "CareerId", "dbo.Careers");
            DropForeignKey("dbo.VacantByCompanies", "AcademicLevelId", "dbo.AcademicLevels");
            DropForeignKey("dbo.ContactByCompanies", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Clients", "CityId", "dbo.Cities");
            DropForeignKey("dbo.AcademicEducations", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.AcademicEducations", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AcademicEducations", "CareerId", "dbo.Careers");
            DropForeignKey("dbo.AcademicEducations", "AcademicLevelId", "dbo.AcademicLevels");
            DropForeignKey("dbo.Careers", "AcademicLevelId", "dbo.AcademicLevels");
            DropIndex("dbo.VacantCovereds", new[] { "EmployeeId" });
            DropIndex("dbo.VacantCovereds", new[] { "VacantByCompanyId" });
            DropIndex("dbo.WorkExperiences", new[] { "ClientId" });
            DropIndex("dbo.WorkExperiences", new[] { "CityId" });
            DropIndex("dbo.TrackingDetails", new[] { "TrackingId" });
            DropIndex("dbo.TrackingDetails", new[] { "ShipmentTypeId" });
            DropIndex("dbo.Trackings", new[] { "VacantByCompanyId" });
            DropIndex("dbo.Trackings", new[] { "StateId" });
            DropIndex("dbo.Trackings", new[] { "CompanyId" });
            DropIndex("dbo.Trackings", new[] { "ClientId" });
            DropIndex("dbo.Trackings", new[] { "TrackingTypeId" });
            DropIndex("dbo.States", new[] { "Alias" });
            DropIndex("dbo.References", new[] { "ClientId" });
            DropIndex("dbo.References", new[] { "CityId" });
            DropIndex("dbo.References", new[] { "ReferenceTypeId" });
            DropIndex("dbo.KnownLanguages", new[] { "ClientId" });
            DropIndex("dbo.KnownLanguages", new[] { "LanguageLevelId" });
            DropIndex("dbo.KnownLanguages", new[] { "LanguageId" });
            DropIndex("dbo.KnownPrograms", new[] { "ClientId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.Employees", new[] { "UserId" });
            DropIndex("dbo.Employees", new[] { "RoleId" });
            DropIndex("dbo.Employees", new[] { "CareerId" });
            DropIndex("dbo.VacantByCompanies", new[] { "InterviewTypeId" });
            DropIndex("dbo.VacantByCompanies", new[] { "VacantLevelId" });
            DropIndex("dbo.VacantByCompanies", new[] { "CityId" });
            DropIndex("dbo.VacantByCompanies", new[] { "CareerId" });
            DropIndex("dbo.VacantByCompanies", new[] { "AcademicLevelId" });
            DropIndex("dbo.VacantByCompanies", new[] { "CompanyId" });
            DropIndex("dbo.ContactByCompanies", new[] { "CompanyId" });
            DropIndex("dbo.Companies", new[] { "Name" });
            DropIndex("dbo.Clients", new[] { "CompanyId" });
            DropIndex("dbo.Clients", new[] { "EmployeeId" });
            DropIndex("dbo.Clients", new[] { "CityId" });
            DropIndex("dbo.Clients", new[] { "StateId" });
            DropIndex("dbo.Clients", new[] { "CorrelativeCode" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Careers", new[] { "AcademicLevelId" });
            DropIndex("dbo.AcademicEducations", new[] { "ClientId" });
            DropIndex("dbo.AcademicEducations", new[] { "EducationTypeId" });
            DropIndex("dbo.AcademicEducations", new[] { "CareerId" });
            DropIndex("dbo.AcademicEducations", new[] { "AcademicLevelId" });
            DropIndex("dbo.AcademicEducations", new[] { "CityId" });
            DropTable("dbo.VacantCovereds");
            DropTable("dbo.Privileges");
            DropTable("dbo.ContractTemplates");
            DropTable("dbo.EducationTypes");
            DropTable("dbo.WorkExperiences");
            DropTable("dbo.TrackingTypes");
            DropTable("dbo.ShipmentTypes");
            DropTable("dbo.TrackingDetails");
            DropTable("dbo.Trackings");
            DropTable("dbo.States");
            DropTable("dbo.ReferenceTypes");
            DropTable("dbo.References");
            DropTable("dbo.LanguageLevels");
            DropTable("dbo.Languages");
            DropTable("dbo.KnownLanguages");
            DropTable("dbo.KnownPrograms");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Employees");
            DropTable("dbo.VacantLevels");
            DropTable("dbo.InterviewTypes");
            DropTable("dbo.VacantByCompanies");
            DropTable("dbo.ContactByCompanies");
            DropTable("dbo.Companies");
            DropTable("dbo.Clients");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Careers");
            DropTable("dbo.AcademicLevels");
            DropTable("dbo.AcademicEducations");
        }
    }
}
