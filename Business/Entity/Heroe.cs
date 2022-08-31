using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entity
{
    public class Heroe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgBase64String { get; set; }
        public DateTimeOffset Appearance { get; set; }
        public string Home { get; set; }
    }
}
