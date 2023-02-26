using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IRole
    {
        Role Select(int id);
        bool ExistById(int id);
        bool ExistByName(string name);
        int Insert(string name);
        bool Update(Role role);
        bool Delete(int id);
        List<Role> ListPaginated(ListPaginatedDTO listPaginatedDTO);
        long TotalRecords();
        List<Role> Search(RoleSearchDTO roleSearchDTO);
        bool ExistByNameAndNotSameEntity(Role role);
        FileDTO Excel(string timeZoneInfoName);
        FileDTO PDF(string timeZoneInfoName);
    }
}
