using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IHero
    {
        Hero Select(int id);
        bool Exist(int id);
        bool ExistByName(string name);
        int Insert(HeroInsertDTO heroInsertDTO);
        bool Update(HeroUpdateDTO heroUpdateDTO);
        bool Delete(int id);
        List<Hero> ListPaginated(ListPaginatedDTO listPaginatedDTO);
        long TotalRecords();
        List<Hero> Search(HeroSearchDTO heroSearchDTO);
        bool ExistByNameAndNotSameEntity(HeroExistByNameAndNotSameEntityDTO heroExistByNameAndNotSameEntityDTO);
        FileDTO Excel(string timeZoneInfoName);
        FileDTO PDF(string timeZoneInfoName);
    }
}
