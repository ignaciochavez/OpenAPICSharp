using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entity
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageBase64String { get; set; }
        public int BiographyId { get; set; }
        public int PowerStatsId { get; set; }

        public Hero()
        {

        }

        public Hero(int id, string name, string description, string imageBase64String, int biographyId, int powerStatsId)
        {
            Id = id;
            Name = name;
            Description = description;
            ImageBase64String = imageBase64String;
            BiographyId = biographyId;
            PowerStatsId = powerStatsId;
        }
    }
}
