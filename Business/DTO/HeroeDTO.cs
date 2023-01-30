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
    }

    public class HeroeListDTO
    {
        [Required]
        public int PageIndex { get; set; }

        [Required]
        public int PageSize { get; set; }
    }

    public class HeroeExistByNameAndNotSameEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class HeroeExcelDTO
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }
    }

    public class HeroePDFDTO
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileContent { get; set; }
    }
}
