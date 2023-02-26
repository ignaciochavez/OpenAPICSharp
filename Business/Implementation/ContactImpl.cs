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
    public static class ContactImpl
    {

        private static ContactDAO contactDAO = new ContactDAO();

        public static Contact Select(int id)
        {
            return contactDAO.Select(id);
        }

        public static bool ExistById(int id)
        {
            return contactDAO.ExistById(id);
        }

        public static int Insert(ContactInsertDTO contactInsertDTO)
        {
            return contactDAO.Insert(contactInsertDTO);
        }

        public static bool Update(Contact contact)
        {
            return contactDAO.Update(contact);
        }

        public static bool Delete(int id)
        {
            return contactDAO.Delete(id);
        }

        public static List<Contact> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return contactDAO.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return contactDAO.TotalRecords();
        }

        public static List<Contact> Search(ContactSearchDTO contactSearchDTO)
        {
            return contactDAO.Search(contactSearchDTO);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return contactDAO.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return contactDAO.PDF(timeZoneInfoName);
        }
    }
}
