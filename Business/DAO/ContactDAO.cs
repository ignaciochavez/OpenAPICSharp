using Business.DTO;
using Business.Tool;
using DataSource.Comic;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Business.Interface;
using System.Data.SqlClient;

namespace Business.DAO
{
    public class ContactDAO : IContact
    {
        public static ContactDAO Instance
        {
            get
            {
                return new ContactDAO();
            }
        }

        public ContactDAO()
        {

        }

        public Entity.Contact Select(int id)
        {

            Contact entity = ModelComic.ComicEntities.Contact.FirstOrDefault(o => o.Id == id);
            Entity.Contact contact = (entity != null) ? new Entity.Contact(entity.Id, entity.Email, entity.Phone) : null;
            return contact;
        }

        public bool ExistById(int id)
        {
            bool isExist = ModelComic.ComicEntities.Contact.Any(o => o.Id == id);
            return isExist;
        }

        public int Insert(ContactInsertDTO contactInsertDTO)
        {
            int isInsert = 0;
            Contact contact = new Contact();
            contact.Email = contactInsertDTO.Email.Trim().ToLower();
            contact.Phone = contactInsertDTO.Phone.Trim();
            using (var context = ModelComic.ComicEntities)
            {
                context.Contact.Add(contact);
                isInsert = context.SaveChanges();
            }
            return (isInsert > 0) ? contact.Id : 0;
        }

        public bool Update(Entity.Contact contact)
        {
            int isUpdate = 0;
            using (var context = ModelComic.ComicEntities)
            {
                Contact entity = context.Contact.FirstOrDefault(o => o.Id == contact.Id);
                if (entity != null)
                {
                    entity.Email = contact.Email.Trim().ToLower();
                    entity.Phone = contact.Phone.Trim();
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
                Contact entity = context.Contact.FirstOrDefault(o => o.Id == id);
                if (entity != null)
                {
                    context.Contact.Remove(entity);
                    isDelete = context.SaveChanges();
                }
            }            
            return (isDelete > 0);
        }

        public List<Entity.Contact> List()
        {
            List<Entity.Contact> list = new List<Entity.Contact>();
            var entities = ModelComic.ComicEntities.SPListContact();
            foreach (var item in entities)
            {
                Entity.Contact contact = new Entity.Contact(item.Id, item.Email, item.Phone);
                list.Add(contact);
            }
            return list;
        }

        public List<Entity.Contact> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            List<Entity.Contact> list = new List<Entity.Contact>();
            var entities = ModelComic.ComicEntities.SPListContactPaginated(listPaginatedDTO.PageIndex, listPaginatedDTO.PageSize);
            foreach (var item in entities)
            {
                Entity.Contact contact = new Entity.Contact(item.Id, item.Email, item.Phone);
                list.Add(contact);
            }
            return list;
        }

        public long TotalRecords()
        {
            long totalRecords = ModelComic.ComicEntities.Contact.LongCount();
            return totalRecords;
        }

        public List<Entity.Contact> Search(ContactSearchDTO contactSearchDTO)
        {
            string whereClause = string.Empty;
            whereClause = ((contactSearchDTO.Id > 0) ? "[Id] = @Id" : string.Empty);
            whereClause += ((!string.IsNullOrEmpty(contactSearchDTO.Email)) ? ((whereClause.Length > 0) ? " AND [Email] LIKE '%' + @Email + '%'" : "[Email] LIKE '%' + @Email + '%'") : string.Empty);
            whereClause += ((!string.IsNullOrEmpty(contactSearchDTO.Phone)) ? ((whereClause.Length > 0) ? " AND [Phone] LIKE '%' + @Phone + '%'" : "[Phone] LIKE '%' + @Phone + '%'") : string.Empty);
            string paginatedClause = $"ORDER BY [Id] DESC OFFSET {(contactSearchDTO.ListPaginatedDTO.PageIndex - 1) * contactSearchDTO.ListPaginatedDTO.PageSize} ROWS FETCH NEXT {contactSearchDTO.ListPaginatedDTO.PageSize} ROWS ONLY";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (contactSearchDTO.Id > 0)
                parameters.Add(new SqlParameter("Id", contactSearchDTO.Id));
            if (!string.IsNullOrWhiteSpace(contactSearchDTO.Email))
                parameters.Add(new SqlParameter("Email", contactSearchDTO.Email.Trim()));
            if (!string.IsNullOrWhiteSpace(contactSearchDTO.Phone))
                parameters.Add(new SqlParameter("Phone", contactSearchDTO.Phone.Trim()));

            List<Entity.Contact> list = new List<Entity.Contact>();
            List<Contact> entities = ModelComic.ComicEntities.Contact.SqlQuery($"SELECT [Id], [Email], [Phone] FROM [dbo].[Contact] WHERE {whereClause} {paginatedClause}", parameters.ToArray()).ToList();
            foreach (var item in entities)
            {
                Entity.Contact entity = new Entity.Contact(item.Id, item.Email, item.Phone);
                list.Add(entity);
            }
            return list;
        }

