using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IPowerStats
    {
        PowerStats Select(int id);
        bool ExistById(int id);
        int Insert(PowerStatsInsertDTO powerStatsInsertDTO);
        bool Update(PowerStats powerStats);
        bool Delete(int id);
        List<PowerStats> ListPaginated(ListPaginatedDTO listPaginatedDTO);
        long TotalRecords();
        List<PowerStats> Search(PowerStatsSearchDTO powerStatsSearchDTO);
        FileDTO Excel(string timeZoneInfoName);
        FileDTO PDF(string timeZoneInfoName);
    }
}
