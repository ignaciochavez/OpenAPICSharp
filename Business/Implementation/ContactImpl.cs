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
        public static Contact Select(int id)
        {
            return ContactDAO.Instance.Select(id);
        }

        public static bool ExistById(int id)
        {
            return ContactDAO.Instance.ExistById(id);
        }

        public static int Insert(ContactInsertDTO contactInsertDTO)
        {
            return ContactDAO.Instance.Insert(contactInsertDTO);
        }

        public static bool Update(Contact contact)
        {
            return ContactDAO.Instance.Update(contact);
        }

        public static bool Delete(int id)
        {
            return ContactDAO.Instance.Delete(id);
        }

        public static List<Contact> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return ContactDAO.Instance.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return ContactDAO.Instance.TotalRecords();
        }

        public static List<Contact> Search(ContactSearchDTO contactSearchDTO)
        {
            return ContactDAO.Instance.Search(contactSearchDTO);
        }

        public static FileDTO Excel(string timeZoneInfoName)
        {
            return ContactDAO.Instance.Excel(timeZoneInfoName);
        }

        public static FileDTO PDF(string timeZoneInfoName)
        {
            return ContactDAO.Instance.PDF(timeZoneInfoName);
        }
    }
}
