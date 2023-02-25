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
    public static class UserImpl
    {
        private static UserDAO userDAO = new UserDAO();
        public static User Select(UserSelectDTO userSelectDTO)
        {
            return userDAO.Select(userSelectDTO);
        }

        public static bool ExistByRut(string rut)
        {
            return userDAO.ExistByRut(rut);
        }

        public static int Insert(UserInsertDTO userInsertDTO)
        {
            return userDAO.Insert(userInsertDTO);
        }

        public static bool Update(UserUpdateDTO userUpdateDTO)
        {
            return userDAO.Update(userUpdateDTO);
        }

        public static bool Delete(int id)
        {
            return userDAO.Delete(id);
        }
        
        public static List<User> ListPaginated(UserListPaginatedDTO userListPaginatedDTO)
        {
            return userDAO.ListPaginated(userListPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return userDAO.TotalRecords();
        }

        public static List<User> Search(UserSearchDTO userSearchDTO)
        {
            return userDAO.Search(userSearchDTO);
        }

        public static bool ExistByRutAndNotSameEntity(UserExistByRutAndNotSameEntityDTO userExistByRutAndNotSameEntityDTO)
        {
            return userDAO.ExistByRutAndNotSameEntity(userExistByRutAndNotSameEntityDTO);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return userDAO.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return userDAO.PDF(timeZoneInfoName);
        }
    }
}
