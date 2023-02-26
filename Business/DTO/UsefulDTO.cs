using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class FileDTO
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }

        public FileDTO()
        {

        }

        public FileDTO(string fileName, byte[] fileContent)
        {
            FileName = fileName;
            FileContent = fileContent;
        }
    }

    public class ListPaginatedDTO
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public ListPaginatedDTO()
        {

        }

        public ListPaginatedDTO(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    public class UsefulTimeZoneInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public UsefulTimeZoneInfo()
        {

        }

        public UsefulTimeZoneInfo(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
