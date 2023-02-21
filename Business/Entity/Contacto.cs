using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entity
{
    public class Contacto
    {
        public int Id { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }

        public Contacto()
        {

        }

        public Contacto(int id, string correoElectronico, string telefono)
        {
            Id = id;
            CorreoElectronico = correoElectronico;
            Telefono = telefono;
        }
    }
}
