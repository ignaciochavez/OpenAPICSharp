using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class RoleSearchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ListPaginatedDTO ListPaginatedDTO { get; set; }

        public RoleSearchDTO()
        {

        }

        public RoleSearchDTO(int id, string name, ListPaginatedDTO listPaginatedDTO)
        {
            Id = id;
            Name = name;
            ListPaginatedDTO = listPaginatedDTO;
        }
    }
}
