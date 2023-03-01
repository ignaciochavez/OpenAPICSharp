using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entity
{
    public class SelectHero
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

        public SelectHero()
        {

        }

        public SelectHero(int heroId, string name, string description, string imageBase64String, int biographyId, string fullName, string gender, DateTime appearance, string alias, string publisher,
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

    public class SelectUser
    {
        // User
        public int UserId { get; set; }
        public string Rut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset Registered { get; set; }

        // Contact
        public int ContactId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Role
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public SelectUser()
        {

        }

        public SelectUser(int userId, string rut, string name, string lastName, DateTime birthDate, bool active, DateTimeOffset registered, int contactId, string email, string phone, int roleId, string roleName)
        {
            UserId = userId;
            Rut = rut;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Active = active;
            Registered = registered;
            ContactId = contactId;
            Email = email;
            Phone = phone;
            RoleId = roleId;
            RoleName = roleName;
        }
    }
}
