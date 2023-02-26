using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entity
{
    public class Biography
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Appearance { get; set; }
        public string Alias { get; set; }
        public string Publisher { get; set; }

        public Biography()
        {

        }

        public Biography(int id, string fullName, string gender, DateTime appearance, string alias, string publisher)
        {
            Id = id;
            FullName = fullName;
            Gender = gender;
            Appearance = appearance;
            Alias = alias;
            Publisher = publisher;
        }
    }
}
