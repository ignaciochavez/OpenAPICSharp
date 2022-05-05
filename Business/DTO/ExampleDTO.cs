using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class ExampleInsertDTO
    {
        public string Rut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
    }

    public class ExampleUpdateDTO
    {
        public string Rut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
    }

    public class ExampleListDTO
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
