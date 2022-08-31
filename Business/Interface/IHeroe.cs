using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IHeroe
    {
        Heroe Select(string name);
        bool Exist(string name);
        Heroe Insert(HeroeInsertDTO heroeInsertDTO);
        bool Update(HeroeUpdateDTO heroeUpdateDTO);
        bool Delete(string name);
        List<Heroe> List(HeroeListDTO heroeListDTO);
    }
}
