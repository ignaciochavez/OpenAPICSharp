using Business.DAO;
using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementation
{
    public static class HeroImpl
    {
        public static Hero Select(int id)
        {
            return HeroDAO.Instance.Select(id);
        }

        public static bool Exist(int id)
        {
            return HeroDAO.Instance.Exist(id);
        }

        public static bool ExistByName(string name)
        {
            return HeroDAO.Instance.ExistByName(name);
        }

        public static int Insert(HeroInsertDTO heroInsertDTO)
        {
            return HeroDAO.Instance.Insert(heroInsertDTO);
        }

        public static bool Update(HeroUpdateDTO heroUpdateDTO)
        {
            return HeroDAO.Instance.Update(heroUpdateDTO);
        }

        public static bool Delete(int id)
        {
            return HeroDAO.Instance.Delete(id);
        }

        public static List<Hero> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return HeroDAO.Instance.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return HeroDAO.Instance.TotalRecords();
        }

        public static List<Hero> Search(HeroSearchDTO heroSearchDTO)
        {
            return HeroDAO.Instance.Search(heroSearchDTO);
        }

        public static bool ExistByNameAndNotSameEntity(HeroExistByNameAndNotSameEntityDTO heroExistByNameAndNotSameEntityDTO)
        {
            return HeroDAO.Instance.ExistByNameAndNotSameEntity(heroExistByNameAndNotSameEntityDTO);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return HeroDAO.Instance.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return HeroDAO.Instance.PDF(timeZoneInfoName);
        }
    }
}
