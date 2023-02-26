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
    public class HeroDAO : IHero
    {
        public static HeroDAO Instance
        {
            get
            {
                return new HeroDAO();
            }
        }

        public HeroDAO()
        {

        }

        public Entity.Hero Select(int id)
        {
            var entity = ModelComic.ComicEntities.Hero.FirstOrDefault(o => o.Id == id);
            Entity.Hero hero = (entity != null) ? new Entity.Hero(entity.Id, entity.Name, entity.Description, Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}{entity.ImagePath}"), entity.BiographyId, entity.PowerStatsId) : null;
            return hero;
        }

        public bool ExistByName(string name)
        {
            bool exist = ModelComic.ComicEntities.Hero.Any(o => o.Name == Useful.GetTitleCaseWords(name.Trim()));
            return exist;
        }

        public int Insert(HeroInsertDTO heroInsertDTO)
        {
            Hero hero = new Hero();
            hero.Name = Useful.GetTitleCaseWords(heroInsertDTO.Name.Trim());
            hero.Description = heroInsertDTO.Description.Trim();
            hero.ImagePath = Useful.SaveImage(heroInsertDTO.ImageBase64String.Trim(), heroInsertDTO.Name);
            hero.BiographyId = heroInsertDTO.BiographyId;
            hero.PowerStatsId = heroInsertDTO.PowerStatsId;
            int isInsert = ModelComic.ComicEntities.SaveChanges();
            return (isInsert > 0) ? hero.Id : 0;
        }

        public bool Update(HeroUpdateDTO heroUpdateDTO)
        {
            int isUpdate = 0;
            Hero entity = ModelComic.ComicEntities.Hero.FirstOrDefault(o => o.Id == heroUpdateDTO.Id);
            if (entity != null)
            {
                entity.Name = Useful.GetTitleCaseWords(heroUpdateDTO.Name.Trim());
                entity.Description = heroUpdateDTO.Description.Trim();
                entity.ImagePath = Useful.SaveImage(heroUpdateDTO.ImageBase64String.Trim(), heroUpdateDTO.Name);
                entity.BiographyId = heroUpdateDTO.BiographyId;
                entity.PowerStatsId = heroUpdateDTO.PowerStatsId;
                isUpdate = ModelComic.ComicEntities.SaveChanges();
            }
            return (isUpdate > 0);
        }

        public bool Delete(int id)
        {
            int isDelete = 0;
            Hero entity = ModelComic.ComicEntities.Hero.FirstOrDefault(o => o.Id == id);
            if (entity != null)
            {
                ModelComic.ComicEntities.Hero.Remove(entity);
                Useful.DeleteFile($"{Useful.GetApplicationDirectory()}{entity.ImagePath}");
                isDelete = ModelComic.ComicEntities.SaveChanges();
            }
            return (isDelete > 0);
        }

        public List<Entity.Hero> List()
        {
            List<Entity.Hero> list = new List<Entity.Hero>();
            var entities = ModelComic.ComicEntities.SPListHero();
            foreach (var item in entities)
            {
                Entity.Hero hero = new Entity.Hero(item.Id, item.Name, item.Description, Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}{item.ImagePath}"), item.BiographyId, item.PowerStatsId);
                list.Add(hero);
            }
            return list;
        }

        public List<Entity.Hero> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            List<Entity.Hero> list = new List<Entity.Hero>();
            var entities = ModelComic.ComicEntities.SPListHeroPaginated(listPaginatedDTO.PageIndex, listPaginatedDTO.PageSize);
            foreach (var item in entities)
            {
                Entity.Hero hero = new Entity.Hero(item.Id, item.Name, item.Description, Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}{item.ImagePath}"), item.BiographyId, item.PowerStatsId);
                list.Add(hero);
            }
            return list;
        }

        public long TotalRecords()
        {
            long totalRecords = ModelComic.ComicEntities.Hero.LongCount();
            return totalRecords;
        }

        public List<Entity.Hero> Search(HeroSearchDTO heroSearchDTO)
        {
            string whereClause = string.Empty;
            whereClause = ((heroSearchDTO.Id > 0) ? "[Id] = @Id" : string.Empty);
            whereClause += ((!string.IsNullOrEmpty(heroSearchDTO.Name)) ? ((whereClause.Length > 0) ? " AND [Name] LIKE '%' + @Name + '%'" : "[Name] LIKE '%' + @Name + '%'") : string.Empty);
            whereClause += ((!string.IsNullOrEmpty(heroSearchDTO.Description)) ? ((whereClause.Length > 0) ? " AND [Description] LIKE '%' + @Description + '%'" : "[Description] LIKE '%' + @Description + '%'") : string.Empty);
            whereClause += ((heroSearchDTO.BiographyId > 0) ? ((whereClause.Length > 0) ? " AND [BiographyId] = @BiographyId" : "[BiographyId] = @BiographyId") : string.Empty);
            whereClause += ((heroSearchDTO.PowerStatsId > 0) ? ((whereClause.Length > 0) ? " AND [PowerStatsId] = @PowerStatsId" : "[PowerStatsId] = @PowerStatsId") : string.Empty);
            string paginatedClause = $"ORDER BY [Id] ASC OFFSET {(heroSearchDTO.ListPaginatedDTO.PageIndex - 1) * heroSearchDTO.ListPaginatedDTO.PageSize} ROWS FETCH NEXT {heroSearchDTO.ListPaginatedDTO.PageSize} ROWS ONLY";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (heroSearchDTO.Id > 0)
                parameters.Add(new SqlParameter("Id", heroSearchDTO.Id));
            if (!string.IsNullOrWhiteSpace(heroSearchDTO.Name))
                parameters.Add(new SqlParameter("Name", Useful.GetTitleCaseWords(heroSearchDTO.Name.Trim())));
            if (!string.IsNullOrWhiteSpace(heroSearchDTO.Description))
                parameters.Add(new SqlParameter("Description", heroSearchDTO.Description.Trim()));
            if (heroSearchDTO.BiographyId > 0)
                parameters.Add(new SqlParameter("BiographyId", heroSearchDTO.BiographyId));
            if (heroSearchDTO.PowerStatsId > 0)
                parameters.Add(new SqlParameter("PowerStatsId", heroSearchDTO.PowerStatsId));
            
            List<Entity.Hero> list = new List<Entity.Hero>();
            List<Hero> entities = ModelComic.ComicEntities.Hero.SqlQuery($"SELECT [Id], [Name], [Description], [ImagePath], [BiographyId], [PowerStatsId] FROM [dbo].[Hero] WHERE {whereClause} {paginatedClause}", parameters.ToArray()).ToList();
            foreach (var item in entities)
            {
                Entity.Hero hero = new Entity.Hero(item.Id, item.Name, item.Description, Useful.GetImageToBase64String($"{Useful.GetApplicationDirectory()}{item.ImagePath}"), item.BiographyId, item.PowerStatsId);
                list.Add(hero);
            }
            return list;

        }

        public bool ExistByNameAndNotSameEntity(HeroExistByNameAndNotSameEntityDTO heroExistByNameAndNotSameEntityDTO)
        {
            bool exist = ModelComic.ComicEntities.Hero.Any(o => o.Id != heroExistByNameAndNotSameEntityDTO.Id && o.Name == heroExistByNameAndNotSameEntityDTO.Name);
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
                sLDocument = Useful.GetSpreadsheetLightBase("Hero", timeZoneInfoName);

                SLStyle sLStyleHeaderTable = Useful.GetSpreadsheetLightStyleCellTableHeader(sLDocument);
                sLDocument.SetCellValue("D9", "Id");
                sLDocument.SetCellStyle("D9", sLStyleHeaderTable);
                sLDocument.SetCellValue("E9", "Name");
                sLDocument.SetCellStyle("E9", sLStyleHeaderTable);
                sLDocument.SetCellValue("F9", "Description");
                sLDocument.SetCellStyle("F9", sLStyleHeaderTable);
                sLDocument.SetCellValue("G9", "BiographyId");
                sLDocument.SetCellStyle("G9", sLStyleHeaderTable);
                sLDocument.SetCellValue("H9", "PowerStatsId");
                sLDocument.SetCellStyle("H9", sLStyleHeaderTable);

                SLStyle sLStyleId = Useful.GetSpreadsheetLightStyleCellIdTable(sLDocument);
                sLStyleId.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBody = Useful.GetSpreadsheetLightStyleCellTableBody(sLDocument);

                SLStyle sLStyleIdDegrade = Useful.GetSpreadsheetLightStyleCellIdTableDegrade(sLDocument);
                sLStyleIdDegrade.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBodyDegrade = Useful.GetSpreadsheetLightStyleCellTableBodyDegrade(sLDocument);

                int index = 10;
                var heros = List();
                foreach (var item in heros)
                {
                    if ((index % 2) == 0)
                    {
                        sLDocument.SetCellValue($"D{index}", item.Id.ToString());
                        sLDocument.SetCellStyle($"D{index}", sLStyleId);
                        sLDocument.SetCellValue($"E{index}", item.Name);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBody);
                        sLDocument.SetCellValue($"F{index}", item.Description);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                        sLDocument.SetCellValue($"G{index}", item.BiographyId);
                        sLDocument.SetCellStyle($"G{index}", sLStyleBody);
                        sLDocument.SetCellValue($"H{index}", item.PowerStatsId);
                        sLDocument.SetCellStyle($"H{index}", sLStyleBody);
                    }
                    else
                    {
                        sLDocument.SetCellValue($"D{index}", item.Id.ToString());
                        sLDocument.SetCellStyle($"D{index}", sLStyleIdDegrade);
                        sLDocument.SetCellValue($"E{index}", item.Name);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"F{index}", item.Description);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"G{index}", item.BiographyId);
                        sLDocument.SetCellStyle($"G{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"H{index}", item.PowerStatsId);
                        sLDocument.SetCellStyle($"H{index}", sLStyleBodyDegrade);
                    }
                    index++;
                }

                sLDocument.SaveAs(memoryStream);
                fileDTO = new FileDTO("Hero.xlsx", memoryStream.ToArray());
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

                PdfPTable pdfPTableDescription = Useful.GetiTextSharpTableDescription("Hero", totalRecords);
                documentPDF.Add(pdfPTableDescription);

                documentPDF.Add(new Phrase("\n"));

                int index = 1;
                int size = GetPDFHeroSizeMaximunOfRecordsByPage();
                long length = TotalRecords() / size;
                List<Entity.Hero> heros = List();
                for (int i = 0; i <= length; i++)
                {
                    List<Entity.Hero> herosByPage = heros.Skip(size * (index - 1)).Take(size).ToList();

                    if (herosByPage.Count() == 0)
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
                    PdfPCell pdfPCellName = Useful.GetiTextSharpCellTableHeader("Name");
                    PdfPCell pdfPCellDescription = Useful.GetiTextSharpCellTableHeader("Description");
                    PdfPCell pdfPCellImageBase64String = Useful.GetiTextSharpCellTableHeader("ImageBase64String");
                    PdfPCell pdfPCellBiographyId = Useful.GetiTextSharpCellTableHeader("BiographyId");
                    PdfPCell pdfPCellPowerStatsId = Useful.GetiTextSharpCellTableHeader("PowerStatsId");
                    
                    pdfPTable.AddCell(pdfPCellId);
                    pdfPTable.AddCell(pdfPCellName);
                    pdfPTable.AddCell(pdfPCellDescription);
                    pdfPTable.AddCell(pdfPCellImageBase64String);
                    pdfPTable.AddCell(pdfPCellBiographyId);
                    pdfPTable.AddCell(pdfPCellPowerStatsId);

                    int count = 0;
                    foreach (var item in herosByPage)
                    {
                        byte[] imgBase64String = Convert.FromBase64String(Useful.ReplaceConventionImageFromBase64String(item.ImageBase64String));
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imgBase64String);
                        imageLogo.ScaleAbsoluteWidth(50);
                        imageLogo.ScaleAbsoluteHeight(50);
                        imageLogo.BorderWidth = 0;

                        if ((count % 2) == 0)
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBodyDegrade(item.Id.ToString());
                            pdfPCellName = Useful.GetiTextSharpCellTableBodyDegrade(item.Name);
                            pdfPCellDescription = Useful.GetiTextSharpCellTableBodyDegrade(item.Description);
                            pdfPCellImageBase64String = Useful.GetiTextSharpCellImagenDegrade(image);
                            pdfPCellBiographyId = Useful.GetiTextSharpCellTableBodyDegrade(item.BiographyId.ToString());
                            pdfPCellPowerStatsId = Useful.GetiTextSharpCellTableBodyDegrade(item.PowerStatsId.ToString());
                        }
                        else
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBody(item.Id.ToString());
                            pdfPCellName = Useful.GetiTextSharpCellTableBody(item.Name);
                            pdfPCellDescription = Useful.GetiTextSharpCellTableBody(item.Description);
                            pdfPCellImageBase64String = Useful.GetiTextSharpCellImagen(image);
                            pdfPCellBiographyId = Useful.GetiTextSharpCellTableBody(item.BiographyId.ToString());
                            pdfPCellPowerStatsId = Useful.GetiTextSharpCellTableBody(item.PowerStatsId.ToString());
                        }

                        pdfPTable.AddCell(pdfPCellId);
                        pdfPTable.AddCell(pdfPCellName);
                        pdfPTable.AddCell(pdfPCellDescription);
                        pdfPTable.AddCell(pdfPCellImageBase64String);
                        pdfPTable.AddCell(pdfPCellBiographyId);
                        pdfPTable.AddCell(pdfPCellPowerStatsId);

                        count++;
                    }


                    documentPDF.Add(pdfPTable);
                    documentPDF.NewPage();

                    index++;
                }

                documentPDF.Dispose();
                documentPDF.Close();

                fileDTO = new FileDTO("Hero.pdf", memoryStream.ToArray());
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

        private int GetPDFHeroSizeMaximunOfRecordsByPage()
        {
            return Convert.ToInt32(Useful.GetAppSettings("PDFHeroSizeMaximunOfRecordsByPage"));
        }

        private System.Drawing.Color GetBackgroundColorHeaders()
        {
            return System.Drawing.ColorTranslator.FromHtml("#A9D08E");
        }
    }
}
