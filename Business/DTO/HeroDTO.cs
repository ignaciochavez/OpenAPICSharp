using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class HeroInsertDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBase64String { get; set; }
        public int BiographyId { get; set; }
        public int PowerStatsId { get; set; }

        public HeroInsertDTO()
        {

        }

        public HeroInsertDTO(string name, string description, string imageBase64String, int biographyId, int powerStatsId)
        {
            Name = name;
            Description = description;
            ImageBase64String = imageBase64String;
            BiographyId = biographyId;
            PowerStatsId = powerStatsId;
        }
    }

    public class HeroUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBase64String { get; set; }
        public int BiographyId { get; set; }
        public int PowerStatsId { get; set; }

        public HeroUpdateDTO()
        {

        }

        public HeroUpdateDTO(int id, string name, string description, string imageBase64String, int biographyId, int powerStatsId)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageBase64String = imageBase64String;
            BiographyId = biographyId;
            PowerStatsId = powerStatsId;
        }
    }

    public class HeroSearchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BiographyId { get; set; }
        public int PowerStatsId { get; set; }
        public ListPaginatedDTO ListPaginatedDTO { get; set; }

        public HeroSearchDTO()
        {

        }

        public HeroSearchDTO(int id, string name, string description, int biographyId, int powerStatsId, ListPaginatedDTO listPaginatedDTO)
        {
            Id = id;
            Name = name;
            Description = description;
            BiographyId = biographyId;
            PowerStatsId = powerStatsId;
            ListPaginatedDTO = listPaginatedDTO;
        }
    }

    public class HeroExistByNameAndNotSameEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HeroExistByNameAndNotSameEntityDTO()
        {

        }

        public HeroExistByNameAndNotSameEntityDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
