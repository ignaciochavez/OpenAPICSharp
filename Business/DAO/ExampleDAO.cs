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
            listExampleData.Add(new Example() { Id = 1, Rut = "1-9", Name = "Pedro", LastName = "Gutierrez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(-1).AddDays(-5), Active = true, Password = Useful.ConvertSHA256("1234qwer") });
            listExampleData.Add(new Example() { Id = 2, Rut = "2-7", Name = "Jose", LastName = "Gonazalez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-20).AddMonths(-2).AddDays(-4), Active = true, Password = Useful.ConvertSHA256("5678tyui") });
            listExampleData.Add(new Example() { Id = 3, Rut = "3-5", Name = "Luis", LastName = "Romo", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-19).AddMonths(-1).AddDays(-1), Active = true, Password = Useful.ConvertSHA256("9012opqw") });
            listExampleData.Add(new Example() { Id = 4, Rut = "4-3", Name = "Manuel", LastName = "Palma", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(-3).AddDays(-10), Active = true, Password = Useful.ConvertSHA256("3456erty") });
            listExampleData.Add(new Example() { Id = 5, Rut = "5-1", Name = "Diego", LastName = "Muñoz", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-22).AddMonths(-5).AddDays(7), Active = true, Password = Useful.ConvertSHA256("7891uiop") });
            listExampleData.Add(new Example() { Id = 6, Rut = "6-K", Name = "Cristobal", LastName = "Lopez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-25).AddMonths(-1).AddDays(15), Active = true, Password = Useful.ConvertSHA256("2345asdf") });
            listExampleData.Add(new Example() { Id = 7, Rut = "7-8", Name = "Ulises", LastName = "Retamal", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(-3).AddDays(3), Active = true, Password = Useful.ConvertSHA256("6789ghjk") });
            listExampleData.Add(new Example() { Id = 8, Rut = "8-6", Name = "Sebastian", LastName = "Recabarren", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-28).AddMonths(-8).AddDays(18), Active = true, Password = Useful.ConvertSHA256("1234lñas") });
            listExampleData.Add(new Example() { Id = 9, Rut = "9-4", Name = "Angelica", LastName = "Solis", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-19).AddMonths(-4).AddDays(16), Active = true, Password = Useful.ConvertSHA256("5678dfgh") });
            listExampleData.Add(new Example() { Id = 10, Rut = "10-8", Name = "Maria", LastName = "Diaz", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-21).AddMonths(-9).AddDays(23), Active = true, Password = Useful.ConvertSHA256("9123jklñ") });
            listExampleData.Add(new Example() { Id = 11, Rut = "11-6", Name = "Aurora", LastName = "Reyes", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-26).AddMonths(-11).AddDays(7), Active = true, Password = Useful.ConvertSHA256("4567zxcv") });
            listExampleData.Add(new Example() { Id = 12, Rut = "12-4", Name = "Joselyn", LastName = "Labra", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-24).AddMonths(7).AddDays(13), Active = true, Password = Useful.ConvertSHA256("8912bnmz") });
            listExampleData.Add(new Example() { Id = 13, Rut = "13-2", Name = "Fernanda", LastName = "Ibarra", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(2).AddDays(24), Active = true, Password = Useful.ConvertSHA256("3456xcvb") });
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
                Id = listExampleData.Last().Id + 1,
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

        public bool Update(ExampleUpdateDTO exampleUpdateDTO)
        {
            Example exampleExist = listExampleData.FirstOrDefault(o => o.Rut == exampleUpdateDTO.Rut);
            if (exampleExist != null)
            {
                Example example = new Example()
                {
                    Id = exampleExist.Id,
                    Rut = exampleExist.Rut,
                    Name = exampleUpdateDTO.Name,
                    LastName = exampleUpdateDTO.LastName,
                    BirthDate = exampleUpdateDTO.BirthDate.Date,
                    Active = exampleUpdateDTO.Active,
                    Password = Useful.ConvertSHA256(exampleUpdateDTO.Password)
                };
                listExampleData.Remove(exampleExist);
                listExampleData.Add(example);
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
            List<Example> listExample = listExampleData.OrderBy(o => o.Id).Skip((exampleListDTO.PageSize * (exampleListDTO.PageIndex - 1))).Take(exampleListDTO.PageSize).ToList();
            return listExample;
        }

        public long Count()
        {
            long count = listExampleData.LongCount();
            return count;
        }
    }
}
