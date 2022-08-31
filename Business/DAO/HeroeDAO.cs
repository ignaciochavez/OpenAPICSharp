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
    public class HeroeDAO : IHeroe
    {
        List<Heroe> listHeroeData = new List<Heroe>();

        public HeroeDAO()
        {
            listHeroeData.Add(new Heroe()
            {
                Id = 1,
                Name = "Aquaman",
                Description = "El poder más reconocido de Aquaman es la capacidad telepática para comunicarse con la vida marina, la cual puede convocar a grandes distancias.",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\aquaman.png"),
                Appearance = new DateTimeOffset(new DateTime(1941, 11, 1)),
                Home = "DC"
            });
            listHeroeData.Add(new Heroe()
            {
                Id = 2,
                Name = "Batman",
                Description = "Los rasgos principales de Batman se resumen en «destreza física, habilidades deductivas y obsesión». La mayor parte de las características básicas de los cómics han variado por las diferentes interpretaciones que le han dado al personaje.",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\batman.png"),
                Appearance = new DateTimeOffset(new DateTime(1939, 5, 1)),
                Home = "DC"
            });
            listHeroeData.Add(new Heroe()
            {
                Id = 3,
                Name = "Daredevil",
                Description = "Al haber perdido la vista, los cuatro sentidos restantes de Daredevil fueron aumentados por la radiación a niveles superhumanos, en el accidente que tuvo cuando era niño. A pesar de su ceguera, puede \"ver\" a través de un \"sexto sentido\" que le sirve como un radar similar al de los murciélagos.",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\daredevil.png"),
                Appearance = new DateTimeOffset(new DateTime(1964, 1, 1)),
                Home = "Marvel"
            });
            listHeroeData.Add(new Heroe()
            {
                Id = 4,
                Name = "Hulk",
                Description = "Su principal poder es su capacidad de aumentar su fuerza hasta niveles prácticamente ilimitados a la vez que aumenta su furia. Dependiendo de qué personalidad de Hulk esté al mando en ese momento (el Hulk Banner es el más débil, pero lo compensa con su inteligencia).",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\hulk.png"),
                Appearance = new DateTimeOffset(new DateTime(1962, 5, 1)),
                Home = "Marvel"
            });
            listHeroeData.Add(new Heroe()
            {
                Id = 5,
                Name = "Linterna Verde",
                Description = "Poseedor del anillo de poder que posee la capacidad de crear manifestaciones de luz sólida mediante la utilización del pensamiento. Es alimentado por la Llama Verde (revisada por escritores posteriores como un poder místico llamado Starheart), una llama mágica contenida en dentro de un orbe (el orbe era en realidad un meteorito verde de metal que cayó a la Tierra, el cual encontró un fabricante de lámparas llamado Chang)",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\linterna-verde.png"),
                Appearance = new DateTimeOffset(new DateTime(1940, 6, 1)),
                Home = "DC"
            });
            listHeroeData.Add(new Heroe()
            {
                Id = 6,
                Name = "Spider-Man",
                Description = "Tras ser mordido por una araña radiactiva, obtuvo los siguientes poderes sobrehumanos, una gran fuerza, agilidad, poder trepar por paredes. La fuerza de Spider-Man le permite levantar 10 toneladas o más. Gracias a esta gran fuerza Spider-Man puede realizar saltos íncreibles. Un \"sentido arácnido\", que le permite saber si un peligro se cierne sobre él, antes de que suceda. En ocasiones este puede llevar a Spider-Man al origen del peligro.",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\spiderman.png"),
                Appearance = new DateTimeOffset(new DateTime(1962, 8, 1)),
                Home = "Marvel"
            });
            listHeroeData.Add(new Heroe()
            {
                Id = 7,
                Name = "Wolverine",
                Description = "En el universo ficticio de Marvel, Wolverine posee poderes regenerativos que pueden curar cualquier herida, por mortal que ésta sea, además ese mismo poder hace que sea inmune a cualquier enfermedad existente en la Tierra y algunas extraterrestres . Posee también una fuerza sobrehumana, que si bien no se compara con la de otros superhéroes como Hulk, sí sobrepasa la de cualquier humano.",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\wolverine.png"),
                Appearance = new DateTimeOffset(new DateTime(1974, 11, 1)),
                Home = "Marvel"
            });
        }

        public Heroe Select(string name)
        {
            Heroe heroe = listHeroeData.FirstOrDefault(o => o.Name == name);
            return heroe;
        }

        public bool Exist(string name)
        {
            bool exist = listHeroeData.Any(o => o.Name == name);
            return exist;
        }

        public Heroe Insert(HeroeInsertDTO heroeInsertDTO)
        {
            Heroe heroe = new Heroe()
            {
                Id = listHeroeData.Last().Id + 1,
                Name = heroeInsertDTO.Name,
                Description = heroeInsertDTO.Description,
                ImgBase64String = heroeInsertDTO.ImgBase64String,
                Appearance = heroeInsertDTO.Appearance,
                Home = heroeInsertDTO.Home
            };
            listHeroeData.Add(heroe);
            return heroe;
        }

        public bool Update(HeroeUpdateDTO heroeUpdateDTO)
        {
            Heroe heroeExist = listHeroeData.FirstOrDefault(o => o.Name == heroeUpdateDTO.Name);
            if (heroeExist != null)
            {
                Heroe heroe = new Heroe()
                {
                    Id = heroeExist.Id,
                    Name = heroeUpdateDTO.Name,
                    Description = heroeUpdateDTO.Description,
                    ImgBase64String = heroeUpdateDTO.ImgBase64String,
                    Appearance = heroeUpdateDTO.Appearance,
                    Home = heroeUpdateDTO.Home
                };
                listHeroeData.Remove(heroeExist);
                listHeroeData.Add(heroe);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(string name)
        {
            Heroe heroe = listHeroeData.FirstOrDefault(o => o.Name == name);
            if (heroe != null)
            {
                listHeroeData.Remove(heroe);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Heroe> List(HeroeListDTO heroeListDTO)
        {
            List<Heroe> listHeroe = listHeroeData.OrderBy(o => o.Id).Skip((heroeListDTO.PageSize * (heroeListDTO.PageIndex - 1))).Take(heroeListDTO.PageSize).ToList();
            return listHeroe;
        }

        public long Count()
        {
            long count = listHeroeData.LongCount();
            return count;
        }
    }
}
