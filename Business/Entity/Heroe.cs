using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entity
{
    public class Heroe
    {
        [Required]
        public int Id { get; set; }

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
}
