using Business.DTO;
using Business.Entity;
using Business.Interface;
using Business.Tool;
using SpreadsheetLight;
using SpreadsheetLight.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Business.DAO
{
    public class ExampleDAO : IExample
    {
        List<Example> examples = new List<Example>();
        public ExampleDAO()
        {
            examples.Add(new Example() { Id = 1, Rut = "1-9", Name = "Pedro", LastName = "Gutierrez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(-1).AddDays(-5), Active = true, Password = Useful.ConvertSHA256("1234qwer") });
            examples.Add(new Example() { Id = 2, Rut = "2-7", Name = "Jose", LastName = "Gonazalez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-20).AddMonths(-2).AddDays(-4), Active = true, Password = Useful.ConvertSHA256("5678tyui") });
            examples.Add(new Example() { Id = 3, Rut = "3-5", Name = "Luis", LastName = "Romo", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-19).AddMonths(-1).AddDays(-1), Active = true, Password = Useful.ConvertSHA256("9012opqw") });
            examples.Add(new Example() { Id = 4, Rut = "4-3", Name = "Manuel", LastName = "Palma", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(-3).AddDays(-10), Active = true, Password = Useful.ConvertSHA256("3456erty") });
            examples.Add(new Example() { Id = 5, Rut = "5-1", Name = "Diego", LastName = "Muñoz", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-22).AddMonths(-5).AddDays(7), Active = true, Password = Useful.ConvertSHA256("7891uiop") });
            examples.Add(new Example() { Id = 6, Rut = "6-K", Name = "Cristobal", LastName = "Lopez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-25).AddMonths(-1).AddDays(15), Active = true, Password = Useful.ConvertSHA256("2345asdf") });
            examples.Add(new Example() { Id = 7, Rut = "7-8", Name = "Ulises", LastName = "Retamal", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(-3).AddDays(3), Active = true, Password = Useful.ConvertSHA256("6789ghjk") });
            examples.Add(new Example() { Id = 8, Rut = "8-6", Name = "Sebastian", LastName = "Recabarren", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-28).AddMonths(-8).AddDays(18), Active = true, Password = Useful.ConvertSHA256("1234lñas") });
            examples.Add(new Example() { Id = 9, Rut = "9-4", Name = "Angelica", LastName = "Solis", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-19).AddMonths(-4).AddDays(16), Active = true, Password = Useful.ConvertSHA256("5678dfgh") });
            examples.Add(new Example() { Id = 10, Rut = "10-8", Name = "Maria", LastName = "Diaz", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-21).AddMonths(-9).AddDays(23), Active = true, Password = Useful.ConvertSHA256("9123jklñ") });
            examples.Add(new Example() { Id = 11, Rut = "11-6", Name = "Aurora", LastName = "Reyes", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-26).AddMonths(-11).AddDays(7), Active = true, Password = Useful.ConvertSHA256("4567zxcv") });
            examples.Add(new Example() { Id = 12, Rut = "12-4", Name = "Joselyn", LastName = "Labra", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-24).AddMonths(7).AddDays(13), Active = true, Password = Useful.ConvertSHA256("8912bnmz") });
            examples.Add(new Example() { Id = 13, Rut = "13-2", Name = "Fernanda", LastName = "Ibarra", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(2).AddDays(24), Active = true, Password = Useful.ConvertSHA256("3456xcvb") });
            examples.Add(new Example() { Id = 14, Rut = "14-0", Name = "Amenadiel", LastName = "Fleming", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-22).AddMonths(5).AddDays(1), Active = true, Password = Useful.ConvertSHA256("a4s5x2cw") });
            examples.Add(new Example() { Id = 15, Rut = "15-9", Name = "Lucifer", LastName = "Estrella", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-27).AddMonths(9).AddDays(11), Active = true, Password = Useful.ConvertSHA256("g4f7t5h8n") });
            examples.Add(new Example() { Id = 16, Rut = "16-7", Name = "Rafaela ", LastName = "Mercader", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-40).AddMonths(-2).AddDays(-9), Active = true, Password = Useful.ConvertSHA256("1234qwer") });
            examples.Add(new Example() { Id = 17, Rut = "17-5", Name = "Román", LastName = "Ureña", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-25).AddMonths(-6).AddDays(-3), Active = true, Password = Useful.ConvertSHA256("5678tyui") });
            examples.Add(new Example() { Id = 18, Rut = "18-3", Name = "Caridad", LastName = "Almagro", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-19).AddMonths(-1).AddDays(-1), Active = true, Password = Useful.ConvertSHA256("9012opqw") });
            examples.Add(new Example() { Id = 19, Rut = "19-1", Name = "Jimena", LastName = "Madrid", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-45).AddMonths(-9).AddDays(-11), Active = true, Password = Useful.ConvertSHA256("1a5b3i9h") });
            examples.Add(new Example() { Id = 20, Rut = "20-5", Name = "Rosalva", LastName = "Costa", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-87).AddMonths(-11).AddDays(27), Active = true, Password = Useful.ConvertSHA256("5d6f4b2h1") });
            examples.Add(new Example() { Id = 21, Rut = "21-3", Name = "Ámbar", LastName = "Castillo", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-66).AddMonths(-9).AddDays(22), Active = true, Password = Useful.ConvertSHA256("12mn56ee") });
            examples.Add(new Example() { Id = 22, Rut = "22-1", Name = "Fidela", LastName = "Campillo", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-54).AddMonths(-3).AddDays(4), Active = true, Password = Useful.ConvertSHA256("5g47hj55") });
            examples.Add(new Example() { Id = 23, Rut = "23-K", Name = "Tadeo", LastName = "Aguilar", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-23).AddMonths(-7).AddDays(25), Active = true, Password = Useful.ConvertSHA256("11h2t37g") });
            examples.Add(new Example() { Id = 24, Rut = "24-8", Name = "Eric", LastName = "Villar", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-65).AddMonths(-9).AddDays(26), Active = true, Password = Useful.ConvertSHA256("2q6x8uh4") });
            examples.Add(new Example() { Id = 25, Rut = "25-6", Name = "Rafa", LastName = "Heredia", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-51).AddMonths(-10).AddDays(13), Active = true, Password = Useful.ConvertSHA256("9g8t5b2n") });
            examples.Add(new Example() { Id = 26, Rut = "26-4", Name = "Vilma", LastName = "Agudo", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-46).AddMonths(-12).AddDays(25), Active = true, Password = Useful.ConvertSHA256("l6h5j2m1") });
            examples.Add(new Example() { Id = 27, Rut = "27-2", Name = "Florentino", LastName = "Arellano", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-71).AddMonths(6).AddDays(17), Active = true, Password = Useful.ConvertSHA256("s7y5i9b2") });
            examples.Add(new Example() { Id = 28, Rut = "28-0", Name = "Emilia", LastName = "Riera", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-36).AddMonths(8).AddDays(27), Active = true, Password = Useful.ConvertSHA256("5g6s9e4g") });
            examples.Add(new Example() { Id = 29, Rut = "29-9", Name = "Amanda", LastName = "Porta", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-48).AddMonths(6).AddDays(16), Active = true, Password = Useful.ConvertSHA256("8v4f5h6t") });
            examples.Add(new Example() { Id = 30, Rut = "30-2", Name = "Hipólito", LastName = "Reyes", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-57).AddMonths(7).AddDays(1), Active = true, Password = Useful.ConvertSHA256("7f7e5t6r") });
            examples.Add(new Example() { Id = 31, Rut = "31-0", Name = "Olegario", LastName = "Moliner", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-29).AddMonths(-6).AddDays(-2), Active = true, Password = Useful.ConvertSHA256("78ax56d5") });
            examples.Add(new Example() { Id = 32, Rut = "32-9", Name = "Teo", LastName = "Flores", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-57).AddMonths(-6).AddDays(-7), Active = true, Password = Useful.ConvertSHA256("4j5h55nn") });
            examples.Add(new Example() { Id = 33, Rut = "33-7", Name = "Hilario", LastName = "Cerdá", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-61).AddMonths(-9).AddDays(-5), Active = true, Password = Useful.ConvertSHA256("5g69rt8y") });
            examples.Add(new Example() { Id = 34, Rut = "34-5", Name = "Blanca", LastName = "Valdés", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-64).AddMonths(-4).AddDays(-7), Active = true, Password = Useful.ConvertSHA256("d4c155gg") });
            examples.Add(new Example() { Id = 35, Rut = "35-3", Name = "Pío", LastName = "Vélez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-49).AddMonths(-6).AddDays(8), Active = true, Password = Useful.ConvertSHA256("f4a5q6r8") });
            examples.Add(new Example() { Id = 36, Rut = "36-1", Name = "Evelia", LastName = "Benavente", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-50).AddMonths(-7).AddDays(6), Active = true, Password = Useful.ConvertSHA256("5f98tg51") });
            examples.Add(new Example() { Id = 37, Rut = "37-K", Name = "Micaela", LastName = "Meléndez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-30).AddMonths(-9).AddDays(8), Active = true, Password = Useful.ConvertSHA256("akfh4521") });
            examples.Add(new Example() { Id = 38, Rut = "38-8", Name = "Julia", LastName = "Yáñez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-40).AddMonths(-7).AddDays(3), Active = true, Password = Useful.ConvertSHA256("3a2s6hj5") });
            examples.Add(new Example() { Id = 39, Rut = "39-6", Name = "Adrián", LastName = "Olmo", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-55).AddMonths(-11).AddDays(24), Active = true, Password = Useful.ConvertSHA256("5g8q9y7h") });
            examples.Add(new Example() { Id = 40, Rut = "40-K", Name = "Isaura", LastName = "Silva", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-60).AddMonths(-2).AddDays(14), Active = true, Password = Useful.ConvertSHA256("4a6j5c1g") });
            examples.Add(new Example() { Id = 41, Rut = "41-8", Name = "Vito", LastName = "Izaguirre", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-56).AddMonths(-3).AddDays(16), Active = true, Password = Useful.ConvertSHA256("9h5x2v1h") });
            examples.Add(new Example() { Id = 42, Rut = "42-6", Name = "Carmina", LastName = "Sanjuan", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-57).AddMonths(8).AddDays(13), Active = true, Password = Useful.ConvertSHA256("9g5s6f4h") });
            examples.Add(new Example() { Id = 43, Rut = "43-4", Name = "Horacio", LastName = "Ramos", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-39).AddMonths(7).AddDays(19), Active = true, Password = Useful.ConvertSHA256("re87g4h5") });
            examples.Add(new Example() { Id = 44, Rut = "44-2", Name = "Gloria", LastName = "Villalonga", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-33).AddMonths(4).AddDays(14), Active = true, Password = Useful.ConvertSHA256("po51c2v3") });
            examples.Add(new Example() { Id = 45, Rut = "45-0", Name = "Tadeo", LastName = "Fuster", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-25).AddMonths(3).AddDays(18), Active = true, Password = Useful.ConvertSHA256("op5465kj") });
            examples.Add(new Example() { Id = 46, Rut = "46-9", Name = "Eladio", LastName = "Mena", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-75).AddMonths(-11).AddDays(-13), Active = true, Password = Useful.ConvertSHA256("df26g3h5") });
            examples.Add(new Example() { Id = 47, Rut = "47-7", Name = "Goyo", LastName = "Mínguez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-66).AddMonths(-10).AddDays(-18), Active = true, Password = Useful.ConvertSHA256("75gt6h5y") });
            examples.Add(new Example() { Id = 48, Rut = "48-5", Name = "Fabián", LastName = "Quintanilla", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-24).AddMonths(-7).AddDays(-18), Active = true, Password = Useful.ConvertSHA256("r8x6j8i9") });
            examples.Add(new Example() { Id = 49, Rut = "49-3", Name = "Aarón", LastName = "Carranza", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-65).AddMonths(-3).AddDays(-11), Active = true, Password = Useful.ConvertSHA256("s5y6n3o9") });
            examples.Add(new Example() { Id = 50, Rut = "50-7", Name = "Íngrid", LastName = "Almagro", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-51).AddMonths(-5).AddDays(14), Active = true, Password = Useful.ConvertSHA256("d4j5b6k1") });
            examples.Add(new Example() { Id = 51, Rut = "51-5", Name = "Palmira", LastName = "Carvajal", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-53).AddMonths(-10).AddDays(5), Active = true, Password = Useful.ConvertSHA256("9h3b2g1") });
            examples.Add(new Example() { Id = 52, Rut = "52-3", Name = "Dolores", LastName = "Bermejo", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-65).AddMonths(-12).AddDays(31), Active = true, Password = Useful.ConvertSHA256("12gh56yt") });
            examples.Add(new Example() { Id = 53, Rut = "53-1", Name = "Trinidad", LastName = "Mármol", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-61).AddMonths(-8).AddDays(19), Active = true, Password = Useful.ConvertSHA256("16bg89xc") });
            examples.Add(new Example() { Id = 54, Rut = "54-K", Name = "Dan", LastName = "Cuenca", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-60).AddMonths(-9).AddDays(31), Active = true, Password = Useful.ConvertSHA256("49ft15bh") });
            examples.Add(new Example() { Id = 55, Rut = "55-8", Name = "Cristóbal", LastName = "Lumbreras", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-63).AddMonths(-9).AddDays(14), Active = true, Password = Useful.ConvertSHA256("59ky13sg") });
            examples.Add(new Example() { Id = 56, Rut = "56-6", Name = "Clara", LastName = "Martorell", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-70).AddMonths(-12).AddDays(30), Active = true, Password = Useful.ConvertSHA256("h4d6c32n") });
            examples.Add(new Example() { Id = 57, Rut = "57-4", Name = "Javi", LastName = "Villegas", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-58).AddMonths(8).AddDays(27), Active = true, Password = Useful.ConvertSHA256("ru54d3c9") });
            examples.Add(new Example() { Id = 58, Rut = "58-2", Name = "Iris", LastName = "Cabrera", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-66).AddMonths(12).AddDays(24), Active = true, Password = Useful.ConvertSHA256("a4j9c1g3") });
            examples.Add(new Example() { Id = 59, Rut = "59-0", Name = "Regina", LastName = "Requena", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-28).AddMonths(6).AddDays(10), Active = true, Password = Useful.ConvertSHA256("y5f3n6h9") });
            examples.Add(new Example() { Id = 60, Rut = "60-4", Name = "Virgilio", LastName = "Lucena", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-33).AddMonths(9).AddDays(16), Active = true, Password = Useful.ConvertSHA256("q5r9w7y9") });
            examples.Add(new Example() { Id = 61, Rut = "61-2", Name = "Danilo", LastName = "Codina", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-44).AddMonths(-1).AddDays(-31), Active = true, Password = Useful.ConvertSHA256("4f3g5x1b") });
            examples.Add(new Example() { Id = 62, Rut = "62-0", Name = "Ildefonso", LastName = "Vega", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-55).AddMonths(-2).AddDays(-14), Active = true, Password = Useful.ConvertSHA256("9g5o6n4f") });
            examples.Add(new Example() { Id = 63, Rut = "63-9", Name = "Aristides", LastName = "Valbuena", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-19).AddMonths(-8).AddDays(-24), Active = true, Password = Useful.ConvertSHA256("q6y8j9g5") });
            examples.Add(new Example() { Id = 64, Rut = "64-7", Name = "Modesto", LastName = "Donoso", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-25).AddMonths(-11).AddDays(-19), Active = true, Password = Useful.ConvertSHA256("q5k1c6j3") });
            examples.Add(new Example() { Id = 65, Rut = "65-5", Name = "Candelaria", LastName = "Vilar", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-27).AddMonths(-8).AddDays(31), Active = true, Password = Useful.ConvertSHA256("t1n6j5d4") });
            examples.Add(new Example() { Id = 66, Rut = "66-3", Name = "Xiomara", LastName = "Sanz", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-39).AddMonths(-10).AddDays(18), Active = true, Password = Useful.ConvertSHA256("a6t8g4b6u9") });
            examples.Add(new Example() { Id = 67, Rut = "67-1", Name = "Fátima", LastName = "Pavón", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-47).AddMonths(-3).AddDays(31), Active = true, Password = Useful.ConvertSHA256("f5h6c1b2") });
            examples.Add(new Example() { Id = 68, Rut = "68-K", Name = "Bernardino", LastName = "Villanueva", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-51).AddMonths(-7).AddDays(9), Active = true, Password = Useful.ConvertSHA256("p5d6y1b8") });
            examples.Add(new Example() { Id = 69, Rut = "69-8", Name = "Jonatan", LastName = "Porcel", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-58).AddMonths(-12).AddDays(31), Active = true, Password = Useful.ConvertSHA256("g5c6p9h4") });
            examples.Add(new Example() { Id = 70, Rut = "70-1", Name = "Luís", LastName = "Acero", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-62).AddMonths(-7).AddDays(22), Active = true, Password = Useful.ConvertSHA256("e5f6t7k3") });
            examples.Add(new Example() { Id = 71, Rut = "71-K", Name = "María", LastName = "Cabo", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-70).AddMonths(-4).AddDays(23), Active = true, Password = Useful.ConvertSHA256("6f1v9h5") });
            examples.Add(new Example() { Id = 72, Rut = "72-8", Name = "Fabián", LastName = "Cuadrado", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-75).AddMonths(11).AddDays(14), Active = true, Password = Useful.ConvertSHA256("a6s5v9h4") });
            examples.Add(new Example() { Id = 73, Rut = "73-6", Name = "Aroa", LastName = "Casares", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-53).AddMonths(12).AddDays(12), Active = true, Password = Useful.ConvertSHA256("1a3h6f9j") });
            examples.Add(new Example() { Id = 74, Rut = "74-4", Name = "Ágata", LastName = "Caballero", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-42).AddMonths(1).AddDays(14), Active = true, Password = Useful.ConvertSHA256("g9h6w8g6") });
            examples.Add(new Example() { Id = 75, Rut = "75-2", Name = "Domingo", LastName = "Reguera", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-46).AddMonths(2).AddDays(31), Active = true, Password = Useful.ConvertSHA256("d6g5x9k5") });
        }

        public Example Select(int id)
        {
            Example example = examples.FirstOrDefault(o => o.Id == id);
            return example;
        }

        public bool ExistsByRut(string rut)
        {
            bool exist = examples.Any(o => o.Rut == rut);
            return exist;
        }
        
        public Example Insert(ExampleInsertDTO exampleInsertDTO)
        {
            Example example = new Example()
            {
                Id = examples.Last().Id + 1,
                Rut = exampleInsertDTO.Rut.Replace(".", ""),
                Name = exampleInsertDTO.Name,
                LastName = exampleInsertDTO.LastName,
                BirthDate = exampleInsertDTO.BirthDate.Date,
                Active = exampleInsertDTO.Active,
                Password = Useful.ConvertSHA256(exampleInsertDTO.Password)
            };
            examples.Add(example);
            return example;
        }

        public bool Update(Example example)
        {
            Example exampleExist = examples.FirstOrDefault(o => o.Id == example.Id);
            if (exampleExist != null)
            {
                Example entity = new Example()
                {
                    Id = exampleExist.Id,
                    Rut = example.Rut.Replace(".", ""),
                    Name = example.Name,
                    LastName = example.LastName,
                    BirthDate = example.BirthDate.Date,
                    Active = example.Active,
                    Password = Useful.ConvertSHA256(example.Password)
                };
                examples.Remove(exampleExist);
                examples.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            Example example = examples.FirstOrDefault(o => o.Id == id);
            if (example != null)
            {
                examples.Remove(example);
                return true;
            }
            else
            {
                return false;
            } 
        }

        public List<Example> List(ExampleListDTO exampleListDTO)
        {            
            List<Example> listExample = examples.OrderByDescending(o => o.Id).Skip((exampleListDTO.PageSize * (exampleListDTO.PageIndex - 1))).Take(exampleListDTO.PageSize).ToList();
            return listExample;
        }

        public long TotalRecords()
        {
            long totalRecords = examples.LongCount();
            return totalRecords;
        }

        public bool ExistByRutAndNotSameEntity(ExampleExistByRutAndNotSameEntityDTO exampleExistByRutAndNotSameEntityDTO)
        {
            Example example = examples.FirstOrDefault(o => o.Id != exampleExistByRutAndNotSameEntityDTO.Id && o.Rut == exampleExistByRutAndNotSameEntityDTO.Rut);
            if (example == null)
                return false;
            else
                return true;
        }

        public ExampleExcelDTO Excel()
        {
            ExampleExcelDTO exampleExcelDTO = null;
            MemoryStream memoryStream = null;
            SLDocument sLDocument = null;
            try
            {
                memoryStream = new MemoryStream();
                sLDocument = new SLDocument();
                sLDocument.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Example");
                sLDocument.SetCellValue("B3", "Example");

                SLStyle sLStyleTitle = sLDocument.CreateStyle();
                sLStyleTitle.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
                sLStyleTitle.Font.Bold = true;
                sLStyleTitle.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleTitle.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleTitle.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleTitle.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLDocument.MergeWorksheetCells("B3", "D3", sLStyleTitle);
                sLDocument.MergeWorksheetCells("B4", "D4");

                SLPicture sLPicture = new SLPicture(Useful.GetImagePath());
                sLPicture.SetPosition(4, 1);
                sLDocument.InsertPicture(sLPicture);

                SLStyle sLStyleHeader = sLDocument.CreateStyle();
                sLStyleHeader.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
                sLStyleHeader.Font.Bold = true;
                sLStyleHeader.SetWrapText(true);
                sLStyleHeader.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleHeader.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleHeader.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleHeader.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                System.Drawing.Color backgroundColor = GetBackgroundColorHeaders();
                sLStyleHeader.SetPatternFill(PatternValues.Solid, backgroundColor, backgroundColor);
                
                sLDocument.SetCellValue("F3", "Id");
                sLDocument.SetCellStyle("F3", sLStyleHeader);
                sLDocument.SetCellValue("G3", "Rut");
                sLDocument.SetCellStyle("G3", sLStyleHeader);
                sLDocument.SetCellValue("H3", "Name");
                sLDocument.SetCellStyle("H3", sLStyleHeader);
                sLDocument.SetColumnWidth("H3", 12.00);
                sLDocument.SetCellValue("I3", "LastName");
                sLDocument.SetCellStyle("I3", sLStyleHeader);
                sLDocument.SetColumnWidth("I3", 12.00);
                sLDocument.SetCellValue("J3", "BirthDate");
                sLDocument.SetCellStyle("J3", sLStyleHeader);
                sLDocument.SetColumnWidth("J3", 11.00);                
                sLDocument.SetCellValue("K3", "Active");
                sLDocument.SetCellStyle("K3", sLStyleHeader);
                sLDocument.SetColumnWidth("K3", 12.00);
                sLDocument.SetCellValue("L3", "Password");
                sLDocument.SetCellStyle("L3", sLStyleHeader);
                sLDocument.SetColumnWidth("L3", 68.00);

                SLStyle sLStyleBody = sLDocument.CreateStyle();
                sLStyleBody.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
                sLStyleBody.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleBody.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleBody.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleBody.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                int index = 4;
                foreach (var item in examples)
                {
                    sLDocument.SetCellValue($"F{index}", item.Id);
                    sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                    sLDocument.SetCellValue($"G{index}", item.Rut);
                    sLDocument.SetCellStyle($"G{index}", sLStyleBody);
                    sLDocument.SetCellValue($"H{index}", item.Name);
                    sLDocument.SetCellStyle($"H{index}", sLStyleBody);
                    sLDocument.SetCellValue($"I{index}", item.LastName);
                    sLDocument.SetCellStyle($"I{index}", sLStyleBody);
                    sLDocument.SetCellValue($"J{index}", item.BirthDate.ToString("yyyy-MM-dd"));
                    sLDocument.SetCellStyle($"J{index}", sLStyleBody);
                    sLDocument.SetCellValue($"K{index}", item.Active);
                    sLDocument.SetCellStyle($"K{index}", sLStyleBody);
                    sLDocument.SetCellValue($"L{index}", item.Password);
                    sLDocument.SetCellStyle($"L{index}", sLStyleBody);
                    index++;
                }

                sLDocument.SaveAs(memoryStream);

                exampleExcelDTO = new ExampleExcelDTO()
                {
                    FileName = "Examples.xlsx",
                    FileContent = memoryStream.ToArray()
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                if (sLDocument != null)
                    sLDocument.Dispose();                    
            }
            return exampleExcelDTO;
        }

        public ExamplePDFDTO PDF()
        {
            ExamplePDFDTO examplePDFDTO = null;
            MemoryStream memoryStream = null;            
            Document documentPDF = null;
            PdfWriter pdfWriter = null;
            System.Drawing.Image contentImage = null;
            try
            {
                memoryStream = new MemoryStream();
                documentPDF = new Document(PageSize.A3, 25, 25, 25, 25); 
                pdfWriter = PdfWriter.GetInstance(documentPDF, memoryStream);

                documentPDF.Open();
                documentPDF.AddAuthor("Ignacio Chávez");

                PdfContentByte pdfContentByte = pdfWriter.DirectContent;
                
                PdfPTable pdfTableTitle = new PdfPTable(1);
                pdfTableTitle.TotalWidth = 140;                

                iTextSharp.text.Font fontHeader = new iTextSharp.text.Font(FontFactory.GetFont("Calibri").BaseFont, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                PdfPCell pdfPCellTitle = new PdfPCell(new Phrase("Example", fontHeader));
                pdfPCellTitle.BorderWidth = 1;
                pdfPCellTitle.BorderColor = BaseColor.BLACK;
                pdfPCellTitle.BackgroundColor = BaseColor.WHITE;
                pdfPCellTitle.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                
                pdfTableTitle.AddCell(pdfPCellTitle);
                pdfTableTitle.WriteSelectedRows(0, -1, documentPDF.LeftMargin, documentPDF.Top - 20, pdfContentByte);

                contentImage = System.Drawing.Image.FromFile(Useful.GetImagePath());
                Image image = Image.GetInstance(contentImage, System.Drawing.Imaging.ImageFormat.Png);
                image.ScaleToFit(140, 237);
                image.Alignment = Image.UNDERLYING;
                image.SetAbsolutePosition(documentPDF.LeftMargin, documentPDF.Top - 185);

                documentPDF.Add(image);

                int index = 1;
                int size = GetPageSizeMaximun();
                int length = examples.Count() / size;
                for (int i = 0; i <= length; i++)
                {
                    List<Example> examplesByPage = examples.OrderBy(o => o.Id).Skip(size * (index - 1)).Take(size).ToList();

                    if (examplesByPage.Count() == 0)
                        break;

                    PdfPTable pdfTable = new PdfPTable(7);
                    pdfTable.TotalWidth = 547;

                    int widthId = 48;
                    int widthRut = 47;
                    int widthName = 62;
                    int widthLastName = 63;
                    int widthBirthDate = 56;
                    int widthActive = 67;
                    int widthPassword = 201;

                    pdfTable.SetWidths(new int[] { widthId, widthRut, widthName, widthLastName, widthBirthDate, widthActive, widthPassword });

                    PdfPCell pdfPCellId = new PdfPCell(new Phrase("Id", fontHeader));
                    pdfPCellId.BorderWidth = 1;
                    pdfPCellId.BorderColor = BaseColor.BLACK;
                    pdfPCellId.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellId.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellRut = new PdfPCell(new Phrase("Rut", fontHeader));
                    pdfPCellRut.BorderWidth = 1;
                    pdfPCellRut.BorderColor = BaseColor.BLACK;
                    pdfPCellRut.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellRut.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellName = new PdfPCell(new Phrase("Name", fontHeader));
                    pdfPCellName.BorderWidth = 1;
                    pdfPCellName.BorderColor = BaseColor.BLACK;
                    pdfPCellName.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellName.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellLastName = new PdfPCell(new Phrase("LastName", fontHeader));
                    pdfPCellLastName.BorderWidth = 1;
                    pdfPCellLastName.BorderColor = BaseColor.BLACK;
                    pdfPCellLastName.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellLastName.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellBirthDate = new PdfPCell(new Phrase("BirthDate", fontHeader));
                    pdfPCellBirthDate.BorderWidth = 1;
                    pdfPCellBirthDate.BorderColor = BaseColor.BLACK;
                    pdfPCellBirthDate.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellBirthDate.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellActive = new PdfPCell(new Phrase("Active", fontHeader));
                    pdfPCellActive.BorderWidth = 1;
                    pdfPCellActive.BorderColor = BaseColor.BLACK;
                    pdfPCellActive.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellActive.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellPassword = new PdfPCell(new Phrase("Password", fontHeader));
                    pdfPCellPassword.BorderWidth = 1;
                    pdfPCellPassword.BorderColor = BaseColor.BLACK;
                    pdfPCellPassword.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellPassword.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    pdfTable.AddCell(pdfPCellId);
                    pdfTable.AddCell(pdfPCellRut);
                    pdfTable.AddCell(pdfPCellName);
                    pdfTable.AddCell(pdfPCellLastName);
                    pdfTable.AddCell(pdfPCellBirthDate);
                    pdfTable.AddCell(pdfPCellActive);
                    pdfTable.AddCell(pdfPCellPassword);

                    iTextSharp.text.Font fontBody = new iTextSharp.text.Font(FontFactory.GetFont("Calibri").BaseFont, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                    foreach (var item in examplesByPage)
                    {
                        pdfPCellId = new PdfPCell(new Phrase(item.Id.ToString(), fontBody));
                        pdfPCellId.BorderWidth = 1;
                        pdfPCellId.BorderColor = BaseColor.BLACK;
                        pdfPCellId.BackgroundColor = BaseColor.WHITE;

                        pdfPCellRut = new PdfPCell(new Phrase(item.Rut, fontBody));
                        pdfPCellRut.BorderWidth = 1;
                        pdfPCellRut.BorderColor = BaseColor.BLACK;
                        pdfPCellRut.BackgroundColor = BaseColor.WHITE;

                        pdfPCellName = new PdfPCell(new Phrase(item.Name, fontBody));
                        pdfPCellName.BorderWidth = 1;
                        pdfPCellName.BorderColor = BaseColor.BLACK;
                        pdfPCellName.BackgroundColor = BaseColor.WHITE;

                        pdfPCellLastName = new PdfPCell(new Phrase(item.LastName, fontBody));
                        pdfPCellLastName.BorderWidth = 1;
                        pdfPCellLastName.BorderColor = BaseColor.BLACK;
                        pdfPCellLastName.BackgroundColor = BaseColor.WHITE;

                        pdfPCellBirthDate = new PdfPCell(new Phrase(item.BirthDate.ToString("yyyy-MM-dd"), fontBody));
                        pdfPCellBirthDate.BorderWidth = 1;
                        pdfPCellBirthDate.BorderColor = BaseColor.BLACK;
                        pdfPCellBirthDate.BackgroundColor = BaseColor.WHITE;

                        pdfPCellActive = new PdfPCell(new Phrase((item.Active) ? "VERDADERO" : "FALSO", fontBody));
                        pdfPCellActive.BorderWidth = 1;
                        pdfPCellActive.BorderColor = BaseColor.BLACK;
                        pdfPCellActive.BackgroundColor = BaseColor.WHITE;

                        pdfPCellPassword = new PdfPCell(new Phrase(item.Password, fontBody));
                        pdfPCellPassword.BorderWidth = 1;
                        pdfPCellPassword.BorderColor = BaseColor.BLACK;
                        pdfPCellPassword.BackgroundColor = BaseColor.WHITE;

                        pdfTable.AddCell(pdfPCellId);
                        pdfTable.AddCell(pdfPCellRut);
                        pdfTable.AddCell(pdfPCellName);
                        pdfTable.AddCell(pdfPCellLastName);
                        pdfTable.AddCell(pdfPCellBirthDate);
                        pdfTable.AddCell(pdfPCellActive);
                        pdfTable.AddCell(pdfPCellPassword);
                    }

                    pdfTable.WriteSelectedRows(0, -1, 210, documentPDF.Top - 20, pdfContentByte);

                    documentPDF.NewPage();

                    index++;
                }                

                documentPDF.Close();

                examplePDFDTO = new ExamplePDFDTO()
                {
                    FileName = "Examples.pdf",
                    FileContent = memoryStream.ToArray()
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                if (documentPDF != null)
                {
                    if (documentPDF.IsOpen())
                        documentPDF.Close();

                    documentPDF.Dispose();
                }
                if (pdfWriter != null)
                    pdfWriter.Dispose();
                if (contentImage != null)
                    contentImage.Dispose();
            }
            return examplePDFDTO;
        }

        private int GetPageSizeMaximun()
        {
            return Convert.ToInt32(Useful.GetAppSettings("PDFExampleSizeMaximunOfRecords"));
        }

        private System.Drawing.Color GetBackgroundColorHeaders()
        {
            return System.Drawing.ColorTranslator.FromHtml("#A9D08E");
        }
    }
}
