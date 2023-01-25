using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class ExampleInsertDTO
    {
        [Required]
        public string Rut { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset BirthDate { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class ExampleListDTO
    {
        [Required]
        public int PageIndex { get; set; }

        [Required]
        public int PageSize { get; set; }
    }

    public class ExampleExistByRutAndNotSameEntityDTO
    {
        public int Id { get; set; }
        public string Rut { get; set; }
    }

    public class ExampleExcelDTO
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }
    }

    public class ExamplePDFDTO
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }
    }
}
