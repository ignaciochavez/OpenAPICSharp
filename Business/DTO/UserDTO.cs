using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class UserSelectDTO
    {
        public int Id { get; set; }
        public string TimeZoneInfoName { get; set; }

        public UserSelectDTO()
        {

        }

        public UserSelectDTO(int id, string timeZoneInfoName)
        {
            Id = id;
            TimeZoneInfoName = timeZoneInfoName;
        }
    }

    public class UserInsertDTO
    {
        public string Rut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }
        public int ContactId { get; set; }
        public int RoleId { get; set; }

        public UserInsertDTO()
        {

        }

        public UserInsertDTO(string rut, string name, string lastName, DateTime birthDate, string password, bool? active, int contactId, int roleId)
        {
            Rut = rut;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Password = password;
            Active = active;
            ContactId = contactId;
            RoleId = roleId;
        }
    }

    public class UserUpdateDTO
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }
        public int ContactId { get; set; }
        public int RoleId { get; set; }

        public UserUpdateDTO()
        {

        }

        public UserUpdateDTO(int id, string rut, string name, string lastName, DateTime birthDate, string password, bool? active, int contactId, int roleId)
        {
            Id = id;
            Rut = rut;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Password = password;
            Active = active;
            ContactId = contactId;
            RoleId = roleId;
        }
    }

    public class UserListPaginatedDTO
    {
        public string TimeZoneInfoName { get; set; }
        public ListPaginatedDTO ListPaginatedDTO { get; set; }

        public UserListPaginatedDTO()
        {

        }

        public UserListPaginatedDTO(string timeZoneInfoName, ListPaginatedDTO listPaginatedDTO)
        {
            TimeZoneInfoName = timeZoneInfoName;
            ListPaginatedDTO = listPaginatedDTO;
        }
    }

    public class UserSearchDTO
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public System.DateTime BirthDate { get; set; }
        public bool? Active { get; set; }
        public System.DateTimeOffset Registered { get; set; }
        public int ContactId { get; set; }
        public int RoleId { get; set; }
        public string TimeZoneInfoName { get; set; }
        public ListPaginatedDTO ListPaginatedDTO { get; set; }

        public UserSearchDTO()
        {

        }

        public UserSearchDTO(int id, string rut, string name, string lastName, DateTime birthDate, bool? active, DateTimeOffset registered, int contactId, int roleId, string timeZoneInfoName, ListPaginatedDTO listPaginatedDTO)
        {
            Id = id;
            Rut = rut;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Active = active;
            Registered = registered;
            ContactId = contactId;
            RoleId = roleId;
            TimeZoneInfoName = timeZoneInfoName;
            ListPaginatedDTO = listPaginatedDTO;
        }
    }

    public class UserExistByRutAndNotSameEntityDTO
    {
        public int Id { get; set; }
        public string Rut { get; set; }

        public UserExistByRutAndNotSameEntityDTO()
        {

        }

        public UserExistByRutAndNotSameEntityDTO(int id, string rut)
        {
            Id = id;
            Rut = rut;
        }
    }
}
