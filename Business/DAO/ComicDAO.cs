using Business.DTO;
using Business.Interface;
using Business.Tool;
using DataSource.Comic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DAO
{
    public class ComicDAO : IComic
    {
        public static ComicDAO Instance
        {
            get
            {
                return new ComicDAO();
            }
        }

        private ContentHTML contentHTML = new ContentHTML();

        public ComicDAO()
        {

        }

        public bool PingDataBase()
        {
            int ping = ModelComic.ComicEntities.Database.SqlQuery<int>("SELECT 1").FirstOrDefault();
            return (ping > 0);
        }

        public bool DeleteHero(int id)
        {
            int storeProcedure = 0;
            Hero entity = ModelComic.ComicEntities.Hero.FirstOrDefault(o => o.Id == id);
            if (entity != null)
            {
                storeProcedure = ModelComic.ComicEntities.SPDeleteHero(id);
                if (storeProcedure > 0)
                    Useful.DeleteFile($"{Useful.GetApplicationDirectory()}{entity.ImagePath}");
            }
            return (storeProcedure > 0);
        }

        public bool DeleteUser(int id)
        {
            int storeProcedure = ModelComic.ComicEntities.SPDeleteUser(id);
            return (storeProcedure > 0);
        }

        public bool InsertHero(ComicInsertHeroDTO comicInsertHeroDTO)
        {
            int storeProcedure = ModelComic.ComicEntities.SPInsertHero(Useful.GetTitleCaseWords(comicInsertHeroDTO.Name.Trim()), comicInsertHeroDTO.Description.Trim(), Useful.SaveImage(comicInsertHeroDTO.ImageBase64String, Useful.GetTitleCaseWords(comicInsertHeroDTO.Name.Trim())),
                                                                        Useful.GetTitleCaseWords(comicInsertHeroDTO.FullName.Trim()), comicInsertHeroDTO.Gender.Trim().ToUpper(), comicInsertHeroDTO.Appearance, comicInsertHeroDTO.Alias.Trim(), 
                                                                        comicInsertHeroDTO.Publisher.Trim(), comicInsertHeroDTO.Intelligence, comicInsertHeroDTO.Strength, comicInsertHeroDTO.Speed, comicInsertHeroDTO.Durability, comicInsertHeroDTO.Power, 
                                                                        comicInsertHeroDTO.Combat);
            return (storeProcedure > 0);
        }

        public bool InsertUser(ComicInsertUserDTO comicInsertUserDTO)
        {
            int storeProcedure = ModelComic.ComicEntities.SPInsertUser(comicInsertUserDTO.Rut.Trim().ToUpper(), Useful.GetTitleCaseWords(comicInsertUserDTO.Name.Trim()), Useful.GetTitleCaseWords(comicInsertUserDTO.LastName.Trim()), comicInsertUserDTO.BirthDate, 
                                                                        Useful.ConvertSHA256(comicInsertUserDTO.Password), comicInsertUserDTO.Active, DateTimeOffset.UtcNow, comicInsertUserDTO.Email.Trim().ToLower(), comicInsertUserDTO.Phone.Trim(), comicInsertUserDTO.RoleId);
            return (storeProcedure > 0);
        }

        public MessageVO Login(ComicLoginDTO comicLoginDTO)
        {
            var storeProcedure = ModelComic.ComicEntities.SPLogin(comicLoginDTO.Rut.Trim().ToUpper(), Useful.ConvertSHA256(comicLoginDTO.Password)).FirstOrDefault();
            MessageVO messageVO = null;
            if (storeProcedure.Id.Value == 3)
                messageVO = new MessageVO(storeProcedure.Id.Value, contentHTML.GetInnerTextById("correctTitle"), storeProcedure.Message);
            else
                messageVO = new MessageVO(storeProcedure.Id.Value, contentHTML.GetInnerTextById("notAuthorizedTitle"), storeProcedure.Message);
            return messageVO;
        }

        public Entity.SelectHero SelectHero(int id)
        {
            var storeProcedure = ModelComic.ComicEntities.SPSelectHero(id).FirstOrDefault();
            Entity.SelectHero selectHero = (storeProcedure != null) ? new Entity.SelectHero(storeProcedure.HeroId.Value, storeProcedure.Name, storeProcedure.Description, Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}{storeProcedure.ImagePath}"),
                                                                                storeProcedure.BiographyId.Value, storeProcedure.FullName, storeProcedure.Gender.Trim().ToUpper(), storeProcedure.Appearance.Value, storeProcedure.Alias, storeProcedure.Publisher, storeProcedure.PowerStatsId,
                                                                                storeProcedure.Intelligence, storeProcedure.Strength, storeProcedure.Speed, storeProcedure.Durability, storeProcedure.Power, storeProcedure.Combat) : null;
            return selectHero;
        }

        public Entity.SelectUser SelectUser(ComicSelectUserDTO comicSelectUserDTO)
        {
            var storeProcedure = ModelComic.ComicEntities.SPSelectUser(comicSelectUserDTO.Id, comicSelectUserDTO.TimeZoneInfoName).FirstOrDefault();
            Entity.SelectUser selectUser = (storeProcedure != null) ? new Entity.SelectUser(storeProcedure.UserId, storeProcedure.Rut, storeProcedure.Name, storeProcedure.LastName, storeProcedure.BirthDate, storeProcedure.Active, storeProcedure.Registered.Value,
                                                                            storeProcedure.ContactId.Value, storeProcedure.Email, storeProcedure.Phone, storeProcedure.RoleId.Value, storeProcedure.RoleName) : null;
            return selectUser;
        }

        public bool UpdateHero(ComicUpdateHeroDTO comicUpdateHeroDTO)
        {
            int storeProcedure = 0;
            Hero entity = ModelComic.ComicEntities.Hero.FirstOrDefault(o => o.Id == comicUpdateHeroDTO.HeroId);
            if (entity != null)
            {
                string imagePath = Useful.SaveImage(comicUpdateHeroDTO.ImageBase64String.Trim(), Useful.GetTitleCaseWords(comicUpdateHeroDTO.Name.Trim()));
                storeProcedure = ModelComic.ComicEntities.SPUpdateHero(comicUpdateHeroDTO.HeroId, Useful.GetTitleCaseWords(comicUpdateHeroDTO.Name.Trim()), comicUpdateHeroDTO.Description.Trim(), imagePath, comicUpdateHeroDTO.BiographyId, Useful.GetTitleCaseWords(comicUpdateHeroDTO.FullName.Trim()), 
                                                                comicUpdateHeroDTO.Gender.Trim().ToUpper(), comicUpdateHeroDTO.Appearance, comicUpdateHeroDTO.Alias.Trim(), comicUpdateHeroDTO.Publisher.Trim(), comicUpdateHeroDTO.PowerStatsId, comicUpdateHeroDTO.Intelligence,
                                                                comicUpdateHeroDTO.Strength, comicUpdateHeroDTO.Speed, comicUpdateHeroDTO.Durability, comicUpdateHeroDTO.Power, comicUpdateHeroDTO.Combat);
                if (storeProcedure > 0 && entity.ImagePath != imagePath)
                    Useful.DeleteFile($"{Useful.GetApplicationDirectory()}{entity.ImagePath}");
            }
            return (storeProcedure > 0);
        }

        public bool UpdateUser(ComicUpdateUserDTO comicUpdateUserDTO)
        {
            int storeProcedure = ModelComic.ComicEntities.SPUpdateUser(comicUpdateUserDTO.UserId, comicUpdateUserDTO.Rut.Trim().ToUpper(), Useful.GetTitleCaseWords(comicUpdateUserDTO.Name.Trim()), Useful.GetTitleCaseWords(comicUpdateUserDTO.LastName.Trim()), comicUpdateUserDTO.BirthDate, 
                                                                        Useful.ConvertSHA256(comicUpdateUserDTO.Password), comicUpdateUserDTO.Active, comicUpdateUserDTO.ContactId, comicUpdateUserDTO.Email.Trim().ToLower(), comicUpdateUserDTO.Phone.Trim(), comicUpdateUserDTO.RoleId);
            return (storeProcedure > 0);
        }
    }
}
