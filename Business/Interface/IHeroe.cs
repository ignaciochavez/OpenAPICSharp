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
        Heroe Select(int id);
        bool ExistByName(string name);
        Heroe Insert(HeroeInsertDTO heroeInsertDTO);
        bool Update(Heroe heroe);
        bool Delete(int id);
        List<Heroe> List(HeroeListDTO heroeListDTO);
        long TotalRecords();
        bool ExistByNameAndNotSameEntity(HeroeExistByNameAndNotSameEntityDTO heroeExistByNameAndNotSameEntityDTO);
        HeroeExcelDTO Excel();
        HeroePDFDTO PDF();
    }
}
