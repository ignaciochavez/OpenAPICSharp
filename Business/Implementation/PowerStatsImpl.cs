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
    public static class PowerStatsImpl
    {
        public static PowerStats Select(int id)
        {
            return PowerStatsDAO.Instance.Select(id);
        }

        public static bool ExistById(int id)
        {
            return PowerStatsDAO.Instance.ExistById(id);
        }

        public static int Insert(PowerStatsInsertDTO powerStatsInsertDTO)
        {
            return PowerStatsDAO.Instance.Insert(powerStatsInsertDTO);
        }

        public static bool Update(PowerStats powerStats)
        {
            return PowerStatsDAO.Instance.Update(powerStats);
        }

        public static bool Delete(int id)
        {
            return PowerStatsDAO.Instance.Delete(id);
        }

        public static List<PowerStats> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return PowerStatsDAO.Instance.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return PowerStatsDAO.Instance.TotalRecords();
        }

        public static List<PowerStats> Search(PowerStatsSearchDTO powerStatsSearchDTO)
        {
            return PowerStatsDAO.Instance.Search(powerStatsSearchDTO);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return PowerStatsDAO.Instance.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return PowerStatsDAO.Instance.PDF(timeZoneInfoName);
        }
    }
}
