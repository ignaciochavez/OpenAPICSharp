using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IContact
    {
        Contact Select(int id);
        int Insert(ContactInsertDTO contactInsertDTO);
        bool Update(Contact contact);
        bool Delete(int id);
        List<Contact> ListPaginated(ListPaginatedDTO listPaginatedDTO);
        long TotalRecords();
        List<Contact> Search(ContactSearchDTO ContactSearchDTO);
        FileDTO Excel();
        FileDTO PDF();
    }
}
