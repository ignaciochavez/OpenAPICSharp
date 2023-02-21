using Business.DTO;
using DataSource.Comic;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight.Drawing;
using Business.Tool;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Business.Interface;
using System.Data.SqlClient;

namespace Business.DAO
{
    public class RolDAO : IRol
    {

        public RolDAO()
        {

        }

        public Entity.Rol Select(int id)
        {
            Entity.Rol role = null;
            var entity = ModelComic.ComicEntities.Rol.FirstOrDefault(o => o.Id == id);

            if (entity != null)
                role = new Entity.Rol(entity.Id, entity.Nombre);

            return role;
        }

        public bool Exist(string nombre)
        {
            bool exist = ModelComic.ComicEntities.Rol.Any(o => o.Nombre == nombre.Trim());
            return exist;
        }

        public int Insert(string nombre)
        {
            Rol rol = new Rol();
            rol.Nombre = Useful.GetTitleCaseWords(nombre.Trim());
            int insert = ModelComic.ComicEntities.SaveChanges();
            return (insert > 0) ? rol.Id : 0;
        }

        public bool Update(Entity.Rol rol)
        {
            int isUpdate = 0;
            Rol entity = ModelComic.ComicEntities.Rol.FirstOrDefault(o => o.Id == rol.Id);
            if (entity != null)
            {
                entity.Nombre = Useful.GetTitleCaseWords(rol.Nombre.Trim());
                isUpdate = ModelComic.ComicEntities.SaveChanges();
            }
            return (isUpdate > 0);
        }

        public bool Delete(int id)
        {
            int isDelete = 0;
            Rol entity = ModelComic.ComicEntities.Rol.FirstOrDefault(o => o.Id == id);
            if (entity != null)
            {
                ModelComic.ComicEntities.Rol.Remove(entity);
                isDelete = ModelComic.ComicEntities.SaveChanges();
            }
            return (isDelete > 0);
        }

        public List<Entity.Rol> List()
        {
            List<Entity.Rol> list = new List<Entity.Rol>();
            var entities = ModelComic.ComicEntities.SPListRol();
            foreach (var item in entities)
            {
                Entity.Rol rol = new Entity.Rol(item.Id, item.Nombre);
                list.Add(rol);
            }
            return list;
        }

        public List<Entity.Rol> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            List<Entity.Rol> list = new List<Entity.Rol>();
            var entities = ModelComic.ComicEntities.SPListRolPaginated(listPaginatedDTO.PageIndex, listPaginatedDTO.PageSize);
            foreach (var item in entities)
            {
                Entity.Rol rol = new Entity.Rol(item.Id, item.Nombre);
                list.Add(rol);
            }
            return list;
        }

        public long TotalRecords()
        {
            long totalRecords = ModelComic.ComicEntities.Rol.LongCount();
            return totalRecords;
        }

        public List<Entity.Rol> Search(RolSearchDTO rolSearchDTO)
        {
            string whereClause = string.Empty;
            whereClause = ((rolSearchDTO.Id > 0) ? "[Id] = @Id" : string.Empty);
            whereClause += ((!string.IsNullOrEmpty(rolSearchDTO.Nombre)) ? ((whereClause.Length > 0) ? " AND [Nombre] LIKE '%' + @Nombre + '%'" : "[Nombre] LIKE '%' + @Nombre + '%'") : string.Empty);
            string paginatedClause = $"ORDER BY [Id] ASC OFFSET({rolSearchDTO.ListPaginatedDTO.PageIndex - 1}) * {rolSearchDTO.ListPaginatedDTO.PageSize} ROWS FETCH NEXT {rolSearchDTO.ListPaginatedDTO.PageSize} ROWS ONLY";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (rolSearchDTO.Id > 0)
                parameters.Add(new SqlParameter("Id", rolSearchDTO.Id));
            if (!string.IsNullOrWhiteSpace(rolSearchDTO.Nombre))
                parameters.Add(new SqlParameter("Nombre", rolSearchDTO.Nombre.Trim()));

            List<Entity.Rol> list = new List<Entity.Rol>();
            List<Rol> entities = ModelComic.ComicEntities.Rol.SqlQuery($"SELECT [Id], [Nombre] FROM [dbo].[Rol] WHERE {whereClause} {paginatedClause}", parameters.ToArray()).ToList();
            foreach (var item in entities)
            {
                Entity.Rol entity = new Entity.Rol(item.Id, item.Nombre);
                list.Add(entity);
            }
            return list;

        }

