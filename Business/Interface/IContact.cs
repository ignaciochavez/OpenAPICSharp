using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IContacto
    {
        Contacto Select(int id);
        int Insert(ContactoInsertDTO contactoInsertDTO);
        bool Update(Contacto contacto);
        bool Delete(int id);
        List<Contacto> ListPaginated(ListPaginatedDTO listPaginatedDTO);
        long TotalRecords();
        List<Contacto> Search(ContactoSearchDTO contactoSearchDTO);
        FileDTO Excel();
        FileDTO PDF();
    }
}
