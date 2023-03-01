using Business.DTO;
using Business.Entity;
using Business.Interface;
using Business.Tool;
using DataSource.Comic;
using System;
using System.Collections.Generic;
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

        public bool DeleteHero(int id)
        {
            int storeProcedure = ModelComic.ComicEntities.SPDeleteHero(id);
            return (storeProcedure > 0);
        }

        public bool DeleteUser(int id)
        {
            int storeProcedure = ModelComic.ComicEntities.SPDeleteUser(id);
            return (storeProcedure > 0);
        }

        public bool InsertHero(ComicInsertHeroDTO comicInsertHeroDTO)
        {
            int storeProcedure = ModelComic.ComicEntities.SPInsertHero(comicInsertHeroDTO.Name, comicInsertHeroDTO.Description, Useful.SaveImage(comicInsertHeroDTO.ImageBase64String, comicInsertHeroDTO.Name), comicInsertHeroDTO.FullName, comicInsertHeroDTO.Gender,
                                                                        comicInsertHeroDTO.Appearance, comicInsertHeroDTO.Alias, comicInsertHeroDTO.Publisher, comicInsertHeroDTO.Intelligence, comicInsertHeroDTO.Strength,
                                                                        comicInsertHeroDTO.Speed, comicInsertHeroDTO.Durability, comicInsertHeroDTO.Power, comicInsertHeroDTO.Combat);
            return (storeProcedure > 0);
        }

        public bool InsertUser(ComicInsertUserDTO comicInsertUserDTO)
        {
            int storeProcedure = ModelComic.ComicEntities.SPInsertUser(comicInsertUserDTO.Rut, comicInsertUserDTO.Name, comicInsertUserDTO.LastName, comicInsertUserDTO.BirthDate, comicInsertUserDTO.Password,
                                                                        comicInsertUserDTO.Active, DateTimeOffset.UtcNow, comicInsertUserDTO.Email, comicInsertUserDTO.Phone, comicInsertUserDTO.RoleId);
            return (storeProcedure > 0);
        }

        public MessageVO Login(ComicLoginDTO comicLoginDTO)
        {
            var storeProcedure = ModelComic.ComicEntities.SPLogin(comicLoginDTO.Rut, comicLoginDTO.Password).FirstOrDefault();
            MessageVO messageVO = null;
            if (storeProcedure.Id.Value == 3)
                messageVO = new MessageVO(storeProcedure.Id.Value, contentHTML.GetInnerTextById("correctTitle"), storeProcedure.Message);
            else
                messageVO = new MessageVO(storeProcedure.Id.Value, contentHTML.GetInnerTextById("notAuthorizedTitle"), storeProcedure.Message);
            return messageVO;
        }

        public SelectHero SelectHero(int id)
        {
            var storeProcedure = ModelComic.ComicEntities.SPSelectHero(id).FirstOrDefault();
            SelectHero selectHero = (storeProcedure != null) ? new SelectHero(storeProcedure.HeroId.Value, storeProcedure.Name, storeProcedure.Description, Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}{storeProcedure.ImagePath}"),
                                                                                storeProcedure.BiographyId.Value, storeProcedure.FullName, storeProcedure.Gender, storeProcedure.Appearance.Value, storeProcedure.Alias, storeProcedure.Publisher, storeProcedure.PowerStatsId,
                                                                                storeProcedure.Intelligence, storeProcedure.Strength, storeProcedure.Speed, storeProcedure.Durability, storeProcedure.Power, storeProcedure.Combat) : null;
            return selectHero;
        }

        public SelectUser SelectUser(ComicSelectUserDTO comicSelectUserDTO)
        {
            var storeProcedure = ModelComic.ComicEntities.SPSelectUser(comicSelectUserDTO.Id, comicSelectUserDTO.TimeZoneInfoName).FirstOrDefault();
            SelectUser selectUser = (storeProcedure != null) ? new SelectUser(storeProcedure.UserId, storeProcedure.Rut, storeProcedure.Name, storeProcedure.LastName, storeProcedure.BirthDate, storeProcedure.Active, storeProcedure.Registered.Value,
                                                                            storeProcedure.ContactId.Value, storeProcedure.Email, storeProcedure.Phone, storeProcedure.RoleId.Value, storeProcedure.RoleName) : null;
            return selectUser;
        }

        public bool UpdateHero(ComicUpdateHeroDTO comicUpdateHeroDTO)
        {
            int storeProcedure = ModelComic.ComicEntities.SPUpdateHero(comicUpdateHeroDTO.HeroId, comicUpdateHeroDTO.Name, comicUpdateHeroDTO.Description, Useful.SaveImage(comicUpdateHeroDTO.ImageBase64String.Trim(), comicUpdateHeroDTO.Name), comicUpdateHeroDTO.BiographyId,
                                                                comicUpdateHeroDTO.FullName, comicUpdateHeroDTO.Gender, comicUpdateHeroDTO.Appearance, comicUpdateHeroDTO.Alias, comicUpdateHeroDTO.Publisher, comicUpdateHeroDTO.PowerStatsId, comicUpdateHeroDTO.Intelligence,
                                                                comicUpdateHeroDTO.Strength, comicUpdateHeroDTO.Speed, comicUpdateHeroDTO.Durability, comicUpdateHeroDTO.Power, comicUpdateHeroDTO.Combat);
            return (storeProcedure > 0);
        }

        public bool UpdateUser(ComicUpdateUserDTO comicUpdateUserDTO)
        {
            int storeProcedure = ModelComic.ComicEntities.SPUpdateUser(comicUpdateUserDTO.UserId, comicUpdateUserDTO.Rut, comicUpdateUserDTO.Name, comicUpdateUserDTO.LastName, comicUpdateUserDTO.BirthDate, Useful.ConvertSHA256(comicUpdateUserDTO.Password),
                                                                        comicUpdateUserDTO.Active, comicUpdateUserDTO.ContactId, comicUpdateUserDTO.Email, comicUpdateUserDTO.Phone, comicUpdateUserDTO.RoleId);
            return (storeProcedure > 0);
        }
    }
}
