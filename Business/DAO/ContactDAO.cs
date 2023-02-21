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
    public class ContactDAO : IContacto
    {
        public ContactDAO()
        {

        }

        public Entity.Contacto Select(int id)
        {
            Entity.Contacto contact = null;
            var entity = ModelComic.ComicEntities.Contacto.FirstOrDefault(o => o.Id == id);

            if (entity != null)
                contact = new Entity.Contacto(entity.Id, entity.CorreoElectronico, entity.Telefono);

            return contact;
        }

        public int Insert(ContactoInsertDTO contactoInsertDTO)
        {
            Contacto contacto = new Contacto();
            contacto.CorreoElectronico = contactoInsertDTO.CorreoElectronico.Trim().ToLower();
            contacto.Telefono = contactoInsertDTO.Telefono.Trim();
            int insert = ModelComic.ComicEntities.SaveChanges();
            return (insert > 0) ? contacto.Id : 0;
        }

        public bool Update(Entity.Contacto contacto)
        {
            int isUpdate = 0;
            Contacto entity = ModelComic.ComicEntities.Contacto.FirstOrDefault(o => o.Id == contacto.Id);
            if (entity != null)
            {
                entity.CorreoElectronico = contacto.CorreoElectronico.Trim().ToLower();
                entity.Telefono = contacto.Telefono.Trim();
                isUpdate = ModelComic.ComicEntities.SaveChanges();
            }
            return (isUpdate > 0);
        }

        public bool Delete(int id)
        {
            int isDelete = 0;
            Contacto entity = ModelComic.ComicEntities.Contacto.FirstOrDefault(o => o.Id == id);
            if (entity != null)
            {
                ModelComic.ComicEntities.Contacto.Remove(entity);
                isDelete = ModelComic.ComicEntities.SaveChanges();
            }
            return (isDelete > 0);
        }

        public List<Entity.Contacto> List()
        {
            List<Entity.Contacto> list = new List<Entity.Contacto>();
            var entities = ModelComic.ComicEntities.SPListContacto();
            foreach (var item in entities)
            {
                Entity.Contacto contacto = new Entity.Contacto(item.Id, item.CorreoElectronico, item.Telefono);
                list.Add(contacto);
            }
            return list;
        }

        public List<Entity.Contacto> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            List<Entity.Contacto> list = new List<Entity.Contacto>();
            var entities = ModelComic.ComicEntities.SPListContactoPaginated(listPaginatedDTO.PageIndex, listPaginatedDTO.PageSize);
            foreach (var item in entities)
            {
                Entity.Contacto contacto = new Entity.Contacto(item.Id, item.CorreoElectronico, item.Telefono);
                list.Add(contacto);
            }
            return list;
        }

        public long TotalRecords()
        {
            long totalRecords = ModelComic.ComicEntities.Contacto.LongCount();
            return totalRecords;
        }

        public List<Entity.Contacto> Search(ContactoSearchDTO contactoSearchDTO)
        {
            string whereClause = string.Empty;
            whereClause = ((contactoSearchDTO.Id > 0) ? "[Id] = @Id" : string.Empty);
            whereClause += ((!string.IsNullOrEmpty(contactoSearchDTO.CorreoElectronico)) ? ((whereClause.Length > 0) ? " AND [CorreoElectronico] LIKE '%' + @CorreoElectronico + '%'" : "[CorreoElectronico] LIKE '%' + @CorreoElectronico + '%'") : string.Empty);
            whereClause += ((!string.IsNullOrEmpty(contactoSearchDTO.Telefono)) ? ((whereClause.Length > 0) ? " AND [Telefono] LIKE '%' + @Telefono + '%'" : "[Telefono] LIKE '%' + @Telefono + '%'") : string.Empty);
            string paginatedClause = $"ORDER BY [Id] DESC OFFSET({contactoSearchDTO.ListPaginatedDTO.PageIndex - 1}) * {contactoSearchDTO.ListPaginatedDTO.PageSize} ROWS FETCH NEXT {contactoSearchDTO.ListPaginatedDTO.PageSize} ROWS ONLY";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (contactoSearchDTO.Id > 0)
                parameters.Add(new SqlParameter("Id", contactoSearchDTO.Id));
            if (!string.IsNullOrWhiteSpace(contactoSearchDTO.CorreoElectronico))
                parameters.Add(new SqlParameter("CorreoElectronico", contactoSearchDTO.CorreoElectronico.Trim()));
            if (!string.IsNullOrWhiteSpace(contactoSearchDTO.Telefono))
                parameters.Add(new SqlParameter("Telefono", contactoSearchDTO.Telefono.Trim()));

            List<Entity.Contacto> list = new List<Entity.Contacto>();
            List<Contacto> entities = ModelComic.ComicEntities.Contacto.SqlQuery($"SELECT [Id], [Email], [Telefono] FROM [dbo].[Contact] WHERE {whereClause} {paginatedClause}", parameters.ToArray()).ToList();
            foreach (var item in entities)
            {
                Entity.Contacto entity = new Entity.Contacto(item.Id, item.CorreoElectronico, item.Telefono);
                list.Add(entity);
            }
            return list;
        }

        public FileDTO Excel()
        {
            FileDTO fileDTO = null;
            MemoryStream memoryStream = null;
            SLDocument sLDocument = null;
            try
            {
                memoryStream = new MemoryStream();
                sLDocument = Useful.GetSpreadsheetLightBase();

                SLStyle sLStyleHeaderTable = Useful.GetSpreadsheetLightStyleCellTableHeader(sLDocument);
                sLDocument.SetCellValue("D9", "Id");
                sLDocument.SetCellStyle("D9", sLStyleHeaderTable);
                sLDocument.SetCellValue("E9", "CorreoElectronico");
                sLDocument.SetCellStyle("E9", sLStyleHeaderTable);
                sLDocument.SetCellValue("F9", "Telefono");
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
                var contactos = List();
                foreach (var item in contactos)
                {
                    if ((index % 2) == 0)
                    {
                        sLDocument.SetCellValue($"D{index}", item.Id);
                        sLDocument.SetCellStyle($"D{index}", sLStyleId);
                        sLDocument.SetCellValue($"E{index}", item.CorreoElectronico);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBody);
                        sLDocument.SetCellValue($"F{index}", item.Telefono);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                    }
                    else
                    {
                        sLDocument.SetCellValue($"D{index}", item.Id);
                        sLDocument.SetCellStyle($"D{index}", sLStyleIdDegrade);
                        sLDocument.SetCellValue($"E{index}", item.CorreoElectronico);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"F{index}", item.Telefono);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBodyDegrade);
                    }
                    index++;
                }

                sLDocument.SaveAs(memoryStream);
                fileDTO = new FileDTO("Contacto.xlsx", memoryStream.ToArray());
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

        public FileDTO PDF()
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

                PdfPTable pdfPTableHeaderOne = Useful.GetiTextSharpTableHeaderOne();
                documentPDF.Add(pdfPTableHeaderOne);

                PdfPTable pdfPTableHeaderTwo = Useful.GetiTextSharpTableHeaderTwo();
                documentPDF.Add(pdfPTableHeaderTwo);

                iTextSharp.text.Image imageLogo = Useful.GetiTextSharpImageLogo();
                imageLogo.SetAbsolutePosition(documentPDF.LeftMargin + 40, documentPDF.Top - 55);
                documentPDF.Add(imageLogo);

                PdfPTable pdfPTableTitle = Useful.GetiTextSharpTitle();
                pdfPTableTitle.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 41, documentPDF.Top - 50, pdfContentByte);

                PdfPTable pdfPTableDateTime = Useful.GetiTextSharpDateTime();
                pdfPTableDateTime.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 640, documentPDF.Top - 5, pdfContentByte);

                PdfPTable pdfPTablePageNumber = Useful.GetiTextSharpTablePageNumber(1);
                pdfPTablePageNumber.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 760, documentPDF.Bottom + 20, pdfContentByte);

                documentPDF.Add(new Phrase("\n"));

                long totalRecords = TotalRecords();

                PdfPTable pdfPTableDescription = Useful.GetiTextSharpTableDescription("Contacto", totalRecords);
                documentPDF.Add(pdfPTableDescription);

                documentPDF.Add(new Phrase("\n"));

                int index = 1;
                int size = GetPDFContactoSizeMaximunOfRecordsByPage();
                long length = totalRecords / size;
                List<Entity.Contacto> contactos = List();
                for (int i = 0; i <= length; i++)
                {
                    List<Entity.Contacto> contactosByPage = contactos.Skip(size * (index - 1)).Take(size).ToList();

                    if (contactosByPage.Count() == 0)
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

                    PdfPTable pdfPTable = new PdfPTable(4);
                    pdfPTable.HorizontalAlignment = 1;

                    PdfPCell pdfPCellId = Useful.GetiTextSharpCellTableHeader("Id");
                    PdfPCell pdfPCellCorreoElectronico = Useful.GetiTextSharpCellTableHeader("CorreoElectronico");
                    PdfPCell pdfPCellTelefono = Useful.GetiTextSharpCellTableHeader("Telefono");

                    pdfPTable.AddCell(pdfPCellId);
                    pdfPTable.AddCell(pdfPCellCorreoElectronico);
                    pdfPTable.AddCell(pdfPCellTelefono);

                    int count = 0;
                    foreach (var item in contactosByPage)
                    {
                        if ((count % 2) == 0)
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBodyDegrade(item.Id.ToString());
                            pdfPCellCorreoElectronico = Useful.GetiTextSharpCellTableBodyDegrade(item.CorreoElectronico);
                            pdfPCellTelefono = Useful.GetiTextSharpCellTableBodyDegrade(item.Telefono);
                        }
                        else
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBody(item.Id.ToString());
                            pdfPCellCorreoElectronico = Useful.GetiTextSharpCellTableBody(item.CorreoElectronico);
                            pdfPCellTelefono = Useful.GetiTextSharpCellTableBody(item.Telefono);
                        }

                        pdfPTable.AddCell(pdfPCellId);
                        pdfPTable.AddCell(pdfPCellCorreoElectronico);
                        pdfPTable.AddCell(pdfPCellTelefono);

                        count++;
                    }

                    documentPDF.Add(pdfPTable);
                    documentPDF.NewPage();

                    index++;
                }

                documentPDF.Dispose();
                documentPDF.Close();

                fileDTO = new FileDTO("Contacto.pdf", memoryStream.ToArray());
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

        private int GetPDFContactoSizeMaximunOfRecordsByPage()
        {
            return Convert.ToInt32(Useful.GetAppSettings("PDFContactoSizeMaximunOfRecordsByPage"));
        }

        private System.Drawing.Color GetBackgroundColorHeaders()
        {
            return System.Drawing.ColorTranslator.FromHtml("#A9D08E");
        }
    }
}
