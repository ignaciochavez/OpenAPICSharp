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
        public static User Select(UserSelectDTO userSelectDTO)
        {
            return UserDAO.Instance.Select(userSelectDTO);
        }

        public static bool Exist(int id)
        {
            return UserDAO.Instance.Exist(id);
        }

        public static bool ExistByRut(string rut)
        {
            return UserDAO.Instance.ExistByRut(rut);
        }

        public static int Insert(UserInsertDTO userInsertDTO)
        {
            return UserDAO.Instance.Insert(userInsertDTO);
        }

        public static bool Update(UserUpdateDTO userUpdateDTO)
        {
            return UserDAO.Instance.Update(userUpdateDTO);
        }

        public static bool Delete(int id)
        {
            return UserDAO.Instance.Delete(id);
        }
        
        public static List<User> ListPaginated(UserListPaginatedDTO userListPaginatedDTO)
        {
            return UserDAO.Instance.ListPaginated(userListPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return UserDAO.Instance.TotalRecords();
        }

        public static List<User> Search(UserSearchDTO userSearchDTO)
        {
            return UserDAO.Instance.Search(userSearchDTO);
        }

        public static bool ExistByRutAndNotSameEntity(UserExistByRutAndNotSameEntityDTO userExistByRutAndNotSameEntityDTO)
        {
            return UserDAO.Instance.ExistByRutAndNotSameEntity(userExistByRutAndNotSameEntityDTO);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return UserDAO.Instance.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return UserDAO.Instance.PDF(timeZoneInfoName);
        }
    }
}
