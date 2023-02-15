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

        public ExampleInsertDTO()
        {

        }

        public ExampleInsertDTO(string rut, string name, string lastName, DateTimeOffset birthDate, bool active, string password)
        {
            Rut = rut;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Active = active;
            Password = password;
        }
    }

    public class ExampleListDTO
    {
        [Required]
        public int PageIndex { get; set; }

        [Required]
        public int PageSize { get; set; }

        public ExampleListDTO()
        {

        }

        public ExampleListDTO(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    public class ExampleExistByRutAndNotSameEntityDTO
    {
        public int Id { get; set; }
        public string Rut { get; set; }

        public ExampleExistByRutAndNotSameEntityDTO()
        {

        }

        public ExampleExistByRutAndNotSameEntityDTO(int id, string rut)
        {
            Id = id;
            Rut = rut;
        }
    }

    public class ExampleExcelDTO
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        public ExampleExcelDTO()
        {

        }

        public ExampleExcelDTO(string fileName, byte[] fileContent)
        {
            FileName = fileName;
            FileContent = fileContent;
        }
    }

    public class ExamplePDFDTO
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        public ExamplePDFDTO()
        {

        }

        public ExamplePDFDTO(string fileName, byte[] fileContent)
        {
            FileName = fileName;
            FileContent = fileContent;
        }
    }
}
