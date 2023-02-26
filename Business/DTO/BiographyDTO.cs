using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class BiographyInsertDTO
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Appearance { get; set; }
        public string Alias { get; set; }
        public string Publisher { get; set; }

        public BiographyInsertDTO()
        {

        }

        public BiographyInsertDTO(string fullName, string gender, DateTime appearance, string alias, string publisher)
        {
            FullName = fullName;
            Gender = gender;
            Appearance = appearance;
            Alias = alias;
            Publisher = publisher;
        }
    }

    public class BiographySearchDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Appearance { get; set; }
        public string Alias { get; set; }
        public string Publisher { get; set; }
        public ListPaginatedDTO ListPaginatedDTO { get; set; }

        public BiographySearchDTO()
        {

        }

        public BiographySearchDTO(int id, string fullName, string gender, DateTime appearance, string alias, string publisher, ListPaginatedDTO listPaginatedDTO)
        {
            Id = id;
            FullName = fullName;
            Gender = gender;
            Appearance = appearance;
            Alias = alias;
            Publisher = publisher;
            ListPaginatedDTO = listPaginatedDTO;
        }
    }
}
