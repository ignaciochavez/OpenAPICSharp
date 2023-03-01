using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class ComicInsertHeroDTO
    {
        // Hero
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBase64String { get; set; }

        // Biography
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Appearance { get; set; }
        public string Alias { get; set; }
        public string Publisher { get; set; }

        // PowerStats
        public int Intelligence { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Durability { get; set; }
        public int Power { get; set; }
        public int Combat { get; set; }

        public ComicInsertHeroDTO()
        {

        }

        public ComicInsertHeroDTO(string name, string description, string imageBase64String, string fullName, string gender, DateTime appearance, string alias, string publisher, int intelligence, int strength,
                                    int speed, int durability, int power, int combat)
        {
            Name = name;
            Description = description;
            ImageBase64String = imageBase64String;
            FullName = fullName;
            Gender = gender;
            Appearance = appearance;
            Alias = alias;
            Publisher = publisher;
            Intelligence = intelligence;
            Strength = strength;
            Speed = speed;
            Durability = durability;
            Power = power;
            Combat = combat;
        }
    }

    public class ComicInsertUserDTO
    {
        // User
        public string Rut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }

        // Contact
        public string Email { get; set; }
        public string Phone { get; set; }

        // Role
        public int RoleId { get; set; }

        public ComicInsertUserDTO()
        {

        }

        public ComicInsertUserDTO(string rut, string name, string lastName, DateTime birthDate, string password, bool active, string email, string phone, int roleId)
        {
            Rut = rut;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Password = password;
            Active = active;
            Email = email;
            Phone = phone;
            RoleId = roleId;
        }
    }

    public class ComicLoginDTO
    {
        public string Rut { get; set; }
        public string Password { get; set; }

        public ComicLoginDTO()
        {

        }

        public ComicLoginDTO(string rut, string password)
        {
            Rut = rut;
            Password = password;
        }
    }

    public class ComicSelectUserDTO
    {
        public int Id { get; set; }
        public string TimeZoneInfoName { get; set; }

        public ComicSelectUserDTO()
        {

        }

        public ComicSelectUserDTO(int id, string timeZoneInfoName)
        {
            Id = id;
            TimeZoneInfoName = timeZoneInfoName;
        }
    }

    public class ComicUpdateHeroDTO
    {

        // Hero
        public int HeroId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBase64String { get; set; }

        // Biography
        public int BiographyId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Appearance { get; set; }
        public string Alias { get; set; }
        public string Publisher { get; set; }

        // PowerStats
        public int PowerStatsId { get; set; }
        public int Intelligence { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Durability { get; set; }
        public int Power { get; set; }
        public int Combat { get; set; }

        public ComicUpdateHeroDTO()
        {

        }

        public ComicUpdateHeroDTO(int heroId, string name, string description, string imageBase64String, int biographyId, string fullName, string gender, DateTime appearance, string alias, string publisher,
                            int powerStatsId, int intelligence, int strength, int speed, int durability, int power, int combat)
        {
            HeroId = heroId;
            Name = name;
            Description = description;
            ImageBase64String = imageBase64String;
            BiographyId = biographyId;
            FullName = fullName;
            Gender = gender;
            Appearance = appearance;
            Alias = alias;
            Publisher = publisher;
            PowerStatsId = powerStatsId;
            Intelligence = intelligence;
            Strength = strength;
            Speed = speed;
            Durability = durability;
            Power = power;
            Combat = combat;
        }
    }

    public class ComicUpdateUserDTO
    {
        public int UserId { get; set; }
        public string Rut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }

        // Contact
        public int ContactId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Role
        public int RoleId { get; set; }

        public ComicUpdateUserDTO()
        {

        }

        public ComicUpdateUserDTO(int userId, string rut, string name, string lastName, DateTime birthDate, string password, bool active,  int contactId, string email, string phone, int roleId)
        {
            UserId = userId;
            Rut = rut;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Password = password;
            Active = active;
            ContactId = contactId;
            Email = email;
            Phone = phone;
            RoleId = roleId;
        }
    }
}
