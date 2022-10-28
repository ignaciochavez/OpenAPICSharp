using Business.DTO;
using Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IExample
    {
        Example Select(int id);
        bool ExistsByRut(string rut);
        Example Insert(ExampleInsertDTO exampleInsertDTO);
        bool Update(Example example);
        bool Delete(int id);
        List<Example> List(ExampleListDTO exampleListDTO);
        long TotalRecords();
        bool ExistByRutAndNotSameEntity(ExampleExistByRutAndNotSameEntityDTO exampleExistByRutAndNotSameEntityDTO);
        ExampleExcelDTO Excel();
        ExamplePDFDTO PDF();
    }
}
