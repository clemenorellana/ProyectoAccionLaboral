namespace AccionLaboral.Migrations
{
    using AccionLaboral.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AccionLaboral.Models.AccionLaboralContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            //SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema)); //here s the thing.
        }

        protected override void Seed(AccionLaboral.Models.AccionLaboralContext context)
        {
            ApplicationRoleManager _roleManager = new ApplicationRoleManager(new RoleStore<Role>(context));

            context.CreateRole(_roleManager, "Asesor Corporativo", "ASCOR");
            context.CreateRole(_roleManager, "Asesor de Reclutamiento", "ASREC");
            context.CreateRole(_roleManager, "Asistente de Gerencia", "ASIGE");
            context.CreateRole(_roleManager, "Gerente de Agencia", "GTEAG");
            context.CreateRole(_roleManager, "Gerente General", "GTEGE");
            context.CreateRole(_roleManager, "Admin. del Sistema", "ADMIN");

            var countries = new List<Country>
            {
                new Country {CountryId=1, Name="Honduras"},
                new Country {CountryId=2, Name="Estados Unidos"},
                new Country {CountryId=3, Name="España"},
                new Country {CountryId=4, Name="Canadá"},
                new Country {CountryId=5, Name="Costa Rica"},
                new Country {CountryId=6, Name="México"},
                new Country {CountryId=7,Name= "El Salvador"}
            };
            countries.ForEach(s => context.Countries.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var cities = new List<City>
            {
                new City { CityId=1, Name="Colón",     CountryId=1},
                new City { CityId=2, Name="Comayagua", CountryId=1},
                new City { CityId=3, Name="Copán",     CountryId=1},
                new City { CityId=4, Name="Islas de la Bahia", CountryId=1},
                new City { CityId=5, Name="Lempira", CountryId=1},
                new City { CityId=6, Name="Olancho", CountryId=1},
                new City { CityId=7, Name="Santa Barbara", CountryId=1},
                new City { CityId=8, Name="Cortés", CountryId=1},
                new City { CityId=9, Name="Nueva York", CountryId=2},
                new City { CityId=10, Name="Tegucigalpa", CountryId=1 },
                new City { CityId=11, Name="San Pedro Sula", CountryId=1 },
                new City { CityId=12, Name="Villanueva",CountryId= 1},
                new City { CityId=13, Name="La Lima", CountryId=1 },
                new City { CityId=14, Name="Choloma", CountryId=1 },
                new City { CityId=15, Name="El Progreso", CountryId=1}
            };
            cities.ForEach(s => context.Cities.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var educationTypes = new List<EducationType>
            {
                new EducationType {EducationTypeId=1, Name="ACADEMICA"},
                new EducationType {EducationTypeId=2, Name= "ADICIONAL"}
            };
            educationTypes.ForEach(s => context.EducationTypes.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var interviewTypes = new List<InterviewType>
            {
                new InterviewType {InterviewTypeId=1, Name="Correo electrónico"},
                new InterviewType {InterviewTypeId=2, Name= "Videollamada"},
                new InterviewType {InterviewTypeId=3, Name= "Presencial"}
            };
            interviewTypes.ForEach(s => context.InterviewTypes.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var languageLevels = new List<LanguageLevel>
            {
                new LanguageLevel {LanguageLevelId=1, Name="Básico"},
                new LanguageLevel {LanguageLevelId=2, Name= "Intermedio"},
                new LanguageLevel {LanguageLevelId=3, Name= "Avanzado"}
            };
            languageLevels.ForEach(s => context.LanguageLevels.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var languages = new List<Language>
            {
                new Language {LanguageId=1, Name="Alemán"},
                new Language {LanguageId=2, Name= "Árabe"},
                new Language {LanguageId=3, Name= "Catalán"},
                new Language {LanguageId=4, Name="Español"},
                new Language {LanguageId=5, Name= "Francés"},
                new Language {LanguageId=6, Name= "Inglés"},
                new Language {LanguageId=7, Name="Irlandés"},
                new Language {LanguageId=8, Name= "Italiano"},
                new Language {LanguageId=9, Name= "Japonés"},
                new Language {LanguageId=10, Name="Mandarin"},
                new Language {LanguageId=11, Name= "Portugés"},
                new Language {LanguageId=12, Name= "Español"},
                new Language {LanguageId=13, Name="Inglés"},
                new Language {LanguageId=14, Name= "Intermedio"},
                new Language {LanguageId=15, Name= "Avanzado"}
            };
            languages.ForEach(s => context.Languages.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var referenceTypes = new List<ReferenceType>
            {
                new ReferenceType {ReferenceTypeId=1, Name="L"},
                new ReferenceType {ReferenceTypeId=2, Name= "P"}
            };
            referenceTypes.ForEach(s => context.ReferenceTypes.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var trackingTypes = new List<TrackingType>
            {
                new TrackingType {TrackingTypeId=1, Name="Inscripción"},
                new TrackingType {TrackingTypeId=2, Name= "Reclutamiento"}
            };
            trackingTypes.ForEach(s => context.TrackingTypes.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var vacantLevels = new List<VacantLevel>
            {
                new VacantLevel {VacantLevelId=1, Name="A"},
                new VacantLevel {VacantLevelId=2, Name= "B"},
                new VacantLevel {VacantLevelId=3, Name= "C"}
            };
            vacantLevels.ForEach(s => context.VacantLevels.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var states = new List<State>
            {
                new State {StateId=1, Name="Pre inscrito", Alias="PI"},
                new State {StateId=2, Name= "Inscrito", Alias="IN"},
                new State {StateId=3, Name= "Descartado", Alias="DE"},
                new State {StateId=4, Name= "Contratado", Alias="CO"},
                new State {StateId=5, Name= "Enviado a Entrevista", Alias="EE"},
                new State {StateId=6, Name= "Inactivo(Contratado)", Alias="IC"},
                new State {StateId=7, Name= "Rechazado", Alias="RE"}
            };
            states.ForEach(s => context.States.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
