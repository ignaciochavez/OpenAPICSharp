using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class PowerStatsInsertDTO
    {
        public int Intelligence { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Durability { get; set; }
        public int Power { get; set; }
        public int Combat { get; set; }

        public PowerStatsInsertDTO()
        {

        }

        public PowerStatsInsertDTO(int intelligence, int strength, int speed, int durability, int power, int combat)
        {
            Intelligence = intelligence;
            Strength = strength;
            Speed = speed;
            Durability = durability;
            Power = power;
            Combat = combat;
        }
    }

    public class PowerStatsSearchDTO
    {
        public int Id { get; set; }
        public int Intelligence { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Durability { get; set; }
        public int Power { get; set; }
        public int Combat { get; set; }
        public ListPaginatedDTO ListPaginatedDTO { get; set; }

        public PowerStatsSearchDTO()
        {

        }

        public PowerStatsSearchDTO(int id, int intelligence, int strength, int speed, int durability, int power, int combat, ListPaginatedDTO listPaginatedDTO)
        {
            Id = id;
            Intelligence = intelligence;
            Strength = strength;
            Speed = speed;
            Durability = durability;
            Power = power;
            Combat = combat;
            ListPaginatedDTO = listPaginatedDTO;
        }
    }
}
