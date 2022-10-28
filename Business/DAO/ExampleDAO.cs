using Business.DTO;
using Business.Entity;
using Business.Interface;
using Business.Tool;
using SpreadsheetLight;
using SpreadsheetLight.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Business.DAO
{
    public class ExampleDAO : IExample
    {
        List<Example> examples = new List<Example>();
        public ExampleDAO()
        {
            examples.Add(new Example() { Id = 1, Rut = "1-9", Name = "Pedro", LastName = "Gutierrez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(-1).AddDays(-5), Active = true, Password = Useful.ConvertSHA256("1234qwer") });
            examples.Add(new Example() { Id = 2, Rut = "2-7", Name = "Jose", LastName = "Gonazalez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-20).AddMonths(-2).AddDays(-4), Active = true, Password = Useful.ConvertSHA256("5678tyui") });
            examples.Add(new Example() { Id = 3, Rut = "3-5", Name = "Luis", LastName = "Romo", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-19).AddMonths(-1).AddDays(-1), Active = true, Password = Useful.ConvertSHA256("9012opqw") });
            examples.Add(new Example() { Id = 4, Rut = "4-3", Name = "Manuel", LastName = "Palma", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(-3).AddDays(-10), Active = true, Password = Useful.ConvertSHA256("3456erty") });
            examples.Add(new Example() { Id = 5, Rut = "5-1", Name = "Diego", LastName = "Muñoz", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-22).AddMonths(-5).AddDays(7), Active = true, Password = Useful.ConvertSHA256("7891uiop") });
            examples.Add(new Example() { Id = 6, Rut = "6-K", Name = "Cristobal", LastName = "Lopez", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-25).AddMonths(-1).AddDays(15), Active = true, Password = Useful.ConvertSHA256("2345asdf") });
            examples.Add(new Example() { Id = 7, Rut = "7-8", Name = "Ulises", LastName = "Retamal", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(-3).AddDays(3), Active = true, Password = Useful.ConvertSHA256("6789ghjk") });
            examples.Add(new Example() { Id = 8, Rut = "8-6", Name = "Sebastian", LastName = "Recabarren", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-28).AddMonths(-8).AddDays(18), Active = true, Password = Useful.ConvertSHA256("1234lñas") });
            examples.Add(new Example() { Id = 9, Rut = "9-4", Name = "Angelica", LastName = "Solis", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-19).AddMonths(-4).AddDays(16), Active = true, Password = Useful.ConvertSHA256("5678dfgh") });
            examples.Add(new Example() { Id = 10, Rut = "10-8", Name = "Maria", LastName = "Diaz", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-21).AddMonths(-9).AddDays(23), Active = true, Password = Useful.ConvertSHA256("9123jklñ") });
            examples.Add(new Example() { Id = 11, Rut = "11-6", Name = "Aurora", LastName = "Reyes", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-26).AddMonths(-11).AddDays(7), Active = true, Password = Useful.ConvertSHA256("4567zxcv") });
            examples.Add(new Example() { Id = 12, Rut = "12-4", Name = "Joselyn", LastName = "Labra", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-24).AddMonths(7).AddDays(13), Active = true, Password = Useful.ConvertSHA256("8912bnmz") });
            examples.Add(new Example() { Id = 13, Rut = "13-2", Name = "Fernanda", LastName = "Ibarra", BirthDate = DateTimeOffset.UtcNow.Date.AddYears(-18).AddMonths(2).AddDays(24), Active = true, Password = Useful.ConvertSHA256("3456xcvb") });
        }

        public Example Select(int id)
        {
            Example example = examples.FirstOrDefault(o => o.Id == id);
            return example;
        }

        public bool ExistsByRut(string rut)
        {
            bool exist = examples.Any(o => o.Rut == rut);
            return exist;
        }
        
        public Example Insert(ExampleInsertDTO exampleInsertDTO)
        {
            Example example = new Example()
            {
                Id = examples.Last().Id + 1,
                Rut = exampleInsertDTO.Rut,
                Name = exampleInsertDTO.Name,
                LastName = exampleInsertDTO.LastName,
                BirthDate = exampleInsertDTO.BirthDate.Date,
                Active = exampleInsertDTO.Active,
                Password = Useful.ConvertSHA256(exampleInsertDTO.Password)
            };
            examples.Add(example);
            return example;
        }

        public bool Update(Example example)
        {
            Example exampleExist = examples.FirstOrDefault(o => o.Id == example.Id);
            if (exampleExist != null)
            {
                Example entity = new Example()
                {
                    Id = exampleExist.Id,
                    Rut = example.Rut,
                    Name = example.Name,
                    LastName = example.LastName,
                    BirthDate = example.BirthDate.Date,
                    Active = example.Active,
                    Password = Useful.ConvertSHA256(example.Password)
                };
                examples.Remove(exampleExist);
                examples.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            Example example = examples.FirstOrDefault(o => o.Id == id);
            if (example != null)
            {
                examples.Remove(example);
                return true;
            }
            else
            {
                return false;
            } 
        }

        public List<Example> List(ExampleListDTO exampleListDTO)
        {            
            List<Example> listExample = examples.OrderBy(o => o.Id).Skip((exampleListDTO.PageSize * (exampleListDTO.PageIndex - 1))).Take(exampleListDTO.PageSize).ToList();
            return listExample;
        }

        public long TotalRecords()
        {
            long totalRecords = examples.LongCount();
            return totalRecords;
        }

        public bool ExistByRutAndNotSameEntity(ExampleExistByRutAndNotSameEntityDTO exampleExistByRutAndNotSameEntityDTO)
        {
            Example example = examples.FirstOrDefault(o => o.Id != exampleExistByRutAndNotSameEntityDTO.Id && o.Rut == exampleExistByRutAndNotSameEntityDTO.Rut);
            if (example == null)
                return false;
            else
                return true;
        }

        public ExampleExcelDTO Excel()
        {
            ExampleExcelDTO exampleExcelDTO = null;
            MemoryStream memoryStream = null;
            SLDocument sLDocument = null;
            try
            {
                memoryStream = new MemoryStream();
                sLDocument = new SLDocument();
                sLDocument.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Example");
                sLDocument.SetCellValue("B3", "Example");

                SLStyle sLStyleTitle = sLDocument.CreateStyle();
                sLStyleTitle.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
                sLStyleTitle.Font.Bold = true;
                sLStyleTitle.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleTitle.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleTitle.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleTitle.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLDocument.MergeWorksheetCells("B3", "D3", sLStyleTitle);
                sLDocument.MergeWorksheetCells("B4", "D4");

                SLPicture sLPicture = new SLPicture(GetImagePath());
                sLPicture.SetPosition(4, 1);
                sLDocument.InsertPicture(sLPicture);

                SLStyle sLStyleHeader = sLDocument.CreateStyle();
                sLStyleHeader.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
                sLStyleHeader.Font.Bold = true;
                sLStyleHeader.SetWrapText(true);
                sLStyleHeader.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleHeader.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleHeader.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleHeader.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                System.Drawing.Color backgroundColor = System.Drawing.ColorTranslator.FromHtml("#A9D08E");
                sLStyleHeader.SetPatternFill(PatternValues.Solid, backgroundColor, backgroundColor);
                
                sLDocument.SetCellValue("F3", "Id");
                sLDocument.SetCellStyle("F3", sLStyleHeader);
                sLDocument.SetCellValue("G3", "Rut");
                sLDocument.SetCellStyle("G3", sLStyleHeader);
                sLDocument.SetCellValue("H3", "Name");
                sLDocument.SetCellStyle("H3", sLStyleHeader);
                sLDocument.SetColumnWidth("H3", 12.00);
                sLDocument.SetCellValue("I3", "LastName");
                sLDocument.SetCellStyle("I3", sLStyleHeader);
                sLDocument.SetColumnWidth("I3", 12.00);
                sLDocument.SetCellValue("J3", "BirthDate");
                sLDocument.SetCellStyle("J3", sLStyleHeader);
                sLDocument.SetColumnWidth("J3", 11.00);                
                sLDocument.SetCellValue("K3", "Active");
                sLDocument.SetCellStyle("K3", sLStyleHeader);
                sLDocument.SetColumnWidth("K3", 11.00);

                SLStyle sLStyleBody = sLDocument.CreateStyle();
                sLStyleBody.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
                sLStyleBody.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleBody.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleBody.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleBody.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                int index = 4;
                foreach (var item in examples)
                {
                    sLDocument.SetCellValue($"F{index}", item.Id);
                    sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                    sLDocument.SetCellValue($"G{index}", item.Rut);
                    sLDocument.SetCellStyle($"G{index}", sLStyleBody);
                    sLDocument.SetCellValue($"H{index}", item.Name);
                    sLDocument.SetCellStyle($"H{index}", sLStyleBody);
                    sLDocument.SetCellValue($"I{index}", item.LastName);
                    sLDocument.SetCellStyle($"I{index}", sLStyleBody);
                    sLDocument.SetCellValue($"J{index}", item.BirthDate.ToString("yyyy-MM-dd"));
                    sLDocument.SetCellStyle($"J{index}", sLStyleBody);
                    sLDocument.SetCellValue($"K{index}", item.Active);
                    sLDocument.SetCellStyle($"K{index}", sLStyleBody);
                    index++;
                }

                sLDocument.SaveAs(memoryStream);

                exampleExcelDTO = new ExampleExcelDTO()
                {
                    FileName = "Examples.xlsx",
                    FileContent = memoryStream.ToArray()
                };
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
            return exampleExcelDTO;
        }

        public ExamplePDFDTO PDF()
        {
            ExamplePDFDTO examplePDFDTO = null;
            MemoryStream memoryStream = null;
            Document documentPDF = null;
            StringReader stringReader = null;
            PdfWriter pdfWriter = null;
            System.Drawing.Image contentImage = null;
            try
            {
                memoryStream = new MemoryStream();
                string content = Useful.GetAllTextFile(GetTemplatePDFPath());
                string result = string.Empty;
                foreach (var item in examples)
                {
                    string id = $"<tr><td style=\"text-align: left; border: 1px solid black;\"><p style=\"font-family: Calibri; font-size: 11pt; margin-top: 0px; margin-bottom: 0px;\">{item.Id}</p></td>";
                    string rut = $"<td style=\"text-align: left; border: 1px solid black;\"><p style=\"font-family: Calibri; font-size: 11pt; margin-top: 0px; margin-bottom: 0px;\">{item.Rut}</p></td>";
                    string name = $"<td style=\"text-align: left; border: 1px solid black;\"><p style=\"font-family: Calibri; font-size: 11pt; margin-top: 0px; margin-bottom: 0px;\">{item.Name}</p></td>";
                    string lastName = $"<td style=\"text-align: left; border: 1px solid black;\"><p style=\"font-family: Calibri; font-size: 11pt; margin-top: 0px; margin-bottom: 0px;\">{item.LastName}</p></td>";
                    string birthDate = $"<td style=\"text-align: left; border: 1px solid black;\"><p style=\"font-family: Calibri; font-size: 11pt; margin-top: 0px; margin-bottom: 0px;\">{item.BirthDate.ToString("yyyy-MM-dd")}</p></td>";
                    string active = $"<td style=\"text-align: left; border: 1px solid black;\"><p style=\"font-family: Calibri; font-size: 11pt; margin-top: 0px; margin-bottom: 0px;\">{(item.Active? "VERDADERO" : "FALSO")}</p></td></tr>";
                    result = $"{result}{id}{rut}{name}{lastName}{birthDate}{active}";
                }
                content = content.Replace("{CONTENT}", result);

                documentPDF = new Document(PageSize.A3, 25, 25, 25, 25);                
                stringReader = new StringReader(content);
                pdfWriter = PdfWriter.GetInstance(documentPDF, memoryStream);
                contentImage = System.Drawing.Image.FromFile(GetImagePath());
                Image image = Image.GetInstance(contentImage, System.Drawing.Imaging.ImageFormat.Png);
                image.ScaleToFit(140, 237);
                image.Alignment = Image.UNDERLYING;
                image.SetAbsolutePosition(documentPDF.LeftMargin, documentPDF.Top - 185);

                documentPDF.Open();                
                documentPDF.Add(image);
                XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, documentPDF, stringReader);
                documentPDF.Close();

                examplePDFDTO = new ExamplePDFDTO()
                {
                    FileName = "Examples.pdf",
                    FileContent = memoryStream.ToArray()
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                if (documentPDF != null)
                {
                    if (documentPDF.IsOpen())
                        documentPDF.Close();

                    documentPDF.Dispose();
                }
                if (stringReader != null)
                    stringReader.Dispose();
                if (pdfWriter != null)
                    pdfWriter.Dispose();
                if (contentImage != null)
                    contentImage.Dispose();
            }
            return examplePDFDTO;
        }

        private string GetTemplatePDFPath()
        {
            return $"{Useful.GetApplicationDirectory()}Contents\\Useful\\TemplatePDF.html";
        }

        private string GetImagePath()
        {
            return $"{Useful.GetApplicationDirectory()}Contents\\api-200.png";
        }
    }
}
