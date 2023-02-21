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
    public static class RolImpl
    {
        private static RolDAO rolDAO = new RolDAO();

        public static Rol Select(int id)
        {
            return rolDAO.Select(id);
        }

        public static bool Exist(string nombre)
        {
            return rolDAO.Exist(nombre);
        }

        public static int Insert(string nombre)
        {
            return rolDAO.Insert(nombre);
        }

        public static bool Update(Rol rol)
        {
            return rolDAO.Update(rol);
        }

        public static bool Delete(int id)
        {
            return rolDAO.Delete(id);
        }
        
        public static List<Rol> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return rolDAO.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return rolDAO.TotalRecords();
        }
        public static List<Rol> Search(RolSearchDTO rolSearchDTO)
        {
            return rolDAO.Search(rolSearchDTO);
        }

        public static bool ExistAndNotSameEntity(Rol rol)
        {
            return rolDAO.ExistAndNotSameEntity(rol);
        }

        public static FileDTO Excel()
        {
            return rolDAO.Excel();
        }

        public static FileDTO PDF()
        {
            return rolDAO.PDF();
        }
    }
}
