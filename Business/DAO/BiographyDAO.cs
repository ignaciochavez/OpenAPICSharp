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
    public class BiographyDAO : IBiography
    {
        public static BiographyDAO Instance
        {
            get
            {
                return new BiographyDAO();
            }
        }

        public BiographyDAO()
        {

        }

        public Entity.Biography Select(int id)
        {
            Biography entity = ModelComic.ComicEntities.Biography.FirstOrDefault(o => o.Id == id);
            Entity.Biography biography = (entity != null) ? new Entity.Biography(entity.Id, entity.FullName, entity.Gender, entity.Appearance, entity.Alias, entity.Publisher) : null;
            return biography;
        }

        public bool ExistById(int id)
        {
            bool isExist = ModelComic.ComicEntities.Biography.Any(o => o.Id == id);
            return isExist;
        }

        public int Insert(BiographyInsertDTO biographyInsertDTO)
        {
            Biography biography = new Biography();
            biography.FullName = biographyInsertDTO.FullName;
            biography.Gender = biographyInsertDTO.Gender.ToUpper();
            biography.Appearance = biographyInsertDTO.Appearance;
            biography.Alias = biographyInsertDTO.Alias;
            biography.Publisher = biographyInsertDTO.Publisher;
            int isInsert = ModelComic.ComicEntities.SaveChanges();
            return (isInsert > 0) ? biography.Id : 0;
        }

        public bool Update(Entity.Biography biography)
        {
            int isUpdate = 0;
            Biography entity = ModelComic.ComicEntities.Biography.FirstOrDefault(o => o.Id == biography.Id);
            if (entity != null)
            {
                entity.FullName = biography.FullName;
                entity.Gender = biography.Gender.ToUpper();
                entity.Appearance = biography.Appearance;
                entity.Alias = biography.Alias;
                entity.Publisher = biography.Publisher;
                isUpdate = ModelComic.ComicEntities.SaveChanges();
            }
            return (isUpdate > 0);
        }

        public bool Delete(int id)
        {
            int isDelete = 0;
            Biography entity = ModelComic.ComicEntities.Biography.FirstOrDefault(o => o.Id == id);
            if (entity != null)
            {
                ModelComic.ComicEntities.Biography.Remove(entity);
                isDelete = ModelComic.ComicEntities.SaveChanges();
            }
            return (isDelete > 0);
        }

        public List<Entity.Biography> List()
        {
            List<Entity.Biography> list = new List<Entity.Biography>();
            var entities = ModelComic.ComicEntities.SPListBiography();
            foreach (var item in entities)
            {
                Entity.Biography biography = new Entity.Biography(item.Id, item.FullName, item.Gender, item.Appearance, item.Alias, item.Publisher);
                list.Add(biography);
            }
            return list;
        }

        public List<Entity.Biography> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            List<Entity.Biography> list = new List<Entity.Biography>();
            var entities = ModelComic.ComicEntities.SPListBiographyPaginated(listPaginatedDTO.PageIndex, listPaginatedDTO.PageSize);
            foreach (var item in entities)
            {
                Entity.Biography biography = new Entity.Biography(item.Id, item.FullName, item.Gender, item.Appearance, item.Alias, item.Publisher);
                list.Add(biography);
            }
            return list;
        }

        public long TotalRecords()
        {
            long totalRecords = ModelComic.ComicEntities.Biography.LongCount();
            return totalRecords;
        }

        public List<Entity.Biography> Search(BiographySearchDTO biographySearchDTO)
        {
            string whereClause = string.Empty;
            whereClause = ((biographySearchDTO.Id > 0) ? "[Id] = @Id" : string.Empty);
            whereClause += ((!string.IsNullOrWhiteSpace(biographySearchDTO.FullName)) ? ((whereClause.Length > 0) ? " AND [FullName] LIKE '%' + @FullName + '%'" : "[FullName] LIKE '%' + @FullName + '%'") : string.Empty);
            whereClause += ((!string.IsNullOrWhiteSpace(biographySearchDTO.Gender)) ? ((whereClause.Length > 0) ? " AND [Gender] LIKE '%' + @Gender + '%'" : "[Gender] LIKE '%' + @Gender + '%'") : string.Empty);
            whereClause += ((biographySearchDTO.Appearance != null && Useful.ValidateDateTimeOffset(biographySearchDTO.Appearance) && biographySearchDTO.Appearance < DateTimeOffset.Now) ? ((whereClause.Length > 0) ? " AND [Appearance] LIKE '%' + CONVERT(VARCHAR(10), @Appearance, 23) + '%'" : "[Appearance] LIKE '%' + CONVERT(VARCHAR(10), @Appearance, 23) + '%'") : string.Empty);
            whereClause += ((!string.IsNullOrWhiteSpace(biographySearchDTO.Alias)) ? ((whereClause.Length > 0) ? " AND [Alias] LIKE '%' + @Alias + '%'" : "[Alias] LIKE '%' + @Alias + '%'") : string.Empty);
            whereClause += ((!string.IsNullOrWhiteSpace(biographySearchDTO.Publisher)) ? ((whereClause.Length > 0) ? " AND [Publisher] LIKE '%' + @Publisher + '%'" : "[Publisher] LIKE '%' + @Publisher + '%'") : string.Empty);
            string paginatedClause = $"ORDER BY [Id] DESC OFFSET {(biographySearchDTO.ListPaginatedDTO.PageIndex - 1) * biographySearchDTO.ListPaginatedDTO.PageSize} ROWS FETCH NEXT {biographySearchDTO.ListPaginatedDTO.PageSize} ROWS ONLY";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (biographySearchDTO.Id > 0)
                parameters.Add(new SqlParameter("Id", biographySearchDTO.Id));
            if (!string.IsNullOrWhiteSpace(biographySearchDTO.FullName))
                parameters.Add(new SqlParameter("FullName", biographySearchDTO.FullName));
            if (!string.IsNullOrWhiteSpace(biographySearchDTO.Gender))
                parameters.Add(new SqlParameter("Gender", biographySearchDTO.Gender));
            if (biographySearchDTO.Appearance != null && Useful.ValidateDateTimeOffset(biographySearchDTO.Appearance) && biographySearchDTO.Appearance < DateTimeOffset.Now)
                parameters.Add(new SqlParameter("Appearance", biographySearchDTO.Appearance.Date));
            if (!string.IsNullOrWhiteSpace(biographySearchDTO.Alias))
                parameters.Add(new SqlParameter("Alias", biographySearchDTO.Alias));
            if (!string.IsNullOrWhiteSpace(biographySearchDTO.Publisher))
                parameters.Add(new SqlParameter("Publisher", biographySearchDTO.Publisher));

            List<Entity.Biography> list = new List<Entity.Biography>();
            List<Biography> entities = ModelComic.ComicEntities.Biography.SqlQuery($"SELECT [Id], [FullName], [Gender], [Appearance], [Alias], [Publisher] FROM [dbo].[PowerStats] WHERE {whereClause} {paginatedClause}", parameters.ToArray()).ToList();
            foreach (var item in entities)
            {
                Entity.Biography entity = new Entity.Biography(item.Id, item.FullName, item.Gender, item.Appearance, item.Alias, item.Publisher);
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
                sLDocument = Useful.GetSpreadsheetLightBase("Biography", timeZoneInfoName);

                SLStyle sLStyleHeaderTable = Useful.GetSpreadsheetLightStyleCellTableHeader(sLDocument);
                sLDocument.SetCellValue("C9", "Id");
                sLDocument.SetCellStyle("C9", sLStyleHeaderTable);
                sLDocument.SetCellValue("D9", "FullName");
                sLDocument.SetCellStyle("D9", sLStyleHeaderTable);
                sLDocument.SetCellValue("E9", "Gender");
                sLDocument.SetCellStyle("E9", sLStyleHeaderTable);
                sLDocument.SetCellValue("F9", "Appearance");
                sLDocument.SetCellStyle("F9", sLStyleHeaderTable);
                sLDocument.SetCellValue("G9", "Alias");
                sLDocument.SetCellStyle("G9", sLStyleHeaderTable);
                sLDocument.SetCellValue("H9", "Publisher");
                sLDocument.SetCellStyle("H9", sLStyleHeaderTable);

                SLStyle sLStyleId = Useful.GetSpreadsheetLightStyleCellIdTable(sLDocument);
                sLStyleId.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBody = Useful.GetSpreadsheetLightStyleCellTableBody(sLDocument);

                SLStyle sLStyleIdDegrade = Useful.GetSpreadsheetLightStyleCellIdTableDegrade(sLDocument);
                sLStyleIdDegrade.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBodyDegrade = Useful.GetSpreadsheetLightStyleCellTableBodyDegrade(sLDocument);

                int index = 10;
                var biographys = List();
                foreach (var item in biographys)
                {
                    if ((index % 2) == 0)
                    {
                        sLDocument.SetCellValue($"C{index}", item.Id);
                        sLDocument.SetCellStyle($"C{index}", sLStyleId);
                        sLDocument.SetCellValue($"D{index}", item.FullName);
                        sLDocument.SetCellStyle($"D{index}", sLStyleBody);
                        sLDocument.SetCellValue($"E{index}", item.Gender);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBody);
                        sLDocument.SetCellValue($"F{index}", item.Appearance);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                        sLDocument.SetCellValue($"G{index}", item.Alias);
                        sLDocument.SetCellStyle($"G{index}", sLStyleBody);
                        sLDocument.SetCellValue($"H{index}", item.Publisher);
                        sLDocument.SetCellStyle($"H{index}", sLStyleBody);
                    }
                    else
                    {
                        sLDocument.SetCellValue($"C{index}", item.Id);
                        sLDocument.SetCellStyle($"C{index}", sLStyleIdDegrade);
                        sLDocument.SetCellValue($"D{index}", item.FullName);
                        sLDocument.SetCellStyle($"D{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"E{index}", item.Gender);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"F{index}", item.Appearance);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"G{index}", item.Alias);
                        sLDocument.SetCellStyle($"G{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"H{index}", item.Publisher);
                        sLDocument.SetCellStyle($"H{index}", sLStyleBodyDegrade);
                    }
                    index++;
                }

                sLDocument.SaveAs(memoryStream);
                fileDTO = new FileDTO("Biography.xlsx", memoryStream.ToArray());
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

                PdfPTable pdfPTableDescription = Useful.GetiTextSharpTableDescription("Biography", totalRecords);
                documentPDF.Add(pdfPTableDescription);

                documentPDF.Add(new Phrase("\n"));

                int index = 1;
                int size = GetPDFBiographySizeMaximunOfRecordsByPage();
                long length = totalRecords / size;
                List<Entity.Biography> biography = List();
                for (int i = 0; i <= length; i++)
                {
                    List<Entity.Biography> biographyByPage = biography.Skip(size * (index - 1)).Take(size).ToList();

                    if (biographyByPage.Count() == 0)
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

                    PdfPTable pdfPTable = new PdfPTable(6);
                    pdfPTable.HorizontalAlignment = 1;

                    PdfPCell pdfPCellId = Useful.GetiTextSharpCellTableHeader("Id");
                    PdfPCell pdfPCellFullName = Useful.GetiTextSharpCellTableHeader("FullName");
                    PdfPCell pdfPCellGender = Useful.GetiTextSharpCellTableHeader("Gender");
                    PdfPCell pdfPCellAppearance = Useful.GetiTextSharpCellTableHeader("Appearance");
                    PdfPCell pdfPCellAlias = Useful.GetiTextSharpCellTableHeader("Alias");
                    PdfPCell pdfPCellPublisher = Useful.GetiTextSharpCellTableHeader("Publisher");
                    
                    pdfPTable.AddCell(pdfPCellId);
                    pdfPTable.AddCell(pdfPCellFullName);
                    pdfPTable.AddCell(pdfPCellGender);
                    pdfPTable.AddCell(pdfPCellAppearance);
                    pdfPTable.AddCell(pdfPCellAlias);
                    pdfPTable.AddCell(pdfPCellPublisher);

                    int count = 0;
                    foreach (var item in biographyByPage)
                    {
                        if ((count % 2) == 0)
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBodyDegrade(item.Id.ToString());
                            pdfPCellFullName = Useful.GetiTextSharpCellTableBodyDegrade(item.FullName);
                            pdfPCellGender = Useful.GetiTextSharpCellTableBodyDegrade(item.Gender);
                            pdfPCellAppearance = Useful.GetiTextSharpCellTableBodyDegrade(item.Appearance.ToString("yyyy-MM-dd"));
                            pdfPCellAlias = Useful.GetiTextSharpCellTableBodyDegrade(item.Alias);
                            pdfPCellPublisher = Useful.GetiTextSharpCellTableBodyDegrade(item.Publisher);
                        }
                        else
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBody(item.Id.ToString());
                            pdfPCellFullName = Useful.GetiTextSharpCellTableBody(item.FullName);
                            pdfPCellGender = Useful.GetiTextSharpCellTableBody(item.Gender);
                            pdfPCellAppearance = Useful.GetiTextSharpCellTableBody(item.Appearance.ToString("yyyy-MM-dd"));
                            pdfPCellAlias = Useful.GetiTextSharpCellTableBody(item.Alias);
                            pdfPCellPublisher = Useful.GetiTextSharpCellTableBody(item.Publisher);
                        }

                        pdfPTable.AddCell(pdfPCellId);
                        pdfPTable.AddCell(pdfPCellFullName);
                        pdfPTable.AddCell(pdfPCellGender);
                        pdfPTable.AddCell(pdfPCellAppearance);
                        pdfPTable.AddCell(pdfPCellAlias);
                        pdfPTable.AddCell(pdfPCellPublisher);

                        count++;
                    }

                    documentPDF.Add(pdfPTable);
                    documentPDF.NewPage();

                    index++;
                }

                documentPDF.Dispose();
                documentPDF.Close();

                fileDTO = new FileDTO("Biography.pdf", memoryStream.ToArray());
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

        private int GetPDFBiographySizeMaximunOfRecordsByPage()
        {
            return Convert.ToInt32(Useful.GetAppSettings("PDFBiographySizeMaximunOfRecordsByPage"));
        }

        private System.Drawing.Color GetBackgroundColorHeaders()
        {
            return System.Drawing.ColorTranslator.FromHtml("#A9D08E");
        }
    }
}