        public FileDTO Excel(string timeZoneInfoName)
        {
            FileDTO fileDTO = null;
            MemoryStream memoryStream = null;
            SLDocument sLDocument = null;
            try
            {
                memoryStream = new MemoryStream();
                sLDocument = Useful.GetSpreadsheetLightBase("Contact", timeZoneInfoName);

                SLStyle sLStyleHeaderTable = Useful.GetSpreadsheetLightStyleCellTableHeader(sLDocument);
                sLDocument.SetCellValue("D9", "Id");
                sLDocument.SetCellStyle("D9", sLStyleHeaderTable);
                sLDocument.SetCellValue("E9", "Email");
                sLDocument.SetCellStyle("E9", sLStyleHeaderTable);
                sLDocument.SetCellValue("F9", "Phone");
                sLDocument.SetCellStyle("F9", sLStyleHeaderTable);

                SLStyle sLStyleId = Useful.GetSpreadsheetLightStyleCellIdTable(sLDocument);
                sLStyleId.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBody = Useful.GetSpreadsheetLightStyleCellTableBody(sLDocument);

                SLStyle sLStyleIdDegrade = Useful.GetSpreadsheetLightStyleCellIdTableDegrade(sLDocument);
                sLStyleIdDegrade.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBodyDegrade = Useful.GetSpreadsheetLightStyleCellTableBodyDegrade(sLDocument);

                sLDocument.SetColumnWidth("E10", 33.00);
                sLDocument.SetColumnWidth("F10", 25.00);

                int index = 10;
                var contact = List();
                foreach (var item in contact)
                {
                    if ((index % 2) == 0)
                    {
                        sLDocument.SetCellValue($"D{index}", item.Id);
                        sLDocument.SetCellStyle($"D{index}", sLStyleId);
                        sLDocument.SetCellValue($"E{index}", item.Email);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBody);
                        sLDocument.SetCellValue($"F{index}", item.Phone);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                    }
                    else
                    {
                        sLDocument.SetCellValue($"D{index}", item.Id);
                        sLDocument.SetCellStyle($"D{index}", sLStyleIdDegrade);
                        sLDocument.SetCellValue($"E{index}", item.Email);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"F{index}", item.Phone);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBodyDegrade);
                    }
                    index++;
                }

                sLDocument.SaveAs(memoryStream);
                fileDTO = new FileDTO("Contact.xlsx", memoryStream.ToArray());
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

                iTextSharp.text.Image imageLogo = Useful.GetiTextSharpImageLogo();
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

                PdfPTable pdfPTableDescription = Useful.GetiTextSharpTableDescription("Contact", totalRecords);
                documentPDF.Add(pdfPTableDescription);

                documentPDF.Add(new Phrase("\n"));

                int index = 1;
                int size = GetPDFContactSizeMaximunOfRecordsByPage();
                long length = totalRecords / size;
                List<Entity.Contact> contacts = List();
                for (int i = 0; i <= length; i++)
                {
                    List<Entity.Contact> contactsByPage = contacts.Skip(size * (index - 1)).Take(size).ToList();

                    if (contactsByPage.Count() == 0)
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

                    PdfPTable pdfPTable = new PdfPTable(3);
                    pdfPTable.HorizontalAlignment = 1;

                    PdfPCell pdfPCellId = Useful.GetiTextSharpCellTableHeader("Id");
                    PdfPCell pdfPCellEmail = Useful.GetiTextSharpCellTableHeader("Email");
                    PdfPCell pdfPCellPhone = Useful.GetiTextSharpCellTableHeader("Phone");

                    pdfPTable.AddCell(pdfPCellId);
                    pdfPTable.AddCell(pdfPCellEmail);
                    pdfPTable.AddCell(pdfPCellPhone);

                    int count = 0;
                    foreach (var item in contactsByPage)
                    {
                        if ((count % 2) == 0)
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBodyDegrade(item.Id.ToString());
                            pdfPCellEmail = Useful.GetiTextSharpCellTableBodyDegrade(item.Email);
                            pdfPCellPhone = Useful.GetiTextSharpCellTableBodyDegrade(item.Phone);
                        }
                        else
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBody(item.Id.ToString());
                            pdfPCellEmail = Useful.GetiTextSharpCellTableBody(item.Email);
                            pdfPCellPhone = Useful.GetiTextSharpCellTableBody(item.Phone);
                        }

                        pdfPTable.AddCell(pdfPCellId);
                        pdfPTable.AddCell(pdfPCellEmail);
                        pdfPTable.AddCell(pdfPCellPhone);

                        count++;
                    }

                    documentPDF.Add(pdfPTable);
                    documentPDF.NewPage();

                    index++;
                }

                documentPDF.Dispose();
                documentPDF.Close();

                fileDTO = new FileDTO("Contact.pdf", memoryStream.ToArray());
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

        private int GetPDFContactSizeMaximunOfRecordsByPage()
        {
            return Convert.ToInt32(Useful.GetAppSettings("PDFContactSizeMaximunOfRecordsByPage"));
        }

        private System.Drawing.Color GetBackgroundColorHeaders()
        {
            return System.Drawing.ColorTranslator.FromHtml("#A9D08E");
        }
    }
}
