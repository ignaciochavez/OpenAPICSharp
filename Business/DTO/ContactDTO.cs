using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class ContactInsertDTO
    {
        public string Email { get; set; }
        public string Phone { get; set; }

        public ContactInsertDTO()
        {

        }

        public ContactInsertDTO(string email, string phone)
        {
            Email = email;
            Phone = phone;
        }
    }

    public class ContactSearchDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ListPaginatedDTO ListPaginatedDTO { get; set; }

        public ContactSearchDTO()
        {

        }

        public ContactSearchDTO(int id, string email, string phone, ListPaginatedDTO listPaginatedDTO)
        {
            Id = id;
            Email = email;
            Phone = phone;
            ListPaginatedDTO = listPaginatedDTO;
        }
    }
}
