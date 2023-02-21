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
    public static class ContactoImpl
    {

        private static ContactDAO contactoDAO = new ContactDAO();

        public static Contacto Select(int id)
        {
            return contactoDAO.Select(id);
        }

        public static int Insert(ContactoInsertDTO contactoInsertDTO)
        {
            return contactoDAO.Insert(contactoInsertDTO);
        }

        public static bool Update(Contacto contacto)
        {
            return contactoDAO.Update(contacto);
        }

        public static bool Delete(int id)
        {
            return contactoDAO.Delete(id);
        }

        public static List<Contacto> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            return contactoDAO.ListPaginated(listPaginatedDTO);
        }

        public static long TotalRecords()
        {
            return contactoDAO.TotalRecords();
        }

        public static List<Contacto> Search(ContactoSearchDTO contactoSearchDTO)
        {
            return contactoDAO.Search(contactoSearchDTO);
        }

        public static FileDTO Excel()
        {
            return contactoDAO.Excel();
        }

        public static FileDTO PDF()
        {
            return contactoDAO.PDF();
        }
    }
}
