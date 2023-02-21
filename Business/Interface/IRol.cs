using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IRol
    {
        Rol Select(int id);
        bool Exist(string nombre);
        int Insert(string nombre);
        bool Update(Rol rol);
        bool Delete(int id);
        List<Rol> ListPaginated(ListPaginatedDTO listPaginatedDTO);
        long TotalRecords();
        List<Rol> Search(RolSearchDTO rolSearchDTO);
        bool ExistAndNotSameEntity(Rol rol);
        FileDTO Excel();
        FileDTO PDF();
    }
}
