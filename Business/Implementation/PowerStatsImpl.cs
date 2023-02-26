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
        private static PowerStatsDAO powerStatsDAO = new PowerStatsDAO();

        public static PowerStats Select(int id)
        {
            return powerStatsDAO.Select(id);
        }

        public static bool ExistById(int id)
        {
            return powerStatsDAO.ExistById(id);
        }

        public static int Insert(PowerStatsInsertDTO powerStatsInsertDTO)
        {
            return powerStatsDAO.Insert(powerStatsInsertDTO);
        }

        public static bool Update(PowerStats powerStats)
        {
            return powerStatsDAO.Update(powerStats);
        }

        public static bool Delete(int id)
        {
            return powerStatsDAO.Delete(id);
        }

        public static List<PowerStats> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return powerStatsDAO.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return powerStatsDAO.TotalRecords();
        }

        public static List<PowerStats> Search(PowerStatsSearchDTO powerStatsSearchDTO)
        {
            return powerStatsDAO.Search(powerStatsSearchDTO);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return powerStatsDAO.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return powerStatsDAO.PDF(timeZoneInfoName);
        }
    }
}
