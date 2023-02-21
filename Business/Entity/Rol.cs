using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entity
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Rol()
        {

        }

        public Rol(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
