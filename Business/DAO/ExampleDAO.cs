using Business.DTO;
using Business.Entity;
using Business.Interface;
using Business.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DAO
{
    public class ExampleDAO : IExample
    {
        List<Example> listExampleData = new List<Example>();
        public ExampleDAO()
        {
            Example example = new Example()
            {
                Index = 1,
                Rut = "1-9",
                Name = "Pedro",
                LastName = "Gutierrez",
                BirthDate = DateTimeOffset.UtcNow.Date,
                Active = true,
                Password = Useful.ConvertSHA256("1234qwer")
            };
            listExampleData.Add(example);
        }

        public Example Select(string rut)
        {
            Example example = listExampleData.FirstOrDefault(o => o.Rut == rut);
            return example;
        }

        public bool Exist(string rut)
        {
            bool exist = listExampleData.Any(o => o.Rut == rut);
            return exist;
        }
        
        public Example Insert(ExampleInsertDTO exampleInsertDTO)
        {
            Example example = new Example()
            {
                Index = listExampleData.Last().Index + 1,//Esto esta echo asi a modo de ejemplo solo por ser una lista
                Rut = exampleInsertDTO.Rut,
                Name = exampleInsertDTO.Name,
                LastName = exampleInsertDTO.LastName,
                BirthDate = exampleInsertDTO.BirthDate.Date,
                Active = exampleInsertDTO.Active,
                Password = Useful.ConvertSHA256(exampleInsertDTO.Password)
            };
            listExampleData.Add(example);
            return example;
        }

        public bool Udpdate(ExampleUpdateDTO exampleUpdateDTO)
        {
            Example exampleExist = listExampleData.FirstOrDefault(o => o.Rut == exampleUpdateDTO.Rut);
            if (exampleExist != null)
            {
                Example example = new Example()
                {
                    Index = exampleExist.Index,
                    Rut = exampleExist.Rut,
                    Name = exampleUpdateDTO.Name,
                    LastName = exampleUpdateDTO.LastName,
                    BirthDate = exampleUpdateDTO.BirthDate.Date,
                    Active = exampleUpdateDTO.Active,
                    Password = Useful.ConvertSHA256(exampleUpdateDTO.Password)
                };
                listExampleData.Remove(exampleExist);//En un objeto de bd de datos se actualiza con el metodo pertinente segun correponda
                listExampleData.Add(example);//Esto esta echo asi a modo de ejemplo solo por ser una lista
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(string rut)
        {
            Example example = listExampleData.FirstOrDefault(o => o.Rut == rut);
            if (example != null)
            {
                listExampleData.Remove(example);
                return true;
            }
            else
            {
                return false;
            } 
        }

        public List<Example> List(ExampleListDTO exampleListDTO)
        {
            List<Example> listExample = listExampleData.Skip(exampleListDTO.PageSize * exampleListDTO.PageIndex).Take(exampleListDTO.PageSize).ToList();
            return listExample;
        }
    }
}
