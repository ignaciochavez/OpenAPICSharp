using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text;
using SpreadsheetLight;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight.Drawing;
using System.Globalization;
using Business.DTO;

namespace Business.Tool
{
    public static class Useful
    {
        #region Gets

        public static string GetApplicationDirectory()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory.Replace("WebAPIUnitTests\\bin\\Debug", "WebAPI\\");
        }

        public static string GetAppSettings(string keyWebConfig)
        {
            return ConfigurationManager.AppSettings[keyWebConfig];
        }

        public static string GetPngToBase64String(string path)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(path);
            string base64String = Convert.ToBase64String(imageBytes);
            return String.Format("data:image/png;base64,{0}", base64String);
        }

        public static string GetImageToBase64String(string path)
        {
            byte[] imageBytes = File.ReadAllBytes(path);
            string base64String = Convert.ToBase64String(imageBytes);
            if (path.ToLower().Contains(".bmp"))
                return String.Format("data:image/bmp;base64,{0}", base64String);
            else if (path.ToLower().Contains(".emf"))
                return String.Format("data:image/emf;base64,{0}", base64String);
            else if (path.ToLower().Contains(".exif"))
                return String.Format("data:image/exif;base64,{0}", base64String);
            else if (path.ToLower().Contains(".gif"))
                return String.Format("data:image/gif;base64,{0}", base64String);
            else if (path.ToLower().Contains(".icon"))
                return String.Format("data:image/icon;base64,{0}", base64String);
            else if (path.ToLower().Contains(".jpeg"))
                return String.Format("data:image/jpeg;base64,{0}", base64String);
            else if (path.ToLower().Contains(".jpg"))
                return String.Format("data:image/jpg;base64,{0}", base64String);
            else if (path.ToLower().Contains(".png"))
                return String.Format("data:image/png;base64,{0}", base64String);
            else if (path.ToLower().Contains(".tiff"))
                return String.Format("data:image/tiff;base64,{0}", base64String);
            else if (path.ToLower().Contains(".wmf"))
                return String.Format("data:image/wmf;base64,{0}", base64String);
            else
                throw new Exception("Formato de imagen no es el permitido");
        }

        public static int GetPageSizeMaximun()
        {
            return Convert.ToInt32(GetAppSettings("PageSizeMaximun"));
        }

        public static string GetRutCheckDigit(int rut)
        {
            int count = 2;
            int accumulator = 0;

            while (rut != 0)
            {
                int multiple = (rut % 10) * count;
                accumulator = accumulator + multiple;
                rut = rut / 10;
                count = count + 1;
                if (count == 8)
                {
                    count = 2;
                }
            }

            int digit = 11 - (accumulator % 11);
            string rutDigit = digit.ToString().Trim();
            if (digit == 10)
            {
                rutDigit = "K";
            }
            if (digit == 11)
            {
                rutDigit = "0";
            }

            return rutDigit;
        }

        public static string GetEmailContact()
        {
            return GetAppSettings("EmailContact");
        }

        public static string GetPhoneContact()
        {
            return GetAppSettings("PhoneContact");
        }

        public static string GetApplicationNameText()
        {
            return "OpenAPI";
        }

        public static string GetApplicationImagePath()
        {
            return $"{GetApplicationDirectory()}Contents\\api-200.png";
        }

        public static string GetTitleCaseWords(string text)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string pascalCaseWords = textInfo.ToTitleCase(text);
            return pascalCaseWords;
        }

        public static List<UsefulTimeZoneInfo> GetTimeZoneInfo()
        {
            List<UsefulTimeZoneInfo> list = new List<UsefulTimeZoneInfo>();
            IReadOnlyCollection<TimeZoneInfo> tz = TimeZoneInfo.GetSystemTimeZones();
            foreach (var item in tz)
            {
                UsefulTimeZoneInfo timeZoneInfo = new UsefulTimeZoneInfo(item.Id, item.ToString());
                list.Add(timeZoneInfo);
            }
            return list;
        }

        public static string GetFormatImgBase64String(string imgBase64String)
        {
            if (imgBase64String.ToLower().Contains("data:image/bmp;base64"))
                return "bmp";
            else if (imgBase64String.ToLower().Contains("data:image/emf;base64"))
                return "emf";
            else if (imgBase64String.ToLower().Contains("data:image/exif;base64"))
                return "exif";
            else if (imgBase64String.ToLower().Contains("data:image/gif;base64"))
                return "gif";
            else if (imgBase64String.ToLower().Contains("data:image/icon;base64"))
                return "icon";
            else if (imgBase64String.ToLower().Contains("data:image/jpeg;base64"))
                return "jpeg";
            else if (imgBase64String.ToLower().Contains("data:image/jpg;base64"))
                return "jpg";
            else if (imgBase64String.ToLower().Contains("data:image/png;base64"))
                return "png";
            else if (imgBase64String.ToLower().Contains("data:image/tiff;base64"))
                return "tiff";
            else if (imgBase64String.ToLower().Contains("data:image/wmf;base64"))
                return "wmf";
            else
                throw new Exception("Formato de imgBase64String no es el permitido");
        }

        #region SpreadSheetLight
        public static SLDocument GetSpreadsheetLightBase(string nameSheet, string timeZoneInfoName)
        {
            SLDocument sLDocument = new SLDocument();
            sLDocument.RenameWorksheet(SLDocument.DefaultFirstSheetName, nameSheet);

            SLStyle sLStyleTitles = sLDocument.CreateStyle();
            sLStyleTitles.Alignment.Horizontal = HorizontalAlignmentValues.Center;

            sLDocument.MergeWorksheetCells("D3", "H3");
            sLDocument.SetCellValue("D3", $"© Copyright IgnacioChavez {ConvertDateTimeOffsetToTimeZone(DateTimeOffset.UtcNow, timeZoneInfoName).ToString("yyyy")}. Todos los derechos reservados");
            sLDocument.SetCellStyle("D3", sLStyleTitles);
            sLDocument.MergeWorksheetCells("D4", "H4");
            sLDocument.SetCellValue("D4", Useful.GetEmailContact());
            sLDocument.SetCellStyle("D4", sLStyleTitles);
            sLDocument.MergeWorksheetCells("D5", "H5");
            sLDocument.SetCellValue("D5", Useful.GetPhoneContact());
            sLDocument.SetCellStyle("D5", sLStyleTitles);

            SLPicture sLPicture = new SLPicture(GetApplicationImagePath());
            sLPicture.SetPosition(2, 1);
            sLPicture.ResizeInPixels(80, 80);
            sLDocument.InsertPicture(sLPicture);

            SLStyle sLStyleTitleOpenAPICSharp = sLDocument.CreateStyle();
            sLStyleTitleOpenAPICSharp.Alignment.Horizontal = HorizontalAlignmentValues.Center;
            sLStyleTitleOpenAPICSharp.Alignment.Vertical = VerticalAlignmentValues.Center;
            sLStyleTitleOpenAPICSharp.SetWrapText(true);
            sLStyleTitleOpenAPICSharp.Font.Bold = true;
            sLDocument.SetCellStyle("B7", sLStyleTitleOpenAPICSharp);
            sLDocument.SetCellValue("B7", "OpenAPICSharp");
            sLDocument.SetRowHeight(7, 27.57);

            SLStyle sLStyleDateTime = sLDocument.CreateStyle();
            sLStyleDateTime.SetFont("Calibri", 10);
            sLStyleDateTime.SetFontColor(System.Drawing.ColorTranslator.FromHtml("#8E97A0"));
            sLDocument.MergeWorksheetCells("J3", "L3");
            sLDocument.SetCellValue("J3", $"{ConvertDateTimeOffsetToTimeZone(DateTimeOffset.UtcNow, timeZoneInfoName).ToString("yyyy-MM-dd HH:mm:sszzz")}");
            sLDocument.SetCellStyle("J3", sLStyleDateTime);

            return sLDocument;
        }

        public static SLStyle GetSpreadsheetLightStyleCellTableHeader(SLDocument sLDocument)
        {
            SLStyle sLStyleHeaderTable = sLDocument.CreateStyle();
            sLStyleHeaderTable.Font.Bold = true;
            sLStyleHeaderTable.SetFont(GetSpreadsheetLightFontSegoeUI(), 10);
            sLStyleHeaderTable.SetFontColor(System.Drawing.Color.Black);
            return sLStyleHeaderTable;
        }

        public static SLStyle GetSpreadsheetLightStyleCellIdTable(SLDocument sLDocument)
        {
            SLStyle sLStyleHeaderTable = sLDocument.CreateStyle();
            sLStyleHeaderTable.Border.BottomBorder.BorderStyle = BorderStyleValues.DashDot;
            sLStyleHeaderTable.Border.BottomBorder.Color = System.Drawing.ColorTranslator.FromHtml("#dee2e6");
            sLStyleHeaderTable.Font.Bold = true;
            sLStyleHeaderTable.SetFont(GetSpreadsheetLightFontSegoeUI(), 10);
            sLStyleHeaderTable.SetFontColor(System.Drawing.Color.Black);
            return sLStyleHeaderTable;
        }

        public static SLStyle GetSpreadsheetLightStyleCellIdTableDegrade(SLDocument sLDocument)
        {
            SLStyle sLStyleHeaderTable = sLDocument.CreateStyle();
            sLStyleHeaderTable.Border.BottomBorder.BorderStyle = BorderStyleValues.DashDot;
            sLStyleHeaderTable.Border.BottomBorder.Color = System.Drawing.ColorTranslator.FromHtml("#dee2e6");
            System.Drawing.Color backgroundColor = System.Drawing.ColorTranslator.FromHtml("#DADDE0");
            sLStyleHeaderTable.SetPatternFill(PatternValues.Solid, backgroundColor, backgroundColor);
            sLStyleHeaderTable.Font.Bold = true;
            sLStyleHeaderTable.SetFont(GetSpreadsheetLightFontSegoeUI(), 10);
            sLStyleHeaderTable.SetFontColor(System.Drawing.Color.Black);
            return sLStyleHeaderTable;
        }

        public static SLStyle GetSpreadsheetLightStyleCellTableBody(SLDocument sLDocument)
        {
            SLStyle sLStyleHeaderTable = sLDocument.CreateStyle();
            sLStyleHeaderTable.Border.BottomBorder.BorderStyle = BorderStyleValues.DashDot;
            sLStyleHeaderTable.Border.BottomBorder.Color = System.Drawing.ColorTranslator.FromHtml("#dee2e6");
            sLStyleHeaderTable.Font.Bold = false;
            sLStyleHeaderTable.SetFont(GetSpreadsheetLightFontSegoeUI(), 10);
            sLStyleHeaderTable.SetFontColor(System.Drawing.Color.Black);
            return sLStyleHeaderTable;
        }

        public static SLStyle GetSpreadsheetLightStyleCellTableBodyDegrade(SLDocument sLDocument)
        {
            SLStyle sLStyleHeaderTable = sLDocument.CreateStyle();
            sLStyleHeaderTable.Border.BottomBorder.BorderStyle = BorderStyleValues.DashDot;
            sLStyleHeaderTable.Border.BottomBorder.Color = System.Drawing.ColorTranslator.FromHtml("#dee2e6");
            System.Drawing.Color backgroundColor = System.Drawing.ColorTranslator.FromHtml("#DADDE0");
            sLStyleHeaderTable.SetPatternFill(PatternValues.Solid, backgroundColor, backgroundColor);
            sLStyleHeaderTable.Font.Bold = false;
            sLStyleHeaderTable.SetFont(GetSpreadsheetLightFontSegoeUI(), 10);
            sLStyleHeaderTable.SetFontColor(System.Drawing.Color.Black);
            return sLStyleHeaderTable;
        }

        private static string GetSpreadsheetLightFontSegoeUI()
        {
            return "Segoe UI";
        }
        #endregion

        #region iTextSharp
        public static PdfPTable GetiTextSharpTableHeaderOne(string timeZoneInfoName)
        {
            PdfPTable pdfPTableHeaderOne = new PdfPTable(3);
            pdfPTableHeaderOne.SetWidths(new int[] { 100, 56, 190 });

            iTextSharp.text.Font fontTableOne = new iTextSharp.text.Font(FontFactory.GetFont(GetiTextSharpFontCalibri()).BaseFont, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            PdfPCell pdfPCellCopyright = new PdfPCell(new Phrase("© Copyright", fontTableOne));
            pdfPCellCopyright.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pdfPCellCopyright.BorderWidth = 0;

            iTextSharp.text.Font fontTableOneIgnacioChavez = new iTextSharp.text.Font(FontFactory.GetFont("Calibri").BaseFont, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            PdfPCell pdfPCellIgnacioChavez = new PdfPCell(new Phrase("IgnacioChavez", fontTableOneIgnacioChavez));
            pdfPCellIgnacioChavez.BorderWidth = 0;

            PdfPCell pdfPCellAllRightsReserved = new PdfPCell(new Phrase($"{ConvertDateTimeOffsetToTimeZone(DateTimeOffset.UtcNow, timeZoneInfoName).ToString("yyyy")}. Todos los derechos reservados", fontTableOne));
            pdfPCellAllRightsReserved.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            pdfPCellAllRightsReserved.BorderWidth = 0;

            pdfPTableHeaderOne.AddCell(pdfPCellCopyright);
            pdfPTableHeaderOne.AddCell(pdfPCellIgnacioChavez);
            pdfPTableHeaderOne.AddCell(pdfPCellAllRightsReserved);
            return pdfPTableHeaderOne;
        }

        public static PdfPTable GetiTextSharpTableHeaderTwo()
        {
            PdfPTable pdfPTableHeaderTwo = new PdfPTable(1);

            iTextSharp.text.Font fontTableTwo = new iTextSharp.text.Font(FontFactory.GetFont(GetiTextSharpFontCalibri()).BaseFont, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            PdfPCell pdfPCellEmail = new PdfPCell(new Phrase($"{Useful.GetEmailContact()}", fontTableTwo));
            pdfPCellEmail.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pdfPCellEmail.BorderWidth = 0;

            PdfPCell pdfPCellPhone = new PdfPCell(new Phrase($"{Useful.GetPhoneContact()}", fontTableTwo));
            pdfPCellPhone.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pdfPCellPhone.BorderWidth = 0;

            pdfPTableHeaderTwo.AddCell(pdfPCellEmail);
            pdfPTableHeaderTwo.AddCell(pdfPCellPhone);
            return pdfPTableHeaderTwo;
        }

        public static Image GetiTextSharpImageLogo()
        {
            iTextSharp.text.Image imageLogo = iTextSharp.text.Image.GetInstance(Useful.GetApplicationImagePath());
            imageLogo.ScaleAbsoluteWidth(50);
            imageLogo.ScaleAbsoluteHeight(50);
            imageLogo.BorderWidth = 0;
            return imageLogo;
        }

        public static PdfPTable GetiTextSharpTitle()
        {
            PdfPTable pdfPTableTitle = new PdfPTable(1);
            pdfPTableTitle.TotalWidth = 40;
            pdfPTableTitle.SetWidths(new int[] { 40 });

            iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(FontFactory.GetFont(GetiTextSharpFontCalibri()).BaseFont, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            PdfPCell pdfPCellTitle = new PdfPCell(new Phrase("OpenAPICSharp", fontTitle));
            pdfPCellTitle.BorderWidth = 0;
            pdfPCellTitle.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

            pdfPTableTitle.AddCell(pdfPCellTitle);
            return pdfPTableTitle;
        }

        public static PdfPTable GetiTextSharpDateTime(string timeZoneInfoName)
        {
            PdfPTable pdfPTableDateTime = new PdfPTable(1);
            pdfPTableDateTime.TotalWidth = 140;
            pdfPTableDateTime.SetWidths(new int[] { 140 });

            iTextSharp.text.Font fontDateTime = new iTextSharp.text.Font(FontFactory.GetFont(GetiTextSharpFontCalibri()).BaseFont, 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#8E97A0")));
            PdfPCell pdfPCellDateTime = new PdfPCell(new Phrase($"{ConvertDateTimeOffsetToTimeZone(DateTimeOffset.UtcNow, timeZoneInfoName).ToString("yyyy-MM-dd HH:mm:sszzz")}", fontDateTime));
            pdfPCellDateTime.BorderWidth = 0;
            pdfPCellDateTime.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pdfPTableDateTime.AddCell(pdfPCellDateTime);
            return pdfPTableDateTime;
        }

        public static PdfPTable GetiTextSharpTablePageNumber(int number)
        {
            PdfPTable pdfPTableNumberPage = new PdfPTable(1);
            pdfPTableNumberPage.TotalWidth = 20;
            pdfPTableNumberPage.SetWidths(new int[] { 20 });

            iTextSharp.text.Font fontNumberPage = new iTextSharp.text.Font(FontFactory.GetFont(GetiTextSharpFontCalibri()).BaseFont, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            PdfPCell pdfPCellNumberPage = new PdfPCell(new Phrase($"{number.ToString()}", fontNumberPage));
            pdfPCellNumberPage.BorderWidth = 0;
            pdfPCellNumberPage.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pdfPTableNumberPage.AddCell(pdfPCellNumberPage);
            return pdfPTableNumberPage;
        }

        public static PdfPTable GetiTextSharpTableDescription(string tableName, long totalRecords)
        {
            PdfPTable pdfPTableDescription = new PdfPTable(1);
            iTextSharp.text.Font fontDescription = new iTextSharp.text.Font(FontFactory.GetFont("Times New Roman").BaseFont, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            PdfPCell pdfPCellDescription = new PdfPCell(new Phrase($"Ha descargado los registros existentes de la tabla {tableName}. El total de registros es de {totalRecords}.", fontDescription));
            pdfPCellDescription.BorderWidth = 0;
            pdfPCellDescription.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pdfPTableDescription.AddCell(pdfPCellDescription);
            return pdfPTableDescription;
        }
                
        public static PdfPCell GetiTextSharpCellTableHeader(string name)
        {
            iTextSharp.text.Font fontHeaderTable = new iTextSharp.text.Font(FontFactory.GetFont(GetiTextSharpFontSegoeUI()).BaseFont, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            PdfPCell pdfPCell = new PdfPCell(new Phrase(name, fontHeaderTable));
            pdfPCell.BorderWidthTop = 1;
            pdfPCell.BorderColorTop = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#dee2e6"));
            pdfPCell.BorderWidthBottom = 1;
            pdfPCell.BorderColorBottom = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#dee2e6"));
            pdfPCell.BorderWidthLeft = 0;
            pdfPCell.BorderWidthRight = 0;
            return pdfPCell;
        }

        public static PdfPCell GetiTextSharpCellIdTableBody(string value)
        {
            iTextSharp.text.Font fontBody = new iTextSharp.text.Font(FontFactory.GetFont(GetiTextSharpFontSegoeUI()).BaseFont, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            PdfPCell pdfPCell = new PdfPCell(new Phrase(value, fontBody));
            pdfPCell.BorderWidthTop = 1;
            pdfPCell.BorderColorTop = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#dee2e6"));
            pdfPCell.BorderWidthBottom = 0;
            pdfPCell.BorderWidthLeft = 0;
            pdfPCell.BorderWidthRight = 0;
            return pdfPCell;
        }

        public static PdfPCell GetiTextSharpCellIdTableBodyDegrade(string value)
        {
            iTextSharp.text.Font fontBody = new iTextSharp.text.Font(FontFactory.GetFont(GetiTextSharpFontSegoeUI()).BaseFont, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            PdfPCell pdfPCell = new PdfPCell(new Phrase(value, fontBody));
            pdfPCell.BorderWidthTop = 1;
            pdfPCell.BorderColorTop = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#dee2e6"));
            pdfPCell.BorderWidthBottom = 0;
            pdfPCell.BorderWidthLeft = 0;
            pdfPCell.BorderWidthRight = 0;
            pdfPCell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#DADDE0"));
            return pdfPCell;
        }

        public static PdfPCell GetiTextSharpCellTableBody(string value)
        {
            iTextSharp.text.Font fontBody = new iTextSharp.text.Font(FontFactory.GetFont(GetiTextSharpFontSegoeUI()).BaseFont, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            PdfPCell pdfPCell = new PdfPCell(new Phrase(value, fontBody));
            pdfPCell.BorderWidthTop = 1;
            pdfPCell.BorderColorTop = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#dee2e6"));
            pdfPCell.BorderWidthBottom = 0;
            pdfPCell.BorderWidthLeft = 0;
            pdfPCell.BorderWidthRight = 0;
            return pdfPCell;
        }

        public static PdfPCell GetiTextSharpCellTableBodyDegrade(string value)
        {
            iTextSharp.text.Font fontBody = new iTextSharp.text.Font(FontFactory.GetFont(GetiTextSharpFontSegoeUI()).BaseFont, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            PdfPCell pdfPCell = new PdfPCell(new Phrase(value, fontBody));
            pdfPCell.BorderWidthTop = 1;
            pdfPCell.BorderColorTop = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#dee2e6"));
            pdfPCell.BorderWidthBottom = 0;
            pdfPCell.BorderWidthLeft = 0;
            pdfPCell.BorderWidthRight = 0;
            pdfPCell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#DADDE0"));
            return pdfPCell;
        }

        public static PdfPCell GetiTextSharpCellImagen(Image image)
        {
            PdfPCell pdfPCellImage = new PdfPCell(image);
            pdfPCellImage.BorderWidthTop = 1;
            pdfPCellImage.BorderColorTop = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#dee2e6"));
            pdfPCellImage.BorderWidthBottom = 0;
            pdfPCellImage.BorderWidthLeft = 0;
            pdfPCellImage.BorderWidthRight = 0;
            return pdfPCellImage;
        }

        public static PdfPCell GetiTextSharpCellImagenDegrade(Image image)
        {
            PdfPCell pdfPCellImage = new PdfPCell(image);
            pdfPCellImage.BorderWidthTop = 1;
            pdfPCellImage.BorderColorTop = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#dee2e6"));
            pdfPCellImage.BorderWidthBottom = 0;
            pdfPCellImage.BorderWidthLeft = 0;
            pdfPCellImage.BorderWidthRight = 0;
            pdfPCellImage.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#DADDE0"));
            return pdfPCellImage;
        }

        private static string GetiTextSharpFontCalibri()
        {
            return "Calibri";
        }

        private static string GetiTextSharpFontSegoeUI()
        {
            return "Segoe UI";
        }
        #endregion

        #endregion

        #region Validate

        public static bool ValidateDateTimeOffset(DateTimeOffset dateTimeOffset)
        {
            if (dateTimeOffset == DateTimeOffset.MinValue)
                return false;
            else
                return true;
        }

        public static bool ValidateEmail(string email)
        {
            string expresion = GetAppSettings("IsEmail");
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidatePhone(string phone)
        {
            string expresion = GetAppSettings("IsPhone");
            return Regex.IsMatch(phone, expresion);
        }

        public static bool ValidateRut(string rut)
        {
            rut = rut.Replace(".", "").ToUpper();
            Regex expression = new Regex(GetAppSettings("IsRut"));
            string dv = rut.Substring(rut.Length - 1, 1);
            if (!expression.IsMatch(rut))
            {
                return false;
            }
            char[] charCut = { '-' };
            string[] arrayRut = rut.Split(charCut);
            if (dv != GetRutCheckDigit(Convert.ToInt32(arrayRut[0])))
            {
                return false;
            }
            return true;
        }

        public static bool ValidateBase64String(string base64String)
        {
            return (base64String.Trim().Length % 4 == 0) && Regex.IsMatch(base64String.Trim(), @GetAppSettings("IsBase64String"), RegexOptions.None);
        }

        public static bool ValidateIsImageBase64String(string base64String)
        {
            if (!base64String.Contains("data:image/bmp;base64") && !base64String.Contains("data:image/emf;base64") && !base64String.Contains("data:image/exif;base64") && !base64String.Contains("data:image/gif;base64")
                && !base64String.Contains("data:image/icon;base64") && !base64String.Contains("data:image/jpeg;base64") && !base64String.Contains("data:image/jpg;base64") && !base64String.Contains("data:image/png;base64")
                && !base64String.Contains("data:image/tiff;base64") && !base64String.Contains("data:image/wmf;base64"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ValidateTimeZoneInfo(string timeZoneInfoName)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneInfoName);
            return (timeZoneInfo != null);
        }

        #endregion

        #region Convert

        public static string ConvertSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding enconding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(enconding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }

        public static DateTimeOffset ConvertDateTimeOffsetToTimeZone(DateTimeOffset dateTimeOffset, string timeZoneInfoName)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneInfoName);
            return TimeZoneInfo.ConvertTime(dateTimeOffset, timeZoneInfo);
        }

        #endregion

        #region Save

        public static string SaveImage(string imgBase64String, string nameHero)
        {
            string path = $"{GetApplicationDirectory()}Contents\\Img\\{nameHero}.{GetFormatImgBase64String(imgBase64String)}";
            byte[] image = Convert.FromBase64String(ReplaceConventionImageFromBase64String(imgBase64String));

            if (File.Exists(@path))
                DeleteFile(@path);

            File.WriteAllBytes(@path, image);
            path = path.Replace(GetApplicationDirectory(), "");
            return path;
        }

        #endregion

        #region Delete

        public static void DeleteFile(string path)
        {
            File.Delete(@path);
        }

        #endregion
        
        #region Replace
        public static string ReplaceConventionImageFromBase64String(string base64String)
        {
            base64String = base64String.Replace("data:image/bmp;base64,", "");
            base64String = base64String.Replace("data:image/emf;base64,", "");
            base64String = base64String.Replace("data:image/exif;base64,", "");
            base64String = base64String.Replace("data:image/gif;base64,", "");
            base64String = base64String.Replace("data:image/icon;base64,", "");
            base64String = base64String.Replace("data:image/jpeg;base64,", "");
            base64String = base64String.Replace("data:image/jpg;base64,", "");
            base64String = base64String.Replace("data:image/png;base64,", "");
            base64String = base64String.Replace("data:image/tiff;base64,", "");
            base64String = base64String.Replace("data:image/wmf;base64,", "");
            return base64String;
        }
        #endregion
    }
}
