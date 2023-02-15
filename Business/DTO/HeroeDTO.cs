using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class HeroeInsertDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Home { get; set; }

        [Required]
        public DateTimeOffset Appearance { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImgBase64String { get; set; }

        public HeroeInsertDTO()
        {

        }

        public HeroeInsertDTO(string name, string home, DateTimeOffset appearance, string description, string imgBase64String)
        {
            Name = name;
            Home = home;
            Appearance = appearance;
            Description = description;
            ImgBase64String = imgBase64String;
        }
    }

    public class HeroeListDTO
    {
        [Required]
        public int PageIndex { get; set; }

        [Required]
        public int PageSize { get; set; }

        public HeroeListDTO()
        {

        }

        public HeroeListDTO(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    public class HeroeExistByNameAndNotSameEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HeroeExistByNameAndNotSameEntityDTO()
        {

        }

        public HeroeExistByNameAndNotSameEntityDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class HeroeExcelDTO
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        public HeroeExcelDTO()
        {

        }

        public HeroeExcelDTO(string fileName, byte[] fileContent)
        {
            FileName = fileName;
            FileContent = fileContent;
        }
    }

    public class HeroePDFDTO
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        public HeroePDFDTO()
        {

        }

        public HeroePDFDTO(string fileName, byte[] fileContent)
        {
            FileName = fileName;
            FileContent = fileContent;
        }
    }
}
