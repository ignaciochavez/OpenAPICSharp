using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IUser
    {
        User Select(UserSelectDTO userSelectDTO);
        int Insert(UserInsertDTO userInsertDTO);
        bool Update(UserUpdateDTO userUpdateDTO);
        bool Delete(int id);
        List<User> ListPaginated(UserListPaginatedDTO userListPaginatedDTO);
        long TotalRecords();
        List<User> Search(UserSearchDTO userSearchDTO);
        bool ExistByRutAndNotSameEntity(UserExistByRutAndNotSameEntityDTO userExistByRutAndNotSameEntityDTO);
        FileDTO Excel(string timeZoneInfoName);
        FileDTO PDF(string timeZoneInfoName);
    }
}
