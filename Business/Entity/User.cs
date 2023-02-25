using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public System.DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public System.DateTimeOffset Registered { get; set; }
        public int ContactId { get; set; }
        public int RoleId { get; set; }

        public User()
        {

        }

        public User(int id, string rut, string name, string lastName, DateTime birthDate, bool active, DateTimeOffset registered, int contactId, int roleId)
        {
            Id = id;
            Rut = rut;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Active = active;
            Registered = Registered;
            ContactId = contactId;
            RoleId = roleId;
        }
    }
}
