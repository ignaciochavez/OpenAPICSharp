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

        public static Heroe Select(string name)
        {
            return heroeDAO.Select(name);
        }

        public static bool Exist(string name)
        {
            return heroeDAO.Exist(name);
        }

        public static Heroe Insert(HeroeInsertDTO heroeInsertDTO)
        {
            return heroeDAO.Insert(heroeInsertDTO);
        }

        public static bool Update(HeroeUpdateDTO heroeUpdateDTO)
        {
            return heroeDAO.Update(heroeUpdateDTO);
        }

        public static bool Delete(string name)
        {
            return heroeDAO.Delete(name);
        }

        public static List<Heroe> List(HeroeListDTO heroeListDTO)
        {
            return heroeDAO.List(heroeListDTO);
        }

        public static long Count()
        {
            return heroeDAO.Count();
        }
    }
}
