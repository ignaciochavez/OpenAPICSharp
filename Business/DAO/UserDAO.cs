using Business.DTO;
using Business.Tool;
using DataSource.Comic;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Business.Interface;

namespace Business.DAO
{
    public class UserDAO : IUser
    {
        public static UserDAO Instance
        {
            get
            {
                return new UserDAO();
            }
        }

        public UserDAO()
        {

        }

        public Entity.User Select(UserSelectDTO userSelectDTO)
        {
            var entity = ModelComic.ComicEntities.User.SqlQuery("SELECT [Id], [Rut], [Name], [LastName], [BirthDate], NULL AS Password, [Active], ([dbo].[FNDateTimeOffset]([Registered], @TimeZoneInfoName)) AS Registered, [ContactId], [RoleId] FROM [dbo].[User] WHERE [Id] = @Id", new SqlParameter("Id", userSelectDTO.Id), new SqlParameter("TimeZoneInfoName", userSelectDTO.TimeZoneInfoName)).FirstOrDefault();
            Entity.User user = (entity != null) ? new Entity.User(entity.Id, entity.Rut, entity.Name, entity.LastName, entity.BirthDate, entity.Active, entity.Registered, entity.ContactId, entity.RoleId) : null;
            return user;
        }

        public Entity.User SelectByRut(string rut)
        {
            var entity = ModelComic.ComicEntities.User.FirstOrDefault(o => o.Rut == rut);
            Entity.User user = (entity != null) ? new Entity.User(entity.Id, entity.Rut, entity.Name, entity.LastName, entity.BirthDate, entity.Active, entity.Registered, entity.ContactId, entity.RoleId) : null;
            return user;
        }

        public bool Exist(int id)
        {
            bool exist = ModelComic.ComicEntities.User.Any(o => o.Id == id);
            return exist;
        }

        public bool ExistByRut(string rut)
        {
            bool exist = ModelComic.ComicEntities.User.Any(o => o.Name == rut.Trim().ToUpper());
            return exist;
        }

        public int Insert(UserInsertDTO userInsertDTO)
        {
            int isInsert = 0;
            User user = new User();
            user.Rut = userInsertDTO.Rut.Trim().ToUpper();
            user.Name = Useful.GetTitleCaseWords(userInsertDTO.Name.Trim());
            user.LastName = Useful.GetTitleCaseWords(userInsertDTO.LastName.Trim());
            user.BirthDate = userInsertDTO.BirthDate;
            user.Password = Useful.ConvertSHA256(userInsertDTO.Password);
            user.Active = userInsertDTO.Active.Value;
            user.Registered = DateTimeOffset.UtcNow;
            user.ContactId = userInsertDTO.ContactId;
            user.RoleId = userInsertDTO.RoleId;
            using (var context = ModelComic.ComicEntities)
            {
                context.User.Add(user);
                isInsert = context.SaveChanges();
            }
            return (isInsert > 0) ? user.Id : 0;
        }

        public bool Update(UserUpdateDTO userUpdateDTO)
        {
            int isUpdate = 0;
            using (var context = ModelComic.ComicEntities)
            {
                User entity = context.User.FirstOrDefault(o => o.Id == userUpdateDTO.Id);
                if (entity != null)
                {
                    entity.Rut = userUpdateDTO.Rut.Trim().ToUpper();
                    entity.Name = Useful.GetTitleCaseWords(userUpdateDTO.Name.Trim());
                    entity.LastName = Useful.GetTitleCaseWords(userUpdateDTO.LastName.Trim());
                    entity.BirthDate = userUpdateDTO.BirthDate;
                    entity.Password = Useful.ConvertSHA256(userUpdateDTO.Password);
                    entity.Active = userUpdateDTO.Active.Value;
                    entity.ContactId = userUpdateDTO.ContactId;
                    entity.RoleId = userUpdateDTO.RoleId;
                    isUpdate = context.SaveChanges();
                }
            }            
            return (isUpdate > 0);
        }

        public bool Delete(int id)
        {
            int isDelete = 0;
            using (var context = ModelComic.ComicEntities)
            {
                User entity = context.User.FirstOrDefault(o => o.Id == id);
                if (entity != null)
                {
                    context.User.Remove(entity);
                    isDelete = context.SaveChanges();
                }
            }            
            return (isDelete > 0);
        }

