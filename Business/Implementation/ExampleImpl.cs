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
    public static class ExampleImpl
    {
        private static ExampleDAO exampleDAO = new ExampleDAO();

        public static Example Select(int id)
        {
            return exampleDAO.Select(id);
        }

        public static bool ExistsByRut(string rut)
        {
            return exampleDAO.ExistsByRut(rut);
        }

        public static Example Insert(ExampleInsertDTO exampleInsertDTO)
        {
            return exampleDAO.Insert(exampleInsertDTO);
        }

        public static bool Update(Example example)
        {
            return exampleDAO.Update(example);
        }

        public static bool Delete(int id)
        {
            return exampleDAO.Delete(id);
        }

        public static List<Example> List(ExampleListDTO exampleListDTO)
        {
            return exampleDAO.List(exampleListDTO);
        }

        public static long TotalRecords()
        {
            return exampleDAO.TotalRecords();
        }

        public static bool ExistByRutAndNotSameEntity(ExampleExistByRutAndNotSameEntityDTO exampleExistByRutAndNotSameEntityDTO)
        {
            return exampleDAO.ExistByRutAndNotSameEntity(exampleExistByRutAndNotSameEntityDTO);
        }

        public static ExampleExcelDTO Excel()
        {
            return exampleDAO.Excel();
        }

        public static ExamplePDFDTO PDF()
        {
            return exampleDAO.PDF();
        }
    }
}
