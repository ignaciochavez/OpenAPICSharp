﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataSource.Comic
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ComicEntities : DbContext
    {
        public ComicEntities()
            : base("name=ComicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Biography> Biography { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Hero> Hero { get; set; }
        public virtual DbSet<PowerStats> PowerStats { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
    
        public virtual int SPDeleteHero(Nullable<int> heroId)
        {
            var heroIdParameter = heroId.HasValue ?
                new ObjectParameter("HeroId", heroId) :
                new ObjectParameter("HeroId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPDeleteHero", heroIdParameter);
        }
    
        public virtual int SPDeleteUser(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPDeleteUser", userIdParameter);
        }
    
        public virtual int SPInsertHero(string name, string description, string imagePath, string fullName, string gender, Nullable<System.DateTime> appearance, string alias, string publisher, Nullable<int> intelligence, Nullable<int> strength, Nullable<int> speed, Nullable<int> durability, Nullable<int> power, Nullable<int> combat)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var imagePathParameter = imagePath != null ?
                new ObjectParameter("ImagePath", imagePath) :
                new ObjectParameter("ImagePath", typeof(string));
    
            var fullNameParameter = fullName != null ?
                new ObjectParameter("FullName", fullName) :
                new ObjectParameter("FullName", typeof(string));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            var appearanceParameter = appearance.HasValue ?
                new ObjectParameter("Appearance", appearance) :
                new ObjectParameter("Appearance", typeof(System.DateTime));
    
            var aliasParameter = alias != null ?
                new ObjectParameter("Alias", alias) :
                new ObjectParameter("Alias", typeof(string));
    
            var publisherParameter = publisher != null ?
                new ObjectParameter("Publisher", publisher) :
                new ObjectParameter("Publisher", typeof(string));
    
            var intelligenceParameter = intelligence.HasValue ?
                new ObjectParameter("Intelligence", intelligence) :
                new ObjectParameter("Intelligence", typeof(int));
    
            var strengthParameter = strength.HasValue ?
                new ObjectParameter("Strength", strength) :
                new ObjectParameter("Strength", typeof(int));
    
            var speedParameter = speed.HasValue ?
                new ObjectParameter("Speed", speed) :
                new ObjectParameter("Speed", typeof(int));
    
            var durabilityParameter = durability.HasValue ?
                new ObjectParameter("Durability", durability) :
                new ObjectParameter("Durability", typeof(int));
    
            var powerParameter = power.HasValue ?
                new ObjectParameter("Power", power) :
                new ObjectParameter("Power", typeof(int));
    
            var combatParameter = combat.HasValue ?
                new ObjectParameter("Combat", combat) :
                new ObjectParameter("Combat", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPInsertHero", nameParameter, descriptionParameter, imagePathParameter, fullNameParameter, genderParameter, appearanceParameter, aliasParameter, publisherParameter, intelligenceParameter, strengthParameter, speedParameter, durabilityParameter, powerParameter, combatParameter);
        }
    
        public virtual int SPInsertUser(string rut, string name, string lastName, Nullable<System.DateTime> birthDate, string password, Nullable<bool> active, Nullable<System.DateTimeOffset> registered, string email, Nullable<decimal> phone, string address, Nullable<int> roleId)
        {
            var rutParameter = rut != null ?
                new ObjectParameter("Rut", rut) :
                new ObjectParameter("Rut", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var birthDateParameter = birthDate.HasValue ?
                new ObjectParameter("BirthDate", birthDate) :
                new ObjectParameter("BirthDate", typeof(System.DateTime));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var activeParameter = active.HasValue ?
                new ObjectParameter("Active", active) :
                new ObjectParameter("Active", typeof(bool));
    
            var registeredParameter = registered.HasValue ?
                new ObjectParameter("Registered", registered) :
                new ObjectParameter("Registered", typeof(System.DateTimeOffset));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var phoneParameter = phone.HasValue ?
                new ObjectParameter("Phone", phone) :
                new ObjectParameter("Phone", typeof(decimal));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPInsertUser", rutParameter, nameParameter, lastNameParameter, birthDateParameter, passwordParameter, activeParameter, registeredParameter, emailParameter, phoneParameter, addressParameter, roleIdParameter);
        }
    
        public virtual ObjectResult<SPListHero_Result> SPListHero()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPListHero_Result>("SPListHero");
        }
    
        public virtual ObjectResult<SPListHeroPaginated_Result> SPListHeroPaginated(Nullable<int> pageIndex, Nullable<int> pageSize)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPListHeroPaginated_Result>("SPListHeroPaginated", pageIndexParameter, pageSizeParameter);
        }
    
        public virtual ObjectResult<SPListUser_Result> SPListUser(string timeZoneInfoName)
        {
            var timeZoneInfoNameParameter = timeZoneInfoName != null ?
                new ObjectParameter("TimeZoneInfoName", timeZoneInfoName) :
                new ObjectParameter("TimeZoneInfoName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPListUser_Result>("SPListUser", timeZoneInfoNameParameter);
        }
    
        public virtual ObjectResult<SPListUserPaginated_Result> SPListUserPaginated(Nullable<int> pageIndex, Nullable<int> pageSize, string timeZoneInfoName)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var timeZoneInfoNameParameter = timeZoneInfoName != null ?
                new ObjectParameter("TimeZoneInfoName", timeZoneInfoName) :
                new ObjectParameter("TimeZoneInfoName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPListUserPaginated_Result>("SPListUserPaginated", pageIndexParameter, pageSizeParameter, timeZoneInfoNameParameter);
        }
    
        public virtual ObjectResult<SPLogin_Result> SPLogin(string userRut, string userPassword)
        {
            var userRutParameter = userRut != null ?
                new ObjectParameter("UserRut", userRut) :
                new ObjectParameter("UserRut", typeof(string));
    
            var userPasswordParameter = userPassword != null ?
                new ObjectParameter("UserPassword", userPassword) :
                new ObjectParameter("UserPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPLogin_Result>("SPLogin", userRutParameter, userPasswordParameter);
        }
    
        public virtual ObjectResult<SPSelectHero_Result> SPSelectHero(Nullable<int> heroeId)
        {
            var heroeIdParameter = heroeId.HasValue ?
                new ObjectParameter("HeroeId", heroeId) :
                new ObjectParameter("HeroeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPSelectHero_Result>("SPSelectHero", heroeIdParameter);
        }
    
        public virtual ObjectResult<SPSelectUser_Result> SPSelectUser(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SPSelectUser_Result>("SPSelectUser", userIdParameter);
        }
    
        public virtual int SPUpdateHero(Nullable<int> heroId, string name, string description, string imagePath, Nullable<int> biographyId, string fullName, string gender, Nullable<System.DateTime> appearance, string alias, string publisher, Nullable<int> powerStatsId, Nullable<int> intelligence, Nullable<int> strength, Nullable<int> speed, Nullable<int> durability, Nullable<int> power, Nullable<int> combat)
        {
            var heroIdParameter = heroId.HasValue ?
                new ObjectParameter("HeroId", heroId) :
                new ObjectParameter("HeroId", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var imagePathParameter = imagePath != null ?
                new ObjectParameter("ImagePath", imagePath) :
                new ObjectParameter("ImagePath", typeof(string));
    
            var biographyIdParameter = biographyId.HasValue ?
                new ObjectParameter("BiographyId", biographyId) :
                new ObjectParameter("BiographyId", typeof(int));
    
            var fullNameParameter = fullName != null ?
                new ObjectParameter("FullName", fullName) :
                new ObjectParameter("FullName", typeof(string));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            var appearanceParameter = appearance.HasValue ?
                new ObjectParameter("Appearance", appearance) :
                new ObjectParameter("Appearance", typeof(System.DateTime));
    
            var aliasParameter = alias != null ?
                new ObjectParameter("Alias", alias) :
                new ObjectParameter("Alias", typeof(string));
    
            var publisherParameter = publisher != null ?
                new ObjectParameter("Publisher", publisher) :
                new ObjectParameter("Publisher", typeof(string));
    
            var powerStatsIdParameter = powerStatsId.HasValue ?
                new ObjectParameter("PowerStatsId", powerStatsId) :
                new ObjectParameter("PowerStatsId", typeof(int));
    
            var intelligenceParameter = intelligence.HasValue ?
                new ObjectParameter("Intelligence", intelligence) :
                new ObjectParameter("Intelligence", typeof(int));
    
            var strengthParameter = strength.HasValue ?
                new ObjectParameter("Strength", strength) :
                new ObjectParameter("Strength", typeof(int));
    
            var speedParameter = speed.HasValue ?
                new ObjectParameter("Speed", speed) :
                new ObjectParameter("Speed", typeof(int));
    
            var durabilityParameter = durability.HasValue ?
                new ObjectParameter("Durability", durability) :
                new ObjectParameter("Durability", typeof(int));
    
            var powerParameter = power.HasValue ?
                new ObjectParameter("Power", power) :
                new ObjectParameter("Power", typeof(int));
    
            var combatParameter = combat.HasValue ?
                new ObjectParameter("Combat", combat) :
                new ObjectParameter("Combat", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPUpdateHero", heroIdParameter, nameParameter, descriptionParameter, imagePathParameter, biographyIdParameter, fullNameParameter, genderParameter, appearanceParameter, aliasParameter, publisherParameter, powerStatsIdParameter, intelligenceParameter, strengthParameter, speedParameter, durabilityParameter, powerParameter, combatParameter);
        }
    
        public virtual int SPUpdateUser(Nullable<int> userId, string rut, string name, string lastName, Nullable<System.DateTime> birthDate, string password, Nullable<bool> active, Nullable<int> contactId, string email, Nullable<decimal> phone, string address, Nullable<int> roleId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var rutParameter = rut != null ?
                new ObjectParameter("Rut", rut) :
                new ObjectParameter("Rut", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var birthDateParameter = birthDate.HasValue ?
                new ObjectParameter("BirthDate", birthDate) :
                new ObjectParameter("BirthDate", typeof(System.DateTime));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var activeParameter = active.HasValue ?
                new ObjectParameter("Active", active) :
                new ObjectParameter("Active", typeof(bool));
    
            var contactIdParameter = contactId.HasValue ?
                new ObjectParameter("ContactId", contactId) :
                new ObjectParameter("ContactId", typeof(int));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var phoneParameter = phone.HasValue ?
                new ObjectParameter("Phone", phone) :
                new ObjectParameter("Phone", typeof(decimal));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            var roleIdParameter = roleId.HasValue ?
                new ObjectParameter("RoleId", roleId) :
                new ObjectParameter("RoleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPUpdateUser", userIdParameter, rutParameter, nameParameter, lastNameParameter, birthDateParameter, passwordParameter, activeParameter, contactIdParameter, emailParameter, phoneParameter, addressParameter, roleIdParameter);
        }
    }
}
