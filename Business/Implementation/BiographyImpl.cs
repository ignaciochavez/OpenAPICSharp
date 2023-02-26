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
    public static class BiographyImpl
    {
        private static BiographyDAO biographyDAO = new BiographyDAO();

        public static Biography Select(int id)
        {
            return biographyDAO.Select(id);
        }

        public static bool ExistById(int id)
        {
            return biographyDAO.ExistById(id);
        }

        public static int Insert(BiographyInsertDTO biographyInsertDTO)
        {
            return biographyDAO.Insert(biographyInsertDTO);
        }

        public static bool Update(Biography biography)
        {
            return biographyDAO.Update(biography);
        }

        public static bool Delete(int id)
        {
            return biographyDAO.Delete(id);
        }

        public static List<Biography> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return biographyDAO.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return biographyDAO.TotalRecords();
        }

        public static List<Biography> Search(BiographySearchDTO biographySearchDTO)
        {
            return biographyDAO.Search(biographySearchDTO);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return biographyDAO.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return biographyDAO.PDF(timeZoneInfoName);
        }
    }
}
