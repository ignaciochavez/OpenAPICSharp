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
    public static class RoleImpl
    {
        private static RoleDAO roleDAO = new RoleDAO();

        public static Role Select(int id)
        {
            return roleDAO.Select(id);
        }

        public static bool ExistById(int id)
        {
            return roleDAO.ExistById(id);
        }

        public static bool ExistByName(string name)
        {
            return roleDAO.ExistByName(name);
        }

        public static int Insert(string name)
        {
            return roleDAO.Insert(name);
        }

        public static bool Update(Role role)
        {
            return roleDAO.Update(role);
        }

        public static bool Delete(int id)
        {
            return roleDAO.Delete(id);
        }
        
        public static List<Role> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return roleDAO.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return roleDAO.TotalRecords();
        }
        public static List<Role> Search(RoleSearchDTO roleSearchDTO)
        {
            return roleDAO.Search(roleSearchDTO);
        }

        public static bool ExistByNameAndNotSameEntity(Role role)
        {
            return roleDAO.ExistByNameAndNotSameEntity(role);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return roleDAO.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return roleDAO.PDF(timeZoneInfoName);
        }
    }
}
