using Business.DAO;
using Business.DTO;
using Business.Entity;
using Business.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementation
{
    public static class ComicImpl
    {
        public static bool PingDataBase()
        {
            return ComicDAO.Instance.PingDataBase();
        }

        public static bool DeleteHero(int id)
        {
            return ComicDAO.Instance.DeleteHero(id);
        }

        public static bool DeleteUser(int id)
        {
            return ComicDAO.Instance.DeleteUser(id);
        }

        public static bool InsertHero(ComicInsertHeroDTO comicInsertHeroDTO)
        {
            return ComicDAO.Instance.InsertHero(comicInsertHeroDTO);
        }

        public static bool InsertUser(ComicInsertUserDTO comicInsertUserDTO)
        {
            return ComicDAO.Instance.InsertUser(comicInsertUserDTO);
        }

        public static MessageVO Login(ComicLoginDTO comicLoginDTO)
        {
            return ComicDAO.Instance.Login(comicLoginDTO);
        }

        public static SelectHero SelectHero(int id)
        {
            return ComicDAO.Instance.SelectHero(id);
        }

        public static SelectUser SelectUser(ComicSelectUserDTO comicSelectUserDTO)
        {
            return ComicDAO.Instance.SelectUser(comicSelectUserDTO);
        }

        public static bool UpdateHero(ComicUpdateHeroDTO comicUpdateHeroDTO)
        {
            return ComicDAO.Instance.UpdateHero(comicUpdateHeroDTO);
        }

        public static bool UpdateUser(ComicUpdateUserDTO comicUpdateUserDTO)
        {
            return ComicDAO.Instance.UpdateUser(comicUpdateUserDTO);
        }
    }
}
