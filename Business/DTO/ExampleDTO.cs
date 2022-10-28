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

    public class ExampleListDTO
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ExampleExistByRutAndNotSameEntityDTO
    {
        public int Id { get; set; }
        public string Rut { get; set; }
    }

    public class ExampleExcelDTO
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }

    public class ExamplePDFDTO
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
