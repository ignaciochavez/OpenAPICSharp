using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class ContactoInsertDTO
    {
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }

        public ContactoInsertDTO()
        {

        }

        public ContactoInsertDTO(string correoElectronico, string telefono)
        {
            CorreoElectronico = correoElectronico;
            Telefono = telefono;
        }
    }

    public class ContactoSearchDTO
    {
        public int Id { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public ListPaginatedDTO ListPaginatedDTO { get; set; }

        public ContactoSearchDTO()
        {

        }

        public ContactoSearchDTO(int id, string correoElectronico, string telefono, ListPaginatedDTO listPaginatedDTO)
        {
            Id = id;
            CorreoElectronico = correoElectronico;
            Telefono = telefono;
            ListPaginatedDTO = listPaginatedDTO;
        }
    }
}