        public List<Entity.User> List(string timeZoneInfoName)
        {
            List<Entity.User> list = new List<Entity.User>();
            var entities = ModelComic.ComicEntities.SPListUser(timeZoneInfoName);
            foreach (var item in entities)
            {
                Entity.User user = new Entity.User(item.Id, item.Rut, item.Name, item.LastName, item.BirthDate, item.Active, item.Registered.Value, item.ContactId, item.RoleId);
                list.Add(user);
            }
            return list;
        }

        public List<Entity.User> ListPaginated(UserListPaginatedDTO userListPaginatedDTO)
        {
            List<Entity.User> list = new List<Entity.User>();
            var entities = ModelComic.ComicEntities.SPListUserPaginated(userListPaginatedDTO.ListPaginatedDTO.PageIndex, userListPaginatedDTO.ListPaginatedDTO.PageSize, userListPaginatedDTO.TimeZoneInfoName);
            foreach (var item in entities)
            {
                Entity.User user = new Entity.User(item.Id, item.Rut, item.Name, item.LastName, item.BirthDate, item.Active, item.Registered.Value, item.ContactId, item.RoleId);
                list.Add(user);
            }
            return list;
        }

        public long TotalRecords()
        {
            long totalRecords = ModelComic.ComicEntities.User.LongCount();
            return totalRecords;
        }

        public List<Entity.User> Search(UserSearchDTO userSearchDTO)
        {
            string whereClause = string.Empty;
            whereClause = ((userSearchDTO.Id > 0) ? "[Id] = @Id" : string.Empty);
            whereClause += ((!string.IsNullOrEmpty(userSearchDTO.Rut)) ? ((whereClause.Length > 0) ? " AND [Rut] LIKE '%' + @Rut + '%'" : "[Rut] LIKE '%' + @Rut + '%'") : string.Empty);
            whereClause += ((!string.IsNullOrEmpty(userSearchDTO.Name)) ? ((whereClause.Length > 0) ? " AND [Name] LIKE '%' + @Name + '%'" : "[Name] LIKE '%' + @Name + '%'") : string.Empty);
            whereClause += ((!string.IsNullOrEmpty(userSearchDTO.LastName)) ? ((whereClause.Length > 0) ? " AND [LastName] LIKE '%' + @LastName + '%'" : "[LastName] LIKE '%' + @LastName + '%'") : string.Empty);
            whereClause += ((Useful.ValidateDateTime(userSearchDTO.BirthDate)) ? ((whereClause.Length > 0) ? " AND [BirthDate] LIKE '%' + CONVERT(VARCHAR(10), @BirthDate, 23) + '%'" : "[BirthDate] LIKE '%' + CONVERT(VARCHAR(10), @BirthDate, 23) + '%'") : string.Empty);
            whereClause += ((userSearchDTO.Active != null) ? ((whereClause.Length > 0) ? " AND [Active] = @Active" : "[Active] = @Active") : string.Empty);
            whereClause += ((Useful.ValidateDateTimeOffset(userSearchDTO.Registered)) ? ((whereClause.Length > 0) ? " AND CONVERT(VARCHAR(10), [Registered], 23) = CONVERT(VARCHAR(10), @Registered, 23)" : "CONVERT(VARCHAR(10), [Registered], 23) = CONVERT(VARCHAR(10), @Registered, 23)") : string.Empty);
            whereClause += ((userSearchDTO.ContactId > 0) ? ((whereClause.Length > 0) ? " AND [ContactId] = @ContactId" : "[ContactId] = @ContactId") : string.Empty);
            whereClause += ((userSearchDTO.RoleId > 0) ? ((whereClause.Length > 0) ? " AND [RoleId] = @RoleId" : "[RoleId] = @RoleId") : string.Empty);
            string paginatedClause = $"ORDER BY [Id] ASC OFFSET {(userSearchDTO.ListPaginatedDTO.PageIndex - 1) * userSearchDTO.ListPaginatedDTO.PageSize} ROWS FETCH NEXT {userSearchDTO.ListPaginatedDTO.PageSize} ROWS ONLY";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (userSearchDTO.Id > 0)
                parameters.Add(new SqlParameter("Id", userSearchDTO.Id));
            if (!string.IsNullOrWhiteSpace(userSearchDTO.Rut))
                parameters.Add(new SqlParameter("Rut", userSearchDTO.Rut.Trim().ToUpper()));
            if (!string.IsNullOrWhiteSpace(userSearchDTO.Name))
                parameters.Add(new SqlParameter("Name", Useful.GetTitleCaseWords(userSearchDTO.Name.Trim())));
            if (!string.IsNullOrWhiteSpace(userSearchDTO.LastName))
                parameters.Add(new SqlParameter("LastName", Useful.GetTitleCaseWords(userSearchDTO.LastName.Trim())));
            if (Useful.ValidateDateTime(userSearchDTO.BirthDate))
                parameters.Add(new SqlParameter("BirthDate", userSearchDTO.BirthDate.Date));
            if (userSearchDTO.Active != null)
                parameters.Add(new SqlParameter("Active", userSearchDTO.Active));
            if (Useful.ValidateDateTimeOffset(userSearchDTO.Registered))
                parameters.Add(new SqlParameter("Registered", userSearchDTO.Registered.Date));
            if (userSearchDTO.ContactId > 0)
                parameters.Add(new SqlParameter("ContactId", userSearchDTO.ContactId));
            if (userSearchDTO.RoleId > 0)
                parameters.Add(new SqlParameter("RoleId", userSearchDTO.RoleId));

            parameters.Add(new SqlParameter("@TimeZoneInfoName", userSearchDTO.TimeZoneInfoName));

            List<Entity.User> list = new List<Entity.User>();
            List<User> entities = ModelComic.ComicEntities.User.SqlQuery($"SELECT [Id], [Rut], [Name], [LastName], [BirthDate], NULL AS Password, [Active], ([dbo].[FNDateTimeOffset]([Registered], @TimeZoneInfoName)) AS Registered, [ContactId], [RoleId] FROM [dbo].[User] WHERE {whereClause} {paginatedClause}", parameters.ToArray()).ToList();
            foreach (var item in entities)
            {
                Entity.User user = new Entity.User(item.Id, item.Rut, item.Name, item.LastName, item.BirthDate, item.Active, item.Registered, item.ContactId, item.RoleId);
                list.Add(user);
            }
            return list;

        }

