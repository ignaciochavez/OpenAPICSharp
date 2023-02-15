using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entity
{
    public class Example
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Rut { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset BirthDate { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public string Password { get; set; }

        public Example()
        {

        }

        public Example(int id, string rut, string name, string lastName, DateTimeOffset birthDate, bool active, string password)
        {
            Id = id;
            Rut = rut;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Active = active;
            Password = password;
        }
    }
}
