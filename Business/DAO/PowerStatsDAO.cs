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
    public class PowerStatsDAO : IPowerStats
    {
        public PowerStatsDAO()
        {

        }

        public Entity.PowerStats Select(int id)
        {
            PowerStats entity = ModelComic.ComicEntities.PowerStats.FirstOrDefault(o => o.Id == id);
            Entity.PowerStats powerStats = (entity != null) ? new Entity.PowerStats(entity.Id, entity.Intelligence, entity.Strength, entity.Speed, entity.Durability, entity.Power, entity.Combat) : null;
            return powerStats;
        }

        public bool ExistById(int id)
        {
            bool isExist = ModelComic.ComicEntities.PowerStats.Any(o => o.Id == id);
            return isExist;
        }

        public int Insert(PowerStatsInsertDTO powerStatsInsertDTO)
        {
            PowerStats powerStats = new PowerStats();
            powerStats.Intelligence = powerStatsInsertDTO.Intelligence;
            powerStats.Strength = powerStatsInsertDTO.Strength;
            powerStats.Speed = powerStatsInsertDTO.Speed;
            powerStats.Durability = powerStatsInsertDTO.Durability;
            powerStats.Power = powerStatsInsertDTO.Power;
            powerStats.Combat = powerStatsInsertDTO.Combat;
            int isInsert = ModelComic.ComicEntities.SaveChanges();
            return (isInsert > 0) ? powerStats.Id : 0;
        }

        public bool Update(Entity.PowerStats powerStats)
        {
            int isUpdate = 0;
            PowerStats entity = ModelComic.ComicEntities.PowerStats.FirstOrDefault(o => o.Id == powerStats.Id);
            if (entity != null)
            {
                entity.Intelligence = powerStats.Intelligence;
                entity.Strength = powerStats.Strength;
                entity.Speed = powerStats.Speed;
                entity.Durability = powerStats.Durability;
                entity.Power = powerStats.Power;
                entity.Combat = powerStats.Combat;
                isUpdate = ModelComic.ComicEntities.SaveChanges();
            }
            return (isUpdate > 0);
        }

        public bool Delete(int id)
        {
            int isDelete = 0;
            PowerStats entity = ModelComic.ComicEntities.PowerStats.FirstOrDefault(o => o.Id == id);
            if (entity != null)
            {
                ModelComic.ComicEntities.PowerStats.Remove(entity);
                isDelete = ModelComic.ComicEntities.SaveChanges();
            }
            return (isDelete > 0);
        }

        public List<Entity.PowerStats> List()
        {
            List<Entity.PowerStats> list = new List<Entity.PowerStats>();
            var entities = ModelComic.ComicEntities.SPListPowerStats();
            foreach (var item in entities)
            {
                Entity.PowerStats powerStats = new Entity.PowerStats(item.Id, item.Intelligence, item.Strength, item.Speed, item.Durability, item.Power, item.Combat);
                list.Add(powerStats);
            }
            return list;
        }

        public List<Entity.PowerStats> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            List<Entity.PowerStats> list = new List<Entity.PowerStats>();
            var entities = ModelComic.ComicEntities.SPListPowerStatsPaginated(listPaginatedDTO.PageIndex, listPaginatedDTO.PageSize);
            foreach (var item in entities)
            {
                Entity.PowerStats powerStats = new Entity.PowerStats(item.Id, item.Intelligence, item.Strength, item.Speed, item.Durability, item.Power, item.Combat);
                list.Add(powerStats);
            }
            return list;
        }

        public long TotalRecords()
        {
            long totalRecords = ModelComic.ComicEntities.PowerStats.LongCount();
            return totalRecords;
        }

        public List<Entity.PowerStats> Search(PowerStatsSearchDTO powerStatsSearchDTO)
        {
            string whereClause = string.Empty;
            whereClause = ((powerStatsSearchDTO.Id > 0) ? "[Id] = @Id" : string.Empty);
            whereClause += ((powerStatsSearchDTO.Intelligence > 0) ? ((whereClause.Length > 0) ? " AND [Intelligence] LIKE '%' + @Intelligence + '%'" : "[Intelligence] LIKE '%' + @Intelligence + '%'") : string.Empty);
            whereClause += ((powerStatsSearchDTO.Strength > 0) ? ((whereClause.Length > 0) ? " AND [Strength] LIKE '%' + @Strength + '%'" : "[Strength] LIKE '%' + @Strength + '%'") : string.Empty);
            whereClause += ((powerStatsSearchDTO.Speed > 0) ? ((whereClause.Length > 0) ? " AND [Speed] LIKE '%' + @Speed + '%'" : "[Speed] LIKE '%' + @Speed + '%'") : string.Empty);
            whereClause += ((powerStatsSearchDTO.Durability > 0) ? ((whereClause.Length > 0) ? " AND [Durability] LIKE '%' + @Durability + '%'" : "[Durability] LIKE '%' + @Durability + '%'") : string.Empty);
            whereClause += ((powerStatsSearchDTO.Power > 0) ? ((whereClause.Length > 0) ? " AND [Power] LIKE '%' + @Power + '%'" : "[Power] LIKE '%' + @Power + '%'") : string.Empty);
            whereClause += ((powerStatsSearchDTO.Combat > 0) ? ((whereClause.Length > 0) ? " AND [Combat] LIKE '%' + @Combat + '%'" : "[Combat] LIKE '%' + @Combat + '%'") : string.Empty);
            string paginatedClause = $"ORDER BY [Id] DESC OFFSET {(powerStatsSearchDTO.ListPaginatedDTO.PageIndex - 1) * powerStatsSearchDTO.ListPaginatedDTO.PageSize} ROWS FETCH NEXT {powerStatsSearchDTO.ListPaginatedDTO.PageSize} ROWS ONLY";

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (powerStatsSearchDTO.Id > 0)
                parameters.Add(new SqlParameter("Id", powerStatsSearchDTO.Id));
            if (powerStatsSearchDTO.Intelligence > 0)
                parameters.Add(new SqlParameter("Intelligence", powerStatsSearchDTO.Intelligence));
            if (powerStatsSearchDTO.Strength > 0)
                parameters.Add(new SqlParameter("Strength", powerStatsSearchDTO.Strength));
            if (powerStatsSearchDTO.Speed > 0)
                parameters.Add(new SqlParameter("Speed", powerStatsSearchDTO.Speed));
            if (powerStatsSearchDTO.Durability > 0)
                parameters.Add(new SqlParameter("Durability", powerStatsSearchDTO.Durability));
            if (powerStatsSearchDTO.Power > 0)
                parameters.Add(new SqlParameter("Power", powerStatsSearchDTO.Power));
            if (powerStatsSearchDTO.Combat > 0)
                parameters.Add(new SqlParameter("Combat", powerStatsSearchDTO.Combat));

            List<Entity.PowerStats> list = new List<Entity.PowerStats>();
            List<PowerStats> entities = ModelComic.ComicEntities.PowerStats.SqlQuery($"SELECT [Id], [Intelligence], [Strength], [Speed], [Durability], [Power], [Combat] FROM [dbo].[PowerStats] WHERE {whereClause} {paginatedClause}", parameters.ToArray()).ToList();
            foreach (var item in entities)
            {
                Entity.PowerStats entity = new Entity.PowerStats(item.Id, item.Intelligence, item.Strength, item.Speed, item.Durability, item.Power, item.Combat);
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
                sLDocument = Useful.GetSpreadsheetLightBase("PowerStats", timeZoneInfoName);

                SLStyle sLStyleHeaderTable = Useful.GetSpreadsheetLightStyleCellTableHeader(sLDocument);
                sLDocument.SetCellValue("C9", "Id");
                sLDocument.SetCellStyle("C9", sLStyleHeaderTable);
                sLDocument.SetCellValue("D9", "Intelligence");
                sLDocument.SetCellStyle("D9", sLStyleHeaderTable);
                sLDocument.SetCellValue("E9", "Strength");
                sLDocument.SetCellStyle("E9", sLStyleHeaderTable);
                sLDocument.SetCellValue("F9", "Speed");
                sLDocument.SetCellStyle("F9", sLStyleHeaderTable);
                sLDocument.SetCellValue("G9", "Durability");
                sLDocument.SetCellStyle("G9", sLStyleHeaderTable);
                sLDocument.SetCellValue("H9", "Power");
                sLDocument.SetCellStyle("H9", sLStyleHeaderTable);
                sLDocument.SetCellValue("I9", "Combat");
                sLDocument.SetCellStyle("I9", sLStyleHeaderTable);

                SLStyle sLStyleId = Useful.GetSpreadsheetLightStyleCellIdTable(sLDocument);
                sLStyleId.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBody = Useful.GetSpreadsheetLightStyleCellTableBody(sLDocument);

                SLStyle sLStyleIdDegrade = Useful.GetSpreadsheetLightStyleCellIdTableDegrade(sLDocument);
                sLStyleIdDegrade.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBodyDegrade = Useful.GetSpreadsheetLightStyleCellTableBodyDegrade(sLDocument);
                
                int index = 10;
                var powerStats = List();
                foreach (var item in powerStats)
                {
                    if ((index % 2) == 0)
                    {
                        sLDocument.SetCellValue($"C{index}", item.Id);
                        sLDocument.SetCellStyle($"C{index}", sLStyleId);
                        sLDocument.SetCellValue($"D{index}", item.Intelligence);
                        sLDocument.SetCellStyle($"D{index}", sLStyleBody);
                        sLDocument.SetCellValue($"E{index}", item.Strength);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBody);
                        sLDocument.SetCellValue($"F{index}", item.Speed);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                        sLDocument.SetCellValue($"G{index}", item.Durability);
                        sLDocument.SetCellStyle($"G{index}", sLStyleBody);
                        sLDocument.SetCellValue($"H{index}", item.Power);
                        sLDocument.SetCellStyle($"H{index}", sLStyleBody);
                        sLDocument.SetCellValue($"I{index}", item.Combat);
                        sLDocument.SetCellStyle($"I{index}", sLStyleBody);
                    }
                    else
                    {
                        sLDocument.SetCellValue($"C{index}", item.Id);
                        sLDocument.SetCellStyle($"C{index}", sLStyleIdDegrade);
                        sLDocument.SetCellValue($"D{index}", item.Intelligence);
                        sLDocument.SetCellStyle($"D{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"E{index}", item.Strength);
                        sLDocument.SetCellStyle($"E{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"F{index}", item.Speed);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"G{index}", item.Durability);
                        sLDocument.SetCellStyle($"G{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"H{index}", item.Power);
                        sLDocument.SetCellStyle($"H{index}", sLStyleBodyDegrade);
                        sLDocument.SetCellValue($"I{index}", item.Combat);
                        sLDocument.SetCellStyle($"I{index}", sLStyleBodyDegrade);
                    }
                    index++;
                }

                sLDocument.SaveAs(memoryStream);
                fileDTO = new FileDTO("PowerStats.xlsx", memoryStream.ToArray());
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

                PdfPTable pdfPTableDescription = Useful.GetiTextSharpTableDescription("PowerStats", totalRecords);
                documentPDF.Add(pdfPTableDescription);

                documentPDF.Add(new Phrase("\n"));

                int index = 1;
                int size = GetPDFPowerStatsSizeMaximunOfRecordsByPage();
                long length = totalRecords / size;
                List<Entity.PowerStats> powerStats = List();
                for (int i = 0; i <= length; i++)
                {
                    List<Entity.PowerStats> powerStatsByPage = powerStats.Skip(size * (index - 1)).Take(size).ToList();

                    if (powerStatsByPage.Count() == 0)
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

                    PdfPTable pdfPTable = new PdfPTable(7);
                    pdfPTable.HorizontalAlignment = 1;

                    PdfPCell pdfPCellId = Useful.GetiTextSharpCellTableHeader("Id");
                    PdfPCell pdfPCellIntelligence = Useful.GetiTextSharpCellTableHeader("Intelligence");
                    PdfPCell pdfPCellStrength = Useful.GetiTextSharpCellTableHeader("Strength");
                    PdfPCell pdfPCellSpeed = Useful.GetiTextSharpCellTableHeader("Speed");
                    PdfPCell pdfPCellDurability = Useful.GetiTextSharpCellTableHeader("Durability");
                    PdfPCell pdfPCellPower = Useful.GetiTextSharpCellTableHeader("Power");
                    PdfPCell pdfPCellCombat = Useful.GetiTextSharpCellTableHeader("Combat");
                    
                    pdfPTable.AddCell(pdfPCellId);
                    pdfPTable.AddCell(pdfPCellIntelligence);
                    pdfPTable.AddCell(pdfPCellStrength);
                    pdfPTable.AddCell(pdfPCellSpeed);
                    pdfPTable.AddCell(pdfPCellDurability);
                    pdfPTable.AddCell(pdfPCellPower);
                    pdfPTable.AddCell(pdfPCellCombat);

                    int count = 0;
                    foreach (var item in powerStatsByPage)
                    {
                        if ((count % 2) == 0)
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBodyDegrade(item.Id.ToString());
                            pdfPCellIntelligence = Useful.GetiTextSharpCellTableBodyDegrade(item.Intelligence.ToString());
                            pdfPCellStrength = Useful.GetiTextSharpCellTableBodyDegrade(item.Strength.ToString());
                            pdfPCellSpeed = Useful.GetiTextSharpCellTableBodyDegrade(item.Speed.ToString());
                            pdfPCellDurability = Useful.GetiTextSharpCellTableBodyDegrade(item.Durability.ToString());
                            pdfPCellPower = Useful.GetiTextSharpCellTableBodyDegrade(item.Power.ToString());
                            pdfPCellCombat = Useful.GetiTextSharpCellTableBodyDegrade(item.Combat.ToString());
                        }
                        else
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBody(item.Id.ToString());
                            pdfPCellIntelligence = Useful.GetiTextSharpCellTableBody(item.Intelligence.ToString());
                            pdfPCellStrength = Useful.GetiTextSharpCellTableBody(item.Strength.ToString());
                            pdfPCellSpeed = Useful.GetiTextSharpCellTableBody(item.Speed.ToString());
                            pdfPCellDurability = Useful.GetiTextSharpCellTableBody(item.Durability.ToString());
                            pdfPCellPower = Useful.GetiTextSharpCellTableBody(item.Power.ToString());
                            pdfPCellCombat = Useful.GetiTextSharpCellTableBody(item.Combat.ToString());
                        }

                        pdfPTable.AddCell(pdfPCellId);
                        pdfPTable.AddCell(pdfPCellIntelligence);
                        pdfPTable.AddCell(pdfPCellStrength);
                        pdfPTable.AddCell(pdfPCellSpeed);
                        pdfPTable.AddCell(pdfPCellDurability);
                        pdfPTable.AddCell(pdfPCellPower);
                        pdfPTable.AddCell(pdfPCellCombat);

                        count++;
                    }

                    documentPDF.Add(pdfPTable);
                    documentPDF.NewPage();

                    index++;
                }

                documentPDF.Dispose();
                documentPDF.Close();

                fileDTO = new FileDTO("PowerStats.pdf", memoryStream.ToArray());
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

        private int GetPDFPowerStatsSizeMaximunOfRecordsByPage()
        {
            return Convert.ToInt32(Useful.GetAppSettings("PDFPowerStatsSizeMaximunOfRecordsByPage"));
        }

        private System.Drawing.Color GetBackgroundColorHeaders()
        {
            return System.Drawing.ColorTranslator.FromHtml("#A9D08E");
        }
    }
}
