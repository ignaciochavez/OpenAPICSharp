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
        public static Role Select(int id)
        {
            return RoleDAO.Instance.Select(id);
        }

        public static bool ExistById(int id)
        {
            return RoleDAO.Instance.ExistById(id);
        }

        public static bool ExistByName(string name)
        {
            return RoleDAO.Instance.ExistByName(name);
        }

        public static int Insert(string name)
        {
            return RoleDAO.Instance.Insert(name);
        }

        public static bool Update(Role role)
        {
            return RoleDAO.Instance.Update(role);
        }

        public static bool Delete(int id)
        {
            return RoleDAO.Instance.Delete(id);
        }
        
        public static List<Role> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return RoleDAO.Instance.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return RoleDAO.Instance.TotalRecords();
        }
        public static List<Role> Search(RoleSearchDTO roleSearchDTO)
        {
            return RoleDAO.Instance.Search(roleSearchDTO);
        }

        public static bool ExistByNameAndNotSameEntity(Role role)
        {
            return RoleDAO.Instance.ExistByNameAndNotSameEntity(role);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return RoleDAO.Instance.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return RoleDAO.Instance.PDF(timeZoneInfoName);
        }
    }
}