        public bool ExistByRutAndNotSameEntity(UserExistByRutAndNotSameEntityDTO userExistByRutAndNotSameEntityDTO)
        {
            bool exist = false;
            if (ModelComic.ComicEntities.User.Any(o => o.Id == userExistByRutAndNotSameEntityDTO.Id))
            {
                List<User> list = ModelComic.ComicEntities.User.Where(o => o.Rut == userExistByRutAndNotSameEntityDTO.Rut).ToList();
                if (list != null && list.Count() > 0)
                    exist = list.Any(o => o.Id != userExistByRutAndNotSameEntityDTO.Id);
            }            
            return exist;
        }

        public FileDTO Excel(string timeZoneInfoName)
        {
            FileDTO fileDTO = null;
            MemoryStream memoryStream = null;
            SLDocument sLDocument = null;
            try
            {
                memoryStream = new MemoryStream();
                sLDocument = Useful.GetSpreadsheetLightBase("User", timeZoneInfoName);

                SLStyle sLStyleHeaderTable = Useful.GetSpreadsheetLightStyleCellTableHeader(sLDocument);
                sLDocument.SetCellValue("C9", "Id");
                sLDocument.SetCellStyle("C9", sLStyleHeaderTable);
                sLDocument.SetCellValue("D9", "Rut");
                sLDocument.SetCellStyle("D9", sLStyleHeaderTable);
                sLDocument.SetCellValue("E9", "Name");
                sLDocument.SetCellStyle("E9", sLStyleHeaderTable);
                sLDocument.SetCellValue("F9", "LastName");
                sLDocument.SetCellStyle("F9", sLStyleHeaderTable);
                sLDocument.SetCellValue("G9", "BirthDate");
                sLDocument.SetCellStyle("G9", sLStyleHeaderTable);
                sLDocument.SetCellValue("H9", "Active");
                sLDocument.SetCellStyle("H9", sLStyleHeaderTable);
                sLDocument.SetCellValue("I9", "Registered");
                sLDocument.SetCellStyle("I9", sLStyleHeaderTable);
                sLDocument.SetCellValue("J9", "ContactId");
                sLDocument.SetCellStyle("J9", sLStyleHeaderTable);
                sLDocument.SetCellValue("K9", "RoleId");
                sLDocument.SetCellStyle("K9", sLStyleHeaderTable);

                SLStyle sLStyleId = Useful.GetSpreadsheetLightStyleCellIdTable(sLDocument);
                sLStyleId.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBody = Useful.GetSpreadsheetLightStyleCellTableBody(sLDocument);

                SLStyle sLStyleIdDegrade = Useful.GetSpreadsheetLightStyleCellIdTableDegrade(sLDocument);
                sLStyleIdDegrade.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBodyDegrade = Useful.GetSpreadsheetLightStyleCellTableBodyDegrade(sLDocument);
                
                int index = 10;
                var users = List(timeZoneInfoName);
                foreach (var item in users)
                {
                    if ((index % 2) == 0)
                    {
                        sLDocument.SetCellValue($"C{index}", item.Id.ToString());
                        sLDocument.SetCellStyle($"C{index}", sLStyleId);
                        sLDocument.SetCellValue($"D{index}", item.Rut);
                        sLDocument.SetCellStyle($"D{index}", sLStyleBody);
                        sLDocument.SetCellValue($"E{index}", item.Name);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBody);
                        sLDocument.SetCellValue($"F{index}", item.LastName);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                        sLDocument.SetCellValue($"G{index}", item.BirthDate.ToString("yyyy-MM-dd"));
                        sLDocument.SetCellStyle($"G{index}", sLStyleBody);
                        sLDocument.SetCellValue($"H{index}", item.Active);
                        sLDocument.SetCellStyle($"H{index}", sLStyleBody);
                        sLDocument.SetCellValue($"I{index}", Useful.ConvertDateTimeOffsetToTimeZone(item.Registered, timeZoneInfoName).ToString("yyyy-MM-dd HH:mm:sszzz"));
                        sLDocument.SetCellStyle($"I{index}", sLStyleBody);
                        sLDocument.SetCellValue($"J{index}", item.ContactId.ToString());
                        sLDocument.SetCellStyle($"J{index}", sLStyleBody);
                        sLDocument.SetCellValue($"K{index}", item.RoleId.ToString());
                        sLDocument.SetCellStyle($"K{index}", sLStyleBody);                        
                    }
                    else
                    {
                        sLDocument.SetCellValue($"C{index}", item.Id.ToString());
                        sLDocument.SetCellStyle($"C{index}", sLStyleIdDegrade);
                        sLDocument.SetCellValue($"D{index}", item.Rut);
                        sLDocument.SetCellStyle($"D{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"E{index}", item.Name);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"F{index}", item.LastName);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"G{index}", item.BirthDate.ToString("yyyy-MM-dd"));
                        sLDocument.SetCellStyle($"G{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"H{index}", item.Active);
                        sLDocument.SetCellStyle($"H{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"I{index}", Useful.ConvertDateTimeOffsetToTimeZone(item.Registered, timeZoneInfoName).ToString("yyyy-MM-dd HH:mm:sszzz"));
                        sLDocument.SetCellStyle($"I{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"J{index}", item.ContactId.ToString());
                        sLDocument.SetCellStyle($"J{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"K{index}", item.RoleId.ToString());
                        sLDocument.SetCellStyle($"K{index}", sLStyleBodyDegrade);
                    }
                    index++;
                }

                sLDocument.SaveAs(memoryStream);
                fileDTO = new FileDTO("User.xlsx", memoryStream.ToArray());
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
            return fileDTO;
        }

        public FileDTO PDF(string timeZoneInfoName)
        {
            FileDTO fileDTO = null;
            MemoryStream memoryStream = null;
            PdfWriter pdfWriter = null;
            try
            {
                memoryStream = new MemoryStream();
                Document documentPDF = new Document(PageSize.A3, 25, 25, 25, 25);
                pdfWriter = PdfWriter.GetInstance(documentPDF, memoryStream);

                documentPDF.Open();
                documentPDF.AddAuthor("Ignacio Chávez");

                PdfContentByte pdfContentByte = pdfWriter.DirectContent;

                PdfPTable pdfPTableHeaderOne = Useful.GetiTextSharpTableHeaderOne(timeZoneInfoName);
                documentPDF.Add(pdfPTableHeaderOne);

                PdfPTable pdfPTableHeaderTwo = Useful.GetiTextSharpTableHeaderTwo();
                documentPDF.Add(pdfPTableHeaderTwo);

                Image imageLogo = Useful.GetiTextSharpImageLogo();
                imageLogo.SetAbsolutePosition(documentPDF.LeftMargin + 40, documentPDF.Top - 55);
                documentPDF.Add(imageLogo);

                PdfPTable pdfPTableTitle = Useful.GetiTextSharpTitle();
                pdfPTableTitle.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 41, documentPDF.Top - 50, pdfContentByte);

                PdfPTable pdfPTableDateTime = Useful.GetiTextSharpDateTime(timeZoneInfoName);
                pdfPTableDateTime.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 640, documentPDF.Top - 5, pdfContentByte);

                PdfPTable pdfPTablePageNumber = Useful.GetiTextSharpTablePageNumber(1);
                pdfPTablePageNumber.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 760, documentPDF.Bottom + 20, pdfContentByte);

                documentPDF.Add(new Phrase("\n"));

                long totalRecords = TotalRecords();

                PdfPTable pdfPTableDescription = Useful.GetiTextSharpTableDescription("User", totalRecords);
                documentPDF.Add(pdfPTableDescription);

                documentPDF.Add(new Phrase("\n"));

                int index = 1;
                int size = GetPDFUserSizeMaximunOfRecordsByPage();
                long length = TotalRecords() / size;
                List<Entity.User> users = List(timeZoneInfoName);
                for (int i = 0; i <= length; i++)
                {
                    List<Entity.User> usersByPage = users.Skip(size * (index - 1)).Take(size).ToList();

                    if (usersByPage.Count() == 0)
                        break;

                    if (index != 1)
                    {
                        documentPDF.Add(pdfPTableHeaderOne);
                        documentPDF.Add(pdfPTableHeaderTwo);
                        documentPDF.Add(imageLogo);
                        pdfPTableTitle.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 41, documentPDF.Top - 50, pdfContentByte);
                        pdfPTableDateTime.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 640, documentPDF.Top - 5, pdfContentByte);

                        pdfPTablePageNumber = Useful.GetiTextSharpTablePageNumber(index);
                        pdfPTablePageNumber.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 760, documentPDF.Bottom + 20, pdfContentByte);

                        documentPDF.Add(new Phrase("\n\n"));
                    }

                    PdfPTable pdfPTable = new PdfPTable(9);
                    pdfPTable.HorizontalAlignment = 1;

                    PdfPCell pdfPCellId = Useful.GetiTextSharpCellTableHeader("Id");
                    PdfPCell pdfPCellRut = Useful.GetiTextSharpCellTableHeader("Rut");
                    PdfPCell pdfPCellName = Useful.GetiTextSharpCellTableHeader("Name");
                    PdfPCell pdfPCellLastName = Useful.GetiTextSharpCellTableHeader("LastName");
                    PdfPCell pdfPCellBirthDate = Useful.GetiTextSharpCellTableHeader("BirthDate");
                    PdfPCell pdfPCellActive = Useful.GetiTextSharpCellTableHeader("Active");
                    PdfPCell pdfPCellRegistered = Useful.GetiTextSharpCellTableHeader("Registered");
                    PdfPCell pdfPCellContactId = Useful.GetiTextSharpCellTableHeader("ContactId");
                    PdfPCell pdfPCellRoleId = Useful.GetiTextSharpCellTableHeader("RoleId");

                    pdfPTable.AddCell(pdfPCellId);
                    pdfPTable.AddCell(pdfPCellRut);
                    pdfPTable.AddCell(pdfPCellName);
                    pdfPTable.AddCell(pdfPCellLastName);
                    pdfPTable.AddCell(pdfPCellBirthDate);
                    pdfPTable.AddCell(pdfPCellActive);
                    pdfPTable.AddCell(pdfPCellRegistered);
                    pdfPTable.AddCell(pdfPCellContactId);
                    pdfPTable.AddCell(pdfPCellRoleId);

                    int count = 0;
                    foreach (var item in usersByPage)
                    {
                        if ((count % 2) == 0)
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBodyDegrade(item.Id.ToString());
                            pdfPCellRut = Useful.GetiTextSharpCellTableBodyDegrade(item.Rut);
                            pdfPCellName = Useful.GetiTextSharpCellTableBodyDegrade(item.Name);
                            pdfPCellLastName = Useful.GetiTextSharpCellTableBodyDegrade(item.LastName);
                            pdfPCellBirthDate = Useful.GetiTextSharpCellTableBodyDegrade(item.BirthDate.ToString("yyyy-MM-dd"));
                            pdfPCellActive = Useful.GetiTextSharpCellTableBodyDegrade(((item.Active) ? "VERDADERO" : "FALSO"));
                            pdfPCellRegistered = Useful.GetiTextSharpCellTableBodyDegrade(Useful.ConvertDateTimeOffsetToTimeZone(item.Registered, timeZoneInfoName).ToString("yyyy-MM-dd HH:mm:sszzz"));
                            pdfPCellContactId = Useful.GetiTextSharpCellTableBodyDegrade(item.ContactId.ToString());
                            pdfPCellRoleId = Useful.GetiTextSharpCellTableBodyDegrade(item.RoleId.ToString());
                        }
                        else
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBody(item.Id.ToString());
                            pdfPCellRut = Useful.GetiTextSharpCellTableBody(item.Rut);
                            pdfPCellName = Useful.GetiTextSharpCellTableBody(item.Name);
                            pdfPCellLastName = Useful.GetiTextSharpCellTableBody(item.LastName);
                            pdfPCellBirthDate = Useful.GetiTextSharpCellTableBody(item.BirthDate.ToString("yyyy-MM-dd"));
                            pdfPCellActive = Useful.GetiTextSharpCellTableBody(((item.Active) ? "VERDADERO" : "FALSO"));
                            pdfPCellRegistered = Useful.GetiTextSharpCellTableBody(Useful.ConvertDateTimeOffsetToTimeZone(item.Registered, timeZoneInfoName).ToString("yyyy-MM-dd HH:mm:sszzz"));
                            pdfPCellContactId = Useful.GetiTextSharpCellTableBody(item.ContactId.ToString());
                            pdfPCellRoleId = Useful.GetiTextSharpCellTableBody(item.RoleId.ToString());
                        }


                        pdfPTable.AddCell(pdfPCellId);
                        pdfPTable.AddCell(pdfPCellRut);
                        pdfPTable.AddCell(pdfPCellName);
                        pdfPTable.AddCell(pdfPCellLastName);
                        pdfPTable.AddCell(pdfPCellBirthDate);
                        pdfPTable.AddCell(pdfPCellActive);
                        pdfPTable.AddCell(pdfPCellRegistered);
                        pdfPTable.AddCell(pdfPCellContactId);
                        pdfPTable.AddCell(pdfPCellRoleId);

                        count++;
                    }


                    documentPDF.Add(pdfPTable);
                    documentPDF.NewPage();

                    index++;
                }

                documentPDF.Dispose();
                documentPDF.Close();

                fileDTO = new FileDTO("User.pdf", memoryStream.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                if (pdfWriter != null)
                    pdfWriter.Dispose();
            }
            return fileDTO;
        }

        private int GetPDFUserSizeMaximunOfRecordsByPage()
        {
            return Convert.ToInt32(Useful.GetAppSettings("PDFUserSizeMaximunOfRecordsByPage"));
        }

        private System.Drawing.Color GetBackgroundColorHeaders()
        {
            return System.Drawing.ColorTranslator.FromHtml("#A9D08E");
        }
    }
}
