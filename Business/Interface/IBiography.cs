using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IBiography
    {
        Biography Select(int id);
        bool ExistById(int id);
        int Insert(BiographyInsertDTO biographyInsertDTO);
        bool Update(Biography biography);
        bool Delete(int id);
        List<Biography> ListPaginated(ListPaginatedDTO listPaginatedDTO);
        long TotalRecords();
        List<Biography> Search(BiographySearchDTO biographySearchDTO);
        FileDTO Excel(string timeZoneInfoName);
        FileDTO PDF(string timeZoneInfoName);
    }
}