        public bool ExistAndNotSameEntity(Entity.Rol rol)
        {
            bool exist = ModelComic.ComicEntities.Rol.Any(o => o.Id != rol.Id && o.Nombre == rol.Nombre);
            return exist;
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
                sLDocument.SetCellValue("E9", "Id");
                sLDocument.SetCellStyle("E9", sLStyleHeaderTable);
                sLDocument.SetCellValue("F9", "Nombre");
                sLDocument.SetCellStyle("F9", sLStyleHeaderTable);
                            
                SLStyle sLStyleId = Useful.GetSpreadsheetLightStyleCellIdTable(sLDocument);
                sLStyleId.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBody = Useful.GetSpreadsheetLightStyleCellTableBody(sLDocument);

                SLStyle sLStyleIdDegrade = Useful.GetSpreadsheetLightStyleCellIdTableDegrade(sLDocument);
                sLStyleIdDegrade.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBodyDegrade = Useful.GetSpreadsheetLightStyleCellTableBodyDegrade(sLDocument);

                sLDocument.SetColumnWidth("F10", 25.00);

                int index = 10;
                var roles = List();
                foreach (var item in roles)
                {
                    if ((index % 2) == 0)
                    {
                        sLDocument.SetCellValue($"E{index}", item.Id);
                        sLDocument.SetCellStyle($"E{index}", sLStyleId);
                        sLDocument.SetCellValue($"F{index}", item.Nombre);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                    }
                    else
                    {
                        sLDocument.SetCellValue($"E{index}", item.Id);
                        sLDocument.SetCellStyle($"E{index}", sLStyleIdDegrade);
                        sLDocument.SetCellValue($"F{index}", item.Nombre);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBodyDegrade);
                    }                   
                    index++;
                }

                sLDocument.SaveAs(memoryStream);
                fileDTO = new FileDTO("Rol.xlsx", memoryStream.ToArray());
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

                Image imageLogo = Useful.GetiTextSharpImageLogo();
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

                PdfPTable pdfPTableDescription = Useful.GetiTextSharpTableDescription("Rol", totalRecords);
                documentPDF.Add(pdfPTableDescription);

                documentPDF.Add(new Phrase("\n"));
                
                int index = 1;
                int size = GetPDFRolSizeMaximunOfRecordsByPage();
                long length = totalRecords / size;
                List<Entity.Rol> roles = List();
                for (int i = 0; i <= length; i++)
                {
                    List<Entity.Rol> rolesByPage = roles.Skip(size * (index - 1)).Take(size).ToList(); ;

                    if (rolesByPage.Count() == 0)
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

                    PdfPTable pdfPTable = new PdfPTable(2);
                    pdfPTable.HorizontalAlignment = 1;
                    
                    PdfPCell pdfPCellId = Useful.GetiTextSharpCellTableHeader("Id");
                    PdfPCell pdfPCellNombre = Useful.GetiTextSharpCellTableHeader("Nombre");

                    pdfPTable.AddCell(pdfPCellId);
                    pdfPTable.AddCell(pdfPCellNombre);
                    
                    int count = 0;
                    foreach (var item in rolesByPage)
                    {
                        if ((count % 2) == 0)
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBodyDegrade(item.Id.ToString());
                            pdfPCellNombre = Useful.GetiTextSharpCellTableBodyDegrade(item.Nombre);                            
                        }
                        else
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBody(item.Id.ToString());
                            pdfPCellNombre = Useful.GetiTextSharpCellTableBody(item.Nombre);
                        }
                        

                        pdfPTable.AddCell(pdfPCellId);
                        pdfPTable.AddCell(pdfPCellNombre);

                        count++;
                    }
                    

                    documentPDF.Add(pdfPTable);
                    documentPDF.NewPage();

                    index++;
                }

                documentPDF.Dispose();
                documentPDF.Close();

                fileDTO = new FileDTO("Rol.pdf", memoryStream.ToArray());
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

        private int GetPDFRolSizeMaximunOfRecordsByPage()
        {
            return Convert.ToInt32(Useful.GetAppSettings("PDFRolSizeMaximunOfRecordsByPage"));
        }

        private System.Drawing.Color GetBackgroundColorHeaders()
        {
            return System.Drawing.ColorTranslator.FromHtml("#A9D08E");
        }
    }
}
