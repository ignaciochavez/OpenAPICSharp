using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class HeroeInsertDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgBase64String { get; set; }
        public DateTimeOffset Appearance { get; set; }
        public string Home { get; set; }
    }

    public class HeroeUpdateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgBase64String { get; set; }
        public DateTimeOffset Appearance { get; set; }
        public string Home { get; set; }
    }

    public class HeroeListDTO
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
