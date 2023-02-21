using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class RolSearchDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ListPaginatedDTO ListPaginatedDTO { get; set; }

        public RolSearchDTO()
        {

        }

        public RolSearchDTO(int id, string nombre, ListPaginatedDTO listPaginatedDTO)
        {
            Id = id;
            Nombre = nombre;
            ListPaginatedDTO = listPaginatedDTO;
        }
    }
}
