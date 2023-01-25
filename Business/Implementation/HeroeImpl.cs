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
    public static class HeroeImpl
    {
        private static HeroeDAO heroeDAO = new HeroeDAO();

        public static Heroe Select(int id)
        {
            return heroeDAO.Select(id);
        }

        public static bool ExistByName(string name)
        {
            return heroeDAO.ExistByName(name);
        }

        public static Heroe Insert(HeroeInsertDTO heroeInsertDTO)
        {
            return heroeDAO.Insert(heroeInsertDTO);
        }

        public static bool Update(Heroe heroe)
        {
            return heroeDAO.Update(heroe);
        }

        public static bool Delete(int id)
        {
            return heroeDAO.Delete(id);
        }

        public static List<Heroe> List(HeroeListDTO heroeListDTO)
        {
            return heroeDAO.List(heroeListDTO);
        }

        public static long TotalRecords()
        {
            return heroeDAO.TotalRecords();
        }

        public static bool ExistByNameAndNotSameEntity(HeroeExistByNameAndNotSameEntityDTO heroeExistByNameAndNotSameEntityDTO)
        {
            return heroeDAO.ExistByNameAndNotSameEntity(heroeExistByNameAndNotSameEntityDTO);
        }

        public static HeroeExcelDTO Excel()
        {
            return heroeDAO.Excel();
        }

        public static HeroePDFDTO PDF()
        {
            return heroeDAO.PDF();
        }
    }
}
