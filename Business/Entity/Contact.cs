using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entity
{
    public class Contact
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Contact()
        {

        }

        public Contact(int id, string email, string phone)
        {
            Id = id;
            Email = email;
            Phone = phone;
        }
    }
}
