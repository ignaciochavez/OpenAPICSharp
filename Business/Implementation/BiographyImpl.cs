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
        public static Biography Select(int id)
        {
            return BiographyDAO.Instance.Select(id);
        }

        public static bool ExistById(int id)
        {
            return BiographyDAO.Instance.ExistById(id);
        }

        public static int Insert(BiographyInsertDTO biographyInsertDTO)
        {
            return BiographyDAO.Instance.Insert(biographyInsertDTO);
        }

        public static bool Update(Biography biography)
        {
            return BiographyDAO.Instance.Update(biography);
        }

        public static bool Delete(int id)
        {
            return BiographyDAO.Instance.Delete(id);
        }

        public static List<Biography> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return BiographyDAO.Instance.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return BiographyDAO.Instance.TotalRecords();
        }

        public static List<Biography> Search(BiographySearchDTO biographySearchDTO)
        {
            return BiographyDAO.Instance.Search(biographySearchDTO);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return BiographyDAO.Instance.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return BiographyDAO.Instance.PDF(timeZoneInfoName);
        }
    }
}
