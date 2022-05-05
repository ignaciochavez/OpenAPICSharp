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

        public static Example Select(string rut)
        {
            return exampleDAO.Select(rut);
        }

        public static bool Exist(string rut)
        {
            return exampleDAO.Exist(rut);
        }

        public static Example Insert(ExampleInsertDTO exampleInsertDTO)
        {
            return exampleDAO.Insert(exampleInsertDTO);
        }

        public static bool Udpdate(ExampleUpdateDTO exampleUpdateDTO)
        {
            return exampleDAO.Udpdate(exampleUpdateDTO);
        }

        public static bool Delete(string rut)
        {
            return exampleDAO.Delete(rut);
        }

        public static List<Example> List(ExampleListDTO exampleListDTO)
        {
            return exampleDAO.List(exampleListDTO);
        }
    }
}
