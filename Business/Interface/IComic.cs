using Business.DTO;
using Business.Entity;
using Business.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IComic
    {
        bool PingDataBase();
        bool DeleteHero(int id);
        bool DeleteUser(int id);
        bool InsertHero(ComicInsertHeroDTO comicInsertHeroDTO);
        bool InsertUser(ComicInsertUserDTO comicInsertUserDTO);
        MessageVO Login(ComicLoginDTO comicLoginDTO);
        SelectHero SelectHero(int id);
        SelectUser SelectUser(ComicSelectUserDTO comicSelectUserDTO);
        bool UpdateHero(ComicUpdateHeroDTO comicUpdateHeroDTO);
        bool UpdateUser(ComicUpdateUserDTO comicUpdateUserDTO);
    }
}
