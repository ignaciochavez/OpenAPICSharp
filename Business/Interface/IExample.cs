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
        Example Select(string rut);
        bool Exist(string rut);
        Example Insert(ExampleInsertDTO exampleInsertDTO);
        bool Udpdate(ExampleUpdateDTO exampleUpdateDTO);
        bool Delete(string rut);
        List<Example> List(ExampleListDTO exampleListDTO);
    }
}
